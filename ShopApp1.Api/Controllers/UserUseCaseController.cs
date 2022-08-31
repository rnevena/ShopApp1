using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp1.Application;
using ShopApp1.Application.Commands.UserUseCases;
using ShopApp1.Application.DTO;
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
    public class UserUseCaseController : ControllerBase
    {
        private readonly IApplicationActor actor;
        private readonly UseCaseExecutor executor;

        public UserUseCaseController(IApplicationActor actor, UseCaseExecutor executor)
        {
            this.actor = actor;
            this.executor = executor;
        }
        // GET: api/<UserUseCaseController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserUseCaseController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserUseCaseController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserUseCaseController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] UpdateUserUseCaseDto dto, [FromServices] IUpdateUserUseCaseCommand command)
        {
            executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<UserUseCaseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
