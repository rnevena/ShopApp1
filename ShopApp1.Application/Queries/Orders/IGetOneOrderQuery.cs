﻿using ShopApp1.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Application.Queries.Orders
{
    public interface IGetOneOrderQuery : IQuery<int, OneOrderSearchDto>
    {
    }
}
