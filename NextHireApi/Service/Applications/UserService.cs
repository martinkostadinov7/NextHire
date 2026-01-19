using Business.Entities;
using Data.Abstraction;
using NextHiteApi.Service.Abstraction;
using Shared.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Users
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userDto, int senderId)
        {
            User user = new User(
                userDto.Email, userDto.FirstName, userDto.LastName,
                userDto.Phone);

            user = await userRepository.AddAsync(user);

            UserReadDto userReadDto = new UserReadDto(user.Id,
                user.Email, user.FirstName, user.LastName,
                user.Phone, user.Applications);

            return userReadDto;
        }

        public async Task DeleteUserAsync(int id)
        {
            await userRepository.DeleteAsync(id);
        }

        public async Task<List<UserReadDto>> GetAllUsers()
        {
            List<User> users = await userRepository.GetAllAsync();

            List<UserReadDto> userReadDtos = new List<UserReadDto>();
            foreach (var user in users)
            {
                UserReadDto userReadDto = new UserReadDto(user.Id,
               user.Email, user.FirstName, user.LastName,
               user.Phone, user.Applications);

                userReadDtos.Add(userReadDto);
            }

            return userReadDtos;
        }

        public async Task<UserReadDto> GetUserById(int id)
        {
            User user = await userRepository.GetByIdAsync(id);

            UserReadDto userReadDto = new UserReadDto(user.Id,
               user.Email, user.FirstName, user.LastName,
               user.Phone, user.Applications);

            return userReadDto;
        }

        public async Task<UserReadDto> UpdateUserAsync(int id, UserUpdateDto userDto)
        {
            User user = new User(
                userDto.Email, userDto.FirstName, userDto.LastName,
                userDto.Phone);

            user = await userRepository.UpdateAsync(user);

            UserReadDto userReadDto = new UserReadDto(user.Id,
               user.Email, user.FirstName, user.LastName,
               user.Phone, user.Applications);

            return userReadDto;
        }
    }
}
