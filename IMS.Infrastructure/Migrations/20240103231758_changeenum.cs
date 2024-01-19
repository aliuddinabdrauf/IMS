using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeenum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:account_type", "student,staff,industry")
                .Annotation("Npgsql:Enum:user_gender", "male,female")
                .Annotation("Npgsql:Enum:user_role", "user,admin")
                .Annotation("Npgsql:Enum:user_status", "need_activation,active,disabled,deleted")
                .OldAnnotation("Npgsql:Enum:user_gender", "male,female")
                .OldAnnotation("Npgsql:Enum:user_role", "user,admin")
                .OldAnnotation("Npgsql:Enum:user_status", "need_activation,active,disabled,deleted")
                .OldAnnotation("Npgsql:Enum:user_type", "student,staff,industry");

            migrationBuilder.AlterColumn<int>(
                name: "type",
                table: "tbl_user",
                type: "account_type",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "user_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_gender", "male,female")
                .Annotation("Npgsql:Enum:user_role", "user,admin")
                .Annotation("Npgsql:Enum:user_status", "need_activation,active,disabled,deleted")
                .Annotation("Npgsql:Enum:user_type", "student,staff,industry")
                .OldAnnotation("Npgsql:Enum:account_type", "student,staff,industry")
                .OldAnnotation("Npgsql:Enum:user_gender", "male,female")
                .OldAnnotation("Npgsql:Enum:user_role", "user,admin")
                .OldAnnotation("Npgsql:Enum:user_status", "need_activation,active,disabled,deleted");

            migrationBuilder.AlterColumn<int>(
                name: "type",
                table: "tbl_user",
                type: "user_type",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "account_type");
        }
    }
}
