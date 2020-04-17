using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

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
            // _baseUri = $"{config["CatalogUrl"]}/api/catalog/";
            _client = client;
        }

       

        public async Task<Catalog> GetCatalogItemsAsync(int page, int size, int? brand, int? type)
        {
            //made a call to apipath and got uri in variable catalogItemsUri
            var catalogItemsUri =ApiPaths.Catalog.GetAllCatalogItems(_baseuri, page, size);

            /* dont do this way because anyone can implement IHttpClient so it should come from startup.cs of WebMVC
             var HttpClient = new CustomHttpClient();
             HttpClient.GetStringAsync(//passinformation here)*/

            //Now give this uri to httpClient to make call to microservice
            var datastring = await _client.GetStringAsync(catalogItemsUri);// data will be coming back from microservice in form of string

            //Now deserialize the data back to catalogitem format
            return(JsonConvert.DeserializeObject<Catalog>(datastring));// now this data is returned to controller.
            // now controller can take this response and bind it to pages in UI

        }

        public async Task<IEnumerable<SelectListItem>> GetTypesAsync()
        {
            //get the api path
            var typeUri = ApiPaths.Catalog.GetAllTypes(_baseuri);
            //http makes call to microservice
            var dataString = await _client.GetStringAsync(typeUri);
            //bind  data to html
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,//what is behind the scene
                    Text="All",// what is visible to user
                    Selected = true// always going to be selected
                    //it means by default when user comes to catalog page for first time he will see all, whose value is null selected in dropdown list
                }
            };
            // data will come back in Jason format
            var types = JArray.Parse(dataString); //parse the datastring
            foreach (var type in types)// for each catalogtypes in type
            {// start adding one item at a time to dropdownlist
                items.Add(
                    new SelectListItem
                    {
                        Value = type.Value<string>("id"),// now in list i will attach id from jason data to Value of dropdownlist 
                        Text = type.Value<string>("type")// now in list i will attach type from jason data to text of dropdownlist
                    }
                );
            }

            return items;// items going to catalog controller
        }

        public async Task<IEnumerable<SelectListItem>> GetBrandsAsync()
        {
            var brandUri = ApiPaths.Catalog.GetAllBrands(_baseuri);
            var dataString = await _client.GetStringAsync(brandUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text="All",
                    Selected = true
                }
            };
            var brands = JArray.Parse(dataString);
            foreach (var brand in brands)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = brand.Value<string>("id"),
                        Text = brand.Value<string>("brand")
                    }
                );
            }

            return items;
        }

    }
}
