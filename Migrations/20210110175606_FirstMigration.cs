using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zagrean_Robert_project.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientID = table.Column<int>(nullable: false),
                    Nume = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    DataNasterii = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Distribuitor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(maxLength: 50, nullable: false),
                    Adresa = table.Column<string>(maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distribuitor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Supliment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(nullable: true),
                    Producator = table.Column<string>(nullable: true),
                    Pret = table.Column<decimal>(type: "decimal(6, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supliment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    ComenziID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<int>(nullable: false),
                    SuplimentID = table.Column<int>(nullable: false),
                    DataComanda = table.Column<DateTime>(nullable: false),
                    SuplimenteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.ComenziID);
                    table.ForeignKey(
                        name: "FK_Comanda_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comanda_Supliment_SuplimenteID",
                        column: x => x.SuplimenteID,
                        principalTable: "Supliment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suplimente distribuite",
                columns: table => new
                {
                    PublisherID = table.Column<int>(nullable: false),
                    SuplementeID = table.Column<int>(nullable: false),
                    SuplimenteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suplimente distribuite", x => new { x.SuplementeID, x.PublisherID });
                    table.ForeignKey(
                        name: "FK_Suplimente distribuite_Distribuitor_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Distribuitor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suplimente distribuite_Supliment_SuplimenteID",
                        column: x => x.SuplimenteID,
                        principalTable: "Supliment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_ClientID",
                table: "Comanda",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_SuplimenteID",
                table: "Comanda",
                column: "SuplimenteID");

            migrationBuilder.CreateIndex(
                name: "IX_Suplimente distribuite_PublisherID",
                table: "Suplimente distribuite",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_Suplimente distribuite_SuplimenteID",
                table: "Suplimente distribuite",
                column: "SuplimenteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comanda");

            migrationBuilder.DropTable(
                name: "Suplimente distribuite");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Distribuitor");

            migrationBuilder.DropTable(
                name: "Supliment");
        }
    }
}
