using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp1.Application;
using ShopApp1.Application.Commands.Products;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Queries.Products;
using ShopApp1.Application.Searches;
using System;
using System.Collections.Generic;
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
        //[HttpPost]
        //public void Post([FromForm] CreateProductWithImageDto dto, [FromServices] ICreateProductCommand command)
        //{
        //}
        [HttpPost]

        public IActionResult Post([FromBody] CreateProductDto dto, [FromServices] ICreateProductCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateProductDto dto, [FromServices] IUpdateProductCommand command)
        {
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
