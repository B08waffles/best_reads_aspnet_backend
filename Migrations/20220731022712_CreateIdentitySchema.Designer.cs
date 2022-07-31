﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using best_reads_backend.Models;

#nullable disable

namespace best_reads_backend.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20220731022712_CreateIdentitySchema")]
    partial class CreateIdentitySchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("AuthorBook");
                });

            modelBuilder.Entity("best_reads_backend.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DOB")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)");

                    b.HasKey("Id");

                    b.ToTable("Author", (string)null);
                });

            modelBuilder.Entity("best_reads_backend.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("longtext");

                    b.Property<string>("Published")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)");

                    b.HasKey("Id");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.HasOne("best_reads_backend.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("best_reads_backend.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
