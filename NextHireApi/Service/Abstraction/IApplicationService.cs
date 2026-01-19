using Shared.DTOs.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHiteApi.Service.Abstraction
{
    public interface IApplicationService
    {
        Task<ApplicationReadDto> CreateApplicationAsync(ApplicationCreateDto userDto, int senderId);
        Task<ApplicationReadDto> UpdateApplicationAsync(int id, ApplicationUpdateDto userDto);
        Task DeleteApplicationAsync(int id);
        Task<List<ApplicationReadDto>> GetAllApplications();
        Task<ApplicationReadDto> GetApplicationById(int id);
    }
}
