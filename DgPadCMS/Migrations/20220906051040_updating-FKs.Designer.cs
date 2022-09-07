﻿// <auto-generated />
using System;
using DgPadCMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DgPadCMS.Migrations
{
    [DbContext(typeof(DgPadCMSContext))]
    [Migration("20220906051040_updating-FKs")]
    partial class updatingFKs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DgPadCMS.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PostTypeId");

                    b.ToTable("posts");
                });

            modelBuilder.Entity("DgPadCMS.Models.PostTerm", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("TermId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "TermId");

                    b.HasIndex("TermId");

                    b.ToTable("postTerms");
                });

            modelBuilder.Entity("DgPadCMS.Models.PostType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("postTypes");
                });

            modelBuilder.Entity("DgPadCMS.Models.PostTypeTaxonomy", b =>
                {
                    b.Property<int>("postTypeId")
                        .HasColumnType("int");

                    b.Property<int>("taxonomyId")
                        .HasColumnType("int");

                    b.HasKey("postTypeId", "taxonomyId");

                    b.HasIndex("taxonomyId");

                    b.ToTable("postTypeTaxonomies");
                });

            modelBuilder.Entity("DgPadCMS.Models.Taxonomy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("taxonomies");
                });

            modelBuilder.Entity("DgPadCMS.Models.Term", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("taxonomyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("taxonomyId");

                    b.ToTable("terms");
                });

            modelBuilder.Entity("DgPadCMS.Models.Post", b =>
                {
                    b.HasOne("DgPadCMS.Models.PostType", "postType")
                        .WithMany("posts")
                        .HasForeignKey("PostTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DgPadCMS.Models.PostTerm", b =>
                {
                    b.HasOne("DgPadCMS.Models.Post", "Post")
                        .WithMany("postTerms")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DgPadCMS.Models.Term", "Term")
                        .WithMany("postTerms")
                        .HasForeignKey("TermId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DgPadCMS.Models.PostTypeTaxonomy", b =>
                {
                    b.HasOne("DgPadCMS.Models.PostType", "PostType")
                        .WithMany("postTypeTaxonomies")
                        .HasForeignKey("postTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DgPadCMS.Models.Taxonomy", "Taxonomy")
                        .WithMany("postTypeTaxonomies")
                        .HasForeignKey("taxonomyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DgPadCMS.Models.Term", b =>
                {
                    b.HasOne("DgPadCMS.Models.Taxonomy", "taxonomy")
                        .WithMany("terms")
                        .HasForeignKey("taxonomyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
