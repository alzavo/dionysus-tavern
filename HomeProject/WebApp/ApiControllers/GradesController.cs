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
using MapperV1 = PublicApi.DTO.v1.Mappers.GradeMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Grade
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GradesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        /// <summary>
        /// GradesController constructor
        /// </summary>
        /// <param name="bll">Interface from business layer, gives access to the services</param>
        public GradesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Grades
        /// <summary>
        /// Get all Grades
        /// </summary>
        /// <returns>List of Grades</returns>
        [HttpGet]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(IEnumerable<PublicApiDTOv1.Grade>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApiDTOv1.Grade>>> GetGrade()
        {
            return Ok((await _bll.Grades.GetAllWithUsageCountAsync()).Select(MapperV1.MapToPublic));
        }

        // GET: api/Grades/5
        /// <summary>
        /// Get Grade
        /// </summary>
        /// <param name="id">Grade Id</param>
        /// <returns>Grade based on the provided Id or 404 Not Found</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(PublicApiDTOv1.Grade), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.Grade>> GetGrade(Guid id)
        {
            var grade = await _bll.Grades.GetOneWithUsageCountAsync(id);

            if (grade == null) return NotFound();
            
            return MapperV1.MapToPublic(grade);
        }

        // PUT: api/Grades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update Grade by Id.
        /// </summary>
        /// <param name="id">Grade Id</param>
        /// <param name="grade">DTO for updating</param>
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
        public async Task<IActionResult> PutGrade(Guid id, PublicApiDTOv1.GradeUpdate grade)
        {
            if (id != grade.Id) return BadRequest();

            var bllGrade = MapperV1.MapToBLL(grade);
            
            _bll.Grades.Update(bllGrade);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Grades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Create Grade
        /// </summary>
        /// <param name="grade">DTO for creation</param>
        /// <returns>Created Grade</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.Drink), StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicApiDTOv1.Grade>> PostGrade(PublicApiDTOv1.GradeCreate grade)
        {
            var bllGrade = MapperV1.MapToBLL(grade);
            var addedGrade = _bll.Grades.Add(bllGrade);
            await _bll.SaveChangesAsync();
            
            var returnGrade = MapperV1.MapToPublic(addedGrade);

            return CreatedAtAction(
                "GetGrade", 
                new
                {
                    id = returnGrade.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                }, 
                returnGrade);
        }

        // DELETE: api/Grades/5
        /// <summary>
        /// Delete Grade
        /// </summary>
        /// <param name="id">Grade Id</param>
        /// <returns>
        /// Status codes:<br/>
        /// 204 No Content if delete action was successful<br/>
        /// 404 Not Found if server fails to find Additive<br/>
        /// </returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGrade(Guid id)
        {
            var grade = await _bll.Grades.FirstOrDefaultAsync(id); 
            if (grade == null) return NotFound();

            _bll.Grades.Remove(grade);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
