using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_MusicStudioReservation.infrastructure.Migrations.Tenant
{
    /// <inheritdoc />
    public partial class AddTandCAndPromotionRationale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PromotionRationales",
                columns: table => new
                {
                    PromotionRationaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    PurposeType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TargetAudience = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TriggerCondition = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExpectedKpi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MaxRedemptions = table.Column<int>(type: "int", nullable: true),
                    ActualRedemptions = table.Column<int>(type: "int", nullable: false),
                    WorkflowStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApprovedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ApprovedByName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RationaleNotes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ActualRevenueDelta = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    RoiSummary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionRationales", x => x.PromotionRationaleId);
                    table.ForeignKey(
                        name: "FK_PromotionRationales_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TermsAndConditions",
                columns: table => new
                {
                    TandCId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TandCCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TandCType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MajorVersion = table.Column<int>(type: "int", nullable: false),
                    MinorVersion = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AuthorUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ApprovedByUserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ApprovedByName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequiresReAcceptance = table.Column<bool>(type: "bit", nullable: false),
                    ChangeNotes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermsAndConditions", x => x.TandCId);
                });

            migrationBuilder.CreateTable(
                name: "PromotionSegments",
                columns: table => new
                {
                    PromotionSegmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionRationaleId = table.Column<int>(type: "int", nullable: false),
                    SegmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionSegments", x => x.PromotionSegmentId);
                    table.ForeignKey(
                        name: "FK_PromotionSegments_PromotionRationales_PromotionRationaleId",
                        column: x => x.PromotionRationaleId,
                        principalTable: "PromotionRationales",
                        principalColumn: "PromotionRationaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTandCAcknowledgments",
                columns: table => new
                {
                    AcknowledgmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TandCId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TandCVersion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TandCType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AcknowledgmentContext = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AcknowledgedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTandCAcknowledgments", x => x.AcknowledgmentId);
                    table.ForeignKey(
                        name: "FK_UserTandCAcknowledgments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTandCAcknowledgments_TermsAndConditions_TandCId",
                        column: x => x.TandCId,
                        principalTable: "TermsAndConditions",
                        principalColumn: "TandCId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionRationales_PromotionId",
                table: "PromotionRationales",
                column: "PromotionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionRationales_WorkflowStatus",
                table: "PromotionRationales",
                column: "WorkflowStatus");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionSegments_PromotionRationaleId",
                table: "PromotionSegments",
                column: "PromotionRationaleId");

            migrationBuilder.CreateIndex(
                name: "IX_TermsAndConditions_Status",
                table: "TermsAndConditions",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_TermsAndConditions_TandCCode",
                table: "TermsAndConditions",
                column: "TandCCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TermsAndConditions_TandCType",
                table: "TermsAndConditions",
                column: "TandCType");

            migrationBuilder.CreateIndex(
                name: "IX_TermsAndConditions_TandCType_Status",
                table: "TermsAndConditions",
                columns: new[] { "TandCType", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTandCAcknowledgments_AcknowledgedAt",
                table: "UserTandCAcknowledgments",
                column: "AcknowledgedAt");

            migrationBuilder.CreateIndex(
                name: "IX_UserTandCAcknowledgments_CustomerId",
                table: "UserTandCAcknowledgments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTandCAcknowledgments_TandCId",
                table: "UserTandCAcknowledgments",
                column: "TandCId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionSegments");

            migrationBuilder.DropTable(
                name: "UserTandCAcknowledgments");

            migrationBuilder.DropTable(
                name: "PromotionRationales");

            migrationBuilder.DropTable(
                name: "TermsAndConditions");
        }
    }
}
