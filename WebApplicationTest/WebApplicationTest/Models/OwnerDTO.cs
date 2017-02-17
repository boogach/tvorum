using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class OwnerDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<Ferret> FerretName { get; set; }
    }

    public class OwnerDetailsDTO
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int PostCode { get; set; }
        public List<Ferret> Ferrets { get; set; }
    }
}