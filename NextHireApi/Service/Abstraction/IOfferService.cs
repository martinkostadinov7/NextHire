using Shared.DTOs.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public interface IOfferService
    {
        Task<OfferReadDto> CreateOfferAsync(OfferCreateDto offerDto, int senderId);
        Task<OfferReadDto> UpdateOfferAsync(int id, OfferUpdateDto offerDto);
        Task DeleteOfferAsync(int id);
        Task<List<OfferReadDto>> GetAllOffers();
        Task<OfferReadDto> GetOfferById(int id);
    }
}
