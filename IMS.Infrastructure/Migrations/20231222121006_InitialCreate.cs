using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_gender", "male,female")
                .Annotation("Npgsql:Enum:user_role", "user,admin")
                .Annotation("Npgsql:Enum:user_status", "need_activation,active,disabled,deleted")
                .Annotation("Npgsql:Enum:user_type", "student,staff,industry");

            migrationBuilder.CreateTable(
                name: "tbl_base",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timestamp_created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    timestamp_updated = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_base", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timestamp_created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    timestamp_updated = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    password_salt = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    roles = table.Column<int[]>(type: "integer[]", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_staff",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timestamp_created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    timestamp_updated = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    staff_id = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_staff", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_staff_tbl_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tbl_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_student",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timestamp_created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    timestamp_updated = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    student_id = table.Column<string>(type: "character(10)", fixedLength: true, maxLength: 10, nullable: false),
                    ic_no = table.Column<string>(type: "character(12)", fixedLength: true, maxLength: 12, nullable: false),
                    phone_no = table.Column<List<string>>(type: "text[]", nullable: false),
                    address = table.Column<List<string>>(type: "text[]", nullable: false),
                    gender = table.Column<int>(type: "user_gender", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_student", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_student_tbl_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tbl_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tbl_staff_staff_id",
                table: "tbl_staff",
                column: "staff_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_staff_user_id",
                table: "tbl_staff",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_student_student_id_ic_no",
                table: "tbl_student",
                columns: new[] { "student_id", "ic_no" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_student_user_id",
                table: "tbl_student",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_user_email",
                table: "tbl_user",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_base");

            migrationBuilder.DropTable(
                name: "tbl_staff");

            migrationBuilder.DropTable(
                name: "tbl_student");

            migrationBuilder.DropTable(
                name: "tbl_user");
        }
    }
}
