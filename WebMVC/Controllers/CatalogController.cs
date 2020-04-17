using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{// Catalog controller will make call to catalog api
    public class CatalogController : Controller

    {//data is desrialized into the model ie CatalogItem and then controller will give the data from model to view.
        private readonly  ICatalogService _service;
        public CatalogController(ICatalogService service )
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int? page, int? brandFilterApplied,
            int? typesFilterApplied)
        {
            var itemsOnPage = 10;
            //int?? 0 means if int is null ie no page is selected then select 0 page 
            var catalog = await _service.GetCatalogItemsAsync(page ?? 0, itemsOnPage,
                brandFilterApplied, typesFilterApplied);
            var vm = new CatalogIndexViewModel
            {
                CatalogItems = catalog.Data,
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsOnPage,
                    TotalItems = catalog.Count,
                    TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsOnPage)
                },
                Brands = await _service.GetBrandsAsync(),// defined in CatalogServive.cs ie get data from service
                Types = await _service.GetTypesAsync(),
                BrandFilterApplied = brandFilterApplied ?? 0,//BrandFilterApplied is null then leave it as 0 
                TypesFilterApplied = typesFilterApplied ?? 0
            };


            return View(vm);

            /*here it will try to find a view ie folder that matches the controller name ie Catalog so that all data can be rendered 
             through that page . Inside that Catalog folder it will look for page with name index*/
        }
    }
}