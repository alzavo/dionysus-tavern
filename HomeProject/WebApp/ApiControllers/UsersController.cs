using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;
using MapperV1 = PublicApi.DTO.v1.Mappers.UserMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for User.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[ProducesResponseType(typeof(BadRequestResult),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// UsersController constructor
        /// </summary>
        /// <param name="bll">Interface from business layer, gives access to the services</param>
        public UsersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Users
        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns>List of Users</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApiDTOv1.User>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApiDTOv1.User>>> GetUser()
        {
            return Ok((await _bll.Users.GetAllWithCocktailsCountAsync()).Select(MapperV1.MapToPublic));
        }

        // GET: api/Users/5
        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User based on the provided Id or 404 Not Found</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.User>> GetUser(Guid id)
        {
            var user = await _bll.Users.GetOneWithCocktailsCountAsync(id);
            
            if (user == null) return NotFound();

            if (!User.IsInRole("Admin") && User.GetUserId()!.Value != user.Id) return BadRequest();

            return MapperV1.MapToPublic(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="user">DTO for updating</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if put action was successful<br/>
        /// 400 Bad Request if id in url and id in DTO don't match<br/>
        /// 404 Not Found if user tries to update foreign data
        /// </returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUser(Guid id, PublicApiDTOv1.UserUpdate user)
        {
            if (id != user.Id) return BadRequest();
            
            if (User.GetUserId()!.Value != user.Id) return BadRequest();

            var bllUser = await _bll.Users.FirstOrDefaultAsync(id);
            bllUser!.UserName = user.UserName;
            bllUser.Email = user.Email;

            _bll.Users.Update(bllUser);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Users/5
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if delete action was successful<br/>
        /// 404 Not Found if server fails to find User or
        /// if user tries to delete foreign data<br/>
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _bll.Users.FirstOrDefaultAsync(id);
            
            if (user == null) return NotFound();
            
            if (!User.IsInRole("Admin") && User.GetUserId()!.Value != user.Id) return BadRequest();

            _bll.Users.Remove(user);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
