using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLLAppDTO = BLL.App.DTO;
using PublicApiDTOv1 = PublicApi.DTO.v1;
using MapperV1 = PublicApi.DTO.v1.Mappers.StepMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Step
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class StepsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// StepsController constructor
        /// </summary>
        /// <param name="bll">Interface from business layer, gives access to the services</param>
        public StepsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Steps/5
        /// <summary>
        /// Get Step
        /// </summary>
        /// <param name="id">Step Id</param>
        /// <returns>Step based on the provided Id or 404 Not Found</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.Step), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.Step>> GetStep(Guid id)
        {
            var step = await _bll.Steps.GetOneWithCocktailNameAsync(id);

            if (step == null) return NotFound();

            return MapperV1.MapToPublic(step);
        }

        // PUT: api/Steps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Step
        /// </summary>
        /// <param name="id">Step Id</param>
        /// <param name="step">DTO for updating</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if put action was successful<br/>
        /// 400 Bad Request if id in url and id in DTO don't match<br/>
        /// </returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutStep(Guid id, PublicApiDTOv1.StepUpdate step)
        {
            if (id != step.Id) return BadRequest(); 
            
            var bllStep = MapperV1.MapToBLL(step);

            _bll.Steps.Update(bllStep);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Steps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create Step
        /// </summary>
        /// <param name="step">DTO for creation</param>
        /// <returns>Created Step</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.Step), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicApiDTOv1.Step>> PostStep(PublicApiDTOv1.StepCreate step)
        {
            var bllStep = MapperV1.MapToBLL(step);
            var addedStep = _bll.Steps.Add(bllStep);
            await _bll.SaveChangesAsync();
            
            var returnStep = MapperV1.MapToPublic(addedStep);

            return CreatedAtAction(
                "GetStep", 
                new
                {
                    id = returnStep.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnStep);
        }

        // DELETE: api/Steps/5
        /// <summary>
        /// Delete Step
        /// </summary>
        /// <param name="id">Step Id</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if delete action was successful<br/>
        /// 404 Not Found if server fails to find Additive<br/>
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStep(Guid id)
        {
            var step = await _bll.Steps.FirstOrDefaultAsync(id);
            if (step == null) return NotFound();

            _bll.Steps.Remove(step);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
