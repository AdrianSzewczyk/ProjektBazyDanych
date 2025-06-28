using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSPPCars.Migrations
{
    /// <inheritdoc />
    public partial class nowa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rezerwacje.utworzona
            migrationBuilder.DropColumn(
                name: "utworzona",
                table: "Rezerwacje");

            migrationBuilder.AddColumn<DateTime>(
                name: "utworzona",
                table: "Rezerwacje",
                type: "datetime2",
                nullable: true); // lub false, jeśli wymagane

            // Uzytkownicy.utworzony
            migrationBuilder.DropColumn(
                name: "utworzony",
                table: "Uzytkownicy");

            migrationBuilder.AddColumn<DateTime>(
                name: "utworzony",
                table: "Uzytkownicy",
                type: "datetime2",
                nullable: true); // zmień na false, jeśli wymagane
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Rezerwacje.utworzona
            migrationBuilder.DropColumn(
                name: "utworzona",
                table: "Rezerwacje");

            migrationBuilder.AddColumn<byte[]>(
                name: "utworzona",
                table: "Rezerwacje",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            // Uzytkownicy.utworzony
            migrationBuilder.DropColumn(
                name: "utworzony",
                table: "Uzytkownicy");

            migrationBuilder.AddColumn<byte[]>(
                name: "utworzony",
                table: "Uzytkownicy",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }
    }
}
