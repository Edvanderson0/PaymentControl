using Microsoft.AspNetCore.Mvc;
using PaymentControl.Dtos.Request.Login;
using PaymentControl.Dtos.Request.User;
using PaymentControl.Dtos.Response.ErrorResponse;
using PaymentControl.Dtos.Response.Login;
using PaymentControl.UseCases.User.ChangePassword;
using PaymentControl.UseCases.User.Delete;
using PaymentControl.UseCases.User.Register;
using PaymentControl.UseCases.User.Update;

namespace PaymentControl.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRegisterUserCase _registerUserCase;
        private readonly IUpdateUserUseCase _updateUserCase;
        private readonly IChangeUserPasswordUseCase _changeUserPasswordUseCase;
        private readonly IDeleteUserUseCase _deleteUserUseCase;
        
        public UserController(IRegisterUserCase registerUser, IUpdateUserUseCase updateUser, IChangeUserPasswordUseCase changePassword, IDeleteUserUseCase deleteUser)
        {
            _registerUserCase = registerUser;
            _updateUserCase = updateUser;
            _changeUserPasswordUseCase = changePassword;
            _deleteUserUseCase = deleteUser;
        }

        [HttpPost("register-user")]
        [ProducesResponseType(typeof(ResponseUserDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser (RequestRegisterUserDto request)
        {
            var response = await _registerUserCase.ExecuteRegister(request);
            return Created(string.Empty, response);
        }

        [HttpPut("update-user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUser (RequestUpdateUserDto request)
        {
            await _updateUserCase.ExecuteUpdate(request);
            return NoContent();
        }

        [HttpPut("change-password")]
        [ProducesResponseType (StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(RequestChangeUserPassword request)
        {
            await _changeUserPasswordUseCase.Execute(request);
            return NoContent();
        }

        [HttpDelete("delete-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>DeleteUser(RequestDeleteUserDto request)
        {
            await _deleteUserUseCase.Delete(request);
            return NoContent();
        }
    }
}
