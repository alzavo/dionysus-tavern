using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;
using MapperV1 = PublicApi.DTO.v1.Mappers.DrinkTypeMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for DrinkType.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class DrinksTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// DrinksTypesController constructor.
        /// </summary>
        /// <param name="bll">Interface from business layer, gives access to the services.</param>
        public DrinksTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/DrinksTypes
        /// <summary>
        /// Get all DrinksTypes
        /// </summary>
        /// <returns>List of DrinksTypes</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApiDTOv1.DrinkType>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApiDTOv1.DrinkType>>> GetDrinkType()
        {
            return Ok((await _bll.DrinksTypes.GetAllWithDrinksCountAsync()).Select(MapperV1.MapToPublic));
        }

        // GET: api/DrinksTypes/5
        /// <summary>
        /// Get DrinkType 
        /// </summary>
        /// <param name="id">DrinkType Id</param>
        /// <returns>DrinkType based on the provided Id or 404 Not Found</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.DrinkType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.DrinkType>> GetDrinkType(Guid id)
        {
            var drinkType = await _bll.DrinksTypes.GetOneWithDrinksCountAsync(id);

            if (drinkType == null) return NotFound();

            return MapperV1.MapToPublic(drinkType);
        }

        // PUT: api/DrinksTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update DrinkType
        /// </summary>
        /// <param name="id">DrinkType Id</param>
        /// <param name="drinkType">DTO for updating</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if put action was successful<br/>
        /// 400 Bad Request if id in url and id in DTO don't match<br/>
        /// </returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDrinkType(Guid id, PublicApiDTOv1.DrinkTypeUpdate drinkType)
        {
            if (id != drinkType.Id) return BadRequest();

            var bllDrinkType = MapperV1.MapToBLL(drinkType);

            _bll.DrinksTypes.Update(bllDrinkType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/DrinksTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create DrinkType
        /// </summary>
        /// <param name="drinkType">DTO for creation</param>
        /// <returns>Created DrinkType</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.DrinkType), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicApiDTOv1.DrinkType>> PostDrinkType(PublicApiDTOv1.DrinkTypeCreate drinkType)
        {
            var bllDrinkType = MapperV1.MapToBLL(drinkType);
            var addedDrinkType = _bll.DrinksTypes.Add(bllDrinkType);
            await _bll.SaveChangesAsync();

            var returnDrinkType = MapperV1.MapToPublic(addedDrinkType);

            return CreatedAtAction(
                "GetDrinkType", 
                new
                {
                    id = returnDrinkType.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnDrinkType);
        }

        // DELETE: api/DrinksTypes/5
        /// <summary>
        /// Delete DrinkType
        /// </summary>
        /// <param name="id">DrinkType Id</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if delete action was successful<br/>
        /// 404 Not Found if server fails to find Additive<br/>
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDrinkType(Guid id)
        {
            var drinkType = await _bll.DrinksTypes.FirstOrDefaultAsync(id); 
            if (drinkType == null) return NotFound();

            _bll.DrinksTypes.Remove(drinkType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
