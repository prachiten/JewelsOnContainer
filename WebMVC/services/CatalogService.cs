using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Models;

namespace WebMVC.services
{
    //this is class  to implement interface ICatalogService.cs
    // we will desearialize the string data into Caltalog Format here
    public class CatalogService : ICatalogService
    {
        private readonly string _baseuri;
        private readonly IHttpClient _client; // to know the client who is implementing IHttpClient beacuse it is interface
        public CatalogService(IConfiguration config, IHttpClient client) //baseuri will come from configuration file which is needed in GetallCatalogItems function
        {
            _baseuri = $"{config["CatalogUrl"]}/api/catalog"; //this is baseuri
            _client = client;
        }
        public async Task<Catalog> GetCatalogItemsAsync(int page, int size)
        {
            //made a call to apipath and got uri in variable catalogItemsUri
            var catalogItemsUri =ApiPaths.Catalog.GetAllCatalogItems(_baseuri, page, size);

            /* dont do this way because anyone can implement IHttpClient so it should come from startup.cs of WebMvc
             var HttpClient = new CustomHttpClient();
             HttpClient.GetStringAsync(//passinformation here)*/

            //Now give this uri to httpClient to make call to microservice
            var datastring = await _client.GetStringAsync(catalogItemsUri);// data will be coming back from microservice in form of string

            //Now deserialize the data back to catalogitem format
            return(JsonConvert.DeserializeObject<Catalog>(datastring));// now this data is returned to controller.
            // now controller can take this response and bind it to pages in UI

        }
    }
}
