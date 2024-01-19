using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_email",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v1()"),
                    timestamp_created = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    timestamp_updated = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    timestamp_send = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    sender = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    to = table.Column<string[]>(type: "text[]", nullable: false),
                    cc = table.Column<string[]>(type: "text[]", nullable: false),
                    bcc = table.Column<string[]>(type: "text[]", nullable: false),
                    subject = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    body = table.Column<string>(type: "text", maxLength: 2147483647, nullable: false),
                    reference = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    reference_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_email", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tbl_email_reference_id",
                table: "tbl_email",
                column: "reference_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_email");
        }
    }
}
