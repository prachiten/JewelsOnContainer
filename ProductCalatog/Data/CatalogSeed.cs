﻿using Microsoft.EntityFrameworkCore;
using ProductCalatog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCalatog.Data
{
    public static class CatalogSeed  //purpose of this class is to populate database with the data
    {
        public static void Seed(CatalogContext context)  //give database reference where we want to populate data
        {
            context.Database.Migrate();//it is same as update database
            if(!context.CatalogBrands.Any())    //If there is no row in database of CatalogBrands then add data
            {
                context.CatalogBrands.AddRange(GetPreconfiguredCatalogBrands()); //GetPreconfiguredCatalogBrands() 
                                                                                 //is a function which provides the array of data in which will be passed to AddRange function
                context.SaveChanges();// to save changes in database
            }


            if (!context.CatalogTypes.Any())   
            {
                context.CatalogTypes.AddRange(GetPreconfiguredCatalogTypes());
                context.SaveChanges();// to save changes in database
            }

            if (!context.CatalogItems.Any())    //If there is no row in database of CatalogBrands then add data
            {
                context.CatalogItems.AddRange(GetPreconfiguredCatalogItems()); 
                context.SaveChanges();// to save changes in database
            }
        }

        private static IEnumerable<CatalogItem> GetPreconfiguredCatalogItems()
        {
            return new List<CatalogItem>
            {
               //first ring belong to Wedding ring as TypeId is 2, brand 3 ie Graff
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=3, Description = "A ring that has been around for over 100 years", Name = "World Star", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },
                new CatalogItem { CatalogTypeId=1,CatalogBrandId=2, Description = "will make you world champions", Name = "White Line", Price= 88.50M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=3, Description = "You have already won gold medal", Name = "Prism White", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=2, Description = "Olympic runner", Name = "Foundation Hitech", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=1, Description = "Roslyn Red Sheet", Name = "Roslyn White", Price = 188.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=2, Description = "Lala Land", Name = "Blue Star", Price = 112, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=1, Description = "High in the sky", Name = "Roslyn Green", Price = 212, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7"  },
                new CatalogItem { CatalogTypeId=1,CatalogBrandId=1, Description = "Light as carbon", Name = "Deep Purple", Price = 238.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },
                new CatalogItem { CatalogTypeId=1,CatalogBrandId=2, Description = "High Jumper", Name = "Antique Ring", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=3, Description = "Dunker", Name = "Elequent", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },
                new CatalogItem { CatalogTypeId=1,CatalogBrandId=2, Description = "All round", Name = "Inredeible", Price = 248.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },
                new CatalogItem { CatalogTypeId=2,CatalogBrandId=1, Description = "Pricesless", Name = "London Sky", Price = 412, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },
                new CatalogItem { CatalogTypeId=3,CatalogBrandId=3, Description = "You ar ethe star", Name = "Elequent", Price = 123, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },
                new CatalogItem { CatalogTypeId=3,CatalogBrandId=2, Description = "A ring popular in the 16th and 17th century in Western Europe that was used as an engagement wedding ring", Name = "London Star", Price = 218.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },
                new CatalogItem { CatalogTypeId=3,CatalogBrandId=1, Description = "A floppy, bendable ring made out of links of metal", Name = "Paris Blues", Price = 312, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" }

            };

        }

        private static IEnumerable<CatalogType>  GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>
            {

                new CatalogType{Type ="Engagement Ring"},
                new CatalogType{Type="Wedding Ring"},
                new CatalogType{Type="Fashion Ring"}
            };
        }

        private static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands() //AddRange will need an array so CatologBrand id automatically created as array.
            //But we will change it to IEnumerable
        {
            return new List<CatalogBrand>
            {
                
                new CatalogBrand{Brand ="Tiffany & Co"},
                new CatalogBrand{Brand="DeBeers"},
                new CatalogBrand{Brand="Graff"}
            };
            //can do it alternatively this way

           // List<CatalogBrand>MyList=new List<CatalogBrand>;

            /*CatalogBrand C1= new CatalogBrand();
            C1.Brand= "Tiffany & Co";
            MyList.Add(C1);*/


            /* CatalogBrand C2 = new CatalogBrand();
             C2.Brand = "DeBeers";
             MyList.Add(C2); */

            /* CatalogBrand C3 = new CatalogBrand();
            C3.Brand = "Graff";
            MyList.Add(C3); */

            //return(MyList)
        }
    }
}