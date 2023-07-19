using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.Inventory;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(InventoryHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class InventoryConsoleApiClientModule : AbpModule
{

}
