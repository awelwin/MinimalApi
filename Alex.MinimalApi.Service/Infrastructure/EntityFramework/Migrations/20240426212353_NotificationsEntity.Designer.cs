﻿// <auto-generated />
using System;
using Alex.MinimalApi.Service.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Alex.MinimalApi.Service.Repository.EntiryFramework.Migrations
{
    [DbContext(typeof(MinimalApiDbContext))]
    [Migration("20240426212353_NotificationsEntity")]
    partial class NotificationsEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Alex.MinimalApi.Service.Repository.EntityFramework.Employee", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Alex.MinimalApi.Service.Repository.EntityFramework.Notification", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Alex.MinimalApi.Service.Repository.EntityFramework.TaxFile", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique()
                        .HasFilter("[EmployeeId] IS NOT NULL");

                    b.ToTable("TaxFile");
                });

            modelBuilder.Entity("Alex.MinimalApi.Service.Repository.EntityFramework.TaxFileRecord", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<double>("AmountClaimed")
                        .HasColumnType("float");

                    b.Property<double>("AmountPaid")
                        .HasColumnType("float");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("FinancialYear")
                        .HasColumnType("int");

                    b.Property<int>("TaxFileId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TaxFileId");

                    b.ToTable("TaxFileRecord");
                });

            modelBuilder.Entity("Alex.MinimalApi.Service.Repository.EntityFramework.TaxFile", b =>
                {
                    b.HasOne("Alex.MinimalApi.Service.Repository.EntityFramework.Employee", "Employee")
                        .WithOne("TaxFile")
                        .HasForeignKey("Alex.MinimalApi.Service.Repository.EntityFramework.TaxFile", "EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Alex.MinimalApi.Service.Repository.EntityFramework.TaxFileRecord", b =>
                {
                    b.HasOne("Alex.MinimalApi.Service.Repository.EntityFramework.TaxFile", "TaxFile")
                        .WithMany("TaxFileRecords")
                        .HasForeignKey("TaxFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaxFile");
                });

            modelBuilder.Entity("Alex.MinimalApi.Service.Repository.EntityFramework.Employee", b =>
                {
                    b.Navigation("TaxFile");
                });

            modelBuilder.Entity("Alex.MinimalApi.Service.Repository.EntityFramework.TaxFile", b =>
                {
                    b.Navigation("TaxFileRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
