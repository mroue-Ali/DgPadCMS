using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DgPadCMS.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "postTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "taxonomies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taxonomies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    PostTypeId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Detail = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_posts_postTypes_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "postTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "postTypeTaxonomies",
                columns: table => new
                {
                    postTypeId = table.Column<int>(nullable: false),
                    taxonomyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postTypeTaxonomies", x => new { x.postTypeId, x.taxonomyId });
                    table.ForeignKey(
                        name: "FK_postTypeTaxonomies_postTypes_postTypeId",
                        column: x => x.postTypeId,
                        principalTable: "postTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_postTypeTaxonomies_taxonomies_taxonomyId",
                        column: x => x.taxonomyId,
                        principalTable: "taxonomies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "terms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    taxonomyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_terms_taxonomies_taxonomyId",
                        column: x => x.taxonomyId,
                        principalTable: "taxonomies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "postTerms",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    TermId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postTerms", x => new { x.PostId, x.TermId });
                    table.ForeignKey(
                        name: "FK_postTerms_posts_PostId",
                        column: x => x.PostId,
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_postTerms_terms_TermId",
                        column: x => x.TermId,
                        principalTable: "terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_posts_PostTypeId",
                table: "posts",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_postTerms_TermId",
                table: "postTerms",
                column: "TermId");

            migrationBuilder.CreateIndex(
                name: "IX_postTypeTaxonomies_taxonomyId",
                table: "postTypeTaxonomies",
                column: "taxonomyId");

            migrationBuilder.CreateIndex(
                name: "IX_terms_taxonomyId",
                table: "terms",
                column: "taxonomyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "postTerms");

            migrationBuilder.DropTable(
                name: "postTypeTaxonomies");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "terms");

            migrationBuilder.DropTable(
                name: "postTypes");

            migrationBuilder.DropTable(
                name: "taxonomies");
        }
    }
}
