/*CREATE DATABASE Baza_Wypozyczalnia;
GO
use [Baza_Wypozyczalnia];
GO
*/

CREATE TABLE [Rezerwacje] (
  [ID] integer PRIMARY KEY,
  [id_og�oszenia] integer,
  [id_ubezpieczenia] integer,
  [id_u�ytkownika] integer,
  [id_stan_rezerwacji] integer,
  [data_rozpocz�cia_rezerwacji] timestamp,
  [data_zako�czenia_rezerwacji] timestamp,
  [utworzona] timestamp,
  [kwota_ubezpieczenia] float ,
  [kwota_dodatku] float ,
  [kwota_ogloszenia] float ,
  [kwota_rezerwacji] float ,
  [dostepnosc_pojazdu] bit
)
GO

CREATE TABLE [U�ytkownicy] (
  [id] integer PRIMARY KEY,
  [imie] nvarchar(255),
  [nazwisko] nvarchar(255),
  [login] nvarchar(255),
  [haslo] nvarchar(255),
  [utworzony] timestamp,
  [id_rodzaj_konta] integer
)
GO

CREATE TABLE [Og�oszenie] (
  [id] integer PRIMARY KEY,
  [id_pojazdu] integer,
  [dost�pno��] bit,
  [data_dodania] timestamp,
  [kwota] float 
)
GO

CREATE TABLE [Dodatki_Rezerwacje] (
  [id] integer PRIMARY KEY,
  [id_rezerwacji] integer,
  [id_dodatku] integer
)
GO

CREATE TABLE [Rodzaj_Konta] (
  [id] integer PRIMARY KEY,
  [rodzaj] nvarchar(255)
)
GO

CREATE TABLE [Stan_Rezerwacji] (
  [id] integer PRIMARY KEY,
  [stan] nvarchar(255)
)
GO

CREATE TABLE [Typ_Pojazdu] (
  [id] integer PRIMARY KEY,
  [typ] nvarchar(255)
)
GO

CREATE TABLE [Rodzaj_Pakietu] (
  [id] integer PRIMARY KEY,
  [pakiet] nvarchar(255)
)
GO

CREATE TABLE [Pojazd] (
  [id] integer PRIMARY KEY,
  [id_sztuki] integer,
  [liczba_sztuk] integer
)
GO

CREATE TABLE [Dodatki] (
  [id] integer PRIMARY KEY,
  [nazwa] nvarchar(255),
  [liczba_sztuk] nvarchar(255),
  [dostepnosc] bit,
  [kwota] float 
)
GO

CREATE TABLE [Ubezpieczenia] (
  [id] integer PRIMARY KEY,
  [nazwa] nvarchar(255),
  [nazwa_ubezpieczalni] nvarchar(255),
  [id_rodzaj_pakietu] integer,
  [kwota] float ,
  [dostepnosc] bit
)
GO

CREATE TABLE [Pojazd_Sztuka] (
  [id] integer PRIMARY KEY,
  [marka] nvarchar(255),
  [model] integer,
  [id_typ_pojazdu] integer,
  [pojemnosc_silnika] float,
  [liczba_drzwi] integer,
  [liczba_pasa�er�w] integer,
  [automatyczna_skrzynia] bit,
  [rocznik] date
)
GO

ALTER TABLE [Rezerwacje] ADD CONSTRAINT [user_posts] FOREIGN KEY ([id_u�ytkownika]) REFERENCES [U�ytkownicy] ([id])
GO

ALTER TABLE [Rezerwacje] ADD CONSTRAINT [user_posts] FOREIGN KEY ([id_ubezpieczenia]) REFERENCES [Ubezpieczenia] ([id])
GO

ALTER TABLE [Dodatki_Rezerwacje] ADD CONSTRAINT [user_posts] FOREIGN KEY ([id_dodatku]) REFERENCES [Dodatki] ([id])
GO

ALTER TABLE [Dodatki_Rezerwacje] ADD CONSTRAINT [user_posts] FOREIGN KEY ([id_rezerwacji]) REFERENCES [Rezerwacje] ([id])
GO

ALTER TABLE [Og�oszenie] ADD CONSTRAINT [user_posts] FOREIGN KEY ([id_pojazdu]) REFERENCES [Pojazd] ([id])
GO

ALTER TABLE [Rezerwacje] ADD CONSTRAINT [user_posts] FOREIGN KEY ([id_og�oszenia]) REFERENCES [Og�oszenie] ([id])
GO

ALTER TABLE [U�ytkownicy] ADD CONSTRAINT [user_posts] FOREIGN KEY ([id_rodzaj_konta]) REFERENCES [Rodzaj_Konta] ([id])
GO

ALTER TABLE [Rezerwacje] ADD CONSTRAINT [user_posts] FOREIGN KEY ([id_stan_rezerwacji]) REFERENCES [Stan_Rezerwacji] ([id])
GO

ALTER TABLE [Pojazd_Sztuka] ADD CONSTRAINT [user_posts] FOREIGN KEY ([id_typ_pojazdu]) REFERENCES [Typ_Pojazdu] ([id])
GO

ALTER TABLE [Ubezpieczenia] ADD CONSTRAINT [user_posts] FOREIGN KEY ([id_rodzaj_pakietu]) REFERENCES [Rodzaj_Pakietu] ([id])
GO

ALTER TABLE [Pojazd_Sztuka] ADD CONSTRAINT [user_posts] FOREIGN KEY ([id]) REFERENCES [Pojazd] ([id_sztuki])
GO
