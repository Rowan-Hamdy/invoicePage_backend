﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using invoicePage.Data;

#nullable disable

namespace invoicePage.Migrations
{
    [DbContext(typeof(SalesDbContext))]
    partial class SalesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("invoicePage.Models.Invoice", b =>
                {
                    b.Property<int>("inv_ID")
                        .HasColumnType("int");

                    b.Property<string>("cust_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("inv_date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("items_count")
                        .HasColumnType("int");

                    b.Property<double?>("total_price")
                        .HasColumnType("float");

                    b.HasKey("inv_ID")
                        .HasName("PK__Invoice__A7F1E3C1F5D810D5");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("invoicePage.Models.Invoice_Detail", b =>
                {
                    b.Property<int>("inv_ID")
                        .HasColumnType("int");

                    b.Property<string>("item")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<double?>("price")
                        .HasColumnType("float");

                    b.Property<int?>("quantity")
                        .HasColumnType("int");

                    b.Property<double?>("total_per_Quantity")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("float")
                        .HasComputedColumnSql("([price]*[quantity])", false);

                    b.HasKey("inv_ID", "item")
                        .HasName("PK__Invoice___0E6A6BE3F90AE381");

                    b.ToTable("Invoice_Details");
                });

            modelBuilder.Entity("invoicePage.Models.Invoice_Detail", b =>
                {
                    b.HasOne("invoicePage.Models.Invoice", "inv")
                        .WithMany("Invoice_Details")
                        .HasForeignKey("inv_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("invoiceId_FK");

                    b.Navigation("inv");
                });

            modelBuilder.Entity("invoicePage.Models.Invoice", b =>
                {
                    b.Navigation("Invoice_Details");
                });
#pragma warning restore 612, 618
        }
    }
}
