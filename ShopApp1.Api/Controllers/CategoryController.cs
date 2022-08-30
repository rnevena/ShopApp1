using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp1.Application;
using ShopApp1.Application.Commands.Categories;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Exceptions;
using ShopApp1.Application.Queries.Categories;
using ShopApp1.Application.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopApp1.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public CategoryController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/<TestController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] CategoriesPagedSearch search, [FromServices] IGetCategoriesQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices] IGetOneCategoryQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<TestController>
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDto dto, [FromServices] ICreateCategoryCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(201);
            
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] CategoryDto dto, [FromServices] IUpdateCategoryCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
