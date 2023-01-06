using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VuelosAPI.Models
{
    public partial class sistem21_vuelosContext : DbContext
    {
        public sistem21_vuelosContext()
        {
        }

        public sistem21_vuelosContext(DbContextOptions<sistem21_vuelosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Vuelos> Vuelos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Vuelos>(entity =>
            {
                entity.ToTable("vuelos");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Clave)
                    .HasMaxLength(45)
                    .HasColumnName("clave");

                entity.Property(e => e.Destino)
                    .HasMaxLength(200)
                    .HasColumnName("destino");

                entity.Property(e => e.Estado)
                    .HasColumnType("int(11)")
                    .HasColumnName("estado");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Origen)
                    .HasMaxLength(200)
                    .HasColumnName("origen");

                entity.Property(e => e.Puerta)
                    .HasColumnType("int(11)")
                    .HasColumnName("puerta");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
