using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.services
{// this is interface and advantage is it can have multiple implementations
    public interface ICatalogService
    {
        //What all calls can be made to catalog
        // when call is made to service, service makes call to HttpClient, now HttpClient will call microservice and microservice 
        // will send data in string format,now that data is desriallized into Catalog Format.
        Task<Catalog> GetCatalogItemsAsync(int page, int size, int? brand, int? type); // it will get all catalog items depending on which page and what is size of page given
        Task<IEnumerable<SelectListItem>> GetBrandsAsync();
        //IEnumerable is forward only read only collection but rather than reading it as catalog brand 
        // html dropdown has text and each text has id.so to deserealize it we will do selectlistitem.its job is to convert the brands into list.
        Task<IEnumerable<SelectListItem>> GetTypesAsync();

    }
}
