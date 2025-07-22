using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DulcesChurrascosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddChurrascoAndDulce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Churrascos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoCarne = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerminoCoccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guarniciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Porciones = table.Column<int>(type: "int", nullable: false),
                    PorcionesExtra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Churrascos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dulces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoDulce = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PorUnidad = table.Column<bool>(type: "bit", nullable: false),
                    CantidadPorCaja = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dulces", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Churrascos");

            migrationBuilder.DropTable(
                name: "Dulces");
        }
    }
}
