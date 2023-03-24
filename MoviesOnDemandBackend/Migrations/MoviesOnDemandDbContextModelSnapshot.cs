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

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Year")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Movies", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Genre = "Crime",
                            Title = "The Godfather",
                            Year = 1972
                        },
                        new
                        {
                            Id = 2,
                            Genre = "Action",
                            Title = "The Dark Knight",
                            Year = 2008
                        },
                        new
                        {
                            Id = 3,
                            Genre = "Crime",
                            Title = "Pulp Fiction",
                            Year = 1994
                        },
                        new
                        {
                            Id = 4,
                            Genre = "Drama",
                            Title = "Fight Club",
                            Year = 1999
                        },
                        new
                        {
                            Id = 5,
                            Genre = "Sci-Fi",
                            Title = "The Matrix",
                            Year = 1999
                        },
                        new
                        {
                            Id = 6,
                            Genre = "Comedy",
                            Title = "Life is Beautiful",
                            Year = 1997
                        },
                        new
                        {
                            Id = 7,
                            Genre = "Animation",
                            Title = "Spirited Away",
                            Year = 2001
                        },
                        new
                        {
                            Id = 8,
                            Genre = "Horror",
                            Title = "Psycho",
                            Year = 1960
                        },
                        new
                        {
                            Id = 9,
                            Genre = "Western",
                            Title = "Django Unchained",
                            Year = 2012
                        },
                        new
                        {
                            Id = 10,
                            Genre = "Action",
                            Title = "Top Gun: Maverick",
                            Year = 2022
                        });
                });

            modelBuilder.Entity("MoviesOnDemandBackend.Entities.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
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
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

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
                            AccountCreated = new DateTime(2023, 3, 24, 0, 0, 0, 0, DateTimeKind.Local),
                            Email = "admin@mod.com",
                            PasswordHash = new byte[] { 80, 255, 50, 244, 191, 215, 102, 9, 121, 186, 47, 39, 10, 158, 111, 129, 115, 147, 41, 66, 128, 65, 82, 177, 56, 37, 12, 102, 186, 181, 243, 20, 79, 32, 103, 137, 146, 172, 206, 57, 231, 55, 187, 8, 172, 169, 162, 166, 28, 68, 152, 55, 117, 179, 224, 244, 70, 97, 199, 198, 232, 40, 61, 193 },
                            PasswordSalt = new byte[] { 128, 48, 226, 194, 0, 53, 245, 12, 97, 111, 216, 240, 125, 12, 174, 244, 132, 112, 215, 85, 118, 186, 113, 133, 199, 82, 235, 60, 186, 155, 160, 208, 134, 73, 186, 195, 166, 42, 244, 218, 0, 155, 157, 13, 137, 78, 239, 97, 181, 44, 215, 193, 93, 210, 117, 232, 81, 176, 222, 157, 46, 157, 184, 74, 72, 231, 144, 209, 129, 28, 99, 103, 231, 156, 112, 255, 133, 6, 68, 105, 216, 26, 105, 151, 144, 146, 4, 34, 228, 235, 33, 15, 202, 159, 228, 121, 137, 149, 97, 34, 81, 123, 200, 224, 37, 135, 83, 206, 71, 101, 179, 163, 6, 84, 13, 241, 206, 104, 217, 241, 199, 200, 216, 22, 55, 164, 123, 60 },
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

            modelBuilder.Entity("MoviesOnDemandBackend.Entities.Rating", b =>
                {
                    b.HasOne("MoviesOnDemandBackend.Entities.Movie", "Movie")
                        .WithMany("Ratings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesOnDemandBackend.Entities.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
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

            modelBuilder.Entity("MoviesOnDemandBackend.Entities.Movie", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("MoviesOnDemandBackend.Entities.User", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("RefreshToken")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
