using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Companies
{
    public class CompanyReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Offer> Offers { get; set; } = new List<Offer>();

        public CompanyReadDto(int id, string name, string description, List<Offer> offers)
        {
            Id = id;
            Name = name;
            Description = description;
            Offers = offers;
        }
    }
}
