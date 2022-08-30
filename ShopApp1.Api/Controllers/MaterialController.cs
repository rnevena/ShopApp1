using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp1.Application;
using ShopApp1.Application.Commands.Materials;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Queries.Materials;
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
    public class MaterialController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public MaterialController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }

        // GET: api/<MaterialController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] MaterialsPagedSearch search, [FromServices] IGetMaterialsQuery query)
        {
            return Ok(executor.ExecuteQuery(query, search));
        }

        // GET api/<MaterialController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices] IGetOneMaterialQuery query)
        {
            return Ok(executor.ExecuteQuery(query, id));
        }

        // POST api/<MaterialController>
        [HttpPost]
        public IActionResult Post([FromBody] MaterialDto dto, [FromServices] ICreateMaterialCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<MaterialController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MaterialDto dto, [FromServices] IUpdateMaterialCommand command)
        {
            dto.Id = id;
            executor.ExecuteCommand(command, dto);
            return StatusCode(204);
        }

        // DELETE api/<MaterialController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteMaterialCommand command)
        {
            executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
