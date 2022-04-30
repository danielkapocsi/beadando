using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using beadando;

namespace beadando.AutoDir
{
    public partial class DanceDb : DbContext
    {
        public DanceDb()
        {
        }

        public DanceDb(DbContextOptions<DanceDb> options)
            : base(options)
        {
        }

        public virtual DbSet<CompProd> CompProds { get; set; } = null!;
        public virtual DbSet<Competition> Competitions { get; set; } = null!;
        public virtual DbSet<Production> Productions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Temp\\Competition.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompProd>(entity =>
            {
                entity.ToTable("CompProd");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompIds).HasColumnName("compIDs");

                entity.Property(e => e.ProdId).HasColumnName("prodID");
            });

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.ToTable("Competition");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Production>(entity =>
            {
                entity.ToTable("Production");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AgeGroup).HasColumnName("ageGroup");

                entity.Property(e => e.Association)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("association");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.NoOfCompetitors).HasColumnName("noOfCompetitors");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
