using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Infrastructure.Migrations
{
    public partial class dodavanjeKoloneTipZadatka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
            name: "TipZadatka",
            table: "Zadatak",
            nullable: false,
            defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "TipZadatka",
            table: "Zadatak");
        }
    }
}
