using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;
using MapperV1 = PublicApi.DTO.v1.Mappers.CocktailMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Cocktail.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CocktailsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// CocktailsController constructor
        /// </summary>
        /// <param name="bll">Interface from business layer, gives access to the services</param>
        public CocktailsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Cocktails
        /// <summary>
        /// Get all Cocktails
        /// </summary>
        /// <returns>List of Cocktails</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApiDTOv1.Cocktail>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApiDTOv1.Cocktail>>> GetCocktails()
        {
            return Ok((await _bll.Cocktails.GetAllWithSmallOverviewAsync()).Select(MapperV1.MapToPublic));
        }

        // GET: api/Cocktails/5
        /// <summary>
        /// Get Cocktail
        /// </summary>
        /// <param name="id">Cocktail Id</param>
        /// <returns>Cocktail based on the provided Id or 404 Not Found</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.CocktailDetailed), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.CocktailDetailed>> GetCocktail(Guid id)
        {
            var cocktail = await _bll.Cocktails.GetOneWithFullInfoAsync(id);
        
            if (cocktail == null) return NotFound();

            return MapperV1.MapToDetailedPublic(cocktail);
        }
        
        // PUT: api/Cocktails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Cocktail
        /// </summary>
        /// <param name="id">Cocktail Id</param>
        /// <param name="cocktail">DTO for updating</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if put action was successful<br/>
        /// 400 Bad Request if id in url and id in DTO don't match<br/>
        /// </returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCocktail(Guid id, PublicApiDTOv1.CocktailUpdate cocktail)
        {
            if (id != cocktail.Id) return BadRequest();

            var bllCocktail = MapperV1.MapToBLL(cocktail);
            
            _bll.Cocktails.Update(bllCocktail);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }
        
        // POST: api/Cocktails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create Cocktail
        /// </summary>
        /// <param name="cocktail">DTO for creation</param>
        /// <returns>Created Cocktail</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.Cocktail), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicApiDTOv1.Cocktail>> PostCocktail(PublicApiDTOv1.CocktailCreate cocktail)
        {
            var bllCocktail = MapperV1.MapToBLL(cocktail);
            var addedCocktail = _bll.Cocktails.Add(bllCocktail);
            await _bll.SaveChangesAsync();

            var returnCocktail = MapperV1.MapToPublic(addedCocktail);
        
            return CreatedAtAction(
                "GetCocktail", 
                new
                {
                    id = returnCocktail.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnCocktail);
        }
        
        // DELETE: api/Cocktails/5
        /// <summary>
        /// Delete Cocktail
        /// </summary>
        /// <param name="id">Cocktail Id</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if delete action was successful<br/>
        /// 404 Not Found if server fails to find Additive<br/>
        /// </returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCocktail(Guid id)
        {
            var cocktail = await _bll.Cocktails.FirstOrDefaultAsync(id);
            if (cocktail == null) return NotFound();

            _bll.Cocktails.Remove(cocktail);
            await _bll.SaveChangesAsync();
        
            return NoContent();
        }
    }
}
