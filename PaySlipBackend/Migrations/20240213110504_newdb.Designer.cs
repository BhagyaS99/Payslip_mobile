﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaySlipBackend.Context;

#nullable disable

namespace PaySlipBackend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240213110504_newdb")]
    partial class newdb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PaySlipBackend.Models.Employee", b =>
                {
                    b.Property<int>("EmpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmpId"));

                    b.Property<string>("DOJ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmpId");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("PaySlipBackend.Models.PayDetails", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<double>("BPdeduction")
                        .HasColumnType("float");

                    b.Property<double>("BasicPay")
                        .HasColumnType("float");

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<double>("HAdeduction")
                        .HasColumnType("float");

                    b.Property<double>("HouseRentAllowance")
                        .HasColumnType("float");

                    b.Property<double>("MAdeduction")
                        .HasColumnType("float");

                    b.Property<double>("MealAllowance")
                        .HasColumnType("float");

                    b.Property<string>("PayPeriod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransactionId");

                    b.HasIndex("EmpId");

                    b.ToTable("PayDetails", (string)null);
                });

            modelBuilder.Entity("PaySlipBackend.Models.PayDetails", b =>
                {
                    b.HasOne("PaySlipBackend.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
