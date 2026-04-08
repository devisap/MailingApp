using MailingApp.Dtos.Generals;
using MailingApp.Dtos.Requests;
using MailingApp.Services;
using MailingApp.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MailingApp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(ReqLoginDto reqDto)
        {
            // try
            // {
                var jwtFromServer = _authService.CheckAuthentication(reqDto);
                if(!jwtFromServer.status.Equals("success"))
                {
                    return Ok(new ResFailedDto(new ResStatusDto(Const.RESP_FAILED_PERMISSION, jwtFromServer.remark), Const.HTTP_CODE_UNAUTHORIZED, null));
                }
                string jwt = _authService.GenerateToken(jwtFromServer.data.token);
                return Ok(new ResSuccessDto(Const.RESP_SUCCESS_AUTHENTICATION, jwt));
            // }
            // catch (Exception ex)
            // {
                
            //     return Ok(new ResErrorDto(Const.RESP_ERROR, ex.InnerException?.Message ?? ex.Message));
            // }
        }
    }
}