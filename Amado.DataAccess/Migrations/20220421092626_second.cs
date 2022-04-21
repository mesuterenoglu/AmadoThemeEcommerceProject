using Microsoft.EntityFrameworkCore.Migrations;

namespace Amado.DataAccess.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StokShortage",
                table: "Products",
                newName: "StockShortage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockShortage",
                table: "Products",
                newName: "StokShortage");
        }
    }
}
