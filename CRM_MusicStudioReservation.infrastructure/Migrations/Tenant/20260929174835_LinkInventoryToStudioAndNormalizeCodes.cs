using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_MusicStudioReservation.infrastructure.Migrations.Tenant
{
    /// <inheritdoc />
    public partial class LinkInventoryToStudioAndNormalizeCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Sanitization: clean junk test records and duplicate codes
            migrationBuilder.Sql(@"
                DELETE FROM Studios WHERE StudioCode IN ('qwe', '123') AND StudioId NOT IN (SELECT StudioId FROM Bookings);
                DELETE FROM InventoryItems WHERE ItemCode = '123' OR ItemName = '123';

                UPDATE Studios SET StudioCode = 'STD001', StudioName = 'Studio 1' WHERE StudioId = 1;
                UPDATE Studios SET StudioCode = 'STD002', StudioName = 'Studio 2' WHERE StudioId = 2;
                UPDATE Studios SET StudioCode = 'STD003', StudioName = 'Studio 3' WHERE StudioId = 3;

                ;WITH DupItems AS (
                    SELECT InventoryItemId, ItemCode,
                           ROW_NUMBER() OVER(PARTITION BY ItemCode ORDER BY InventoryItemId) as rn
                    FROM InventoryItems
                )
                UPDATE i
                SET ItemCode = i.ItemCode + '-' + CAST(i.InventoryItemId AS VARCHAR(10))
                FROM InventoryItems i
                INNER JOIN DupItems d ON i.InventoryItemId = d.InventoryItemId
                WHERE d.rn > 1;
            ");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Studios",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "InventoryItems",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Condition",
                table: "InventoryItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Availability",
                table: "InventoryItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "StudioId",
                table: "InventoryItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "InventoryItems",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemCode",
                table: "InventoryItems",
                column: "ItemCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_StudioId",
                table: "InventoryItems",
                column: "StudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_Studios_StudioId",
                table: "InventoryItems",
                column: "StudioId",
                principalTable: "Studios",
                principalColumn: "StudioId",
                onDelete: ReferentialAction.SetNull);

            // Link existing items to studios based on Location ("Studio A" -> Studio 1, "Studio B" -> Studio 2, etc.)
            migrationBuilder.Sql(@"
                UPDATE i
                SET i.StudioId = s.StudioId
                FROM InventoryItems i
                CROSS JOIN Studios s
                WHERE (i.Location = 'Studio A' AND (s.StudioCode = 'STD001' OR s.StudioName = 'Studio 1'))
                   OR (i.Location = 'Studio B' AND (s.StudioCode = 'STD002' OR s.StudioName = 'Studio 2'))
                   OR (i.Location = 'Studio C' AND (s.StudioCode = 'STD003' OR s.StudioName = 'Studio 3'));

                UPDATE i
                SET i.Location = s.StudioName
                FROM InventoryItems i
                INNER JOIN Studios s ON i.StudioId = s.StudioId;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Studios_StudioId",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_ItemCode",
                table: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryItems_StudioId",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Studios");

            migrationBuilder.DropColumn(
                name: "StudioId",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "InventoryItems");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "InventoryItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Condition",
                table: "InventoryItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Availability",
                table: "InventoryItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
