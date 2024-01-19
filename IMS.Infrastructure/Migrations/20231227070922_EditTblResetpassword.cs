using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditTblResetpassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "confirm_url",
                table: "tbl_reset_password",
                newName: "reset_url");

            migrationBuilder.RenameColumn(
                name: "confirm_key",
                table: "tbl_reset_password",
                newName: "reset_key");

            migrationBuilder.AddColumn<bool>(
                name: "is_first_time_user",
                table: "tbl_reset_password",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_first_time_user",
                table: "tbl_reset_password");

            migrationBuilder.RenameColumn(
                name: "reset_url",
                table: "tbl_reset_password",
                newName: "confirm_url");

            migrationBuilder.RenameColumn(
                name: "reset_key",
                table: "tbl_reset_password",
                newName: "confirm_key");
        }
    }
}
