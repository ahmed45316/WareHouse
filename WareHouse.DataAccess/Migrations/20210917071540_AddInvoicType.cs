using Microsoft.EntityFrameworkCore.Migrations;

namespace WareHouse.DataAccess.Migrations
{
    public partial class AddInvoicType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoicType",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoicType",
                table: "Invoices");
        }
    }
}
