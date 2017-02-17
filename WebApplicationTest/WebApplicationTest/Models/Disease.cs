using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class Disease
    {
        public int ID { get; set; }
        public string DiseaseName { get; set; }
        public int YearOfDisease { get; set; }

        public int FerretID { get; set; }
        public Ferret Ferret { get; set; }
    }
}