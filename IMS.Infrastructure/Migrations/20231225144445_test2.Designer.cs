﻿// <auto-generated />
using System;
using System.Collections.Generic;
using IMS.Infrastructure.DbContext.IMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IMS.Infrastructure.Migrations
{
    [DbContext(typeof(ImsContext))]
    [Migration("20231225144445_test2")]
    partial class test2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_gender", new[] { "male", "female" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_role", new[] { "user", "admin" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_status", new[] { "need_activation", "active", "disabled", "deleted" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_type", new[] { "student", "staff", "industry" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_generate_v1()");

                    b.Property<Instant>("TimestampCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp_created");

                    b.Property<Instant>("TimestampUpdated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp_updated");

                    b.HasKey("Id");

                    b.ToTable("tbl_base", (string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblCourse", b =>
                {
                    b.HasBaseType("IMS.Infrastructure.DbContext.IMS.TblBase");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<Guid>("FacultyId")
                        .HasColumnType("uuid")
                        .HasColumnName("faculty_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_course_code");

                    b.HasIndex("FacultyId")
                        .HasDatabaseName("ix_tbl_course_faculty_id");

                    b.ToTable("tbl_course", (string)null);
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblFaculty", b =>
                {
                    b.HasBaseType("IMS.Infrastructure.DbContext.IMS.TblBase");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_faculty_code");

                    b.ToTable("tbl_faculty", (string)null);
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblStaff", b =>
                {
                    b.HasBaseType("IMS.Infrastructure.DbContext.IMS.TblBase");

                    b.Property<int>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.Property<string>("StaffId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character(10)")
                        .HasColumnName("staff_id")
                        .IsFixedLength();

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasIndex("StaffId")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_staff_staff_id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_staff_user_id");

                    b.ToTable("tbl_staff", (string)null);
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblStudent", b =>
                {
                    b.HasBaseType("IMS.Infrastructure.DbContext.IMS.TblBase");

                    b.Property<List<string>>("Address")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("text[]")
                        .HasColumnName("address");

                    b.Property<int>("Gender")
                        .HasColumnType("user_gender")
                        .HasColumnName("gender");

                    b.Property<string>("IcNo")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character(12)")
                        .HasColumnName("ic_no")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.Property<List<string>>("PhoneNo")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("text[]")
                        .HasColumnName("phone_no");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("bytea")
                        .HasColumnName("profile_picture");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character(10)")
                        .HasColumnName("student_id")
                        .IsFixedLength();

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_student_user_id");

                    b.HasIndex("StudentId", "IcNo")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_student_student_id_ic_no");

                    b.ToTable("tbl_student", (string)null);
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblStudentCourse", b =>
                {
                    b.HasBaseType("IMS.Infrastructure.DbContext.IMS.TblBase");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid")
                        .HasColumnName("course_id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid")
                        .HasColumnName("student_id");

                    b.HasIndex("CourseId")
                        .HasDatabaseName("ix_tbl_student_course_course_id");

                    b.HasIndex("StudentId")
                        .HasDatabaseName("ix_tbl_student_course_student_id");

                    b.ToTable("tbl_student_course", (string)null);
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblUser", b =>
                {
                    b.HasBaseType("IMS.Infrastructure.DbContext.IMS.TblBase");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("password_hash");

                    b.Property<string>("PasswordSalt")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("password_salt");

                    b.Property<int[]>("Roles")
                        .IsRequired()
                        .HasColumnType("user_role[]")
                        .HasColumnName("roles");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_tbl_user_email");

                    b.ToTable("tbl_user", (string)null);
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblCourse", b =>
                {
                    b.HasOne("IMS.Infrastructure.DbContext.IMS.TblFaculty", "Faculty")
                        .WithMany("Courses")
                        .HasForeignKey("FacultyId")
                        .IsRequired()
                        .HasConstraintName("fk_tbl_course_tbl_faculty_faculty_id");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblStaff", b =>
                {
                    b.HasOne("IMS.Infrastructure.DbContext.IMS.TblUser", "User")
                        .WithOne("Staff")
                        .HasForeignKey("IMS.Infrastructure.DbContext.IMS.TblStaff", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tbl_staff_tbl_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblStudent", b =>
                {
                    b.HasOne("IMS.Infrastructure.DbContext.IMS.TblUser", "User")
                        .WithOne("Student")
                        .HasForeignKey("IMS.Infrastructure.DbContext.IMS.TblStudent", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tbl_student_tbl_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblStudentCourse", b =>
                {
                    b.HasOne("IMS.Infrastructure.DbContext.IMS.TblCourse", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .IsRequired()
                        .HasConstraintName("fk_tbl_student_course_tbl_course_course_id");

                    b.HasOne("IMS.Infrastructure.DbContext.IMS.TblStudent", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("fk_tbl_student_course_tbl_student_student_id");

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblCourse", b =>
                {
                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblFaculty", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblStudent", b =>
                {
                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("IMS.Infrastructure.DbContext.IMS.TblUser", b =>
                {
                    b.Navigation("Staff");

                    b.Navigation("Student");
                });
#pragma warning restore 612, 618
        }
    }
}
