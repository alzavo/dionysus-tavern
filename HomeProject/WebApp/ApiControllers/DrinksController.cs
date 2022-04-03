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
using MapperV1 = PublicApi.DTO.v1.Mappers.DrinkMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Drink.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class DrinksController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// DrinksController constructor
        /// </summary>
        /// <param name="bll">Interface from business layer, gives access to the services.</param>
        public DrinksController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Drinks
        /// <summary>
        /// Get all Drinks
        /// </summary>
        /// <returns>List of Drink</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApiDTOv1.Drink>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApiDTOv1.Drink>>> GetDrink()
        {
            return Ok((await _bll.Drinks.GetAllWithDrinkTypeAndCocktailsCountAsync()).Select(MapperV1.MapToPublic));
        }

        // GET: api/Drinks/5
        /// <summary>
        /// Get Drink
        /// </summary>
        /// <param name="id">Drink Id</param>
        /// <returns>Drink based on the provided Id or 404 Not Found</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.Drink), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.Drink>> GetDrink(Guid id)
        {
            var drink = await _bll.Drinks.GetOneWithDrinkTypeAndCocktailsCountAsync(id);

            if (drink == null) return NotFound();
            
            return MapperV1.MapToPublic(drink);
        }

        // PUT: api/Drinks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Drink
        /// </summary>
        /// <param name="id">Drink Id</param>
        /// <param name="drink">DTO for updating</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if put action was successful<br/>
        /// 400 Bad Request if id in url and id in DTO don't match<br/>
        /// </returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDrink(Guid id, PublicApiDTOv1.DrinkUpdate drink)
        {
            if (id != drink.Id) return BadRequest();

            var bllDrink = MapperV1.MapToBLL(drink);

            _bll.Drinks.Update(bllDrink);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Drinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create Drink
        /// </summary>
        /// <param name="drink">DTO for creation</param>
        /// <returns>Created Drink</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.Drink), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicApiDTOv1.Drink>> PostDrink(PublicApiDTOv1.DrinkCreate drink)
        {
            var bllDrink = MapperV1.MapToBLL(drink);
            var addedDrink = _bll.Drinks.Add(bllDrink);
            await _bll.SaveChangesAsync();
            
            var returnDrink = MapperV1.MapToPublic(addedDrink);
        
            return CreatedAtAction("GetDrink", 
                new
                {
                    id = returnDrink.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnDrink);
        }

        // DELETE: api/Drinks/5
        /// <summary>
        /// Delete Drink
        /// </summary>
        /// <param name="id">Drink Id</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if delete action was successful<br/>
        /// 404 Not Found if server fails to find Additive<br/>
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDrink(Guid id)
        {
            var drink = await _bll.Drinks.FirstOrDefaultAsync(id); 
            if (drink == null) return NotFound();

            _bll.Drinks.Remove(drink);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
