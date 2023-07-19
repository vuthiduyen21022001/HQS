using HQSOFT.eBiz.Inventory.LotSerSegments;
using HQSOFT.eBiz.Inventory.LotSerClasses;
using Volo.Abp.EntityFrameworkCore.Modeling;
using HQSOFT.eBiz.Inventory.ReasonCodes;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace HQSOFT.eBiz.Inventory.EntityFrameworkCore;

public static class InventoryDbContextModelCreatingExtensions
{
    public static void ConfigureInventory(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(InventoryDbProperties.DbTablePrefix + "Questions", InventoryDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<ReasonCode>(b =>
{
    b.ToTable(InventoryDbProperties.DbTablePrefix + "ReasonCodes", InventoryDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.ReasonCodeID).HasColumnName(nameof(ReasonCode.ReasonCodeID)).IsRequired();
    b.Property(x => x.Descr).HasColumnName(nameof(ReasonCode.Descr)).IsRequired();
    b.Property(x => x.Usage).HasColumnName(nameof(ReasonCode.Usage));
    b.Property(x => x.SubMask).HasColumnName(nameof(ReasonCode.SubMask));
    b.Property(x => x.AccountID).HasColumnName(nameof(ReasonCode.AccountID));
    b.Property(x => x.SubID).HasColumnName(nameof(ReasonCode.SubID));
    b.Property(x => x.SalesAcctID).HasColumnName(nameof(ReasonCode.SalesAcctID));
    b.Property(x => x.SalesSubID).HasColumnName(nameof(ReasonCode.SalesSubID));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<LotSerClass>(b =>
{
    b.ToTable(InventoryDbProperties.DbTablePrefix + "LotSerClasses", InventoryDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.ClassID).HasColumnName(nameof(LotSerClass.ClassID)).IsRequired();
    b.Property(x => x.Description).HasColumnName(nameof(LotSerClass.Description)).IsRequired();
    b.Property(x => x.TrackingMethod).HasColumnName(nameof(LotSerClass.TrackingMethod));
    b.Property(x => x.TrackExpriationDate).HasColumnName(nameof(LotSerClass.TrackExpriationDate));
    b.Property(x => x.RequiredforDropShip).HasColumnName(nameof(LotSerClass.RequiredforDropShip));
    b.Property(x => x.AssignMethod).HasColumnName(nameof(LotSerClass.AssignMethod));
    b.Property(x => x.IssueMethod).HasColumnName(nameof(LotSerClass.IssueMethod));
    b.Property(x => x.ShareAutoIncremenitalValueBetwenAllClassItems).HasColumnName(nameof(LotSerClass.ShareAutoIncremenitalValueBetwenAllClassItems));
    b.Property(x => x.AutoIncremetalValue).HasColumnName(nameof(LotSerClass.AutoIncremetalValue));
    b.Property(x => x.AutoGenerateNextNumber).HasColumnName(nameof(LotSerClass.AutoGenerateNextNumber));
    b.Property(x => x.MaxAutoNumbers).HasColumnName(nameof(LotSerClass.MaxAutoNumbers));
});

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<LotSerSegment>(b =>
{
    b.ToTable(InventoryDbProperties.DbTablePrefix + "LotSerSegments", InventoryDbProperties.DbSchema);
    b.ConfigureByConvention();
    b.Property(x => x.SegmentID).HasColumnName(nameof(LotSerSegment.SegmentID));
    b.Property(x => x.AsgmentType).HasColumnName(nameof(LotSerSegment.AsgmentType));
    b.Property(x => x.Value).HasColumnName(nameof(LotSerSegment.Value));
    b.HasOne<LotSerClass>().WithMany().HasForeignKey(x => x.LotSerClassId).OnDelete(DeleteBehavior.NoAction);
});

        }
    }
}