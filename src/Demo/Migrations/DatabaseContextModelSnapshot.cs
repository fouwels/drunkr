using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using Demo.Models.DB;

namespace DemoMigrations
{
    [ContextType(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        public override void BuildModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815")
                .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

            builder.Entity("Demo.Models.DB.Bottle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("LiquidId");

                    b.Property<int?>("ManufacturerId");

                    b.Property<float>("QuantityMl");

                    b.Key("Id");
                });

            builder.Entity("Demo.Models.DB.Liquid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("ABV");

                    b.Property<int?>("ManufacturerId");

                    b.Property<string>("Name");

                    b.Property<int?>("SpiritId");

                    b.Key("Id");
                });

            builder.Entity("Demo.Models.DB.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayName");

                    b.Property<string>("Name");

                    b.Key("Id");
                });

            builder.Entity("Demo.Models.DB.Spirit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Key("Id");
                });

            builder.Entity("Demo.Models.DB.Bottle", b =>
                {
                    b.Reference("Demo.Models.DB.Liquid")
                        .InverseCollection()
                        .ForeignKey("LiquidId");

                    b.Reference("Demo.Models.DB.Manufacturer")
                        .InverseCollection()
                        .ForeignKey("ManufacturerId");
                });

            builder.Entity("Demo.Models.DB.Liquid", b =>
                {
                    b.Reference("Demo.Models.DB.Manufacturer")
                        .InverseCollection()
                        .ForeignKey("ManufacturerId");

                    b.Reference("Demo.Models.DB.Spirit")
                        .InverseCollection()
                        .ForeignKey("SpiritId");
                });
        }
    }
}
