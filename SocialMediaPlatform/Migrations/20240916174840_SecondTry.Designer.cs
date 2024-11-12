﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialMediaPlatform.Data;

#nullable disable

namespace SocialMediaPlatform.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240916174840_SecondTry")]
    partial class SecondTry
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SocialMediaPlatform.Models.Creator", b =>
                {
                    b.Property<int>("CreatorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CreatorID"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("Followers")
                        .HasColumnType("int");

                    b.Property<int>("Following")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPost")
                        .HasColumnType("int");

                    b.Property<string>("PredictionImage")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserDetailsID")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CreatorID");

                    b.HasIndex("UserDetailsID")
                        .IsUnique();

                    b.ToTable("Creators");
                });

            modelBuilder.Entity("SocialMediaPlatform.Models.UserDetails", b =>
                {
                    b.Property<int>("UserDetailsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserDetailsID"));

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserDetailsID");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("SocialMediaPlatform.Models.Creator", b =>
                {
                    b.HasOne("SocialMediaPlatform.Models.UserDetails", "UserDetails")
                        .WithOne()
                        .HasForeignKey("SocialMediaPlatform.Models.Creator", "UserDetailsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
