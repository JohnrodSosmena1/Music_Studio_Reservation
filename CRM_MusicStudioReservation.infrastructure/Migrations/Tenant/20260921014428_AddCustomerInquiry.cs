using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_MusicStudioReservation.infrastructure.Migrations.Tenant
{
    /// <inheritdoc />
    public partial class AddCustomerInquiry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerInquiries",
                columns: table => new
                {
                    CustomerInquiryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Response = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    RespondedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RespondedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInquiries", x => x.CustomerInquiryId);
                    table.ForeignKey(
                        name: "FK_CustomerInquiries_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInquiries_CreatedAt",
                table: "CustomerInquiries",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInquiries_CustomerId",
                table: "CustomerInquiries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInquiries_Status",
                table: "CustomerInquiries",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerInquiries");
        }
    }
}
