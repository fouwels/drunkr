using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using Demo_core.Models.DB;

namespace DemocoreMigrations
{
    [ContextType(typeof(DatabaseContext))]
    partial class initial
    {
        public override string Id
        {
            get { return "20150824154952_initial"; }
        }

        public override string ProductVersion
        {
            get { return "7.0.0-beta6-13815"; }
        }

        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815")
                .Annotation("SqlServer:ValueGenerationStrategy", "IdentityColumn");

            builder.Entity("Demo_core.Models.DB.Abv", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Percentage");

                    b.Key("Id");
                });

            builder.Entity("Demo_core.Models.DB.Base", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Key("Id");
                });

            builder.Entity("Demo_core.Models.DB.Bottle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ProductId");

                    b.Property<float>("SizeInCentiLiters");

                    b.Key("Id");
                });

            builder.Entity("Demo_core.Models.DB.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Key("Id");
                });

            builder.Entity("Demo_core.Models.DB.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Key("Id");
                });

            builder.Entity("Demo_core.Models.DB.CountryOfOrigin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Key("Id");
                });

            builder.Entity("Demo_core.Models.DB.Grade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Key("Id");
                });

            builder.Entity("Demo_core.Models.DB.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ByteArray");

                    b.Property<string>("FileExtension");

                    b.Key("Id");
                });

            builder.Entity("Demo_core.Models.DB.Producer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Key("Id");
                });

            builder.Entity("Demo_core.Models.DB.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AbvId");

                    b.Property<Guid?>("BrandId");

                    b.Property<Guid?>("CategoryId");

                    b.Property<Guid?>("CountryOfOriginId");

                    b.Property<string>("Description");

                    b.Property<Guid?>("GradeId");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ProducerId");

                    b.Property<Guid?>("ThumbnailId");

                    b.Key("Id");
                });

            builder.Entity("Demo_core.Models.DB.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ProductId");

                    b.Key("Id");
                });

            builder.Entity("Demo_core.Models.DB.Bottle", b =>
                {
                    b.Reference("Demo_core.Models.DB.Product")
                        .InverseCollection()
                        .ForeignKey("ProductId");
                });

            builder.Entity("Demo_core.Models.DB.Product", b =>
                {
                    b.Reference("Demo_core.Models.DB.Abv")
                        .InverseCollection()
                        .ForeignKey("AbvId");

                    b.Reference("Demo_core.Models.DB.Brand")
                        .InverseCollection()
                        .ForeignKey("BrandId");

                    b.Reference("Demo_core.Models.DB.Category")
                        .InverseCollection()
                        .ForeignKey("CategoryId");

                    b.Reference("Demo_core.Models.DB.CountryOfOrigin")
                        .InverseCollection()
                        .ForeignKey("CountryOfOriginId");

                    b.Reference("Demo_core.Models.DB.Grade")
                        .InverseCollection()
                        .ForeignKey("GradeId");

                    b.Reference("Demo_core.Models.DB.Producer")
                        .InverseCollection()
                        .ForeignKey("ProducerId");

                    b.Reference("Demo_core.Models.DB.Image")
                        .InverseCollection()
                        .ForeignKey("ThumbnailId");
                });

            builder.Entity("Demo_core.Models.DB.Review", b =>
                {
                    b.Reference("Demo_core.Models.DB.Product")
                        .InverseCollection()
                        .ForeignKey("ProductId");
                });
        }
    }
}
