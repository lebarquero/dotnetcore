using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CobranzaAPI.Migrations
{
    public partial class CreatedbCobranza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FecRegistro = table.Column<DateTime>(nullable: false),
                    NombreCliente = table.Column<string>(maxLength: 200, nullable: false),
                    DireccionCliente = table.Column<string>(maxLength: 300, nullable: true),
                    TelefonoCliente = table.Column<string>(maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
