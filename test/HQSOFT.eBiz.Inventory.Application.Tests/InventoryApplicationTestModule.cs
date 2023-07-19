using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.Inventory;

[DependsOn(
    typeof(InventoryApplicationModule),
    typeof(InventoryDomainTestModule)
    )]
public class InventoryApplicationTestModule : AbpModule
{

}
