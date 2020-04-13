using Microsoft.EntityFrameworkCore;
using ProductCalatog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCalatog.Data
{// a class to answer three questions
    //1)which database is connected and where it is located
    //2)which classes are converted to table
    //)how tables need to be created
    public class CatalogContext:DbContext// by inheriting this class we are making this class  answer our database questions
    {
        public CatalogContext(DbContextOptions options): base(options)//whenever which type of database option i get they
           // will be passed to parent constructors. This is dependency injection
           
        {

        }
        //which classes to convert to tables. we will make those classes as properties
        public DbSet<CatalogBrand> CatalogBrands { get; set; }//tells brand 
        public DbSet<CatalogType> CatalogTypes { get; set; }// tells whether it is ring or neckalce etc
        public DbSet<CatalogItem> CatalogItems { get; set; }//tells the exact item eg a ring with its price, description etc

        //how tables are to be created 
        //OnModelCreating means ie when tables are being created
        protected override void OnModelCreating(ModelBuilder modelBuilder)//we can instruct modelBuilder how to create table 
        {
            //base.OnModelCreating(modelBuilder);//deleting beacuse instead of using parents way of creating tables i want to define my own ways.
            modelBuilder.Entity<CatalogBrand>(e =>{e.ToTable("CatalogBrand"); //name of the table
                //constarints on Id display
                e.Property(b => b.ID)
                            .IsRequired() //Id is defenitely required
                            .UseHiLo("catalog_brand_hilo");//database is giving hi and low range of values to generate order id
                                                           //Constraints on brand
                e.Property(b => b.Brand)
                          .IsRequired()
                          .HasMaxLength(100);//it means brand has varchar of 100 
            });

            //constraints on catalog type table
            modelBuilder.Entity<CatalogType>(e => {
                e.ToTable("CatalogTypes");//Table name
                //constarints on Id display
                e.Property(t => t.ID)
                            .IsRequired() //Id is defenitely required
                            .UseHiLo("catalog_type_hilo");//database is giving hi and low range of values to generate order id
                                                           //Constraints on type
                e.Property(t => t.Type)
                          .IsRequired()
                          .HasMaxLength(100);//it means brand has varchar of 100 
            });
            modelBuilder.Entity<CatalogItem>(e =>
            {
                e.ToTable("Catalog");
                e.Property(c => c.Id)
                .IsRequired() //Id is defenitely required
                .UseHiLo("catalog_hilo");//database is giving hi and low range of values to generate order id
                                              //Constraints on type
                e.Property(c => c.Name)
                .IsRequired() //Id is defenitely required
                .HasMaxLength(100);

                e.Property(c => c.Price)
                .IsRequired();

                e.HasOne(c => c.CatalogType)//CatalogItem  has one relation with CatalogType table but that table in turn has many relationship
                .WithMany()
                .HasForeignKey(c => c.CatalogTypeId);//CatlogTypeId is forign key to CatalogType table

                e.HasOne(c => c.CatalogBrand)//CatalogItem  has one relation with CatalogBrand table but that table in turn has many relationship
                .WithMany()
                .HasForeignKey(c => c.CatalogBrandId);


            });

        }


    }
}
