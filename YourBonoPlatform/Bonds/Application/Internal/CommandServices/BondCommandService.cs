using YourBonoPlatform.Shared.Application.Internal.OutboundServices;
using YourBonoPlatform.Shared.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Model.Aggregates;
using YourBonoPlatform.Bonds.Domain.Model.Commands;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Repositories;
using YourBonoPlatform.Bonds.Domain.Services;

namespace YourBonoPlatform.Bonds.Application.Internal.CommandServices;

public class BondCommandService(
    IBondRepository bondRepository,
    IBondMetricsRepository bondMetricsRepository,
    ICashFlowItemRepository cashFlowItemRepository,
    IBondValuationService bondValuationService,
    IUserExternalService userExternalService,
    IUnitOfWork unitOfWork
    ): IBondCommandService
{
    public async Task<Bond?> Handle(CreateBondCommand command)
    {
        // Verificar si el usuario existe
        var userExists = userExternalService.UserExists(command.UserId);
        if (!userExists)
        {
            throw new ArgumentException($"User with ID {command.UserId} does not exist.");
        }
    
        // Crear el bono
        var bond = new Bond(command);
        await bondRepository.AddAsync(bond);
        await unitOfWork.CompleteAsync();
    
        // Generar flujos de caja
        var cashFlowItems = await bondValuationService.CalculateCashFlows(bond);
        var flowItems = cashFlowItems?.ToList() ?? [];
        if (flowItems.Count == 0)
        {
            throw new InvalidOperationException("No cash flow items were generated for the bond.");
        }
    
        // Guardar flujos
        await cashFlowItemRepository.SaveAllCashFlowItems(flowItems);
    
        // Calcular métricas
        var bondMetrics = await bondValuationService.CalculateBondMetrics(bond, flowItems);
        if (bondMetrics == null)
        {
            throw new InvalidOperationException("Bond metrics could not be calculated.");
        }
    
        // Guardar métricas solo si aún no existen
        var existingMetrics = await bondMetricsRepository.GetBondMetricsByBondId(bond.Id);
        if (existingMetrics != null)
        {
            throw new InvalidOperationException($"Bond metrics for bond ID {bond.Id} already exist.");
        }
    
        // Guardar nuevas métricas
        await bondMetricsRepository.AddAsync(bondMetrics);
        await unitOfWork.CompleteAsync();
    
        return bond;
    }

    public async Task<Bond?> Handle(UpdateBondCommand command)
    {
        var existingBond = await bondRepository.FindByIdAsync(command.Id);
        if (existingBond == null)
        {
            throw new ArgumentException($"Bond with ID {command.Id} does not exist.");
        }

        var bond = existingBond;
        bond.Update(command);
        bondRepository.Update(bond);
        await unitOfWork.CompleteAsync();
        
        var cashFlowItems = await bondValuationService.CalculateCashFlows(bond);
        var flowItems = cashFlowItems.ToList();
        if (cashFlowItems == null || !flowItems.Any())
        {
            throw new InvalidOperationException("No cash flow items were generated for the bond.");
        }
        await cashFlowItemRepository.SaveAllCashFlowItems(flowItems);
        
        var bondMetrics = await bondValuationService.CalculateBondMetrics(bond, flowItems);
        if (bondMetrics == null)
        {
            throw new InvalidOperationException("Bond metrics could not be calculated.");
        }
        var existingMetrics = await bondMetricsRepository.GetBondMetricsByBondId(bond.Id);
        if (existingMetrics == null)
        {
            throw new InvalidOperationException($"Bond metrics for bond ID {bond.Id} do not exist.");
        }
        existingMetrics.Update(bondMetrics);
        bondMetricsRepository.Update(existingMetrics);
        await unitOfWork.CompleteAsync();
        return bond;

    }

    public async Task<Bond?> Handle(DeleteBondCommand command)
    {
        var bond = await bondRepository.FindByIdAsync(command.Id);
        if (bond == null)
        {
            throw new ArgumentException($"Bond with ID {command.Id} does not exist.");
        }
        var cashFlowItems = await cashFlowItemRepository.DeleteAllCashFlowItemsByBondId(bond.Id);
        if (cashFlowItems == null || !cashFlowItems.Any())
        {
            throw new InvalidOperationException("No cash flow items were found for the bond.");
        }
        var bondMetrics = await bondMetricsRepository.GetBondMetricsByBondId(command.Id);
        if (bondMetrics != null)
        {
            await bondMetricsRepository.DeleteBondMetricsByBondId(command.Id);
        }
        bondRepository.Remove(bond);
        await unitOfWork.CompleteAsync();
        return bond;
    }
}