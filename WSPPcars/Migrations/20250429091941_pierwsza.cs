using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WSPPCars.Migrations
{
    /// <inheritdoc />
    public partial class pierwsza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dodatki",
                columns: table => new
                {
                    id_Dodatku = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    liczba_sztuk = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    dostepnosc = table.Column<bool>(type: "bit", nullable: true),
                    kwota = table.Column<decimal>(type: "smallmoney", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dodatki__EDC18B725C43E0B7", x => x.id_Dodatku);
                });

            migrationBuilder.CreateTable(
                name: "Rodzaj_Konta",
                columns: table => new
                {
                    id_Rodzaju_Konta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rodzaj = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rodzaj_K__3DEB5E62B1B97259", x => x.id_Rodzaju_Konta);
                });

            migrationBuilder.CreateTable(
                name: "Rodzaj_Pakietu",
                columns: table => new
                {
                    id_Rodzaj_Pakietu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pakiet = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rodzaj_P__5AC44AECBDD60B6E", x => x.id_Rodzaj_Pakietu);
                });

            migrationBuilder.CreateTable(
                name: "Stan_Rezerwacji",
                columns: table => new
                {
                    id_Stanu_Rezerwacji = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stan_Rez__64566693192CC9E1", x => x.id_Stanu_Rezerwacji);
                });

            migrationBuilder.CreateTable(
                name: "Typ_Pojazdu",
                columns: table => new
                {
                    id_Typu_Pojazdu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typ = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Typ_Poja__16E966408762E54F", x => x.id_Typu_Pojazdu);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownicy",
                columns: table => new
                {
                    id_Uzytkownika = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imie = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    nazwisko = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    login = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    haslo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    utworzony = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    id_rodzaj_konta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Uzytkown__6242D14327CB7F51", x => x.id_Uzytkownika);
                    table.ForeignKey(
                        name: "Uzytkownicy_Rodz",
                        column: x => x.id_rodzaj_konta,
                        principalTable: "Rodzaj_Konta",
                        principalColumn: "id_Rodzaju_Konta");
                });

            migrationBuilder.CreateTable(
                name: "Ubezpieczenia",
                columns: table => new
                {
                    id_Ubezpieczenia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    nazwa_ubezpieczalni = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    id_rodzaj_pakietu = table.Column<int>(type: "int", nullable: true),
                    kwota = table.Column<decimal>(type: "smallmoney", nullable: true),
                    dostepnosc = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ubezpiec__0BDD86A241F355D7", x => x.id_Ubezpieczenia);
                    table.ForeignKey(
                        name: "Ubezpieczenia_Rodz",
                        column: x => x.id_rodzaj_pakietu,
                        principalTable: "Rodzaj_Pakietu",
                        principalColumn: "id_Rodzaj_Pakietu");
                });

            migrationBuilder.CreateTable(
                name: "Pojazd_Sztuka",
                columns: table => new
                {
                    id_Pojazd_Sztuka = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    marka = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_typ_pojazdu = table.Column<int>(type: "int", nullable: true),
                    pojemnosc_silnika = table.Column<decimal>(type: "smallmoney", nullable: true),
                    liczba_drzwi = table.Column<int>(type: "int", nullable: true),
                    liczba_pasazerow = table.Column<int>(type: "int", nullable: true),
                    automatyczna_skrzynia = table.Column<bool>(type: "bit", nullable: true),
                    rocznik = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pojazd_S__8E13BC71D8D6972E", x => x.id_Pojazd_Sztuka);
                    table.ForeignKey(
                        name: "Pojazd_Sztuka_Typ",
                        column: x => x.id_typ_pojazdu,
                        principalTable: "Typ_Pojazdu",
                        principalColumn: "id_Typu_Pojazdu");
                });

            migrationBuilder.CreateTable(
                name: "Pojazd",
                columns: table => new
                {
                    id_Pojazdu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_sztuki = table.Column<int>(type: "int", nullable: true),
                    liczba_sztuk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pojazd__8DA22F9818265292", x => x.id_Pojazdu);
                    table.ForeignKey(
                        name: "Pojazd_Sztuka_Poj",
                        column: x => x.id_sztuki,
                        principalTable: "Pojazd_Sztuka",
                        principalColumn: "id_Pojazd_Sztuka");
                });

            migrationBuilder.CreateTable(
                name: "Ogloszenia",
                columns: table => new
                {
                    id_Ogloszenia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_pojazdu = table.Column<int>(type: "int", nullable: true),
                    dostepnosc = table.Column<bool>(type: "bit", nullable: true),
                    data_dodania = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    kwota = table.Column<decimal>(type: "smallmoney", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ogloszen__DFA8FDD45C039118", x => x.id_Ogloszenia);
                    table.ForeignKey(
                        name: "Ogloszenia_Poj",
                        column: x => x.id_pojazdu,
                        principalTable: "Pojazd",
                        principalColumn: "id_Pojazdu");
                });

            migrationBuilder.CreateTable(
                name: "Rezerwacje",
                columns: table => new
                {
                    id_Rezerwacji = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ogloszenia = table.Column<int>(type: "int", nullable: true),
                    id_ubezpieczenia = table.Column<int>(type: "int", nullable: true),
                    id_uzytkownika = table.Column<int>(type: "int", nullable: true),
                    id_stan_rezerwacji = table.Column<int>(type: "int", nullable: true),
                    data_rozpoczecia_rezerwacji = table.Column<DateTime>(type: "datetime", nullable: true),
                    data_zakonczenia_rezerwacji = table.Column<DateTime>(type: "datetime", nullable: true),
                    utworzona = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    kwota_ubezpieczenia = table.Column<decimal>(type: "smallmoney", nullable: true),
                    kwota_dodatku = table.Column<decimal>(type: "smallmoney", nullable: true),
                    kwota_ogloszenia = table.Column<decimal>(type: "smallmoney", nullable: true),
                    kwota_rezerwacji = table.Column<decimal>(type: "smallmoney", nullable: true),
                    dostepnosc_pojazdu = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rezerwac__AE3F74D16EB12FDB", x => x.id_Rezerwacji);
                    table.ForeignKey(
                        name: "Rezerwacje_Og",
                        column: x => x.id_ogloszenia,
                        principalTable: "Ogloszenia",
                        principalColumn: "id_Ogloszenia");
                    table.ForeignKey(
                        name: "Rezerwacje_Stan",
                        column: x => x.id_stan_rezerwacji,
                        principalTable: "Stan_Rezerwacji",
                        principalColumn: "id_Stanu_Rezerwacji");
                    table.ForeignKey(
                        name: "Rezerwacje_Ub",
                        column: x => x.id_ubezpieczenia,
                        principalTable: "Ubezpieczenia",
                        principalColumn: "id_Ubezpieczenia");
                    table.ForeignKey(
                        name: "Rezerwacje_Uz",
                        column: x => x.id_uzytkownika,
                        principalTable: "Uzytkownicy",
                        principalColumn: "id_Uzytkownika");
                });

            migrationBuilder.CreateTable(
                name: "Dodatki_Rezerwacje",
                columns: table => new
                {
                    id_Dodatku_Rezerwacji = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_rezerwacji = table.Column<int>(type: "int", nullable: true),
                    id_dodatku = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dodatki___05E4C649E6502C2A", x => x.id_Dodatku_Rezerwacji);
                    table.ForeignKey(
                        name: "Dodatki_Rezerwacje_Dod",
                        column: x => x.id_dodatku,
                        principalTable: "Dodatki",
                        principalColumn: "id_Dodatku");
                    table.ForeignKey(
                        name: "Dodatki_Rezerwacje_Rez",
                        column: x => x.id_rezerwacji,
                        principalTable: "Rezerwacje",
                        principalColumn: "id_Rezerwacji");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dodatki_Rezerwacje_id_dodatku",
                table: "Dodatki_Rezerwacje",
                column: "id_dodatku");

            migrationBuilder.CreateIndex(
                name: "IX_Dodatki_Rezerwacje_id_rezerwacji",
                table: "Dodatki_Rezerwacje",
                column: "id_rezerwacji");

            migrationBuilder.CreateIndex(
                name: "IX_Ogloszenia_id_pojazdu",
                table: "Ogloszenia",
                column: "id_pojazdu");

            migrationBuilder.CreateIndex(
                name: "IX_Pojazd_id_sztuki",
                table: "Pojazd",
                column: "id_sztuki");

            migrationBuilder.CreateIndex(
                name: "IX_Pojazd_Sztuka_id_typ_pojazdu",
                table: "Pojazd_Sztuka",
                column: "id_typ_pojazdu");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacje_id_ogloszenia",
                table: "Rezerwacje",
                column: "id_ogloszenia");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacje_id_stan_rezerwacji",
                table: "Rezerwacje",
                column: "id_stan_rezerwacji");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacje_id_ubezpieczenia",
                table: "Rezerwacje",
                column: "id_ubezpieczenia");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacje_id_uzytkownika",
                table: "Rezerwacje",
                column: "id_uzytkownika");

            migrationBuilder.CreateIndex(
                name: "IX_Ubezpieczenia_id_rodzaj_pakietu",
                table: "Ubezpieczenia",
                column: "id_rodzaj_pakietu");

            migrationBuilder.CreateIndex(
                name: "IX_Uzytkownicy_id_rodzaj_konta",
                table: "Uzytkownicy",
                column: "id_rodzaj_konta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dodatki_Rezerwacje");

            migrationBuilder.DropTable(
                name: "Dodatki");

            migrationBuilder.DropTable(
                name: "Rezerwacje");

            migrationBuilder.DropTable(
                name: "Ogloszenia");

            migrationBuilder.DropTable(
                name: "Stan_Rezerwacji");

            migrationBuilder.DropTable(
                name: "Ubezpieczenia");

            migrationBuilder.DropTable(
                name: "Uzytkownicy");

            migrationBuilder.DropTable(
                name: "Pojazd");

            migrationBuilder.DropTable(
                name: "Rodzaj_Pakietu");

            migrationBuilder.DropTable(
                name: "Rodzaj_Konta");

            migrationBuilder.DropTable(
                name: "Pojazd_Sztuka");

            migrationBuilder.DropTable(
                name: "Typ_Pojazdu");
        }
    }
}
