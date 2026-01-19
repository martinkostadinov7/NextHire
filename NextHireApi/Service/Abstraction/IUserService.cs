using Shared.DTOs;
using Shared.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHireApi.Service.Abstraction
{
    public interface IUserService
    {
        Task<UserReadDto> CreateUserAsync(UserCreateDto userDto, int senderId);
        Task<UserReadDto> UpdateUserAsync(int id, UserUpdateDto userDto);
        Task DeleteUserAsync(int id);
        Task<List<UserReadDto>> GetAllUsers();
        Task<UserReadDto> GetUserById(int id);
    }
}
