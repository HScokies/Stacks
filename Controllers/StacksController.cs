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
        public async Task<ActionResult<UserStack>> Create(UserStack stack)
        {
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
        public async Task<ActionResult<AdvertisementStack>> Create(AdvertisementStack stack)
        {
            try
            {
                var res = await stacksRepos.CreateAdvertisementStack(stack);
                return Created(res.id!, res);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }

        [HttpGet("stacks/{id:length(24)}")]
        public async Task<ActionResult<UserStack>> Read(string id,[FromHeader] string token,[FromHeader] string uid)
        {
            try
            {
                return Ok(await stacksRepos.GetUserStack(
                    id: id,
                    token: token,
                    uid: uid
                ));
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }

        [HttpGet("stacks/ad/{id:length(24)}")]
        public async Task<ActionResult<AdvertisementStack>> Read(string id)
        {
            try
            {
                return Ok(await stacksRepos.GetAdStack(id));
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }

        [HttpPut("stacks/{id:length(24)}")]
        public async Task<ActionResult<UserStack>> Update(string id, [FromHeader] string token, [FromHeader] string uid, UpdateStack newStack)
        {
            try
            {
                return await stacksRepos.UpdateUserStack(
                    id: id,
                    ownerid: uid,
                    token: token,
                    newStack: newStack
                );
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }
        
        [HttpPut("stacks/ad/{id:length(24)}")]
        public async Task<ActionResult<AdvertisementStack>> Update(string id, [FromHeader] string token, UpdateStack newStack)
        {
            try
            {
                return await stacksRepos.UpdateOrgStack(
                    id: id,
                    token: token,
                    newStack: newStack
                );
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }

        [HttpDelete("stacks/{id:length(24)}")]
        public async Task<ActionResult> Delete(string id, [FromHeader] string token, [FromHeader] string uid)
        {
            try
            {
                await stacksRepos.DropUserStack(
                    id: id,
                    ownerid: uid,
                    token: token
                );
                return NoContent();
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }

        [HttpDelete("stacks/ad/{id:length(24)}")]
        public async Task<ActionResult> Delete(string id, [FromHeader] string token)
        {
            try
            {
                await stacksRepos.DropOrgStack(
                    id: id,
                    token: token
                );
                return NoContent();
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }


    }
}
