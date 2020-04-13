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
        Task<Catalog> GetCatalogItemsAsync(int page, int size); // it will get all catalog items depending on which page and what is size of page given
    }
}
