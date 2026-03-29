using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemConfigurator.Config.Database.Migrations.UMA
{
    /// <inheritdoc />
    public partial class MigrateUser_Device_DeviceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "uma_tbl_lov_device_type",
                columns: table => new
                {
                    device_type_id = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uma_tbl_lov_device_type", x => x.device_type_id);
                });

            migrationBuilder.CreateTable(
                name: "uma_tbl_mobile_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uma_tbl_mobile_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "uma_tbl_user_devices",
                columns: table => new
                {
                    device_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_login = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    device_type_id = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uma_tbl_user_devices", x => x.device_id);
                    table.ForeignKey(
                        name: "FK_uma_tbl_user_devices_uma_tbl_lov_device_type_device_type_id",
                        column: x => x.device_type_id,
                        principalTable: "uma_tbl_lov_device_type",
                        principalColumn: "device_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_uma_tbl_user_devices_uma_tbl_mobile_users_user_id",
                        column: x => x.user_id,
                        principalTable: "uma_tbl_mobile_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_uma_tbl_mobile_users_email",
                table: "uma_tbl_mobile_users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_uma_tbl_user_devices_device_type_id",
                table: "uma_tbl_user_devices",
                column: "device_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_uma_tbl_user_devices_user_id",
                table: "uma_tbl_user_devices",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "uma_tbl_user_devices");

            migrationBuilder.DropTable(
                name: "uma_tbl_lov_device_type");

            migrationBuilder.DropTable(
                name: "uma_tbl_mobile_users");
        }
    }
}
