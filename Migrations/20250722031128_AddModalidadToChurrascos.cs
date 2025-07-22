using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DulcesChurrascosAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddModalidadToChurrascos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Modalidad",
                table: "Churrascos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modalidad",
                table: "Churrascos");
        }
    }
}
