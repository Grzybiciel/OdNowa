using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdNowa.Models
{
    public class AnimalsRequest
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int IdOwner { get; set; }

        //public int IdAnimal { get; set; }

        //public int IdProcedure { get; set; }
        public string NameProcedure { get; set; }
        public string Description { get; set; }
    }
}