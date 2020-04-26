using Microsoft.AspNetCore.Mvc;
using OdNowa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdNowa.Services
{
    public interface IDbService
    {
        public List<AnimalsResponse> PobierzDaneZwierzat(string sortBy);

        public void DodajZwierze(AnimalsRequest request);
    }
}