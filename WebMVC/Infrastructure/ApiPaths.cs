using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    //It will have all Urls. 
    public class ApiPaths
    {
        public static class Catalog 
        {
            //asking api path to return Url of Where to get items 
            public static string GetAllCatalogItems(string baseUri, 
                int page, int take,int?brand, int?type) //baseUri is path of catalog api page meanshow many pages do i need to take. i can write brand also if i need particular brand
            {
                var filterQs = string.Empty;
                if (brand.HasValue || type.HasValue)
                {
                    //if either brand or type has value then make filtered query
                    var brandQs = (brand.HasValue) ? brand.Value.ToString() : "null";
                    var typeQs = (type.HasValue) ? type.Value.ToString() : "null";
                    filterQs = $"/type/{typeQs}/brand/{brandQs}";
                }
              //  return $"{baseUri}/items?pageIndex={page}&pageSize={take}"; //it is query parameter
                                                                            //api path is api/catalog/item/=baseuri
                                                                            //api/catalog/item/
                return $"{baseUri}/items{filterQs}?pageIndex={page}&pageSize={take}";
            }

            public static string GetAllTypes(string baseuri)
            {
                return $"{baseuri}/catalogtypes"; // it means it will return baseuri/catalogtypes
            }
            public static string GetAllBrands(string baseuri)
            {
                return $"{baseuri}/catalogbrands"; // it means it will return baseuri/catalogbrands
            }
        }
        

        public static class Basket
        {

        }
    }
}
