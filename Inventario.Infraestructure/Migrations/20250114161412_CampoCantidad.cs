using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventario.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CampoCantidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "ProductoPrecioLote",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "ProductoPrecioLote");
        }
    }
}
