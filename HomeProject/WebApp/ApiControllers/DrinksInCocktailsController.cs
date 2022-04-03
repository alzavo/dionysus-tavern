using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;
using MapperV1 = PublicApi.DTO.v1.Mappers.DrinkInCocktailMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for DrinkInCocktail.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class DrinksInCocktailsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// DrinksInCocktailsController constructor
        /// </summary>
        /// <param name="bll">Interface from business layer, gives access to the services</param>
        public DrinksInCocktailsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/DrinksInCocktails/5
        /// <summary>
        /// Get one DrinkInCocktail
        /// </summary>
        /// <param name="id">DrinkInCocktail Id</param>
        /// <returns>DrinkInCocktail based on the provided id or 404 Not Found</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.DrinkInCocktail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.DrinkInCocktail>> GetDrinkInCocktail(Guid id)
        {
            var drinkInCocktail = await _bll.DrinkInCocktails.GetOneDetailedAsync(id);

            if (drinkInCocktail == null) return NotFound();

            return MapperV1.MapToPublic(drinkInCocktail);
        }

        // PUT: api/DrinksInCocktails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update DrinkInCocktail
        /// </summary>
        /// <param name="id">DrinkInCocktail Id</param>
        /// <param name="drinkInCocktail">DTO for updating</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if put action was successful<br/>
        /// 400 Bad Request if id in url and id in DTO don't match<br/>
        /// </returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDrinkInCocktail(Guid id, PublicApiDTOv1.DrinkInCocktailUpdate drinkInCocktail)
        {
            if (id != drinkInCocktail.Id) return BadRequest();
            
            var bllDrinkInCocktail = MapperV1.MapToBLL(drinkInCocktail);
            
            _bll.DrinkInCocktails.Update(bllDrinkInCocktail);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/DrinksInCocktails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create DrinkInCocktail
        /// </summary>
        /// <param name="drinkInCocktail">DTO for creation</param>
        /// <returns>Created DrinkInCocktail</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.Drink), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicApiDTOv1.DrinkInCocktail>> PostDrinkInCocktail(PublicApiDTOv1.DrinkInCocktailCreate drinkInCocktail)
        {
            var bllDrinkInCocktail = MapperV1.MapToBLL(drinkInCocktail);
            var addedDrinkInCocktail = _bll.DrinkInCocktails.Add(bllDrinkInCocktail);
            await _bll.SaveChangesAsync();
            
            var returnDrinkInCocktail = MapperV1.MapToPublic(addedDrinkInCocktail);

            return CreatedAtAction(
                "GetDrinkInCocktail", 
                new
                {
                    id = returnDrinkInCocktail.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnDrinkInCocktail);
        }

        // DELETE: api/DrinksInCocktails/5
        /// <summary>
        /// Delete DrinkInCocktail
        /// </summary>
        /// <param name="id">DrinkInCocktail Id</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if delete action was successful<br/>
        /// 404 Not Found if server fails to find Additive<br/>
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDrinkInCocktail(Guid id)
        {
            var drinkInCocktail = await _bll.DrinkInCocktails.FirstOrDefaultAsync(id);
            if (drinkInCocktail == null) return NotFound();
            
            _bll.DrinkInCocktails.Remove(drinkInCocktail);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
