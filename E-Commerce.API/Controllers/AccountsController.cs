using E_Commerce.API.DTOs.UserDTOs;
using E_Commerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Registers a new user with the specified registration details.
        /// </summary>
        /// <remarks>The method validates the input model and checks for default placeholder values before
        /// proceeding. If the model state is invalid or default values are detected, the method returns a bad request
        /// response. Callers should ensure that all registration fields are populated with meaningful values.</remarks>
        /// <param name="registerUserDto">The data transfer object containing the user's registration information. All fields must be provided with
        /// valid, non-default values.</param>
        /// <returns>An IActionResult that indicates the outcome of the registration operation. Returns a success response if
        /// registration is completed successfully; otherwise, returns a bad request response if the input data is
        /// invalid or contains default values.</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (registerUserDto.UserName == "string"
                    || registerUserDto.UserEmail == "user@example.com"
                    || registerUserDto.UserAddress == "string"
                    || registerUserDto.UserPassword == "stringstring"
                    || registerUserDto.UserFullName == "string")
                    return BadRequest("please change the data from default values to your own values");

                return Ok(_accountService.Register(registerUserDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Authenticates a user based on the provided login credentials.
        /// </summary>
        /// <remarks>The method validates the model state before processing the login request. Default
        /// placeholder values for username, email, or password are not accepted and will result in a bad request
        /// response.</remarks>
        /// <param name="loginUserDto">The login credentials containing the username, email, and password used for authentication. The values must
        /// not be default placeholders; ensure that valid, user-specific data is supplied.</param>
        /// <returns>An IActionResult indicating the result of the login attempt. Returns 200 OK with user information if
        /// authentication is successful; otherwise, returns 400 Bad Request with validation errors or failure messages.</returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (loginUserDto.UserName == "string"
                    || loginUserDto.UserEmail == "user@example.com"
                    || loginUserDto.UserPassword == "stringstring")
                    return BadRequest("please change the data from default values to your own values");

                return Ok(_accountService.Login(loginUserDto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Logs out the user with the specified user ID and invalidates their session.
        /// </summary>
        /// <remarks>A BadRequest result is returned if the provided user ID is less than or equal to
        /// zero. Any exceptions encountered during the logout process will also result in a BadRequest response
        /// containing the exception message.</remarks>
        /// <param name="id">The unique identifier of the user to log out. Must be a positive integer.</param>
        /// <returns>An IActionResult indicating the outcome of the logout operation. Returns an OK result if the logout is
        /// successful; otherwise, returns a BadRequest result with an error message.</returns>
        [HttpDelete("{id}")]
        public IActionResult Logout(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid user ID.");

                var result = _accountService.Logout(id);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Change the password of a user based on the provided change password details.
        /// </summary>
        /// <remarks>
        /// The method validates the input model and processes the password change request accordingly.
        /// If the model state is invalid, a bad request response is returned with the validation errors. 
        /// Any exceptions encountered during the password change process will also result in a bad request response containing the exception message.
        /// </remarks>
        /// <param name="changePasswordDto"></param>
        /// <returns>
        /// returns an IActionResult indicating the outcome of the password change operation.
        /// If the password change is successful, an OK result is returned with the result of the operation.
        /// If the model state is invalid or an exception occurs, 
        /// a BadRequest result is returned with the appropriate error message.
        /// </returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult ChangePassowrd([FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                var result = _accountService.ChangePassword(changePasswordDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}