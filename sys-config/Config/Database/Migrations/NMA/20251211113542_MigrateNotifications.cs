using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemConfigurator.Config.Database.Migrations.NMA
{
    /// <inheritdoc />
    public partial class MigrateNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nma_lov_notification_platform_type",
                columns: table => new
                {
                    notification_platform_type_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nma_lov_notification_platform_type", x => x.notification_platform_type_id);
                });

            migrationBuilder.CreateTable(
                name: "nma_tbl_personal_notifications",
                columns: table => new
                {
                    notification_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    notification_platform_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    notification_title = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    notification_body = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nma_tbl_personal_notifications", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_nma_tbl_personal_notifications_nma_lov_notification_platfor~",
                        column: x => x.notification_platform_type,
                        principalTable: "nma_lov_notification_platform_type",
                        principalColumn: "notification_platform_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nma_tbl_personal_notifications_notification_platform_type",
                table: "nma_tbl_personal_notifications",
                column: "notification_platform_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nma_tbl_personal_notifications");

            migrationBuilder.DropTable(
                name: "nma_lov_notification_platform_type");
        }
    }
}
