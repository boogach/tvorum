using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class FerretDTO
    {
        public int ID { get; set; }
        public string FerretName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<string> Disease { get; set; }
    }

    public class FerretDetailsDTO
    {
        public int ID { get; set; }
        public string FerretName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Castration { get; set; }
        public bool Vaccination { get; set; }
        public bool VaccLepto { get; set; }
        public string CoatColor { get; set; }

        public int OwnerID { get; set; }
        public string OwnerName { get; set; }

        public List<Disease> Diseases { get; set; }
    }
}