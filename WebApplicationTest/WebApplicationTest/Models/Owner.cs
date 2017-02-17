using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class Owner
    {
        public Owner()
        {
            Ferrets = new List<Ferret>();
        }

        public int ID { get; set; }
        [Required]
        public string FristName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int PostCode { get; set; }

        public ICollection<Ferret> Ferrets { get; set; }
    }
}