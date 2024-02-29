using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTblFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_picture",
                table: "tbl_student");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:account_type", "all,student,staff,industry")
                .Annotation("Npgsql:Enum:file_type", "image,document")
                .Annotation("Npgsql:Enum:user_gender", "male,female")
                .Annotation("Npgsql:Enum:user_role", "user,admin")
                .Annotation("Npgsql:Enum:user_status", "need_activation,active,disabled,deleted")
                .OldAnnotation("Npgsql:Enum:account_type", "student,staff,industry")
                .OldAnnotation("Npgsql:Enum:user_gender", "male,female")
                .OldAnnotation("Npgsql:Enum:user_role", "user,admin")
                .OldAnnotation("Npgsql:Enum:user_status", "need_activation,active,disabled,deleted");

            migrationBuilder.AddColumn<Guid>(
                name: "profile_picture",
                table: "tbl_user",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_file",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v1()"),
                    timestamp_created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    timestamp_updated = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    file = table.Column<byte[]>(type: "bytea", maxLength: 1000000000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_file", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_file_detail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v1()"),
                    timestamp_created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    timestamp_updated = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    type = table.Column<int>(type: "file_type", nullable: false),
                    is_external = table.Column<bool>(type: "boolean", nullable: false),
                    preview = table.Column<Guid>(type: "uuid", nullable: true),
                    actual = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_file_detail", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_file_detail_tbl_file_actual",
                        column: x => x.actual,
                        principalTable: "tbl_file",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tbl_file_detail_tbl_file_preview",
                        column: x => x.preview,
                        principalTable: "tbl_file",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_profile_picture",
                table: "tbl_user",
                column: "profile_picture",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_file_detail_actual",
                table: "tbl_file_detail",
                column: "actual");

            migrationBuilder.CreateIndex(
                name: "ix_tbl_file_detail_preview",
                table: "tbl_file_detail",
                column: "preview");

            migrationBuilder.AddForeignKey(
                name: "fk_tbl_user_tbl_file_details_profile_picture",
                table: "tbl_user",
                column: "profile_picture",
                principalTable: "tbl_file_detail",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tbl_user_tbl_file_details_profile_picture",
                table: "tbl_user");

            migrationBuilder.DropTable(
                name: "tbl_file_detail");

            migrationBuilder.DropTable(
                name: "tbl_file");

            migrationBuilder.DropIndex(
                name: "ix_tbl_user_profile_picture",
                table: "tbl_user");

            migrationBuilder.DropColumn(
                name: "profile_picture",
                table: "tbl_user");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:account_type", "student,staff,industry")
                .Annotation("Npgsql:Enum:user_gender", "male,female")
                .Annotation("Npgsql:Enum:user_role", "user,admin")
                .Annotation("Npgsql:Enum:user_status", "need_activation,active,disabled,deleted")
                .OldAnnotation("Npgsql:Enum:account_type", "all,student,staff,industry")
                .OldAnnotation("Npgsql:Enum:file_type", "image,document")
                .OldAnnotation("Npgsql:Enum:user_gender", "male,female")
                .OldAnnotation("Npgsql:Enum:user_role", "user,admin")
                .OldAnnotation("Npgsql:Enum:user_status", "need_activation,active,disabled,deleted");

            migrationBuilder.AddColumn<byte[]>(
                name: "profile_picture",
                table: "tbl_student",
                type: "bytea",
                nullable: true);
        }
    }
}
