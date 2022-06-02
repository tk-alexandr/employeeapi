using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeesApi.Models;
using EmployeesApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeesApi.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserManager               _userManager;
        private readonly JwtGenerator               _jwtGenerator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserManager               userManager,
                                 JwtGenerator               jwtGenerator,
                                 ILogger<AccountController> logger)
        {
            _userManager  = userManager;
            _jwtGenerator = jwtGenerator;
            _logger       = logger;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginModel loginModel)
        {
            try
            {
                if (!_userManager.CheckCredentials(loginModel))
                    return Forbid(EmployeeApiLogs.WrongUserNameOrPassword);

                var token = _jwtGenerator
                    .AddClaim(new Claim("UserName", loginModel.UserName));

                return Ok(new
                {
                    Token                = token.GetToken(),
                    ExpirationInUnixTime = token.GetTokenExpirationInUnixTime
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, EmployeeApiLogs.InternalServierError);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = EmployeeApiLogs.InternalServierError });
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterUser(RegisterModel registerModel)
        {
            try
            {
                await _userManager.RegisterUser(registerModel);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, EmployeeApiLogs.InternalServierError);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = EmployeeApiLogs.InternalServierError });
            }
        }

#if DEBUG
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("test_token")]
        public ActionResult TestToken()
        {
            return Ok();
        }
#endif
    }
}
