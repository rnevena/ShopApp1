using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp1.Application;
using ShopApp1.Application.Commands.Products;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Queries.Products;
using ShopApp1.Application.Searches;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopApp1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public ProductController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }
        // GET: api/<ProductController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] ProductsPagedSearch search, [FromServices] IGetProductsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices] IGetOneProductQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<ProductController>
        [HttpPost]

        public IActionResult Post([FromForm] CreateProductWithImageDto dto, [FromServices] ICreateProductCommand command)
        {
            if(dto.Image!=null)
            {
                foreach(var img in dto.Image)
                {
                    var guid = Guid.NewGuid();
                    var extension = Path.GetExtension(img.FileName);
                    var newName = guid + extension;
                    var path = Path.Combine("wwwroot", "images", newName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }
                    dto.ImageName.Add(newName);
                }
            }
            executor.ExecuteCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] CreateProductWithImageDto dto, [FromServices] IUpdateProductCommand command)
        {
            if (dto.Image != null)
            {
                foreach (var img in dto.Image)
                {
                    var guid = Guid.NewGuid();
                    var extension = Path.GetExtension(img.FileName);
                    var newName = guid + extension;
                    var path = Path.Combine("wwwroot", "images", newName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        img.CopyTo(fileStream);
                    }
                    dto.ImageName.Add(newName);
                }
            }
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteProductCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
