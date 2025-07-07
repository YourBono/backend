using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourBonoPlatform.Bonds.Domain.Model.Commands;
using YourBonoPlatform.Bonds.Domain.Model.Queries;
using YourBonoPlatform.Bonds.Domain.Services;
using YourBonoPlatform.Bonds.Interfaces.REST.Resources;
using YourBonoPlatform.Bonds.Interfaces.REST.Transform;

namespace YourBonoPlatform.Bonds.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class BondsController(
    IBondQueryService bondQueryService, 
    IBondCommandService bondCommandService,
    ICashFlowItemQueryService cashFlowItemQueryService,
    IBondMetricsQueryService bondMetricsQueryService
    ): ControllerBase
{
    [HttpGet("user-id/{userId:int}")]
    public async Task<IActionResult> GetBondsByUserId(int userId)
    {
        var query = new GetAllBondsByUserIdQuery(userId);
        var bonds = await bondQueryService.Handle(query);
        var bondResources = bonds.Select(BondResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(bondResources);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetBondById(int id)
    {
        var query = new GetBondByIdQuery(id);
        var bond = await bondQueryService.Handle(query);
        if (bond == null)
        {
            return NotFound();
        }
        var bondResource = BondResourceFromEntityAssembler.ToResourceFromEntity(bond);
        return Ok(bondResource);
    }
    
    [HttpGet("get-cash-flow/{bondId:int}")]
    public async Task<IActionResult> GetCashFlowByBondId(int bondId)
    {
        var query = new GetCashFlowByBondIdQuery(bondId);
        var cashFlows = await cashFlowItemQueryService.Handle(query);
        var cashFlowResources = cashFlows.Select(CashFlowItemResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(cashFlows);
    }
    
    [HttpGet("get-bond-metrics/{bondId:int}")]
    public async Task<IActionResult> GetBondMetricsByBondId(int bondId)
    {
        var query = new GetBondMetricsByBondIdQuery(bondId);
        var bondMetrics = await bondMetricsQueryService.Handle(query);
        if (bondMetrics == null)
        {
            return NotFound();
        }
        var bondMetricsResource = BondMetricsResourceFromEntityAssembler.ToResourceFromEntity(bondMetrics);
        return Ok(bondMetricsResource);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBond([FromBody] CreateBondResource resource)
    {
        var command = CreateBondCommandFromResourceAssembler.ToCommandFromResource(resource);
        var bond = await bondCommandService.Handle(command);
        var bondResource = BondResourceFromEntityAssembler.ToResourceFromEntity(bond!);
        return Ok(bondResource);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBond(int id, [FromBody] UpdateBondResource resource)
    {
        var command = UpdateBondCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var bond = await bondCommandService.Handle(command);
        if (bond == null)
        {
            return NotFound();
        }
        var bondResource = BondResourceFromEntityAssembler.ToResourceFromEntity(bond);
        return Ok(bondResource);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBond(int id)
    {
        var command = new DeleteBondCommand(id);
        var bond = await bondCommandService.Handle(command);
        if (bond == null)
        {
            return NotFound();
        }

        return Ok("Bond deleted successfully.");
    }
    
    
}