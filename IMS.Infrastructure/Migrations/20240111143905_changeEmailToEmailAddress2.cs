using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeEmailToEmailAddress2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email_adress",
                table: "tbl_user",
                newName: "email_address");

            migrationBuilder.RenameIndex(
                name: "ix_tbl_user_email_adress",
                table: "tbl_user",
                newName: "ix_tbl_user_email_address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email_address",
                table: "tbl_user",
                newName: "email_adress");

            migrationBuilder.RenameIndex(
                name: "ix_tbl_user_email_address",
                table: "tbl_user",
                newName: "ix_tbl_user_email_adress");
        }
    }
}
