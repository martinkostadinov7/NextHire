using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;

namespace Shared.DTOs.Applications
{
    public class ApplicationReadDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Education { get; set; }

        public string CoverLetter { get; set; }

        public Cv Cv { get; set; }

        public DateTime SubmittedAt { get; set; }

        public User User { get; set; }

        public Offer Offer { get; set; }

        public ApplicationReadDto(int id, string email, string firstName, string lastName, string phone, string education, string coverLetter, Cv cv, DateTime submittedAt, User user, Offer offer)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Education = education;
            CoverLetter = coverLetter;
            Cv = cv;
            SubmittedAt = submittedAt;
            User = user;
            Offer = offer;
        }
    }
}
