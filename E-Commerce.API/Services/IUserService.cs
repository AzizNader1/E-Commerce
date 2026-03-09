using E_Commerce.API.DTOs.UserDTOs;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This interface defines the contract for the user service in an e-commerce application. It includes methods for retrieving all users, retrieving a user by their ID, adding a new user, updating an existing user, and deleting a user. Each method is designed to handle specific operations related to user management, ensuring that the application can manage its user base effectively. The service is responsible for implementing the business logic associated with users and interacting with the data layer to perform CRUD operations on user data.
    /// </summary>
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        UserDto GetUserById(int userId);
        void AddUser(RegisterUserDto createUserDto);
        void UpdateUser(UserDto userDto);
        void DeleteUser(int userId);
    }
}
