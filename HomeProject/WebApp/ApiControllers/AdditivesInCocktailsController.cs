using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;
using MapperV1 = PublicApi.DTO.v1.Mappers.AdditiveInCocktailMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for AdditiveInCocktail.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdditivesInCocktailsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// AdditivesInCocktailsController constructor
        /// </summary>
        /// <param name="bll">Interface from business layer, gives access to the services</param>
        public AdditivesInCocktailsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/AdditivesInCocktails/5
        /// <summary>
        /// Get one AdditiveInCocktail
        /// </summary>
        /// <param name="id">AdditiveInCocktail Id</param>
        /// <returns>AdditiveInCocktail based on the provided id or 404 Not Found</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.AdditiveInCocktail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.AdditiveInCocktail>> GetAdditiveInCocktail(Guid id)
        {
            var additiveInCocktail = await _bll.AdditivesInCocktails.GetOneDetailedAsync(id);

            if (additiveInCocktail == null) return NotFound();

            return MapperV1.MapToPublic(additiveInCocktail);
        }

        // PUT: api/AdditivesInCocktails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update AdditiveInCocktail
        /// </summary>
        /// <param name="id">AdditiveInCocktail Id</param>
        /// <param name="additiveInCocktail">DTO for updating</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if put action was successful<br/>
        /// 400 Bad Request if id in url and id in DTO don't match<br/>
        /// </returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAdditiveInCocktail(Guid id, PublicApiDTOv1.AdditiveInCocktailUpdate additiveInCocktail)
        {
            if (id != additiveInCocktail.Id) return BadRequest();
            
            var bllAdditiveInCocktail = MapperV1.MapToBLL(additiveInCocktail);

            _bll.AdditivesInCocktails.Update(bllAdditiveInCocktail);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/AdditivesInCocktails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create AdditiveInCocktail
        /// </summary>
        /// <param name="additiveInCocktail">DTO for creation</param>
        /// <returns>Created AdditiveInCocktail</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.Drink), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicApiDTOv1.AdditiveInCocktail>> PostAdditiveInCocktail(PublicApiDTOv1.AdditiveInCocktailCreate additiveInCocktail)
        {
            var bllAdditiveInCocktail = MapperV1.MapToBLL(additiveInCocktail);
            var addedAdditiveInCocktail = _bll.AdditivesInCocktails.Add(bllAdditiveInCocktail);
            await _bll.SaveChangesAsync();
            
            var returnAdditiveInCocktail = MapperV1.MapToPublic(addedAdditiveInCocktail);

            return CreatedAtAction(
                "GetAdditiveInCocktail", 
                new
                {
                    id = returnAdditiveInCocktail.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnAdditiveInCocktail);
        }

        // DELETE: api/AdditivesInCocktails/5
        /// <summary>
        /// Delete AdditiveInCocktail
        /// </summary>
        /// <param name="id">AdditiveInCocktail Id</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if delete action was successful<br/>
        /// 404 Not Found if server fails to find Additive<br/>
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAdditiveInCocktail(Guid id)
        {
            var additiveInCocktail = await _bll.AdditivesInCocktails.FirstOrDefaultAsync(id);
            if (additiveInCocktail == null) return NotFound();

            _bll.AdditivesInCocktails.Remove(additiveInCocktail);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
