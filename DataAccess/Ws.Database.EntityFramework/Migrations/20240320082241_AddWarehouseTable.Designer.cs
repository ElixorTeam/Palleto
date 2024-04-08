﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ws.Database.EntityFramework;

#nullable disable

namespace Ws.Database.EntityFramework.Migrations
{
    [DbContext(typeof(WsDbContext))]
    [Migration("20240320082241_AddWarehouseTable")]
    partial class AddWarehousesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Boxes.BoxEntity", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.Property<Guid>("Uid1C")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID_1C");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(4,3)")
                        .HasColumnName("WEIGHT");

                    b.HasKey("Uid");

                    b.HasIndex(new[] { "Name" }, "UQ_BOXES_NAME")
                        .IsUnique();

                    b.HasIndex(new[] { "Uid1C" }, "UQ_BOXES_UID_1C")
                        .IsUnique();

                    b.ToTable("BOXES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Brands.BrandEntity", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.Property<Guid>("Uid1C")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID_1C");

                    b.HasKey("Uid");

                    b.HasIndex(new[] { "Name" }, "UQ_BRANDS_NAME")
                        .IsUnique();

                    b.HasIndex(new[] { "Uid1C" }, "UQ_BRANDS_UID_1C")
                        .IsUnique();

                    b.ToTable("BRANDS");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Bundles.BundleEntity", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.Property<Guid>("Uid1C")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID_1C");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(4,3)")
                        .HasColumnName("WEIGHT");

                    b.HasKey("Uid");

                    b.HasIndex(new[] { "Name" }, "UQ_BUNDLES_NAME")
                        .IsUnique();

                    b.HasIndex(new[] { "Uid1C" }, "UQ_BUNDLES_UID_1C")
                        .IsUnique();

                    b.ToTable("BUNDLES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Claims.ClaimEntity", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasColumnName("NAME");

                    b.HasKey("Uid");

                    b.HasIndex(new[] { "Name" }, "UQ_CLAIMS_NAME")
                        .IsUnique();

                    b.ToTable("CLAIMS");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Clips.ClipEntity", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.Property<Guid>("Uid1C")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID_1C");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(4,3)")
                        .HasColumnName("WEIGHT");

                    b.HasKey("Uid");

                    b.HasIndex(new[] { "Name" }, "UQ_CLIPS_NAME")
                        .IsUnique();

                    b.HasIndex(new[] { "Uid1C" }, "UQ_CLIPS_UID_1C")
                        .IsUnique();

                    b.ToTable("CLIPS");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.PalletMen.PalletManEntity", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("PATRONYMIC");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("SURNAME");

                    b.Property<Guid>("Uid1C")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID_1C");

                    b.HasKey("Uid");

                    b.HasIndex(new[] { "Name", "Surname", "Patronymic" }, "UQ_PALLET_MEN_FIO")
                        .IsUnique();

                    b.HasIndex(new[] { "Uid1C" }, "UQ_PALLET_MEN_UID_1C")
                        .IsUnique();

                    b.ToTable("PALLET_MEN");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.ProductionSites.ProductionSiteEntity", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("ADDRESS");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("NAME");

                    b.HasKey("Uid");

                    b.HasIndex(new[] { "Address" }, "UQ_PRODUCTION_SITES_ADDRESS")
                        .IsUnique();

                    b.HasIndex(new[] { "Name" }, "UQ_PRODUCTION_SITES_NAME")
                        .IsUnique();

                    b.ToTable("PRODUCTION_SITES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Zpl.StorageMethods.StorageMethodEntity", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.Property<string>("Zpl")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)")
                        .HasColumnName("DESCRIPTION");

                    b.HasKey("Uid");

                    b.HasIndex(new[] { "Name" }, "UQ_STORAGE_METHODS_NAME")
                        .IsUnique();

                    b.HasIndex(new[] { "Zpl" }, "UQ_STORAGE_METHODS_ZPL")
                        .IsUnique();

                    b.ToTable("STORAGE_METHODS");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Zpl.Templates.TemplateEntity", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(10240)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BODY");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.HasKey("Uid");

                    b.HasIndex(new[] { "Name" }, "UQ_TEMPLATES_NAME")
                        .IsUnique();

                    b.ToTable("TEMPLATES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Warehouses.WarehouseEntity", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.Property<Guid>("PRODUCTION_SITE_UID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Uid");

                    b.HasIndex("PRODUCTION_SITE_UID");

                    b.HasIndex(new[] { "Name" }, "UQ_WAREHOUSES_NAME")
                        .IsUnique();

                    b.ToTable("WAREHOUSES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Zpl.ZplResources.ZplResourceEntity", b =>
                {
                    b.Property<Guid>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("NAME");

                    b.Property<string>("Zpl")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)")
                        .HasColumnName("ZPL");

                    b.HasKey("Uid");

                    b.HasIndex(new[] { "Name" }, "UQ_ZPL_RESOURCES_NAME")
                        .IsUnique();

                    b.ToTable("ZPL_RESOURCES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Warehouses.WarehouseEntity", b =>
                {
                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref.ProductionSites.ProductionSiteEntity", "ProductionSites")
                        .WithMany()
                        .HasForeignKey("PRODUCTION_SITE_UID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductionSites");
                });
#pragma warning restore 612, 618
        }
    }
}
