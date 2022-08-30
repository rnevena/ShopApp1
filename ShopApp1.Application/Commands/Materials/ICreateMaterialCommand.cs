using ShopApp1.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Application.Commands.Materials
{
    public interface ICreateMaterialCommand : ICommand<MaterialDto>
    {
    }
}
