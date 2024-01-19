using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtablefacultycourseandstudentcourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_faculty",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timestamp_created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    timestamp_updated = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_faculty", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_course",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timestamp_created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    timestamp_updated = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    faculty_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_course", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_course_tbl_faculty_faculty_id",
                        column: x => x.faculty_id,
                        principalTable: "tbl_faculty",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_student_course",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    timestamp_created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    timestamp_updated = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    student_id = table.Column<Guid>(type: "uuid", nullable: false),
                    course_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_student_course", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_student_course_tbl_course_course_id",
                        column: x => x.course_id,
                        principalTable: "tbl_course",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tbl_student_course_tbl_student_student_id",
                        column: x => x.student_id,
                        principalTable: "tbl_student",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_tbl_course_code",
                table: "tbl_course",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_course_faculty_id",
                table: "tbl_course",
                column: "faculty_id");

            migrationBuilder.CreateIndex(
                name: "ix_tbl_faculty_code",
                table: "tbl_faculty",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tbl_student_course_course_id",
                table: "tbl_student_course",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_tbl_student_course_student_id",
                table: "tbl_student_course",
                column: "student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_student_course");

            migrationBuilder.DropTable(
                name: "tbl_course");

            migrationBuilder.DropTable(
                name: "tbl_faculty");
        }
    }
}
