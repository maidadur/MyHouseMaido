﻿// <auto-generated />
using System;
using Maid.Manga.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Maid.Manga.DB.Migrations
{
    [DbContext(typeof(MangaDbContext))]
    [Migration("20190408180837_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Maid.Manga.DB.MangaChapterInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Date");

                    b.Property<string>("Href");

                    b.Property<Guid?>("MangaId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MangaId");

                    b.ToTable("MangaChapterInfo");
                });

            modelBuilder.Entity("Maid.Manga.DB.MangaInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Href");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MangaInfo");
                });

            modelBuilder.Entity("Maid.Manga.DB.MangaChapterInfo", b =>
                {
                    b.HasOne("Maid.Manga.DB.MangaInfo", "Manga")
                        .WithMany("Chapters")
                        .HasForeignKey("MangaId");
                });
#pragma warning restore 612, 618
        }
    }
}
