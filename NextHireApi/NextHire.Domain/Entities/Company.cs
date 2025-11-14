using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHire.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Offer> Offers { get; set; } = new List<Offer>();

        private Company()
        {
            
        }
        public Company(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
