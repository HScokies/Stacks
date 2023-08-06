using Microsoft.AspNetCore.Mvc;
using Stacks_rework.Models;
using Stacks_rework.Models.Output;
using Stacks_rework.Services;

namespace Stacks_rework.Controllers
{
    [Route("api")]
    [ApiController]
    public class StacksController : Controller
    {
        private readonly IStacksService stackRepos;
        public StacksController(IStacksService _stackRepos)
        {
            this.stackRepos = _stackRepos;
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
                var res = await stackRepos.Create(stack);
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
                var res = await stackRepos.CreateAdvertisementStack(stack);
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
                return Ok(await stackRepos.GetUserStack(
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
        public async Task<ActionResult<AdvertisementStack>> Read(string id, [FromHeader] string? token = null)
        {
            try
            {
                return Ok(await stackRepos.GetAdStack(id, token));
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
                return await stackRepos.UpdateUserStack(
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
                return await stackRepos.UpdateOrgStack(
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
                await stackRepos.DropUserStack(
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
                await stackRepos.DropOrgStack(
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

        [HttpGet("stacks/ads")]
        public async Task<ActionResult<StackPreview>> GetAdPreview()
        {
            try
            {
                return Ok(await stackRepos.GetAdsPreview());
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }

        [HttpGet("stacks/organisation/{id:length(24)}")]
        public async Task<ActionResult<StackPreview>> GetOrgPreview(string id)
        {
            try
            {
                return Ok(await stackRepos.ListOrgStacks(id));
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }

        [HttpGet("stacks/users")]
        public async Task<ActionResult<StackPreview>> GetFriendsPreview(FriendIDs Friends)
        {
            try
            {
                return Ok(await stackRepos.GetFriendsPreview(Friends.ids));
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }

        [HttpGet("stacks/users/{uid:length(9)}")]
        public async Task<ActionResult<StackPreview>> GetUserPreview(string uid,[FromHeader] string? token = null)
        {
            try
            {
                var res = token is null ? await stackRepos.ListUserStacks(uid) : await stackRepos.ListUserStacks(uid, token);
                return  Ok(res);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(ex.status);
            }
        }
    }
}
