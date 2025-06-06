﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(PaymentDbContext))]
    [Migration("20250505172731_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence<int>("PaymentSequence", "dbo");

            modelBuilder.Entity("Domain.Entities.BankTransferLeg", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BankTransferPaymentId")
                        .HasColumnType("int");

                    b.Property<string>("Beneficiary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BankTransferPaymentId");

                    b.ToTable("BankTransferLegs", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR dbo.PaymentSequence");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ProcessedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.BankTransferPayment", b =>
                {
                    b.HasBaseType("Domain.Entities.Payment");

                    b.Property<string>("BankAccount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoutingNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("BankTransferPayments", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.CreditCardPayment", b =>
                {
                    b.HasBaseType("Domain.Entities.Payment");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardholderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Expiry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CreditCardPayments", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.PaypalPayment", b =>
                {
                    b.HasBaseType("Domain.Entities.Payment");

                    b.Property<string>("PayerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaypalTransactionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("PaypalPayments", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.BankTransferLeg", b =>
                {
                    b.HasOne("Domain.Entities.BankTransferPayment", "BankTransferPayment")
                        .WithMany("TransferLegs")
                        .HasForeignKey("BankTransferPaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankTransferPayment");
                });

            modelBuilder.Entity("Domain.Entities.BankTransferPayment", b =>
                {
                    b.Navigation("TransferLegs");
                });
#pragma warning restore 612, 618
        }
    }
}
