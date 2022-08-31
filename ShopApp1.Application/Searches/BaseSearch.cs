using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Application.Searches
{
    public class BaseSearch
    {
        public string Keyword { get; set; }
    }
    public abstract class PagedSearch
    {
        public int? PerPage { get; set; } = 10;
        public int? Page { get; set; } = 1;
    }

    public class BasePagedSearch : PagedSearch
    {
        public string Keyword { get; set; }
    }
    public class CategoriesPagedSearch : PagedSearch
    {
        public string Name { get; set; }
    }
    public class MaterialsPagedSearch : PagedSearch
    {
        public string Name { get; set; }
    }

    public class ProductsPagedSearch : PagedSearch
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class OrdersPagedSearch : PagedSearch
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        //public string Name { get; set; }
    }
    public class UseCaseLogsPagedSearch : BasePagedSearch
    {
        public string UserId { get; set; }
    }

}
