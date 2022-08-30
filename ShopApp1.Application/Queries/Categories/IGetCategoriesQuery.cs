using ShopApp1.Application.DTO;
using ShopApp1.Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Application.Queries.Categories
{
    public interface IGetCategoriesQuery : IQuery<CategoriesPagedSearch, PagedResponse<CategoryDto>>
    {
    }
}
