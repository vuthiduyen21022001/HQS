using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HQSOFT.eBiz.Inventory.Migrations
{
    /// <inheritdoc />
    public partial class Inventoryclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "InventoryLotSerClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassID = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TrackingMethod = table.Column<int>(type: "integer", nullable: false),
                    TrackExpriationDate = table.Column<bool>(type: "boolean", nullable: false),
                    RequiredforDropShip = table.Column<bool>(type: "boolean", nullable: false),
                    AssignMethod = table.Column<int>(type: "integer", nullable: false),
                    IssueMethod = table.Column<int>(type: "integer", nullable: false),
                    ShareAutoIncremenitalValueBetwenAllClassItems = table.Column<bool>(type: "boolean", nullable: false),
                    AutoIncremetalValue = table.Column<int>(type: "integer", nullable: false),
                    AutoGenerateNextNumber = table.Column<bool>(type: "boolean", nullable: false),
                    MaxAutoNumbers = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLotSerClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryReasonCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReasonCodeID = table.Column<string>(type: "text", nullable: false),
                    Descr = table.Column<string>(type: "text", nullable: false),
                    Usage = table.Column<int>(type: "integer", nullable: false),
                    SubMask = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    AccountID = table.Column<int>(type: "integer", nullable: true),
                    SubID = table.Column<int>(type: "integer", nullable: true),
                    SalesAcctID = table.Column<int>(type: "integer", nullable: true),
                    SalesSubID = table.Column<int>(type: "integer", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryReasonCodes", x => x.Id);
                });

       
         

            migrationBuilder.CreateTable(
                name: "InventoryLotSerSegments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SegmentID = table.Column<int>(type: "integer", nullable: false),
                    AsgmentType = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    LotSerClassId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLotSerSegments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryLotSerSegments_InventoryLotSerClasses_LotSerClassId",
                        column: x => x.LotSerClassId,
                        principalTable: "InventoryLotSerClasses",
                        principalColumn: "Id");
                });

      

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLotSerSegments_LotSerClassId",
                table: "InventoryLotSerSegments",
                column: "LotSerClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpAuditLogActions");

            migrationBuilder.DropTable(
                name: "AbpEntityPropertyChanges");

            migrationBuilder.DropTable(
                name: "AbpPermissionGrants");

            migrationBuilder.DropTable(
                name: "AbpPermissionGroups");

            migrationBuilder.DropTable(
                name: "AbpPermissions");

            migrationBuilder.DropTable(
                name: "AbpSettings");

            migrationBuilder.DropTable(
                name: "InventoryLotSerSegments");

            migrationBuilder.DropTable(
                name: "InventoryReasonCodes");

            migrationBuilder.DropTable(
                name: "AbpEntityChanges");

            migrationBuilder.DropTable(
                name: "InventoryLotSerClasses");

            migrationBuilder.DropTable(
                name: "AbpAuditLogs");
        }
    }
}
