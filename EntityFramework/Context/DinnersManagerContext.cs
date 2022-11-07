using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DinnerWebScraper.EntityFramework.Models;

namespace DinnerWebScraper.EntityFramework.Context
{
    public partial class DinnersManagerContext : DbContext
    {
        public DinnersManagerContext()
        {
        }

        public DinnersManagerContext(DbContextOptions<DinnersManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Models.Dinner> Dinners { get; set; } = null!;
        public virtual DbSet<DinnersStatus> DinnersStatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=KAMIL-PC\\SQLEXPRESS;Database=DinnersManager;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Dinner>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Type).HasMaxLength(10);
            });

            modelBuilder.Entity<DinnersStatus>(entity =>
            {
                entity.ToTable("DinnersStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DinnersDate).HasColumnType("date");

                entity.Property(e => e.Downloaded).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
