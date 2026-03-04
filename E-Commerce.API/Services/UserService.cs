using E_Commerce.API.DTOs.UserDTOs;
using E_Commerce.API.Models;
using E_Commerce.API.UnitOfWork;

namespace E_Commerce.API.Services
{
    public class UserService : IUserService
    {
        private readonly UOW _uow;
        public UserService(UOW uow)
        {
            _uow = uow;
        }

        public void AddUserAsync(RegisterUserDto createUserDto)
        {
            if (createUserDto == null)
                throw new ArgumentNullException(nameof(createUserDto), "the user data can not be left empty");

            _uow.UserRepository.AddAsync(new User
            {
                UserFullName = createUserDto.UserFullName,
                UserEmail = createUserDto.UserEmail,
                UserAddress = createUserDto.UserAddress,
                UserPhoneNumber = createUserDto.UserPhoneNumber,
                UserName = createUserDto.UserName,
                UserPassword = createUserDto.UserPassword,
            });
        }

        public void DeleteUserAsync(int userId)
        {
            if (userId == null || userId == 0)
                throw new ArgumentNullException(nameof(userId), "invalid user id");

            User selectedUser = _uow.UserRepository.GetByIdAsync(userId);
            if (selectedUser == null)
                throw new ArgumentNullException(nameof(selectedUser), "there is no user exists in the database for that id");

            _uow.UserRepository.DeleteAsync(userId);
        }

        public List<UserDto> GetAllUsersAsync()
        {
            List<User> users = _uow.UserRepository.GetAllAsync();
            if (users == null || users.Count == 0)
                throw new ArgumentNullException(nameof(users), "there is no users exists in the database");

            List<UserDto> userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                userDtos.Add(new UserDto
                {
                    UserId = user.UserId,
                    UserFullName = user.UserFullName,
                    UserEmail = user.UserEmail,
                    UserAddress = user.UserAddress,
                    UserPhoneNumber = user.UserPhoneNumber,
                    UserName = user.UserName,
                    UserPassword = user.UserPassword
                });
            }
            return userDtos;
        }

        public UserDto GetUserByIdAsync(int userId)
        {
            if (userId == null || userId == 0)
                throw new ArgumentNullException(nameof(userId), "invalid user id");

            User user = _uow.UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new ArgumentNullException(nameof(user), "there is no user exists for that specific id you want");

            return new UserDto
            {
                UserFullName = user.UserFullName,
                UserAddress = user.UserAddress,
                UserPhoneNumber = user.UserPhoneNumber,
                UserEmail = user.UserEmail,
                UserId = user.UserId,
                UserName = user.UserName,
                UserPassword = user.UserPassword
            };
        }

        public void UpdateUserAsync(UserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException(nameof(userDto), "user data can not be null");

            User selectedUser = _uow.UserRepository.GetByIdAsync(userDto.UserId);
            if (selectedUser == null)
                throw new ArgumentNullException(nameof(selectedUser), "there is no user exists for that specific data you send");

            _uow.UserRepository.UpdateAsync(new User
            {
                UserFullName = userDto.UserFullName,
                UserEmail = userDto.UserEmail,
                UserAddress = userDto.UserAddress,
                UserPhoneNumber = userDto.UserPhoneNumber,
                UserName = userDto.UserName,
                UserPassword = userDto.UserPassword
            });
        }
    }
}
