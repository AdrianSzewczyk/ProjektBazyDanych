using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSPPCars.Migrations
{
    /// <inheritdoc />
    public partial class trzecia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "utworzony",
                table: "Uzytkownicy",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "rowversion",
                oldRowVersion: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "utworzony",
                table: "Uzytkownicy",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
