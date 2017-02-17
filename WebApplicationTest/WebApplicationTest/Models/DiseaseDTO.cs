using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class DiseaseDTO
    {
        public int Id { get; set; }
        public string DiseaseName { get; set; }
        public string Ferret { get; set; }
    }

    public class DiseaseDetailDTO
    {
        public int ID { get; set; }
        public string DiseaseName { get; set; }
        public int FerretId { get; set; }
        public string Ferret { get; set; }
    }
}