using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSPPCars.Migrations
{
    /// <inheritdoc />
    public partial class Sortowanie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "utworzona",
                table: "Rezerwacje",
                type: "datetime2",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "rowversion",
                oldRowVersion: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "utworzona",
                table: "Rezerwacje",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldRowVersion: true,
                oldNullable: true);
        }
    }
}
