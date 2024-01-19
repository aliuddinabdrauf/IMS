using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "type",
                table: "tbl_user",
                type: "user_type",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "tbl_user",
                type: "user_status",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "gender",
                table: "tbl_staff",
                type: "user_gender",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "type",
                table: "tbl_user",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "user_type");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "tbl_user",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "user_status");

            migrationBuilder.AlterColumn<int>(
                name: "gender",
                table: "tbl_staff",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "user_gender");
        }
    }
}
