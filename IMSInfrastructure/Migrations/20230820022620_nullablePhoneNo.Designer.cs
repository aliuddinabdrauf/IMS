﻿// <auto-generated />
using System;
using IMSInfrastructure.DbContext.IMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IMSInfrastructure.Migrations
{
    [DbContext(typeof(ImsContext))]
    [Migration("20230820022620_nullablePhoneNo")]
    partial class nullablePhoneNo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_role", new[] { "user", "admin" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_status", new[] { "need_activation", "active", "disabled" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_type", new[] { "student", "staff", "industry" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IMSInfrastructure.DbContext.IMS.TblUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Instant>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_time");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("Text")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("varchar(12)")
                        .HasColumnName("phone_no");

                    b.Property<int[]>("Roles")
                        .IsRequired()
                        .HasColumnType("user_role[]")
                        .HasColumnName("roles");

                    b.Property<int>("Status")
                        .HasColumnType("user_status")
                        .HasColumnName("status");

                    b.Property<int>("Type")
                        .HasColumnType("user_type")
                        .HasColumnName("type");

                    b.Property<Instant>("UpdateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_time");

                    b.HasKey("Id")
                        .HasName("pk_tbl_user");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_user_email");

                    b.ToTable("tbl_user", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
