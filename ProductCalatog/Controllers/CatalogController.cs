using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCalatog.Data;
using ProductCalatog.Domain;
using ProductCalatog.ViewModels;

namespace ProductCalatog.Controllers
{
    //whatever method we write in this class becomes an Api
    [Route("api/[controller]")]
    [ApiController]  
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;
        public CatalogController(CatalogContext context, IConfiguration config) //injest it with configuration from startup.cs because we added external configuration in JASON 
        {
            _context = context;
            _config = config;
        }

        /*[HttpGet]
        [Route("[Action]/{pageindex/{pagesize}")]
        public async Task<IActionResult> Items(int pageindex=0, int pagesize=6) *///dont want to bombard Ui with all data at a time 
                                                                                  //so giving just 1 page data at one time and telling total there will be 6 pages

        //query way to give route parameter
        [HttpGet]  // we will give query as http/localhost:50529/api/catalog/items   and give values of key as pagesize and pageindex
        [Route("[Action]")] //Action is Items
        public async Task<IActionResult> Items([FromQuery]int pageindex = 0, [FromQuery]int pagesize = 6)

        {
            var itemcount = await _context.CatalogItems.LongCountAsync();  // linq query  to database to get total no of items
            //CatalogItems name comes from CatalogContext DbSet
             var items = await _context.CatalogItems.OrderBy(c=>c.Name).Skip(pageindex * pagesize).Take(pagesize).ToListAsync();
            //OrderBy it sort on basis of name as we have given name in linq query and OrderBy is done before skip and take beacuse otherwise only data per page is sorted.
            //it means it will take 6 items to show on page and will skip 6 items in columns
            // initial pageindex=0 and pagesize=6 so initiallly 0 items are skipped and 6 is taken.
            // 2nd time  pageindex=1 and pagesize=6 so 6 items are skipped in table  and 6 is taken

            items = ChangePictureUrl(items); // as in postman we are not able to see actual pictures

            var model = new PaginatedItemsViewModels<CatalogItem>
            {
                PageIndex = pageindex,
                PageSize = pagesize,
                Count = itemcount,
                Data = items 
            };
            //return Ok(items)
            return Ok(model);  // wrap this message with with list of items

        }


        //-------------Brand  and type filtering Api-----------------------------------------------------------------------

        [HttpGet]  // we will give query as http/localhost:50529/api/catalog/items/type/{catalogTypeId}/brand/{catalogBrandId}
        [Route("[Action]/type/{catalogTypeId}/brand/{catalogBrandId}")] //Action is Items
        public async Task<IActionResult> Items(int?catalogTypeId,int?catalogBrandId,[FromQuery]int pageindex = 0, [FromQuery]int pagesize = 6)

        {
            //now itemcount will depends on on which brand and which type is selected.
            //line below is equivalent to Select* from table which means only selecting data from database but not fetching it
            var root = (IQueryable<CatalogItem>)_context.CatalogItems;

            //now check if user has given some value for catalogtypeid and if yes then for each catalogtype item, check CatalogTypeId
            // given in catalogitem.cs and match it with parameter coming from route ie catalogTypeId.

            if(catalogTypeId.HasValue)                                         //way to check value in nullable type
            {
                root = root.Where(c => c.CatalogTypeId == catalogTypeId);     //Added Where class to root.
            }

            if (catalogBrandId.HasValue) 
            {
                root = root.Where(c => c.CatalogBrandId == catalogBrandId);

            }

            // for above lines of code if user has given both brand and type then they will be appended in temp list, otherwise whatever user has given will be appended
            var itemcount = await root.LongCountAsync();  // now how many items are there in root will be the total count.
            var items = await root.OrderBy(c => c.Name).Skip(pageindex * pagesize).Take(pagesize).ToListAsync();
            //based on no of items in root pagesize and no of pages left will be shown.

            items = ChangePictureUrl(items); // as in postman we are not able to see actual pictures

            var model = new PaginatedItemsViewModels<CatalogItem>
            {
                PageIndex = pageindex,
                PageSize = pagesize,
                Count = itemcount,
                Data = items
            };
            //return Ok(items)
            return Ok(model);  // wrap this message with with list of items

        }




        //method to generate actual picture list
        // go to each value in list of catalog items and change  picture url with http://localhost/pic/{id} value from config
        private List<CatalogItem> ChangePictureUrl(List<CatalogItem> items)
        {
            //since strings are immutable so after replacing we are putting replaced url in same variable ie PictureUrl
            items.ForEach(c =>c.PictureUrl= c.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced", _config["ExternalCatalogBaseUrl"] ));
            return (items);
        }




        [HttpGet]  // we will give query as http/localhost:50529/api/Catalog/CatalogTypes and it is a get request
        [Route("[Action]")]

        public async Task <IActionResult> CatalogTypes()
        {
            //whatever are catalog types just convert them into List.so that they can be displayed as dropdown list.
           var items= await _context.CatalogTypes.ToListAsync();
            return Ok(items);
        }





        [HttpGet]  // we will give query as http/localhost:50529/api/Catalog/CatalogBrands and it is a get request
        [Route("[Action]")]

        public async Task<IActionResult> CatalogBrands()
        {
            //whatever are catalog types just convert them into List.so that they can be displayed as dropdown list.
            var items = await _context.CatalogBrands.ToListAsync();
            return Ok(items);
        }

    }
}