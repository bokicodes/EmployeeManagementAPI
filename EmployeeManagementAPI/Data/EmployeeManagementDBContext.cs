using System;
using System.Collections.Generic;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmployeeManagementAPI.Data
{
    public partial class EmployeeManagementDBContext : DbContext
    {
        public EmployeeManagementDBContext()
        {
        }

        public EmployeeManagementDBContext(DbContextOptions<EmployeeManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DodeljenZadatak> DodeljeniZadaci { get; set; } = null!;
        public virtual DbSet<OrganizacionaCelina> OrganizacioneCeline { get; set; } = null!;
        public virtual DbSet<RadnoMesto> RadnaMesta { get; set; } = null!;
        public virtual DbSet<TipZadatka> TipoviZadataka { get; set; } = null!;
        public virtual DbSet<Zaposleni> Zaposleni { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DodeljenZadatak>(entity =>
            {
                entity.HasKey(e => new { e.RadnoMestoId, e.ZadatakId, e.ZaposleniId });

                entity.ToTable("DodeljenZadatak");

                entity.Property(e => e.DatumZadavanja).HasColumnType("datetime");

                entity.Property(e => e.DatumZavrsetka).HasColumnType("datetime");

                entity.HasOne(d => d.Zaposleni)
                    .WithMany(p => p.DodeljeniZadaci)
                    .HasForeignKey(d => d.ZaposleniId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DodeljenZadatak_Zaposleni");

                entity.HasOne(d => d.TipZadatka)
                    .WithMany(p => p.DodeljeniZadaci)
                    .HasForeignKey(d => new { d.RadnoMestoId, d.ZadatakId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DodeljenZadatak_TipZadatka");
            });

            modelBuilder.Entity<OrganizacionaCelina>(entity =>
            {
                entity.HasKey(e => e.OrgCelinaId);

                entity.ToTable("OrganizacionaCelina");

                entity.Property(e => e.NazivOC)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NazivOC");

                entity.Property(e => e.OpisOC)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OpisOC");
            });

            modelBuilder.Entity<RadnoMesto>(entity =>
            {
                entity.ToTable("RadnoMesto");

                entity.Property(e => e.NazivRM)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NazivRM");

                entity.Property(e => e.OpisRM)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OpisRM");
            });

            modelBuilder.Entity<TipZadatka>(entity =>
            {
                entity.HasKey(e => new { e.RadnoMestoId, e.ZadatakId });

                entity.ToTable("TipZadatka");

                entity.Property(e => e.ZadatakId).ValueGeneratedOnAdd();

                entity.Property(e => e.NazivZad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpisZad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.RadnoMesto)
                    .WithMany(p => p.TipoviZadataka)
                    .HasForeignKey(d => d.RadnoMestoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipZadatka_RadnoMesto");
            });

            modelBuilder.Entity<Zaposleni>(entity =>
            {
                entity.ToTable("Zaposleni");

                entity.Property(e => e.DatumZaposlenja).HasColumnType("datetime");

                entity.Property(e => e.Ime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prezime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.OrgCelina)
                    .WithMany(p => p.Zaposleni)
                    .HasForeignKey(d => d.OrgCelinaId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Zaposleni_OrganizacionaCelina");

                entity.HasOne(d => d.RadnoMesto)
                    .WithMany(p => p.Zaposleni)
                    .HasForeignKey(d => d.RadnoMestoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zaposleni_RadnoMesto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
