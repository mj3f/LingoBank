﻿// <auto-generated />
using LingoBank.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LingoBank.Database.Migrations
{
    [DbContext(typeof(LingoContext))]
    [Migration("20210405211157_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("LingoBank.Database.Entities.LanguageEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("LingoBank.Database.Entities.PhraseEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("LanguageEntityId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LanguageId")
                        .HasColumnType("longtext");

                    b.Property<string>("SourceLanguage")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TargetLanguage")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Translation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("LanguageEntityId");

                    b.ToTable("Phrases");
                });

            modelBuilder.Entity("LingoBank.Database.Entities.PhraseEntity", b =>
                {
                    b.HasOne("LingoBank.Database.Entities.LanguageEntity", null)
                        .WithMany("Phrases")
                        .HasForeignKey("LanguageEntityId");
                });

            modelBuilder.Entity("LingoBank.Database.Entities.LanguageEntity", b =>
                {
                    b.Navigation("Phrases");
                });
#pragma warning restore 612, 618
        }
    }
}
