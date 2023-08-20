using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMSInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class genderType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_gender", "male,female")
                .Annotation("Npgsql:Enum:user_role", "user,admin")
                .Annotation("Npgsql:Enum:user_status", "need_activation,active,disabled")
                .Annotation("Npgsql:Enum:user_type", "student,staff,industry")
                .OldAnnotation("Npgsql:Enum:user_role", "user,admin")
                .OldAnnotation("Npgsql:Enum:user_status", "need_activation,active,disabled")
                .OldAnnotation("Npgsql:Enum:user_type", "student,staff,industry");

            migrationBuilder.AddColumn<int>(
                name: "gender",
                table: "tbl_student",
                type: "user_gender",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gender",
                table: "tbl_student");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_role", "user,admin")
                .Annotation("Npgsql:Enum:user_status", "need_activation,active,disabled")
                .Annotation("Npgsql:Enum:user_type", "student,staff,industry")
                .OldAnnotation("Npgsql:Enum:user_gender", "male,female")
                .OldAnnotation("Npgsql:Enum:user_role", "user,admin")
                .OldAnnotation("Npgsql:Enum:user_status", "need_activation,active,disabled")
                .OldAnnotation("Npgsql:Enum:user_type", "student,staff,industry");
        }
    }
}
