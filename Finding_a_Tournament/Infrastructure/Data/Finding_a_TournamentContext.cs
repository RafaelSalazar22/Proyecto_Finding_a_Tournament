using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Finding_a_Tournament.Domain.Entities;

#nullable disable

namespace Finding_a_Tournament.Infrastructure.Data
{
    public partial class Finding_a_TournamentContext : DbContext
    {
        public Finding_a_TournamentContext()
        {
        }

        public Finding_a_TournamentContext(DbContextOptions<Finding_a_TournamentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Club> Club { get; set; }
        public virtual DbSet<ClubServicio> ClubServicio { get; set; }
        public virtual DbSet<Torneo> Torneos { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=Finding_a_Tournament");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Club>(entity =>
            {
                entity.HasKey(e => e.IdClub);
                entity.ToTable("Club");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Logotipo).IsRequired();

                entity.Property(e => e.NombreClub)
                    .IsRequired()
                    .HasMaxLength(200) 
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoContacto)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClubServicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio);
                entity.ToTable("ServiciosClub");
                
                entity.Property(e => e.Diciplina)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.HorarioDiciplina)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
                entity.HasOne(d => d.Club)
                    /*.WithOne(p => p.ClubServicio)
                    .HasForeignKey<ClubServicio>(d => d.IdClub)*/
                    .WithOne(p => p.ClubServicio)
                    .HasForeignKey<ClubServicio>(d => d.IdClub)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Club_Servicio_Club");
            });

            modelBuilder.Entity<Torneo>(entity =>
            {
                entity.HasKey(e => e.IdTorneo);
                entity.ToTable("Torneo");

                entity.Property(e => e.NombreTorneo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TipoTorneo)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
