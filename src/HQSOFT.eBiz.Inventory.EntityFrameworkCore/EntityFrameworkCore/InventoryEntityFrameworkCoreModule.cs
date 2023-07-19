using HQSOFT.eBiz.Inventory.LotSerSegments;
using HQSOFT.eBiz.Inventory.LotSerClasses;
using HQSOFT.eBiz.Inventory.ReasonCodes;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.Inventory.EntityFrameworkCore;

[DependsOn(
    typeof(InventoryDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class InventoryEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<InventoryDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<ReasonCode, ReasonCodes.EfCoreReasonCodeRepository>();

            options.AddRepository<LotSerClass, LotSerClasses.EfCoreLotSerClassRepository>();

            options.AddRepository<LotSerSegment, LotSerSegments.EfCoreLotSerSegmentRepository>();

        });
    }
}