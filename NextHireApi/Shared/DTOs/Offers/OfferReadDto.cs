using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Offers
{
    public class OfferReadDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Company Company { get; set; }

        public List<Application> Applications { get; set; } = new List<Application>();

        public OfferReadDto(int id, string title, string description, Company company, List<Application> applications)
        {
            Id = id;
            Title = title;
            Description = description;
            Company = company;
            Applications = applications;
        }
    }
}
