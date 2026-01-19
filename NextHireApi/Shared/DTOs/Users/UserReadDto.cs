using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Users
{
    public class UserReadDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public List<Application> Applications { get; set; } = new List<Application>();

        public UserReadDto(int id, string email, string firstName, string lastName, string phone, List<Application> applications)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Applications = applications;
        }
    }
}
