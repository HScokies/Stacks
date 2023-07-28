using Microsoft.AspNetCore.Mvc;
using Stacks_rework.Services;

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

        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(500);
        }

        [HttpGet("token")]
        public ActionResult<string> IssueToken()
        {
            return Ok(Guid.NewGuid().ToString());
            // Write it into VK WebApp Storage
            // Send it with Post/Delete/Put Requests
        }


    }
}
