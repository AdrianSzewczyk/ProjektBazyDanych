using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSPPCars.Migrations
{
    /// <inheritdoc />
    public partial class Dodatek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Zdjecie",
                table: "Pojazd_Sztuka",
                type: "nvarchar(max)",
                nullable: true);

            // Usuń istniejącą kolumnę rowversion
            migrationBuilder.DropColumn(
                name: "data_dodania",
                table: "Ogloszenia");

            // Dodaj nową kolumnę jako datetime2
            migrationBuilder.AddColumn<DateTime>(
                name: "data_dodania",
                table: "Ogloszenia",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()"); // albo defaultValue: new DateTime(...)

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zdjecie",
                table: "Pojazd_Sztuka");

            // Usuń kolumnę datetime2
            migrationBuilder.DropColumn(
                name: "data_dodania",
                table: "Ogloszenia");

            // Przywróć rowversion
            migrationBuilder.AddColumn<byte[]>(
                name: "data_dodania",
                table: "Ogloszenia",
                type: "rowversion",
                rowVersion: true,
                nullable: false);
        }
    }
}
