using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHire.Domain.Entities
{
    public class Offer
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public List<Application> Applications { get; set; } = new List<Application>();

        private Offer()
        {
            
        }

        public Offer(string title, string description, int companyId, Company company)
        {
            Title = title;
            Description = description;
            CompanyId = companyId;
            Company = company;
        }
    }
}
