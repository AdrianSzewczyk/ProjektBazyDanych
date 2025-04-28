CREATE TABLE [Rezerwacje] (
  [id_Rezerwacji] integer PRIMARY KEY NOT NULL IDENTITY,
  [id_ogloszenia] integer,
  [id_ubezpieczenia] integer,
  [id_uzytkownika] integer,
  [id_stan_rezerwacji] integer,
  [data_rozpoczecia_rezerwacji] datetime,
  [data_zakonczenia_rezerwacji] datetime,
  [utworzona] timestamp,
  [kwota_ubezpieczenia] smallmoney,
  [kwota_dodatku] smallmoney,
  [kwota_ogloszenia] smallmoney,
  [kwota_rezerwacji] smallmoney,
  [dostepnosc_pojazdu] bit
)
GO

CREATE TABLE [Uzytkownicy] (
  [id_Uzytkownika] integer PRIMARY KEY NOT NULL IDENTITY,
  [imie] nvarchar(255),
  [nazwisko] nvarchar(255),
  [login] nvarchar(255),
  [haslo] nvarchar(255),
  [utworzony] timestamp,
  [id_rodzaj_konta] integer
)
GO

CREATE TABLE [Ogloszenia] (
  [id_Ogloszenia] integer PRIMARY KEY NOT NULL IDENTITY,
  [id_pojazdu] integer,
  [dostepnosc] bit,
  [data_dodania] timestamp,
  [kwota] smallmoney
)
GO

CREATE TABLE [Dodatki_Rezerwacje] (
  [id_Dodatku_Rezerwacji] integer PRIMARY KEY NOT NULL IDENTITY,
  [id_rezerwacji] integer,
  [id_dodatku] integer
)
GO

CREATE TABLE [Rodzaj_Konta] (
  [id_Rodzaju_Konta] integer PRIMARY KEY NOT NULL IDENTITY,
  [rodzaj] nvarchar(255)
)
GO

CREATE TABLE [Stan_Rezerwacji] (
  [id_Stanu_Rezerwacji] integer PRIMARY KEY NOT NULL IDENTITY,
  [stan] nvarchar(255)
)
GO

CREATE TABLE [Typ_Pojazdu] (
  [id_Typu_Pojazdu] integer PRIMARY KEY NOT NULL IDENTITY,
  [typ] nvarchar(255)
)
GO

CREATE TABLE [Rodzaj_Pakietu] (
  [id_Rodzaj_Pakietu] integer PRIMARY KEY NOT NULL IDENTITY,
  [pakiet] nvarchar(255)
)
GO

CREATE TABLE [Pojazd] (
  [id_Pojazdu] integer PRIMARY KEY NOT NULL IDENTITY,
  [id_sztuki] integer,
  [liczba_sztuk] integer
)
GO

CREATE TABLE [Dodatki] (
  [id_Dodatku] integer PRIMARY KEY NOT NULL IDENTITY,
  [nazwa] nvarchar(255),
  [liczba_sztuk] nvarchar(255),
  [dostepnosc] bit,
  [kwota] smallmoney
)
GO

CREATE TABLE [Ubezpieczenia] (
  [id_Ubezpieczenia] integer PRIMARY KEY NOT NULL IDENTITY,
  [nazwa] nvarchar(255),
  [nazwa_ubezpieczalni] nvarchar(255),
  [id_rodzaj_pakietu] integer,
  [kwota] smallmoney,
  [dostepnosc] bit
)
GO

CREATE TABLE [Pojazd_Sztuka] (
  [id_Pojazd_Sztuka] integer PRIMARY KEY NOT NULL IDENTITY,
  [marka] nvarchar(255),
  [model] integer,
  [id_typ_pojazdu] integer,
  [pojemnosc_silnika] smallmoney,
  [liczba_drzwi] integer,
  [liczba_pasazerow] integer,
  [automatyczna_skrzynia] bit,
  [rocznik] date
)
GO