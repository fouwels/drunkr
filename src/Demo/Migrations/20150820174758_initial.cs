using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace DemoMigrations
{
    public partial class initial : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    DisplayName = table.Column(type: "nvarchar(max)", nullable: true),
                    Name = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });
            migration.CreateTable(
                name: "Spirit",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    Name = table.Column(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spirit", x => x.Id);
                });
            migration.CreateTable(
                name: "Liquid",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    ABV = table.Column(type: "real", nullable: false),
                    ManufacturerId = table.Column(type: "int", nullable: true),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    SpiritId = table.Column(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liquid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Liquid_Manufacturer_ManufacturerId",
                        columns: x => x.ManufacturerId,
                        referencedTable: "Manufacturer",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Liquid_Spirit_SpiritId",
                        columns: x => x.SpiritId,
                        referencedTable: "Spirit",
                        referencedColumn: "Id");
                });
            migration.CreateTable(
                name: "Bottle",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn"),
                    LiquidId = table.Column(type: "int", nullable: true),
                    ManufacturerId = table.Column(type: "int", nullable: true),
                    QuantityMl = table.Column(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bottle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bottle_Liquid_LiquidId",
                        columns: x => x.LiquidId,
                        referencedTable: "Liquid",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bottle_Manufacturer_ManufacturerId",
                        columns: x => x.ManufacturerId,
                        referencedTable: "Manufacturer",
                        referencedColumn: "Id");
                });
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("Bottle");
            migration.DropTable("Liquid");
            migration.DropTable("Manufacturer");
            migration.DropTable("Spirit");
        }
    }
}
