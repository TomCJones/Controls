using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Controls.Migrations
{
    public partial class fixkeyonresult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    locator = table.Column<decimal>(nullable: false),
                    sub = table.Column<string>(nullable: true),
                    created = table.Column<decimal>(nullable: false),
                    updated = table.Column<decimal>(nullable: false),
                    publicKey = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    purpose = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.locator);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    id = table.Column<decimal>(nullable: false),
                    cli_id = table.Column<Guid>(nullable: false),
                    doi = table.Column<string>(nullable: true),
                    doi_date = table.Column<decimal>(nullable: false),
                    first_use = table.Column<decimal>(nullable: false),
                    count_use = table.Column<long>(nullable: false),
                    status = table.Column<string>(nullable: true),
                    methods = table.Column<string>(nullable: true),
                    cert = table.Column<string>(nullable: true),
                    Clientlocator = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.id);
                    table.ForeignKey(
                        name: "FK_requests_clients_Clientlocator",
                        column: x => x.Clientlocator,
                        principalTable: "clients",
                        principalColumn: "locator",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_requests_Clientlocator",
                table: "requests",
                column: "Clientlocator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
