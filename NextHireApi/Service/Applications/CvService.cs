using Business.Entities;
using Data.Abstraction;
using Service.Abstraction;
using Shared.DTOs.Cvs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Applications
{
    public class CvService : ICvService
    {
        private ICvRepository cvRepository;

        public CvService(ICvRepository cvRepository)
        {
            this.cvRepository = cvRepository;
        }

        public async Task<CvReadDto> CreateCvAsync(CvCreateDto cvDto, int senderId)
        {
            Cv cv = new Cv(cvDto.UserId);

            cv = await cvRepository.AddAsync(cv);

            CvReadDto cvReadDto = new CvReadDto(cv.Id, cv.User);

            return cvReadDto;
        }

        public async Task DeleteCvAsync(int id)
        {
            await cvRepository.DeleteAsync(id);
        }

        public async Task<List<CvReadDto>> GetAllCvs()
        {
            List<Cv> cvs = await cvRepository.GetAllAsync();

            List<CvReadDto> cvReadDtos = new List<CvReadDto>();
            foreach (var cv in cvs)
            {
                CvReadDto cvReadDto = new CvReadDto(cv.Id, cv.User);

                cvReadDtos.Add(cvReadDto);
            }

            return cvReadDtos;
        }

        public async Task<CvReadDto> GetCvById(int id)
        {
            Cv cv = await cvRepository.GetByIdAsync(id);

            CvReadDto cvReadDto = new CvReadDto(cv.Id, cv.User);

            return cvReadDto;
        }

        public async Task<CvReadDto> UpdateCvAsync(int id, CvUpdateDto cvDto)
        {
            Cv cv = await cvRepository.GetByIdAsync(id);

            if (cv == null)
                throw new Exception("CV not found");

            cv.UserId = cvDto.UserId;

            cv = await cvRepository.UpdateAsync(cv);

            return new CvReadDto(
                cv.Id,
                cv.User
            );
        }

    }
}
