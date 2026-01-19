using Shared.DTOs.Cvs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public interface ICvService
    {
        Task<CvReadDto> CreateCvAsync(CvCreateDto cvDto, int senderId);
        Task<CvReadDto> UpdateCvAsync(int id, CvUpdateDto cvDto);
        Task DeleteCvAsync(int id);
        Task<List<CvReadDto>> GetAllCvs();
        Task<CvReadDto> GetCvById(int id);
    }
}
