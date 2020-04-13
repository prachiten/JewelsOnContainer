using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCalatog.Domain
{// Copy of paginatedItemsViewModels.cs because my Catalog.cs will be returning CatalogItems so i need to replicate it here as well from backend
    public class CatalogItem
    {
        public int Id{ get; set; }
        public string Name  { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int CatalogTypeId { get; set; }// to relate it to which type of catalogtype
        public int CatalogBrandId { get; set; }// to relate it to which brand in catalog brand
        public virtual CatalogType CatalogType { get; set; } // virtual to navigate to catalog type page
       // public string CatalogType { get; set; }// i dont need virtual relationships here. I need Catalog Type but string because data is in Jason format
        public virtual CatalogBrand CatalogBrand { get; set; }//virtual to navigate to catalog Brand page


    }
}
