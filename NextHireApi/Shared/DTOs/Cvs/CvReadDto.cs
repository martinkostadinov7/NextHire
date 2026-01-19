using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Cvs
{
    public class CvReadDto
    {
        public int Id { get; set; }

        public User User { get; set; }

        public CvReadDto(int id, User user)
        {
            Id = id;
            User = user;
        }
    }
}
