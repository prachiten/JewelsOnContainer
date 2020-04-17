using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{//Created this class to display you are on which page and how many pages are left to display and all this information will be given to CatalogIndexViewModel.cs
    public class PaginationInfo
    {
        public long TotalItems { get; set; }
        public int ItemsPerPage { get; set; }

        public int ActualPage { get; set; }
        public int TotalPages { get; set; }
        public string Previous { get; set; }
        public string Next { get; set; }

    }
}
