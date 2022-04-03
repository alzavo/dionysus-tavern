using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;
using MapperV1 = PublicApi.DTO.v1.Mappers.UserWithCocktailMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for UserWithCocktail.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersWithCocktailsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// UsersWithCocktailsController constructor
        /// </summary>
        /// <param name="bll">Interface from business layer, gives access to the services</param>
        public UsersWithCocktailsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/UsersWithCocktails
        /// <summary>
        /// Get all UsersWithCocktails
        /// </summary>
        /// <returns>List of UsersWithCocktails</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApiDTOv1.UserWithCocktail>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApiDTOv1.UserWithCocktail>>> GetUserWithCocktail()
        {
            return Ok((await _bll.UsersWithCocktails.GetAllDetailedAsync(User.GetUserId()!.Value)).Select(MapperV1.MapToPublic));
        }

        // GET: api/UsersWithCocktails/5
        /// <summary>
        /// Get UserWithCocktail
        /// </summary>
        /// <param name="id">UserWithCocktail Id</param>
        /// <returns>UserWithCocktail based on the provided Id or 404 Not Found</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.UserWithCocktail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.UserWithCocktail>> GetUserWithCocktail(Guid id)
        {
            var userWithCocktail = await _bll.UsersWithCocktails.GetOneDetailedAsync(id, User.GetUserId()!.Value);
        
            if (userWithCocktail == null) return NotFound();

            return MapperV1.MapToPublic(userWithCocktail);
        }
        
        // PUT: api/UsersWithCocktails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update UserWithCocktail
        /// </summary>
        /// <param name="id">UserWithCocktail Id</param>
        /// <param name="userWithCocktail">DTO for updating</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if put action was successful<br/>
        /// 400 Bad Request if id in url and id in DTO don't match<br/>
        /// </returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUserWithCocktail(Guid id, PublicApiDTOv1.UserWithCocktailUpdate userWithCocktail)
        {
            if (id != userWithCocktail.Id) return BadRequest();
            
            var userWithCocktailBll = await _bll.UsersWithCocktails.FirstOrDefaultAsync(id, User.GetUserId()!.Value);
            if (userWithCocktailBll == null) return BadRequest();

            var bllUserWithCocktail = MapperV1.MapToBLL(userWithCocktail);
            
            _bll.UsersWithCocktails.Update(bllUserWithCocktail);
            await _bll.SaveChangesAsync();
        
            return NoContent();
        }
        
        // POST: api/UsersWithCocktails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create UserWithCocktail
        /// </summary>
        /// <param name="userWithCocktail">DTO for creation</param>
        /// <returns>Created UserWithCocktail</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.UserWithCocktail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PublicApiDTOv1.UserWithCocktail>> PostUserWithCocktail(PublicApiDTOv1.UserWithCocktailCreate userWithCocktail)
        {
            if (userWithCocktail.UserId != User.GetUserId()!.Value) return BadRequest();
            
            var bllUserWithCocktail = MapperV1.MapToBLL(userWithCocktail);
            
            var addedUserWithCocktail = _bll.UsersWithCocktails.Add(bllUserWithCocktail);
            await _bll.SaveChangesAsync();
            
            var returnUserWithCocktail = MapperV1.MapToPublic(addedUserWithCocktail);
        
            return CreatedAtAction(
                "GetUserWithCocktail", 
                new
                {
                    id = returnUserWithCocktail.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnUserWithCocktail);
        }
        
        // DELETE: api/UsersWithCocktails/5
        /// <summary>
        /// Delete UserWithCocktail
        /// </summary>
        /// <param name="id">UserWithCocktail Id</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if delete action was successful<br/>
        /// 404 Not Found if server fails to find Additive<br/>
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserWithCocktail(Guid id)
        {
            var userWithCocktail = await _bll.UsersWithCocktails.FirstOrDefaultAsync(id, User.GetUserId()!.Value);
            
            if (userWithCocktail == null) return NotFound();

            _bll.UsersWithCocktails.Remove(userWithCocktail);
            await _bll.SaveChangesAsync();
        
            return NoContent();
        }
    }
}
