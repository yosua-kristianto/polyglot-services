using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemConfigurator.Config.Database.Migrations.CMA
{
    /// <inheritdoc />
    public partial class MigrateCMA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cma_tbl_lov_customer_gender",
                columns: table => new
                {
                    gender_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cma_tbl_lov_customer_gender", x => x.gender_id);
                });

            migrationBuilder.CreateTable(
                name: "cma_tbl_lov_customer_identity_type",
                columns: table => new
                {
                    identity_type_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cma_tbl_lov_customer_identity_type", x => x.identity_type_id);
                });

            migrationBuilder.CreateTable(
                name: "cma_tbl_lov_customer_marital_status",
                columns: table => new
                {
                    marital_status_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cma_tbl_lov_customer_marital_status", x => x.marital_status_id);
                });

            migrationBuilder.CreateTable(
                name: "cma_tbl_lov_customer_title",
                columns: table => new
                {
                    title_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cma_tbl_lov_customer_title", x => x.title_id);
                });

            migrationBuilder.CreateTable(
                name: "cma_tbl_lov_nationality",
                columns: table => new
                {
                    nationality_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nationality_name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cma_tbl_lov_nationality", x => x.nationality_id);
                });

            migrationBuilder.CreateTable(
                name: "cma_tbl_customer",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    gender_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    title_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    marital_status_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    birthplace_district_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_number = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    identity_type_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    nationality_id = table.Column<Guid>(type: "uuid", nullable: false),
                    legal_address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    legal_province_id = table.Column<int>(type: "integer", nullable: false),
                    legal_district_id = table.Column<int>(type: "integer", nullable: false),
                    legal_city_id = table.Column<int>(type: "integer", nullable: false),
                    legal_village_id = table.Column<int>(type: "integer", nullable: false),
                    legal_zipcode_id = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    domicile_address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    domicile_province_id = table.Column<int>(type: "integer", nullable: true),
                    domicile_district_id = table.Column<int>(type: "integer", nullable: true),
                    domicile_city_id = table.Column<int>(type: "integer", nullable: true),
                    domicile_village_id = table.Column<int>(type: "integer", nullable: true),
                    domicile_zipcode_id = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    home_phone_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    mobile_phone_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    mobile_phone_no_2 = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    contact_phone_no = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    photo = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    id_card_photo = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    kyc_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    kyc_validated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    kyc_validated_by = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cma_tbl_customer", x => x.customer_id);
                    table.ForeignKey(
                        name: "FK_cma_tbl_customer_cma_tbl_lov_customer_gender_gender_id",
                        column: x => x.gender_id,
                        principalTable: "cma_tbl_lov_customer_gender",
                        principalColumn: "gender_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cma_tbl_customer_cma_tbl_lov_customer_identity_type_identit~",
                        column: x => x.identity_type_id,
                        principalTable: "cma_tbl_lov_customer_identity_type",
                        principalColumn: "identity_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cma_tbl_customer_cma_tbl_lov_customer_marital_status_marita~",
                        column: x => x.marital_status_id,
                        principalTable: "cma_tbl_lov_customer_marital_status",
                        principalColumn: "marital_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cma_tbl_customer_cma_tbl_lov_customer_title_title_id",
                        column: x => x.title_id,
                        principalTable: "cma_tbl_lov_customer_title",
                        principalColumn: "title_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cma_tbl_customer_cma_tbl_lov_nationality_nationality_id",
                        column: x => x.nationality_id,
                        principalTable: "cma_tbl_lov_nationality",
                        principalColumn: "nationality_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cma_tbl_customer_account_no",
                table: "cma_tbl_customer",
                column: "account_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cma_tbl_customer_email",
                table: "cma_tbl_customer",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cma_tbl_customer_gender_id",
                table: "cma_tbl_customer",
                column: "gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_cma_tbl_customer_identity_type_id",
                table: "cma_tbl_customer",
                column: "identity_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_cma_tbl_customer_marital_status_id",
                table: "cma_tbl_customer",
                column: "marital_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_cma_tbl_customer_nationality_id",
                table: "cma_tbl_customer",
                column: "nationality_id");

            migrationBuilder.CreateIndex(
                name: "IX_cma_tbl_customer_title_id",
                table: "cma_tbl_customer",
                column: "title_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cma_tbl_customer");

            migrationBuilder.DropTable(
                name: "cma_tbl_lov_customer_gender");

            migrationBuilder.DropTable(
                name: "cma_tbl_lov_customer_identity_type");

            migrationBuilder.DropTable(
                name: "cma_tbl_lov_customer_marital_status");

            migrationBuilder.DropTable(
                name: "cma_tbl_lov_customer_title");

            migrationBuilder.DropTable(
                name: "cma_tbl_lov_nationality");
        }
    }
}
