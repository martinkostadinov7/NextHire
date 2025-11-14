using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHire.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public List<Application> Applications { get; set; } = new List<Application>();

        private User()
        {
            
        }

        public User(string email, string firstName, string lastName, string phone)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }
    }
}
