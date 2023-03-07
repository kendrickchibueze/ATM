﻿// <auto-generated />
using System;
using ATM_DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ATM_DAL.Migrations
{
    [DbContext(typeof(AtmDbContext))]
    [Migration("20230305221634_DisableCascadeBehaviorForTransacToBankRelationship")]
    partial class DisableCascadeBehaviorForTransacToBankRelationship
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ATM_DAL.Entities.BankAccounts", b =>
                {
                    b.Property<long>("CardNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BankAccountId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PinCode")
                        .HasColumnType("int");

                    b.Property<bool>("isLocked")
                        .HasColumnType("bit");

                    b.HasKey("CardNumber")
                        .HasName("PK_BankAccounts_CardNumber");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("ATM_DAL.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("BankAccountNoFrom")
                        .HasColumnType("bigint");

                    b.Property<long>("BankAccountNoTo")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TransactionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountNoFrom");

                    b.HasIndex("BankAccountNoTo");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ATM_DAL.Entities.Transaction", b =>
                {
                    b.HasOne("ATM_DAL.Entities.BankAccounts", "FromAccount")
                        .WithMany("FromTransactions")
                        .HasForeignKey("BankAccountNoFrom")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ATM_DAL.Entities.BankAccounts", "ToAccount")
                        .WithMany("ToTransactions")
                        .HasForeignKey("BankAccountNoTo")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("FromAccount");

                    b.Navigation("ToAccount");
                });

            modelBuilder.Entity("ATM_DAL.Entities.BankAccounts", b =>
                {
                    b.Navigation("FromTransactions");

                    b.Navigation("ToTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
