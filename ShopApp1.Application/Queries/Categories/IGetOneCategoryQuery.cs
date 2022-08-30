using ShopApp1.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Application.Queries.Categories
{
    public interface IGetOneCategoryQuery : IQuery<int, OneCategoryDto>
    {
    }
}
