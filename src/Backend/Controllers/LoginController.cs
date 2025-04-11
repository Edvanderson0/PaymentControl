using Microsoft.AspNetCore.Mvc;
using PaymentControl.Dtos.Request.Login;
using PaymentControl.Dtos.Response.ErrorResponse;
using PaymentControl.Dtos.Response.Login;
using PaymentControl.UseCases.Login;

namespace PaymentControl.Controllers
{
    [Route("Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginUseCase _loginUseCase;
        public LoginController(ILoginUseCase loginUsecase)
        {
            _loginUseCase = loginUsecase;
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(RequestLoginDto request)
        {
            var result = await _loginUseCase.DoLogin(request);
            return Ok(result);
        }
    }
}
