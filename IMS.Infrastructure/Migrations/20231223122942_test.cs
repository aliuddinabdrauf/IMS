using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_user",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_student_course",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_student",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_staff",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_faculty",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_course",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_base",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v1()",
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_user",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_student_course",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_student",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_staff",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_faculty",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_course",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tbl_base",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "uuid_generate_v1()");
        }
    }
}
