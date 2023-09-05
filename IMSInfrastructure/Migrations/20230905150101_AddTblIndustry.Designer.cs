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
    [Migration("20230905150101_AddTblIndustry")]
    partial class AddTblIndustry
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_gender", new[] { "male", "female" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_role", new[] { "user", "admin" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_status", new[] { "need_activation", "active", "disabled" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_type", new[] { "student", "staff", "industry" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IMSInfrastructure.DbContext.IMS.TblIndustry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Instant>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("name");

                    b.Property<Instant>("UpdateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_time");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_tbl_industry");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_tbl_industry_user_id");

                    b.ToTable("tbl_industry", (string)null);
                });

            modelBuilder.Entity("IMSInfrastructure.DbContext.IMS.TblStaff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Instant>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_time");

                    b.Property<int>("Gender")
                        .HasColumnType("user_gender")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("name");

                    b.Property<string>("StaffId")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("staff_id");

                    b.Property<Instant>("UpdateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_time");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_tbl_staff");

                    b.HasIndex("StaffId")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_staff_staff_id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_staff_user_id");

                    b.ToTable("tbl_staff", (string)null);
                });

            modelBuilder.Entity("IMSInfrastructure.DbContext.IMS.TblStudent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Instant>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_time");

                    b.Property<int>("Gender")
                        .HasColumnType("user_gender")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("student_id");

                    b.Property<Instant>("UpdateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("update_time");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_tbl_student");

                    b.HasIndex("StudentId")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_student_student_id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_student_user_id");

                    b.ToTable("tbl_student", (string)null);
                });

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

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("Text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("Text")
                        .HasColumnName("password_salt");

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

            modelBuilder.Entity("IMSInfrastructure.DbContext.IMS.TblIndustry", b =>
                {
                    b.HasOne("IMSInfrastructure.DbContext.IMS.TblUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tbl_industry_tbl_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IMSInfrastructure.DbContext.IMS.TblStaff", b =>
                {
                    b.HasOne("IMSInfrastructure.DbContext.IMS.TblUser", "User")
                        .WithOne("Staff")
                        .HasForeignKey("IMSInfrastructure.DbContext.IMS.TblStaff", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tbl_staff_tbl_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IMSInfrastructure.DbContext.IMS.TblStudent", b =>
                {
                    b.HasOne("IMSInfrastructure.DbContext.IMS.TblUser", "User")
                        .WithOne("Student")
                        .HasForeignKey("IMSInfrastructure.DbContext.IMS.TblStudent", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tbl_student_tbl_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IMSInfrastructure.DbContext.IMS.TblUser", b =>
                {
                    b.Navigation("Staff")
                        .IsRequired();

                    b.Navigation("Student")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
