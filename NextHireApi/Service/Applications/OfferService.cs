using Business.Entities;
using Data.Abstraction;
using Service.Abstraction;
using Shared.DTOs.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Applications
{
    public class OfferService : IOfferService
    {
        private IOfferRepository offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            this.offerRepository = offerRepository;
        }

        public async Task<OfferReadDto> CreateOfferAsync(OfferCreateDto offerDto, int senderId)
        {
            Offer offer = new Offer(
                offerDto.Title, offerDto.Description, offerDto.CompanyId);

            offer = await offerRepository.AddAsync(offer);

            OfferReadDto offerReadDto = new OfferReadDto(offer.Id,
                offer.Title, offer.Description, offer.Company ,offer.Applications);

            return offerReadDto;
        }

        public async Task DeleteOfferAsync(int id)
        {
            await offerRepository.DeleteAsync(id);
        }

        public async Task<List<OfferReadDto>> GetAllOffers()
        {
            List<Offer> offers = await offerRepository.GetAllAsync();

            List<OfferReadDto> offerReadDtos = new List<OfferReadDto>();
            foreach (var offer in offers)
            {
                OfferReadDto offerReadDto = new OfferReadDto(offer.Id,
                offer.Title, offer.Description, offer.Company, offer.Applications);

                offerReadDtos.Add(offerReadDto);
            }

            return offerReadDtos;
        }

        public async Task<OfferReadDto> GetOfferById(int id)
        {
            Offer offer = await offerRepository.GetByIdAsync(id);

            OfferReadDto offerReadDto = new OfferReadDto(offer.Id,
               offer.Title, offer.Description, offer.Company, offer.Applications);

            return offerReadDto;
        }

        public async Task<OfferReadDto> UpdateOfferAsync(int id, OfferUpdateDto offerDto)
        {
            Offer offer = await offerRepository.GetByIdAsync(id);

            if (offer == null)
                throw new Exception("Offer not found");

            offer.Title = offerDto.Title;
            offer.Description = offerDto.Description;
            offer.CompanyId = offerDto.CompanyId;

            offer = await offerRepository.UpdateAsync(offer);

            return new OfferReadDto(
                offer.Id,
                offer.Title,
                offer.Description,
                offer.Company,
                offer.Applications
            );
        }

    }
}
