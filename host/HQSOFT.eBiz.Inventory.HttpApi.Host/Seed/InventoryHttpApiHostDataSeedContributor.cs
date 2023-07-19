using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace HQSOFT.eBiz.Inventory.Seed;

public class InventoryHttpApiHostDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly InventorySampleDataSeeder _inventorySampleDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public InventoryHttpApiHostDataSeedContributor(
        InventorySampleDataSeeder inventorySampleDataSeeder,
        ICurrentTenant currentTenant)
    {
        _inventorySampleDataSeeder = inventorySampleDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _inventorySampleDataSeeder.SeedAsync(context!);
        }
    }
}
