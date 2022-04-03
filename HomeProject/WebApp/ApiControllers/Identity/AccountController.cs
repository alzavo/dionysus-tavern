using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicApiDTOv1 = PublicApi.DTO.v1;

namespace WebApp.ApiControllers.Identity
{
    /// <summary>
    /// API controller for Account.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// AccountController constructor.
        /// </summary>
        /// <param name="signInManager">Provides the APIs for user sign in.</param>
        /// <param name="userManager">Provides the APIs for managing user in a persistence store.</param>
        /// <param name="logger">See ILogger and AccountController documentations for this parameter.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<AccountController> logger, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Login with the existing user account.
        /// </summary>
        /// <param name="dto">DTO with email and password</param>
        /// <returns>Success: DTO with JWT token and username. Failure: DTO with list of error messages.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PublicApiDTOv1.Message), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicApiDTOv1.JwtResponse>> Login([FromBody] PublicApiDTOv1.Login dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                _logger.LogWarning("WebApi login failed. User {User} not found!", dto.Email);
                return NotFound(new PublicApiDTOv1.Message("User or Password problem!"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                var jwt = Extensions.Base.IdentityExtensions.GenerateGwt(
                    claimsPrincipal.Claims,
                    _configuration["JWT:Key"],
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Issuer"],
                    DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                );
                _logger.LogInformation("WebApi login. User {User}", dto.Email);
                return Ok(new PublicApiDTOv1.JwtResponse()
                {
                    Token = jwt,
                    Username = user.UserName
                });
            }
            
            _logger.LogWarning("WebApi login failed. User {User} - bad password!", dto.Email);
            return NotFound(new PublicApiDTOv1.Message("User or Password problem!"));
        }

        /// <summary>
        /// Register new user account.
        /// </summary>
        /// <param name="dto">DTO with username, email and password.</param>
        /// <returns>Success: DTO with JWT token and username. Failure: DTO with list of error messages.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApiDTOv1.JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PublicApiDTOv1.Message), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Register([FromBody] PublicApiDTOv1.Register dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null)
            {
                _logger.LogWarning("User {User} already registered!", dto.Email);
                return BadRequest(new PublicApiDTOv1.Message("User already registered!"));
            }

            user = new User()
            {
                Email = dto.Email,
                UserName = dto.Username,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} created a new account with password!", dto.Email);

                var appUser = await _userManager.FindByEmailAsync(user.Email);
                if (appUser != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                    var jwt = Extensions.Base.IdentityExtensions.GenerateGwt(
                        claimsPrincipal.Claims,
                        _configuration["JWT:Key"],
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Issuer"],
                        DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                    _logger.LogInformation("WebApi login. User {User}", dto.Email);
                    return Ok(new PublicApiDTOv1.JwtResponse()
                    {
                        Token = jwt,
                        Username = appUser.UserName
                    });
                }

                _logger.LogWarning("User {Email} not found after creation!", dto.Email);
                return BadRequest(new PublicApiDTOv1.Message("User not found after creation!"));
            }

            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new PublicApiDTOv1.Message() {Messages = errors});
        }
    }
}