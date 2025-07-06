using YourBonoPlatform.IAM.Application.Internal.CommandServices;
using YourBonoPlatform.IAM.Application.Internal.OutboundServices;
using YourBonoPlatform.IAM.Application.Internal.QueryServices;
using YourBonoPlatform.IAM.Domain.Model.Commands;
using YourBonoPlatform.IAM.Domain.Repositories;
using YourBonoPlatform.IAM.Domain.Services;
using YourBonoPlatform.IAM.Infrastructure.Hashing.BCrypt.Services;
using YourBonoPlatform.IAM.Infrastructure.Persistence.EFC.Respositories;
using YourBonoPlatform.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using YourBonoPlatform.IAM.Infrastructure.Tokens.JWT.Configuration;
using YourBonoPlatform.IAM.Infrastructure.Tokens.JWT.Services;
using YourBonoPlatform.IAM.Interfaces.ACL;
using YourBonoPlatform.IAM.Interfaces.ACL.Service;
using YourBonoPlatform.Shared.Application.Internal.OutboundServices;
using YourBonoPlatform.Shared.Application.Internal.OutboundServices.ExternalServices;
using YourBonoPlatform.Shared.Domain.Repositories;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using YourBonoPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using YourBonoPlatform.Bonds.Application.Internal.CommandServices;
using YourBonoPlatform.Bonds.Application.Internal.OutboundServices;
using YourBonoPlatform.Bonds.Application.Internal.QueryServices;
using YourBonoPlatform.Bonds.Domain.Model.Commands;
using YourBonoPlatform.Bonds.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Services;
using YourBonoPlatform.Bonds.Infrastructure.Persistence.EFC.Repositories;
using YourBonoPlatform.Shared.Interfaces.ASP.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var developmentString = builder.Configuration.GetConnectionString("DevelopmentConnection");
var allowedOrigins = builder.Configuration.GetSection("AllowedFrontEndOrigins").Get<string[]>();


// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (builder.Environment.IsDevelopment())
        {
            options.UseMySql(developmentString, ServerVersion.AutoDetect(developmentString))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        else if (builder.Environment.IsProduction())
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        }
    });
// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "YourBono.API",
                Version = "v1",
                Description = "Your Bono API",
                TermsOfService = new Uri("https://yourbono.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "Your Bono",
                    Email = "contact@yourbono.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    });

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendPolicy", policy =>
        policy.WithOrigins(allowedOrigins!)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserExternalService, UserExternalService>();

// IAM Bounded Context Injection Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<ISeedUserRoleCommandService, SeedUserRoleCommandService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
builder.Services.AddJwtAuthentication(builder.Configuration);

// Bonds Bounded Context Injection Configuration
builder.Services.AddScoped<IBondRepository, BondRepository>();
builder.Services.AddScoped<IBondCommandService, BondCommandService>();
builder.Services.AddScoped<IBondQueryService, BondQueryService>();
builder.Services.AddScoped<IBondValuationService, BondValuationService>();

builder.Services.AddScoped<ISeedCurrencyTypesCommandService, SeedCurrencyTypesCommandService>();
builder.Services.AddScoped<ISeedInterestTypesCommandService, SeedInterestTypesCommandService>();
builder.Services.AddScoped<ISeedGracePeriodTypesCommandService, SeedGracePeriodTypesCommandService>();

builder.Services.AddScoped<IBondMetricsRepository, BondMetricsRepository>();
builder.Services.AddScoped<IBondMetricsQueryService, BondMetricsQueryService>();

builder.Services.AddScoped<ICashFlowItemRepository, CashFlowItemRepository>();
builder.Services.AddScoped<ICashFlowItemQueryService, CashFlowItemQueryService>();

builder.Services.AddScoped<IInterestTypeRepository, InterestTypeRepository>();
builder.Services.AddScoped<IGracePeriodTypeRepository, GracePeriodTypeRepository>();
builder.Services.AddScoped<ICurrencyTypeRepository, CurrencyTypeRepository>();






var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    
    var userRoleCommandService = services.GetRequiredService<ISeedUserRoleCommandService>();
    await userRoleCommandService.Handle(new SeedUserRolesCommand());
    
    var currencyTypeCommandService = services.GetRequiredService<ISeedCurrencyTypesCommandService>();
    await currencyTypeCommandService.Handle(new SeedCurrencyTypesCommand());
    
    var interestTypeCommandService = services.GetRequiredService<ISeedInterestTypesCommandService>();
    await interestTypeCommandService.Handle(new SeedInterestTypesCommand());
    
    var gracePeriodTypeCommandService = services.GetRequiredService<ISeedGracePeriodTypesCommandService>();
    await gracePeriodTypeCommandService.Handle(new SeedGracePeriodTypesCommand());
    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontendPolicy");


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
