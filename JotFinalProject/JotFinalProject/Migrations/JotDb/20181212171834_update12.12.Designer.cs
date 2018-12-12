﻿// <auto-generated />
using JotFinalProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JotFinalProject.Migrations.JotDb
{
    [DbContext(typeof(JotDbContext))]
    [Migration("20181212171834_update12.12")]
    partial class update1212
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JotFinalProject.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("JotFinalProject.Models.ImageUploaded", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageUrl");

                    b.Property<string>("OperationLocation");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("ImageUploadeds");
                });

            modelBuilder.Entity("JotFinalProject.Models.Note", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryID");

                    b.Property<string>("Text");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("JotFinalProject.Models.Note", b =>
                {
                    b.HasOne("JotFinalProject.Models.Category", "Category")
                        .WithMany("Notes")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
