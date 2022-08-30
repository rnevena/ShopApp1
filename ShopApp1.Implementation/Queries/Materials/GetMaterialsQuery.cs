using ShopApp1.Application.DTO;
using ShopApp1.Application.Queries;
using ShopApp1.Application.Queries.Materials;
using ShopApp1.Application.Searches;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Queries.Materials
{
    public class GetMaterialsQuery : IGetMaterialsQuery
    {
        private readonly ShopApp1Context _context;

        public GetMaterialsQuery(ShopApp1Context context)
        {
            _context = context;
        }
        public int Id => 4;

        public string Name => "search materials";

        public PagedResponse<MaterialDto> Execute(MaterialsPagedSearch search)
        {
            var query = _context.Materials.AsQueryable();
            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.Contains(search.Name));
            }

            var skipItems = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<MaterialDto>();
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipItems).Take(search.PerPage.Value).Select(x => new MaterialDto
            {
                Id = x.Id,
                Name = x.Name

            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
