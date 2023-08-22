using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMSInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class passwordhashsalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "tbl_user",
                newName: "password_salt");

            migrationBuilder.AddColumn<string>(
                name: "password_hash",
                table: "tbl_user",
                type: "Text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password_hash",
                table: "tbl_user");

            migrationBuilder.RenameColumn(
                name: "password_salt",
                table: "tbl_user",
                newName: "password");
        }
    }
}
