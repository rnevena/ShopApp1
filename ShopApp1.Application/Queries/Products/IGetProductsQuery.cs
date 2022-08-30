using ShopApp1.Application.DTO;
using ShopApp1.Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Application.Queries.Products
{
    public interface IGetProductsQuery : IQuery<ProductsPagedSearch, PagedResponse<ProductSearchDto>>
    {
    }
}
