using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WSPPCars.Models;

public partial class DbWsppcarsContext : DbContext
{
    public DbWsppcarsContext()
    {
    }

    public DbWsppcarsContext(DbContextOptions<DbWsppcarsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dodatki> Dodatkis { get; set; }

    public virtual DbSet<DodatkiRezerwacje> DodatkiRezerwacjes { get; set; }

    public virtual DbSet<Ogloszenium> Ogloszenia { get; set; }

    public virtual DbSet<Pojazd> Pojazds { get; set; }

    public virtual DbSet<PojazdSztuka> PojazdSztukas { get; set; }

    public virtual DbSet<Rezerwacje> Rezerwacjes { get; set; }

    public virtual DbSet<RodzajKontum> RodzajKonta { get; set; }

    public virtual DbSet<RodzajPakietu> RodzajPakietus { get; set; }

    public virtual DbSet<StanRezerwacji> StanRezerwacjis { get; set; }

    public virtual DbSet<TypPojazdu> TypPojazdus { get; set; }

    public virtual DbSet<Ubezpieczenium> Ubezpieczenia { get; set; }

    public virtual DbSet<Uzytkownicy> Uzytkownicies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbWSPPcars;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dodatki>(entity =>
        {
            entity.HasKey(e => e.IdDodatku).HasName("PK__Dodatki__EDC18B725C43E0B7");

            entity.ToTable("Dodatki");

            entity.Property(e => e.IdDodatku).HasColumnName("id_Dodatku");
            entity.Property(e => e.Dostepnosc).HasColumnName("dostepnosc");
            entity.Property(e => e.Kwota)
                .HasColumnType("smallmoney")
                .HasColumnName("kwota");
            entity.Property(e => e.LiczbaSztuk)
                .HasMaxLength(255)
                .HasColumnName("liczba_sztuk");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(255)
                .HasColumnName("nazwa");
        });

        modelBuilder.Entity<DodatkiRezerwacje>(entity =>
        {
            entity.HasKey(e => e.IdDodatkuRezerwacji).HasName("PK__Dodatki___05E4C649E6502C2A");

            entity.ToTable("Dodatki_Rezerwacje");

            entity.Property(e => e.IdDodatkuRezerwacji).HasColumnName("id_Dodatku_Rezerwacji");
            entity.Property(e => e.IdDodatku).HasColumnName("id_dodatku");
            entity.Property(e => e.IdRezerwacji).HasColumnName("id_rezerwacji");

            entity.HasOne(d => d.IdDodatkuNavigation).WithMany(p => p.DodatkiRezerwacjes)
                .HasForeignKey(d => d.IdDodatku)
                .HasConstraintName("Dodatki_Rezerwacje_Dod");

            entity.HasOne(d => d.IdRezerwacjiNavigation).WithMany(p => p.DodatkiRezerwacjes)
                .HasForeignKey(d => d.IdRezerwacji)
                .HasConstraintName("Dodatki_Rezerwacje_Rez");
        });

        modelBuilder.Entity<Ogloszenium>(entity =>
        {
            entity.HasKey(e => e.IdOgloszenia).HasName("PK__Ogloszen__DFA8FDD45C039118");

            entity.Property(e => e.IdOgloszenia).HasColumnName("id_Ogloszenia");
            entity.Property(e => e.DataDodania)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("data_dodania");
            entity.Property(e => e.Dostepnosc).HasColumnName("dostepnosc");
            entity.Property(e => e.IdPojazdu).HasColumnName("id_pojazdu");
            entity.Property(e => e.Kwota)
                .HasColumnType("smallmoney")
                .HasColumnName("kwota");

            entity.HasOne(d => d.IdPojazduNavigation).WithMany(p => p.Ogloszenia)
                .HasForeignKey(d => d.IdPojazdu)
                .HasConstraintName("Ogloszenia_Poj");
        });

        modelBuilder.Entity<Pojazd>(entity =>
        {
            entity.HasKey(e => e.IdPojazdu).HasName("PK__Pojazd__8DA22F9818265292");

            entity.ToTable("Pojazd");

            entity.Property(e => e.IdPojazdu).HasColumnName("id_Pojazdu");
            entity.Property(e => e.IdSztuki).HasColumnName("id_sztuki");
            entity.Property(e => e.LiczbaSztuk).HasColumnName("liczba_sztuk");

            entity.HasOne(d => d.IdSztukiNavigation).WithMany(p => p.Pojazds)
                .HasForeignKey(d => d.IdSztuki)
                .HasConstraintName("Pojazd_Sztuka_Poj");
        });

        modelBuilder.Entity<PojazdSztuka>(entity =>
        {
            entity.HasKey(e => e.IdPojazdSztuka).HasName("PK__Pojazd_S__8E13BC71D8D6972E");

            entity.ToTable("Pojazd_Sztuka");

            entity.Property(e => e.IdPojazdSztuka).HasColumnName("id_Pojazd_Sztuka");
            entity.Property(e => e.AutomatycznaSkrzynia).HasColumnName("automatyczna_skrzynia");
            entity.Property(e => e.IdTypPojazdu).HasColumnName("id_typ_pojazdu");
            entity.Property(e => e.LiczbaDrzwi).HasColumnName("liczba_drzwi");
            entity.Property(e => e.LiczbaPasazerow).HasColumnName("liczba_pasazerow");
            entity.Property(e => e.Marka)
                .HasMaxLength(255)
                .HasColumnName("marka");
            entity.Property(e => e.Model).HasColumnName("model");
            entity.Property(e => e.PojemnoscSilnika)
                .HasColumnType("smallmoney")
                .HasColumnName("pojemnosc_silnika");
            entity.Property(e => e.Rocznik).HasColumnName("rocznik");

            entity.HasOne(d => d.IdTypPojazduNavigation).WithMany(p => p.PojazdSztukas)
                .HasForeignKey(d => d.IdTypPojazdu)
                .HasConstraintName("Pojazd_Sztuka_Typ");
        });

        modelBuilder.Entity<Rezerwacje>(entity =>
        {
            entity.HasKey(e => e.IdRezerwacji).HasName("PK__Rezerwac__AE3F74D16EB12FDB");

            entity.ToTable("Rezerwacje");

            entity.Property(e => e.IdRezerwacji).HasColumnName("id_Rezerwacji");
            entity.Property(e => e.DataRozpoczeciaRezerwacji)
                .HasColumnType("datetime")
                .HasColumnName("data_rozpoczecia_rezerwacji");
            entity.Property(e => e.DataZakonczeniaRezerwacji)
                .HasColumnType("datetime")
                .HasColumnName("data_zakonczenia_rezerwacji");
            entity.Property(e => e.DostepnoscPojazdu).HasColumnName("dostepnosc_pojazdu");
            entity.Property(e => e.IdOgloszenia).HasColumnName("id_ogloszenia");
            entity.Property(e => e.IdStanRezerwacji).HasColumnName("id_stan_rezerwacji");
            entity.Property(e => e.IdUbezpieczenia).HasColumnName("id_ubezpieczenia");
            entity.Property(e => e.IdUzytkownika).HasColumnName("id_uzytkownika");
            entity.Property(e => e.KwotaDodatku)
                .HasColumnType("smallmoney")
                .HasColumnName("kwota_dodatku");
            entity.Property(e => e.KwotaOgloszenia)
                .HasColumnType("smallmoney")
                .HasColumnName("kwota_ogloszenia");
            entity.Property(e => e.KwotaRezerwacji)
                .HasColumnType("smallmoney")
                .HasColumnName("kwota_rezerwacji");
            entity.Property(e => e.KwotaUbezpieczenia)
                .HasColumnType("smallmoney")
                .HasColumnName("kwota_ubezpieczenia");
            entity.Property(e => e.Utworzona)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("utworzona");

            entity.HasOne(d => d.IdOgloszeniaNavigation).WithMany(p => p.Rezerwacjes)
                .HasForeignKey(d => d.IdOgloszenia)
                .HasConstraintName("Rezerwacje_Og");

            entity.HasOne(d => d.IdStanRezerwacjiNavigation).WithMany(p => p.Rezerwacjes)
                .HasForeignKey(d => d.IdStanRezerwacji)
                .HasConstraintName("Rezerwacje_Stan");

            entity.HasOne(d => d.IdUbezpieczeniaNavigation).WithMany(p => p.Rezerwacjes)
                .HasForeignKey(d => d.IdUbezpieczenia)
                .HasConstraintName("Rezerwacje_Ub");

            entity.HasOne(d => d.IdUzytkownikaNavigation).WithMany(p => p.Rezerwacjes)
                .HasForeignKey(d => d.IdUzytkownika)
                .HasConstraintName("Rezerwacje_Uz");
        });

        modelBuilder.Entity<RodzajKontum>(entity =>
        {
            entity.HasKey(e => e.IdRodzajuKonta).HasName("PK__Rodzaj_K__3DEB5E62B1B97259");

            entity.ToTable("Rodzaj_Konta");

            entity.Property(e => e.IdRodzajuKonta).HasColumnName("id_Rodzaju_Konta");
            entity.Property(e => e.Rodzaj)
                .HasMaxLength(255)
                .HasColumnName("rodzaj");
        });

        modelBuilder.Entity<RodzajPakietu>(entity =>
        {
            entity.HasKey(e => e.IdRodzajPakietu).HasName("PK__Rodzaj_P__5AC44AECBDD60B6E");

            entity.ToTable("Rodzaj_Pakietu");

            entity.Property(e => e.IdRodzajPakietu).HasColumnName("id_Rodzaj_Pakietu");
            entity.Property(e => e.Pakiet)
                .HasMaxLength(255)
                .HasColumnName("pakiet");
        });

        modelBuilder.Entity<StanRezerwacji>(entity =>
        {
            entity.HasKey(e => e.IdStanuRezerwacji).HasName("PK__Stan_Rez__64566693192CC9E1");

            entity.ToTable("Stan_Rezerwacji");

            entity.Property(e => e.IdStanuRezerwacji).HasColumnName("id_Stanu_Rezerwacji");
            entity.Property(e => e.Stan)
                .HasMaxLength(255)
                .HasColumnName("stan");
        });

        modelBuilder.Entity<TypPojazdu>(entity =>
        {
            entity.HasKey(e => e.IdTypuPojazdu).HasName("PK__Typ_Poja__16E966408762E54F");

            entity.ToTable("Typ_Pojazdu");

            entity.Property(e => e.IdTypuPojazdu).HasColumnName("id_Typu_Pojazdu");
            entity.Property(e => e.Typ)
                .HasMaxLength(255)
                .HasColumnName("typ");
        });

        modelBuilder.Entity<Ubezpieczenium>(entity =>
        {
            entity.HasKey(e => e.IdUbezpieczenia).HasName("PK__Ubezpiec__0BDD86A241F355D7");

            entity.Property(e => e.IdUbezpieczenia).HasColumnName("id_Ubezpieczenia");
            entity.Property(e => e.Dostepnosc).HasColumnName("dostepnosc");
            entity.Property(e => e.IdRodzajPakietu).HasColumnName("id_rodzaj_pakietu");
            entity.Property(e => e.Kwota)
                .HasColumnType("smallmoney")
                .HasColumnName("kwota");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(255)
                .HasColumnName("nazwa");
            entity.Property(e => e.NazwaUbezpieczalni)
                .HasMaxLength(255)
                .HasColumnName("nazwa_ubezpieczalni");

            entity.HasOne(d => d.IdRodzajPakietuNavigation).WithMany(p => p.Ubezpieczenia)
                .HasForeignKey(d => d.IdRodzajPakietu)
                .HasConstraintName("Ubezpieczenia_Rodz");
        });

        modelBuilder.Entity<Uzytkownicy>(entity =>
        {
            entity.HasKey(e => e.IdUzytkownika).HasName("PK__Uzytkown__6242D14327CB7F51");

            entity.ToTable("Uzytkownicy");

            entity.Property(e => e.IdUzytkownika).HasColumnName("id_Uzytkownika");
            entity.Property(e => e.Haslo)
                .HasMaxLength(255)
                .HasColumnName("haslo");
            entity.Property(e => e.IdRodzajKonta).HasColumnName("id_rodzaj_konta");
            entity.Property(e => e.Imie)
                .HasMaxLength(255)
                .HasColumnName("imie");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Nazwisko)
                .HasMaxLength(255)
                .HasColumnName("nazwisko");
            entity.Property(e => e.Utworzony)
                .HasColumnName("utworzony");

            entity.HasOne(d => d.IdRodzajKontaNavigation).WithMany(p => p.Uzytkownicies)
                .HasForeignKey(d => d.IdRodzajKonta)
                .HasConstraintName("Uzytkownicy_Rodz");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
