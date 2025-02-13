using Microsoft.AspNetCore.Mvc;
using PaymentControl.Dtos.Request.Login;
using PaymentControl.Dtos.Response.Login;
using PaymentControl.UseCases.User.Register;

namespace PaymentControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRegisterUserCase _registerUserCase;
        public UserController(IRegisterUserCase registerUser)
        {
            _registerUserCase = registerUser;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseUserDto), StatusCodes.Status201Created)]
        public IActionResult RegisterUser (RequestUserDto UserLogin)
        {
            var response = _registerUserCase.ExecuteRegister(UserLogin);
            return Created(string.Empty, response);
        }
    }
}
