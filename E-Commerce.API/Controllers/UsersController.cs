using E_Commerce.API.DTOs.UserDTOs;
using E_Commerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Gets a list of all users in the system.
        /// </summary>
        /// <remarks>
        /// The method retrieves all user records from the database and returns them as a response. 
        /// If any exceptions occur during the retrieval process, a bad request response is returned with the error message.
        /// </remarks>
        /// <returns>
        /// Returns an IActionResult containing the list of users if the retrieval is successful. 
        /// If an exception occurs, it returns a BadRequest response with the error message.
        /// </returns>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets a user by their unique identifier (ID).
        /// </summary>
        /// <remarks>
        /// The method takes an integer ID as a parameter and retrieves the corresponding user from the database.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>
        /// Returns an IActionResult containing the user information if the retrieval is successful.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid User Id");

                var user = userService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Adds a new user to the system based on the provided RegisterUserDto object.
        /// </summary>
        /// <remarks>
        /// The method takes a RegisterUserDto object as input, which contains the necessary information to create a new user.
        /// </remarks>
        /// <param name="createUserDto"></param>
        /// <returns>
        /// Returns an IActionResult indicating the outcome of the add operation.
        /// </returns>
        [HttpPost]
        public IActionResult AddUser([FromBody] RegisterUserDto createUserDto)
        {
            try
            {
                userService.AddUser(createUserDto);
                return Ok("this user created successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates the information of an existing user in the system based on the provided UserDto object.
        /// </summary>
        /// <remarks>
        /// The method takes a UserDto object as input, which contains the updated user information.
        /// </remarks>
        /// <param name="userDto"></param>
        /// <returns>
        /// returns an IActionResult indicating the outcome of the update operation.
        /// </returns>
        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserDto userDto)
        {
            try
            {
                userService.UpdateUser(userDto);
                return Ok("this user data updated successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes a user with the specified ID from the system.
        /// </summary>
        /// <remarks>The method checks if the provided user ID is less than or equal to zero, which is considered invalid.
        /// If such a case occurs, it returns a bad request response indicating the issue.
        /// If the ID is valid, it proceeds to delete the user and returns a success message.
        /// It is important for callers to provide a valid user ID to ensure the operation completes successfully.</remarks>
        /// <param name="id"></param>
        /// <returns>
        /// Returns an IActionResult indicating the outcome of the delete operation. 
        /// If the provided user ID is invalid (less than or equal to zero), 
        /// it returns a 400 Bad Request response with an appropriate error message.
        /// If the deletion is successful, it returns a 200 OK response with a confirmation message indicating that the user was deleted successfully.
        /// Callers should ensure that the user ID provided is valid and corresponds to an existing user in the system to avoid errors during deletion.
        /// </returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                userService.DeleteUser(id);
                return Ok("this user deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
