using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MagicBirdApiCore.Models
{
    public partial class MTSLoggerCenterContext : DbContext
    {
        public MTSLoggerCenterContext()
        {
        }

        public MTSLoggerCenterContext(DbContextOptions<MTSLoggerCenterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MtsToolsWebServiceLogger> MtsToolsWebServiceLoggers { get; set; } = null!;
        public virtual DbSet<MagicBirdAccount> MagicBirdAccounts { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MtsToolsWebServiceLogger>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("mtsToolsWebServiceLogger");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Exception)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Level)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Logger)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.Thread)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MagicBirdAccount>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
