using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class Ferret
    {
        public int ID { get; set; }
        public string FerretName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public bool Castration { get; set; }
        public bool Vaccination { get; set; }
        public bool VaccLepto { get; set; }
        public string CoatColor { get; set; }

        public int OwnerID { get; set; }
        public Owner Owner { get; set; }

        public ICollection<Disease> Diseases { get; set; }
    }
}