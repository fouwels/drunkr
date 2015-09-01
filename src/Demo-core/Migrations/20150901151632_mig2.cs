using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace DemocoreMigrations
{
    public partial class mig2 : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Product_Image_ThumbnailId", table: "Product");
            migration.DropColumn(name: "ThumbnailId", table: "Product");
            migration.AddColumn(
                name: "ImageId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);
            migration.AddColumn(
                name: "Price",
                table: "Bottle",
                type: "real",
                nullable: false,
                defaultValue: 0f);
            migration.AddColumn(
                name: "RetailName",
                table: "Bottle",
                type: "nvarchar(max)",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Product_Image_ImageId",
                table: "Product",
                column: "ImageId",
                referencedTable: "Image",
                referencedColumn: "Id");
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropForeignKey(name: "FK_Product_Image_ImageId", table: "Product");
            migration.DropColumn(name: "ImageId", table: "Product");
            migration.DropColumn(name: "Price", table: "Bottle");
            migration.DropColumn(name: "RetailName", table: "Bottle");
            migration.AddColumn(
                name: "ThumbnailId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true);
            migration.AddForeignKey(
                name: "FK_Product_Image_ThumbnailId",
                table: "Product",
                column: "ThumbnailId",
                referencedTable: "Image",
                referencedColumn: "Id");
        }
    }
}
