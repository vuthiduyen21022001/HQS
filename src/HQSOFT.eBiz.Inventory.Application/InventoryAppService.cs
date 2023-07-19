using HQSOFT.eBiz.Inventory.Localization;
using Volo.Abp.Application.Services;

namespace HQSOFT.eBiz.Inventory;

public abstract class InventoryAppService : ApplicationService
{
    protected InventoryAppService()
    {
        LocalizationResource = typeof(InventoryResource);
        ObjectMapperContext = typeof(InventoryApplicationModule);
    }
}
