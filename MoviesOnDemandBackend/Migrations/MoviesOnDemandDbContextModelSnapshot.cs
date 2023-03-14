﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesOnDemandBackend.Entities;

#nullable disable

namespace MoviesOnDemandBackend.Migrations
{
    [DbContext(typeof(MoviesOnDemandDbContext))]
    partial class MoviesOnDemandDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieUser", b =>
                {
                    b.Property<int>("FavoriteMoviesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteMoviesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("MovieUser");
                });

            modelBuilder.Entity("MoviesOnDemandBackend.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Genre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumberOfRatings")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<decimal>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)")
                        .HasDefaultValue(0m);

                    b.Property<int>("SumOfRatings")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Year")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Movies", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Genre = "Crime",
                            NumberOfRatings = 0,
                            Rating = 0m,
                            SumOfRatings = 0,
                            Title = "The Godfather",
                            Year = 1972
                        },
                        new
                        {
                            Id = 2,
                            Genre = "Action",
                            NumberOfRatings = 0,
                            Rating = 0m,
                            SumOfRatings = 0,
                            Title = "The Dark Knight",
                            Year = 2008
                        },
                        new
                        {
                            Id = 3,
                            Genre = "Crime",
                            NumberOfRatings = 0,
                            Rating = 0m,
                            SumOfRatings = 0,
                            Title = "Pulp Fiction",
                            Year = 1994
                        },
                        new
                        {
                            Id = 4,
                            Genre = "Drama",
                            NumberOfRatings = 0,
                            Rating = 0m,
                            SumOfRatings = 0,
                            Title = "Fight Club",
                            Year = 1999
                        },
                        new
                        {
                            Id = 5,
                            Genre = "Sci-Fi",
                            NumberOfRatings = 0,
                            Rating = 0m,
                            SumOfRatings = 0,
                            Title = "The Matrix",
                            Year = 1999
                        },
                        new
                        {
                            Id = 6,
                            Genre = "Comedy",
                            NumberOfRatings = 0,
                            Rating = 0m,
                            SumOfRatings = 0,
                            Title = "Life is Beautiful",
                            Year = 1997
                        },
                        new
                        {
                            Id = 7,
                            Genre = "Animation",
                            NumberOfRatings = 0,
                            Rating = 0m,
                            SumOfRatings = 0,
                            Title = "Spirited Away",
                            Year = 2001
                        },
                        new
                        {
                            Id = 8,
                            Genre = "Horror",
                            NumberOfRatings = 0,
                            Rating = 0m,
                            SumOfRatings = 0,
                            Title = "Psycho",
                            Year = 1960
                        },
                        new
                        {
                            Id = 9,
                            Genre = "Western",
                            NumberOfRatings = 0,
                            Rating = 0m,
                            SumOfRatings = 0,
                            Title = "Django Unchained",
                            Year = 2012
                        },
                        new
                        {
                            Id = 10,
                            Genre = "Action",
                            NumberOfRatings = 0,
                            Rating = 0m,
                            SumOfRatings = 0,
                            Title = "Top Gun: Maverick",
                            Year = 2022
                        });
                });

            modelBuilder.Entity("MoviesOnDemandBackend.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("MoviesOnDemandBackend.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AccountCreated")
                        .HasColumnType("Date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("user");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountCreated = new DateTime(2023, 3, 4, 0, 0, 0, 0, DateTimeKind.Local),
                            Email = "admin@mod.com",
                            PasswordHash = new byte[] { 101, 207, 129, 49, 5, 83, 83, 19, 163, 102, 249, 234, 3, 229, 125, 125, 12, 47, 212, 209, 188, 3, 205, 99, 136, 211, 218, 15, 125, 162, 83, 94, 225, 240, 223, 199, 85, 156, 249, 225, 87, 42, 207, 248, 147, 62, 247, 4, 240, 250, 0, 33, 56, 5, 243, 194, 82, 224, 199, 173, 187, 207, 161, 106 },
                            PasswordSalt = new byte[] { 205, 52, 30, 120, 71, 17, 147, 136, 125, 20, 15, 141, 152, 58, 207, 205, 172, 21, 187, 229, 69, 205, 134, 212, 50, 31, 144, 175, 251, 163, 82, 153, 28, 207, 203, 126, 71, 29, 54, 66, 134, 32, 62, 39, 199, 94, 186, 95, 177, 14, 220, 92, 82, 238, 114, 2, 70, 208, 1, 156, 63, 60, 89, 93, 87, 189, 233, 246, 192, 146, 226, 186, 41, 219, 124, 114, 25, 146, 16, 13, 164, 19, 11, 5, 119, 229, 173, 207, 66, 35, 17, 131, 38, 28, 12, 45, 187, 29, 150, 193, 213, 117, 204, 54, 127, 186, 63, 226, 238, 206, 143, 78, 118, 78, 165, 224, 158, 89, 208, 86, 125, 23, 89, 31, 112, 195, 105, 210 },
                            Role = "admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("MovieUser", b =>
                {
                    b.HasOne("MoviesOnDemandBackend.Entities.Movie", null)
                        .WithMany()
                        .HasForeignKey("FavoriteMoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesOnDemandBackend.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoviesOnDemandBackend.Entities.RefreshToken", b =>
                {
                    b.HasOne("MoviesOnDemandBackend.Entities.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("MoviesOnDemandBackend.Entities.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MoviesOnDemandBackend.Entities.User", b =>
                {
                    b.Navigation("RefreshToken")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
