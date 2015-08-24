using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace DemocoreMigrations
{
    public partial class initial : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Abv",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false),
                    Percentage = table.Column(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abv", x => x.Id);
                });
            migration.CreateTable(
                name: "Base",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base", x => x.Id);
                });
            migration.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });
            migration.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });
            migration.CreateTable(
                name: "CountryOfOrigin",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryOfOrigin", x => x.Id);
                });
            migration.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                });
            migration.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false),
                    ByteArray = table.Column(type: "varbinary(max)", nullable: true),
                    FileExtension = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });
            migration.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false),
                    Name = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.Id);
                });
            migration.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false),
                    AbvId = table.Column(type: "uniqueidentifier", nullable: true),
                    BrandId = table.Column(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column(type: "uniqueidentifier", nullable: true),
                    CountryOfOriginId = table.Column(type: "uniqueidentifier", nullable: true),
                    Description = table.Column(type: "nvarchar(max)", nullable: true),
                    GradeId = table.Column(type: "uniqueidentifier", nullable: true),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    ProducerId = table.Column(type: "uniqueidentifier", nullable: true),
                    ThumbnailId = table.Column(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Abv_AbvId",
                        columns: x => x.AbvId,
                        referencedTable: "Abv",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Brand_BrandId",
                        columns: x => x.BrandId,
                        referencedTable: "Brand",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        columns: x => x.CategoryId,
                        referencedTable: "Category",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_CountryOfOrigin_CountryOfOriginId",
                        columns: x => x.CountryOfOriginId,
                        referencedTable: "CountryOfOrigin",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Grade_GradeId",
                        columns: x => x.GradeId,
                        referencedTable: "Grade",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Producer_ProducerId",
                        columns: x => x.ProducerId,
                        referencedTable: "Producer",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_Image_ThumbnailId",
                        columns: x => x.ThumbnailId,
                        referencedTable: "Image",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "Bottle",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column(type: "uniqueidentifier", nullable: true),
                    SizeInCentiLiters = table.Column(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bottle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bottle_Product_ProductId",
                        columns: x => x.ProductId,
                        referencedTable: "Product",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Product_ProductId",
                        columns: x => x.ProductId,
                        referencedTable: "Product",
                        referencedColumn: "Id");
                });
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("Base");
            migration.DropTable("Bottle");
            migration.DropTable("Review");
            migration.DropTable("Product");
            migration.DropTable("Abv");
            migration.DropTable("Brand");
            migration.DropTable("Category");
            migration.DropTable("CountryOfOrigin");
            migration.DropTable("Grade");
            migration.DropTable("Producer");
            migration.DropTable("Image");
        }
    }
}
