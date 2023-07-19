using HQSOFT.eBiz.Inventory.Localization;
using Volo.Abp.AspNetCore.Components;

namespace HQSOFT.eBiz.Inventory.Blazor;

public abstract class InventoryComponentBase : AbpComponentBase
{
    protected InventoryComponentBase()
    {
        LocalizationResource = typeof(InventoryResource);
    }
}
