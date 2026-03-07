using Microsoft.AspNetCore.Mvc;
using NextHireApi.Service.Abstraction;
using Shared.DTOs.Users;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateUser(UserCreateDto request)
        {
            UserReadDto result = await userService.CreateUserAsync(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetUserById(int id)
        {
            UserReadDto result = await userService.GetUserById(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserReadDto>>> GetAll()
        {
            List<UserReadDto> result = await userService.GetAllUsers();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserReadDto>> DeleteUser(int id)
        {
            await userService.DeleteUserAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserReadDto>> UpdateUser(int id, [FromBody] UserUpdateDto request)
        {
            UserReadDto result = await userService.UpdateUserAsync(id, request);
            return Ok(result);
        }
    }
}
