using E_Commerce.API.DTOs.UserDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.UnitOfWork;

namespace E_Commerce.API.Services
{
    /// <summary>
    /// This class represents the service layer for managing users in an e-commerce application. It implements the IUserService interface and provides methods for adding, deleting, retrieving, and updating user information. The service interacts with the database through a Unit of Work (UOW) pattern to perform operations on user data. Each method includes validation checks to ensure that the input data is valid and that the necessary related entities exist in the database. The service is responsible for handling all business logic related to users, ensuring that the application functions correctly when users create or modify their accounts.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UOW _uow;
        public UserService(UOW uow)
        {
            _uow = uow;
        }

        public void AddUser(RegisterUserDto createUserDto)
        {
            if (createUserDto == null)
                throw new ArgumentNullException(nameof(createUserDto), "the user data can not be left empty");

            _uow.UserRepository.AddModel(new User
            {
                UserFullName = createUserDto.UserFullName,
                UserEmail = createUserDto.UserEmail,
                UserAddress = createUserDto.UserAddress,
                UserPhoneNumber = createUserDto.UserPhoneNumber,
                UserName = createUserDto.UserName,
                UserPassword = createUserDto.UserPassword,
            });
        }

        public void DeleteUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentNullException(nameof(userId), "invalid user id");

            if (_uow.UserRepository.GetModelById(userId) == null)
                throw new ArgumentNullException("there is no user exists in the database for that id");

            _uow.UserRepository.DeleteModel(userId);
        }

        public List<UserDto> GetAllUsers()
        {
            var users = _uow.UserRepository.GetAllModels();
            if (users == null || users.Count == 0)
                throw new ArgumentNullException(nameof(users), "there is no users exists in the database");

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                userDtos.Add(MapModelToDto(user));
            }
            return userDtos;
        }

        public UserDto GetUserById(int userId)
        {
            if (userId <= 0)
                throw new ArgumentNullException(nameof(userId), "invalid user id");

            var user = _uow.UserRepository.GetModelById(userId);
            if (user == null)
                throw new ArgumentNullException(nameof(user), "there is no user exists for that specific id you want");

            return MapModelToDto(user);
        }

        public void UpdateUser(UserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto), "user data can not be null");

            var selectedUser = _uow.UserRepository.GetModelById(userDto.UserId);
            if (selectedUser == null)
                throw new ArgumentNullException(nameof(selectedUser), "there is no user exists for that specific data you send");

            _uow.UserRepository.UpdateModel(new User
            {
                UserFullName = userDto.UserFullName,
                UserEmail = userDto.UserEmail,
                UserAddress = userDto.UserAddress,
                UserPhoneNumber = userDto.UserPhoneNumber,
                UserName = userDto.UserName,
                UserPassword = userDto.UserPassword
            });
        }

        private UserDto MapModelToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                UserFullName = user.UserFullName,
                UserEmail = user.UserEmail!,
                UserAddress = user.UserAddress,
                UserPhoneNumber = user.UserPhoneNumber!,
                UserName = user.UserName!,
                UserPassword = user.UserPassword!
            };
        }
    }
}
