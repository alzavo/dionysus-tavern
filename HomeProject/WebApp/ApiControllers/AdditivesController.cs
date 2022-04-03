using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MapperV1 = PublicApi.DTO.v1.Mappers.AdditiveMapper;
using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Additive
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdditivesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// AdditivesController constructor
        /// </summary>
        /// <param name="bll">Interface from business layer, gives access to the services</param>
        public AdditivesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Additives
        /// <summary>
        /// Get all Additives
        /// </summary>
        /// <returns>List of Additives</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApiDTOv1.Additive>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApiDTOv1.Additive>>> GetAdditive()
        {
            return Ok((await _bll.Additives.GetAllWithCocktailsCountAsync()).Select(MapperV1.MapToPublic).ToList());
        }
        
        // // GET: api/Additives
        // /// <summary>
        // /// Get all Additives
        // /// </summary>
        // /// <returns>List of Additives</returns>
        // [HttpGet("filter/cocktails-count")]
        // [Produces("application/json")]
        // [ProducesResponseType(typeof(IEnumerable<PublicApiDTOv1.Additive>), StatusCodes.Status200OK)]
        // public async Task<ActionResult<IEnumerable<PublicApiDTOv1.Additive>>> GetSortedAdditive()
        // {
        //     return Ok((await _bll.Additives.GetAllWithCocktailsCountAsync()).Select(MapperV1.MapToPublic));
        // }

        // GET: api/Additives/5
        /// <summary>
        /// Get Additive
        /// </summary>
        /// <param name="id">Additive Id</param>
        /// <returns>Additive based on the provided Id or 404 Not Found</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.Additive), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.Additive>> GetAdditive(Guid id)
        {
            var additive = await _bll.Additives.GetOneWithCocktailsCountAsync(id);

            if (additive == null) return NotFound();

            return MapperV1.MapToPublic(additive);
        }

        // PUT: api/Additives/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Additive
        /// </summary>
        /// <param name="id">Additive Id</param>
        /// <param name="additive">DTO for updating</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if put action was successful<br/>
        /// 400 Bad Request if id in url and id in DTO don't match<br/>
        /// </returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAdditive(Guid id, PublicApiDTOv1.AdditiveUpdate additive)
        {
            if (id != additive.Id) return BadRequest();

            var bllAdditive = MapperV1.MapToBLL(additive);
            
            _bll.Additives.Update(bllAdditive);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Additives
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create Additive
        /// </summary>
        /// <param name="additive">DTO for creation</param>
        /// <returns>Created Additive</returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.Additive), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicApiDTOv1.Additive>> PostAdditive(PublicApiDTOv1.AdditiveCreate additive)
        {
            var bllAdditive = MapperV1.MapToBLL(additive);
            var addedAdditive = _bll.Additives.Add(bllAdditive);
            await _bll.SaveChangesAsync();

            var returnAdditive = MapperV1.MapToPublic(addedAdditive);

            return CreatedAtAction("GetAdditive", 
                new
                {
                    id = returnAdditive.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnAdditive);
        }

        // DELETE: api/Additives/5
        /// <summary>
        /// Delete Additive
        /// </summary>
        /// <param name="id">Additive Id</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if delete action was successful<br/>
        /// 404 Not Found if server fails to find Additive<br/>
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAdditive(Guid id)
        {
            var additive = await _bll.Additives.FirstOrDefaultAsync(id); 
            if (additive == null) return NotFound();

            _bll.Additives.Remove(additive);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
