using Microsoft.AspNetCore.Mvc;
using Stacks_rework.Models;
using Stacks_rework.Services;

namespace Stacks_rework.Controllers
{
    [Route("api")]
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
            return Ok(Guid.NewGuid().ToString()); //VK WebApp Storage
        }

        [HttpPost("stacks")]
        public async Task<ActionResult<UserStack>> Create(UserStack stack, [FromHeader] string token)
        {
            if (!ValidateUID(stack.uid)) return BadRequest();
            stack.token = stack.token is null ? token : stack.token;
            try
            {
                var res = await stacksRepos.Create(stack);
                return Created(res.id!, res);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }   
        }
        
        [HttpPost("stacks/ad")]
        public async Task<ActionResult<AdvertisementStack>> Create(AdvertisementStack stack, [FromHeader] string token)
        {
            try
            {
                var res = await stacksRepos.CreateAdvertisementStack(stack, token);
                return Created(res.id!, res);
            }
            catch(DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }



        private bool ValidateUID(string uid)
        {
            if (uid.Length != 9) return false;
            return int.TryParse(uid, out _);
        }
    }
}
