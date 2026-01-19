using Business.Entities;
using Data;
using Data.Abstraction;
using NextHireApi.Service.Abstraction;
using Shared.DTOs.Applications;
using System.Collections.Generic;

namespace NextHireApi.Service.Applications
{
    public class ApplicationService : IApplicationService
    {
        private IApplicationRepository userRepository;

        public ApplicationService(IApplicationRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ApplicationReadDto> CreateApplicationAsync(ApplicationCreateDto userDto, int senderId)
        {
            Application user = new Application(
                userDto.Email, userDto.FirstName, userDto.LastName, 
                userDto.Phone, userDto.Education, userDto.CoverLetter, 
                userDto.CvId, userDto.UserId, userDto.OfferId);

            user = await userRepository.AddAsync(user);
            
            ApplicationReadDto userReadDto = new ApplicationReadDto(user.Id,
                user.Email, user.FirstName, user.LastName,
                user.Phone, user.Education, user.CoverLetter,
                user.Cv, user.SubmittedAt, user.User, user.Offer);

            return userReadDto;
        }

        public async Task DeleteApplicationAsync(int id)
        {
            await userRepository.DeleteAsync(id);
        }

        public async Task<List<ApplicationReadDto>> GetAllApplications()
        {
            List<Application> users = await userRepository.GetAllAsync();

            List<ApplicationReadDto> userReadDtos = new List<ApplicationReadDto>();
            foreach (var user in users)
            {
                ApplicationReadDto userReadDto = new ApplicationReadDto(user.Id,
                user.Email, user.FirstName, user.LastName,
                user.Phone, user.Education, user.CoverLetter,
                user.Cv, user.SubmittedAt, user.User, user.Offer);

                userReadDtos.Add(userReadDto);
            }

            return userReadDtos;
        }

        public async Task<ApplicationReadDto> GetApplicationById(int id)
        {
            Application user = await userRepository.GetByIdAsync(id);

            if (user == null) return null;

            ApplicationReadDto userReadDto = new ApplicationReadDto(user.Id,
                user.Email, user.FirstName, user.LastName,
                user.Phone, user.Education, user.CoverLetter,
                user.Cv, user.SubmittedAt, user.User, user.Offer);

            return userReadDto;
        }

        public async Task<ApplicationReadDto> UpdateApplicationAsync(int id, ApplicationUpdateDto userDto)
        {
            Application application = await userRepository.GetByIdAsync(id);

            if (application == null)
                throw new Exception("Application not found");

            application.Email = userDto.Email;
            application.FirstName = userDto.FirstName;
            application.LastName = userDto.LastName;
            application.Phone = userDto.Phone;
            application.Education = userDto.Education;
            application.CoverLetter = userDto.CoverLetter;
            application.CvId = userDto.CvId;
            application.UserId = userDto.UserId;
            application.OfferId = userDto.OfferId;

            application = await userRepository.UpdateAsync(application);

            return new ApplicationReadDto(
                application.Id, application.Email, application.FirstName,
                application.LastName,application.Phone,application.Education,
                application.CoverLetter,application.Cv,application.SubmittedAt,
                application.User, application.Offer
            );
        }
    }
}
