using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace IMSInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_role", "user,admin")
                .Annotation("Npgsql:Enum:user_status", "need_activation,active,disabled")
                .Annotation("Npgsql:Enum:user_type", "student,staff,industry");

            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "varchar(200)", nullable: false),
                    password = table.Column<string>(type: "Text", nullable: false),
                    phone_no = table.Column<string>(type: "varchar(12)", nullable: false),
                    roles = table.Column<int[]>(type: "user_role[]", nullable: false),
                    status = table.Column<int>(type: "user_status", nullable: false),
                    type = table.Column<int>(type: "user_type", nullable: false),
                    create_time = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    update_time = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tbl_user", x => x.id);
                });

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
                name: "tbl_user");
        }
    }
}
