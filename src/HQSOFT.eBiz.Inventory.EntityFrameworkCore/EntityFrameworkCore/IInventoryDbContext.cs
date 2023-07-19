using HQSOFT.eBiz.Inventory.LotSerSegments;
using HQSOFT.eBiz.Inventory.LotSerClasses;
using HQSOFT.eBiz.Inventory.ReasonCodes;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HQSOFT.eBiz.Inventory.EntityFrameworkCore;

[ConnectionStringName(InventoryDbProperties.ConnectionStringName)]
public interface IInventoryDbContext : IEfCoreDbContext
{
    DbSet<LotSerSegment> LotSerSegments { get; set; }
    DbSet<LotSerClass> LotSerClasses { get; set; }
    DbSet<ReasonCode> ReasonCodes { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}