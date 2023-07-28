using Microsoft.AspNetCore.Mvc;
using Stacks_rework.Models;
using Stacks_rework.Services;
using System.ComponentModel.DataAnnotations;

namespace Stacks_rework.Controllers
{
    [Route("api/stacks")]
    [ApiController]
    public class StacksController : Controller
    {
        private readonly IStacksService stacksRepos;
        public StacksController(IStacksService _stacksRepos)
        {
            this.stacksRepos = _stacksRepos;
        }

        [HttpGet("token")]
        public ActionResult<string> IssueToken()
        {
            return Guid.NewGuid().ToString();
            // Write it into VK WebApp Storage
            // Send it with Post/Delete/Put Requests
        }
        [HttpGet]
        public async Task<ActionResult> GetList()
        {
            return Ok(await stacksRepos.GetAsync());
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<UserStack>> Get(string id)
        {
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult Test(string id, [FromHeader, StringLength(36, ErrorMessage = "invalid token")] string token, [FromHeader, StringLength(9, ErrorMessage = "invalid user id")] string user)
        {
            return Ok(token + " " + user);
        }


        [HttpPost]
        public async Task<ActionResult> Create(UserStack newStack)
        {
            await stacksRepos.CreateAsync(newStack);
            return Created(newStack.id!, newStack);
        }


    }
}
