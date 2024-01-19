using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeEmailToEmailAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "tbl_user",
                newName: "email_adress");

            migrationBuilder.RenameIndex(
                name: "ix_tbl_user_email",
                table: "tbl_user",
                newName: "ix_tbl_user_email_adress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email_adress",
                table: "tbl_user",
                newName: "email");

            migrationBuilder.RenameIndex(
                name: "ix_tbl_user_email_adress",
                table: "tbl_user",
                newName: "ix_tbl_user_email");
        }
    }
}
