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
    [Migration("20240404071250_ВуDeleteUqIndex_NestingTable")]
    partial class ВуDeleteUqIndex_NestingTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LINES_PLUS_FK", b =>
                {
                    b.Property<Guid>("PLU_UID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LINE_UID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PLU_UID", "LINE_UID");

                    b.HasIndex("LINE_UID");

                    b.ToTable("LINES_PLUS_FK");
                });

            modelBuilder.Entity("USERS_СLAIMS_FK", b =>
                {
                    b.Property<Guid>("CLAIM_UID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("USER_UID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CLAIM_UID", "USER_UID");

                    b.HasIndex("USER_UID");

                    b.ToTable("USERS_СLAIMS_FK");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Claims.ClaimEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasColumnName("NAME");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "UQ_CLAIMS_NAME")
                        .IsUnique();

                    b.ToTable("CLAIMS");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Lines.LineEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("Counter")
                        .HasColumnType("int")
                        .HasColumnName("COUNTER");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.Property<int>("Number")
                        .HasColumnType("int")
                        .HasColumnName("NUMBER");

                    b.Property<Guid>("PRINTER_UID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PcName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasColumnName("PC_NAME");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(8)")
                        .HasColumnName("TYPE");

                    b.Property<Guid>("WAREHOUSE_UID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PRINTER_UID");

                    b.HasIndex("WAREHOUSE_UID");

                    b.HasIndex(new[] { "Name" }, "UQ_LINES_NAME")
                        .IsUnique();

                    b.HasIndex(new[] { "Number" }, "UQ_LINES_NUMBER")
                        .IsUnique();

                    b.HasIndex(new[] { "PcName" }, "UQ_LINES_PC_NAME")
                        .IsUnique();

                    b.ToTable("LINES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.PalletMen.PalletManEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

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

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name", "Surname", "Patronymic" }, "UQ_PALLET_MEN_FIO")
                        .IsUnique();

                    b.HasIndex(new[] { "Uid1C" }, "UQ_PALLET_MEN_UID_1C")
                        .IsUnique();

                    b.ToTable("PALLET_MEN");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Zpl.PluResources.PluResourceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<Guid>("STORAGE_METHOD_UID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TEMPLATE_UID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("STORAGE_METHOD_UID");

                    b.HasIndex("TEMPLATE_UID");

                    b.ToTable("PLUS_RESOURCES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Printers.PrinterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("IP_V4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasColumnName("NAME");

                    b.Property<Guid>("PRODUCTION_SITE_UID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(8)")
                        .HasColumnName("TYPE");

                    b.HasKey("Id");

                    b.HasIndex("PRODUCTION_SITE_UID");

                    b.HasIndex(new[] { "Ip" }, "UQ_PRINTERS_IP_V4")
                        .IsUnique();

                    b.HasIndex(new[] { "Name" }, "UQ_PRINTERS_NAME")
                        .IsUnique();

                    b.ToTable("PRINTERS");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.ProductionSites.ProductionSiteEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("ADDRESS");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("NAME");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Address" }, "UQ_PRODUCTION_SITES_ADDRESS")
                        .IsUnique();

                    b.HasIndex(new[] { "Name" }, "UQ_PRODUCTION_SITES_NAME")
                        .IsUnique();

                    b.ToTable("PRODUCTION_SITES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Zpl.StorageMethods.StorageMethodEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.Property<string>("Zpl")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)")
                        .HasColumnName("ZPL");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "UQ_STORAGE_METHODS_NAME")
                        .IsUnique();

                    b.HasIndex(new[] { "Zpl" }, "UQ_STORAGE_METHODS_ZPL")
                        .IsUnique();

                    b.ToTable("STORAGE_METHODS");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Zpl.Templates.TemplateEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(10240)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BODY");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "UQ_TEMPLATES_NAME")
                        .IsUnique();

                    b.ToTable("TEMPLATES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Users.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("LoginDt")
                        .HasColumnType("datetime2")
                        .HasColumnName("LOGIN_DT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.Property<Guid?>("PRODUCTION_SITE_UID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PRODUCTION_SITE_UID");

                    b.HasIndex(new[] { "Name" }, "UQ_USERS_NAME")
                        .IsUnique();

                    b.ToTable("USERS");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Warehouses.WarehouseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.Property<Guid>("PRODUCTION_SITE_UID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PRODUCTION_SITE_UID");

                    b.HasIndex(new[] { "Name" }, "UQ_WAREHOUSES_NAME")
                        .IsUnique();

                    b.ToTable("WAREHOUSES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Zpl.ZplResources.ZplResourceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

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

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "UQ_ZPL_RESOURCES_NAME")
                        .IsUnique();

                    b.ToTable("ZPL_RESOURCES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Boxes.BoxEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("NAME");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(4,3)")
                        .HasColumnName("WEIGHT");

                    b.HasKey("Id");

                    b.ToTable("BOXES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Brands.BrandEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("NAME");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "UQ_BRANDS_NAME")
                        .IsUnique();

                    b.ToTable("BRANDS");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Bundles.BundleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("NAME");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(4,3)")
                        .HasColumnName("WEIGHT");

                    b.HasKey("Id");

                    b.ToTable("BUNDLES");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Clips.ClipEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("NAME");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(4,3)")
                        .HasColumnName("WEIGHT");

                    b.HasKey("Id");

                    b.ToTable("CLIPS");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Nestings.NestingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<Guid>("BoxId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BOX_UID");

                    b.Property<short>("BundleCount")
                        .HasColumnType("smallint")
                        .HasColumnName("BUNDLE_COUNT");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("Id");

                    b.HasIndex("BoxId");

                    b.ToTable("NESTINGS");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Plus.PluEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UID");

                    b.Property<Guid>("BrandEntityId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BRAND_UID");

                    b.Property<Guid>("BundleEntityId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BUNDLE_UID");

                    b.Property<DateTime>("ChangeDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CHANGE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<Guid>("ClipEntityId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CLIP_UID");

                    b.Property<DateTime>("CreateDt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATE_DT")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("Ean13")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar")
                        .HasColumnName("EAN_13");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("FULL_NAME");

                    b.Property<bool>("IsWeight")
                        .HasColumnType("bit")
                        .HasColumnName("IS_WEIGHT");

                    b.Property<string>("Itf14")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar")
                        .HasColumnName("ITF_14");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NAME");

                    b.Property<short>("Number")
                        .HasColumnType("smallint")
                        .HasColumnName("NUMBER");

                    b.Property<short>("ShelfLifeDays")
                        .HasColumnType("smallint")
                        .HasColumnName("SHELF_LIFE_DAYS");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(4,3)")
                        .HasColumnName("WEIGHT");

                    b.HasKey("Id");

                    b.HasIndex("BrandEntityId");

                    b.HasIndex("BundleEntityId");

                    b.HasIndex("ClipEntityId");

                    b.HasIndex(new[] { "Number" }, "UQ_LINES_NUMBER")
                        .IsUnique();

                    b.ToTable("PLUS");
                });

            modelBuilder.Entity("LINES_PLUS_FK", b =>
                {
                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref.Lines.LineEntity", null)
                        .WithMany()
                        .HasForeignKey("LINE_UID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref1C.Plus.PluEntity", null)
                        .WithMany()
                        .HasForeignKey("PLU_UID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("USERS_СLAIMS_FK", b =>
                {
                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref.Claims.ClaimEntity", null)
                        .WithMany()
                        .HasForeignKey("CLAIM_UID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref.Users.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("USER_UID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Lines.LineEntity", b =>
                {
                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref.Printers.PrinterEntity", "Printer")
                        .WithMany()
                        .HasForeignKey("PRINTER_UID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref.Warehouses.WarehouseEntity", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WAREHOUSE_UID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Printer");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Zpl.PluResources.PluResourceEntity", b =>
                {
                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref1C.Plus.PluEntity", null)
                        .WithOne("Resource")
                        .HasForeignKey("Ws.Database.EntityFramework.Entities.Zpl.PluResources.PluResourceEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ws.Database.EntityFramework.Entities.Zpl.StorageMethods.StorageMethodEntity", "StorageMethod")
                        .WithMany()
                        .HasForeignKey("STORAGE_METHOD_UID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ws.Database.EntityFramework.Entities.Zpl.Templates.TemplateEntity", "Template")
                        .WithMany()
                        .HasForeignKey("TEMPLATE_UID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StorageMethod");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Printers.PrinterEntity", b =>
                {
                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref.ProductionSites.ProductionSiteEntity", "ProductionSite")
                        .WithMany()
                        .HasForeignKey("PRODUCTION_SITE_UID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductionSite");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Users.UserEntity", b =>
                {
                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref.ProductionSites.ProductionSiteEntity", "ProductionSite")
                        .WithMany()
                        .HasForeignKey("PRODUCTION_SITE_UID");

                    b.Navigation("ProductionSite");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref.Warehouses.WarehouseEntity", b =>
                {
                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref.ProductionSites.ProductionSiteEntity", "ProductionSite")
                        .WithMany()
                        .HasForeignKey("PRODUCTION_SITE_UID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductionSite");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Nestings.NestingEntity", b =>
                {
                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref1C.Boxes.BoxEntity", "Box")
                        .WithMany()
                        .HasForeignKey("BoxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref1C.Plus.PluEntity", null)
                        .WithOne()
                        .HasForeignKey("Ws.Database.EntityFramework.Entities.Ref1C.Nestings.NestingEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Box");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Plus.PluEntity", b =>
                {
                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref1C.Brands.BrandEntity", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref1C.Bundles.BundleEntity", "Bundle")
                        .WithMany()
                        .HasForeignKey("BundleEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ws.Database.EntityFramework.Entities.Ref1C.Clips.ClipEntity", "Clip")
                        .WithMany()
                        .HasForeignKey("ClipEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Bundle");

                    b.Navigation("Clip");
                });

            modelBuilder.Entity("Ws.Database.EntityFramework.Entities.Ref1C.Plus.PluEntity", b =>
                {
                    b.Navigation("Resource")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
