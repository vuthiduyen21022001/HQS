using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace HQSOFT.eBiz.Inventory.Seed;

public class InventoryAuthServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly InventorySampleIdentityDataSeeder _inventorySampleIdentityDataSeeder;
    private readonly InventoryAuthServerDataSeeder _inventoryAuthServerDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public InventoryAuthServerDataSeedContributor(
        InventoryAuthServerDataSeeder inventoryAuthServerDataSeeder,
        InventorySampleIdentityDataSeeder inventorySampleIdentityDataSeeder,
        ICurrentTenant currentTenant)
    {
        _inventoryAuthServerDataSeeder = inventoryAuthServerDataSeeder;
        _inventorySampleIdentityDataSeeder = inventorySampleIdentityDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _inventorySampleIdentityDataSeeder.SeedAsync(context!);
            await _inventoryAuthServerDataSeeder.SeedAsync(context!);
        }
    }
}
