using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.services;

namespace WebMVC.Controllers
{// Catalog controller will make call to catalog api
    public class CatalogController : Controller

    {//data is desrialized into the model ie CatalogItem and then controller will give the data from model to view.
        private readonly  ICatalogService _service;
        public CatalogController(ICatalogService service )
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int page)
        {
            var itemsOnPage = 10;       //no of items on page
            var catalog = await _service.GetCatalogItemsAsync(page, itemsOnPage);
            return View(catalog);    //giving all data ie catalog to view page

            /*here it will try to find a view ie folder that matches the controller name ie Catalog so that all data can be rendered 
             through that page . Inside that Catalog folder it will look for page with name index*/
        }
    }
}