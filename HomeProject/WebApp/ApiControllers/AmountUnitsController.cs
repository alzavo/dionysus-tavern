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
using MapperV1 = PublicApi.DTO.v1.Mappers.AmountUnitMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for AmountUnit
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AmountUnitsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// AmountUnitsController constructor
        /// </summary>
        /// <param name="bll">Interface from business layer, gives access to the services</param>
        public AmountUnitsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/AmountUnits
        /// <summary>
        /// Get all AmountUnits
        /// </summary>
        /// <returns>List of AmountUnits</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PublicApiDTOv1.AmountUnit>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApiDTOv1.AmountUnit>>> GetAmountUnit()
        {
            return Ok((await _bll.AmountUnits.GetAllWithUsageCountAsync()).Select(MapperV1.MapToPublic));
        }

        // GET: api/AmountUnits/5
        /// <summary>
        /// Get AmountUnit
        /// </summary>
        /// <param name="id">AmountUnit Id</param>
        /// <returns>AmountUnit based on the provided Id or 404 Not Found</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.AmountUnit), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.AmountUnit>> GetAmountUnit(Guid id)
        {
            var amountUnit = await _bll.AmountUnits.GetOneWithUsageCountAsync(id);

            if (amountUnit == null) return NotFound();
            
            return MapperV1.MapToPublic(amountUnit);
        }

        // PUT: api/AmountUnits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update AmountUnit
        /// </summary>
        /// <param name="id">AmountUnit Id</param>
        /// <param name="amountUnit">DTO for updating</param>
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
        public async Task<IActionResult> PutAmountUnit(Guid id, PublicApiDTOv1.AmountUnitUpdate amountUnit)
        {
            if (id != amountUnit.Id) return BadRequest();
            
            var bllAmountUnit = MapperV1.MapToBLL(amountUnit);

            _bll.AmountUnits.Update(bllAmountUnit);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/AmountUnits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create AmountUnit.
        /// </summary>
        /// <param name="amountUnit">AmountUnit DTO.</param>
        /// <returns>Created AmountUnit.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.AmountUnit), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicApiDTOv1.AmountUnit>> PostAmountUnit(PublicApiDTOv1.AmountUnitCreate amountUnit)
        {
            var bllAmountUnit = MapperV1.MapToBLL(amountUnit);
            var addedAmountUnit =  _bll.AmountUnits.Add(bllAmountUnit);
            await _bll.SaveChangesAsync();

            var returnAmountUnit = MapperV1.MapToPublic(addedAmountUnit);

            return CreatedAtAction(
                "GetAmountUnit", 
                new
                {
                    id = returnAmountUnit.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnAmountUnit);
        }

        // DELETE: api/AmountUnits/5
        /// <summary>
        /// Delete AmountDrink
        /// </summary>
        /// <param name="id">AmountDrink Id</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if delete action was successful<br/>
        /// 404 Not Found if server fails to find Additive<br/>
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAmountUnit(Guid id)
        {
            var amountUnit = await _bll.AmountUnits.FirstOrDefaultAsync(id);
            if (amountUnit == null) return NotFound();
            
            _bll.AmountUnits.Remove(amountUnit);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
