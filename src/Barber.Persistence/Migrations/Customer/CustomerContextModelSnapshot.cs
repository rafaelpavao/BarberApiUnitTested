﻿// <auto-generated />
using System;
using Barber.Api.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Barber.Api.Migrations.Customer
{
    [DbContext(typeof(CustomerContext))]
    partial class CustomerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Barber.Api.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("character(9)")
                        .IsFixedLength();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character(2)")
                        .IsFixedLength();

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CEP = "88301-650",
                            City = "Itajaí",
                            CustomerId = 1,
                            District = "District1",
                            Number = 157,
                            State = "SC",
                            Street = "Agostinho Fernandes Vieira"
                        },
                        new
                        {
                            Id = 2,
                            CEP = "11111-111",
                            City = "Niterói",
                            CustomerId = 1,
                            District = "District2",
                            Number = 7,
                            State = "RJ",
                            Street = "Avenida Presidente Roosevelt"
                        },
                        new
                        {
                            Id = 3,
                            CEP = "22222-22",
                            City = "Itajaí",
                            CustomerId = 2,
                            District = "District3",
                            Number = 157,
                            State = "SC",
                            Street = "Street1"
                        },
                        new
                        {
                            Id = 4,
                            CEP = "33333-33",
                            City = "Itajaí",
                            CustomerId = 2,
                            District = "District4",
                            Number = 157,
                            State = "SC",
                            Street = "Agostinho Fernandes Vieira"
                        });
                });

            modelBuilder.Entity("Barber.Api.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("BirthdayDate")
                        .HasColumnType("date");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character(14)")
                        .IsFixedLength();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthdayDate = new DateOnly(2023, 8, 22),
                            CPF = "181.851.057-07",
                            Email = "guimasthomy@gmail.com",
                            Gender = 0,
                            Name = "Guilherme Thomy"
                        },
                        new
                        {
                            Id = 2,
                            BirthdayDate = new DateOnly(2023, 8, 22),
                            CPF = "111.111.111-11",
                            Email = "guirosa@gmail.com",
                            Gender = 0,
                            Name = "Guilherme Rosa"
                        });
                });

            modelBuilder.Entity("Barber.Api.Entities.Telephone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Telephones");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            Number = "+55 (47) 99238-1783",
                            Type = 1
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 1,
                            Number = "+55 (47) 99999-9999",
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            CustomerId = 2,
                            Number = "+55 (47) 88888-8888",
                            Type = 1
                        },
                        new
                        {
                            Id = 4,
                            CustomerId = 2,
                            Number = "+55 (47) 77777-7777",
                            Type = 0
                        });
                });

            modelBuilder.Entity("Barber.Api.Entities.Address", b =>
                {
                    b.HasOne("Barber.Api.Entities.Customer", "Customer")
                        .WithMany("Addresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Barber.Api.Entities.Telephone", b =>
                {
                    b.HasOne("Barber.Api.Entities.Customer", "Customer")
                        .WithMany("Telephones")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Barber.Api.Entities.Customer", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Telephones");
                });
#pragma warning restore 612, 618
        }
    }
}
