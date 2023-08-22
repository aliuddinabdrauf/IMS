using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace IMSInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtblstaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_staff",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(200)", nullable: false),
                    staff_id = table.Column<string>(type: "varchar(10)", nullable: false),
                    gender = table.Column<int>(type: "user_gender", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_time = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    update_time = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_staff", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_staff_tbl_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tbl_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tbl_student_student_id",
                table: "tbl_student",
                column: "student_id",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_staff");

            migrationBuilder.DropIndex(
                name: "ix_tbl_student_student_id",
                table: "tbl_student");
        }
    }
}
