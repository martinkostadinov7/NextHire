using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHire.Domain.Entities
{
    public class Application
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Education { get; set; }

        public string CoverLetter { get; set; }

        public int CvId { get; set; }
        public Cv Cv { get; set; }

        public DateTime SubmittedAt { get; set; }

        public int UserId { get; set; } 
        public User User { get; set; }

        public int OfferId { get; set; }
        public Offer Offer { get; set; }

        private Application()
        {
            
        }

        public Application(string email, string firstName, string lastName, string phone, string education, 
            string coverLetter, int cvId, Cv cv, int userId, User user, int offerId, Offer offer)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Education = education;
            CoverLetter = coverLetter;
            CvId = cvId;
            Cv = cv;
            UserId = userId;
            User = user;
            OfferId = offerId;
            Offer = offer;
            SubmittedAt = DateTime.Now;
        }
    }
}
