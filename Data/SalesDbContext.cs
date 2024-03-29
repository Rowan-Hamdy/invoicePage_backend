﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using invoicePage.Models;

namespace invoicePage.Data
{
    public partial class SalesDbContext : DbContext
    {
        public SalesDbContext()
        {
        }

        public SalesDbContext(DbContextOptions<SalesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Invoice_Detail> Invoice_Details { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SalesDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.inv_ID)
                    .HasName("PK__Invoice__A7F1E3C1F5D810D5");

                entity.Property(e => e.inv_ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Invoice_Detail>(entity =>
            {
                entity.HasKey(e => new { e.inv_ID, e.item })
                    .HasName("PK__Invoice___0E6A6BE3F90AE381");

                entity.Property(e => e.total_per_Quantity).HasComputedColumnSql("([price]*[quantity])", false);

                entity.HasOne(d => d.inv)
                    .WithMany(p => p.Invoice_Details)
                    .HasForeignKey(d => d.inv_ID)
                    .HasConstraintName("invoiceId_FK");
            });

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}