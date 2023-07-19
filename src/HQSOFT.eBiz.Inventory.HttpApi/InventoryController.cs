using HQSOFT.eBiz.Inventory.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HQSOFT.eBiz.Inventory;

public abstract class InventoryController : AbpControllerBase
{
    protected InventoryController()
    {
        LocalizationResource = typeof(InventoryResource);
    }
}
