using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoicePage.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cust_name",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "inv_date",
                table: "Invoice",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cust_name",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "inv_date",
                table: "Invoice");
        }
    }
}
