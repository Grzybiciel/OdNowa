using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdNowa.Models;
using OdNowa.Services;

namespace OdNowa.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class CosTamController : ControllerBase
    {
        IDbService service;
        public CosTamController(IDbService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetAnimals(string sortBy)
        {
            var sort1 = $"{sortBy}";
            var sort = sort1.ToLower();
            if (sort == "")
            {
                sort = "AdmissionDate";
            }
            else if (sort != "admissiondate" & sort != "name" & sort != "type" & sort != "lastname")
            {
                return BadRequest("Niepoprawny parametr");
            }

            var result = service.PobierzDaneZwierzat(sort);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult InsertAnimal(AnimalsRequest request)
        {
            if (request.AdmissionDate == null ||
                request.Description == null ||
                //request.IdAnimal == null || 
                //request.IdOwner == null || 
                //request.IdProcedure == null ||
                request.Name == null ||
                request.NameProcedure == null ||
                request.Type == null)
            {
                return BadRequest("Nie podano wszystkich danych!");
            }

            service.DodajZwierze(request);

            return Ok("Zwierze dodano!");
        }
    }
}
/*     [HttpGet("{sortBy}")]//poprzez URL
public IActionResult GetAnimals(string sortBy)
{
    var sort1 = $"{sortBy}";
    var sort = sort1.ToLower();
    if (sort == "")
    {
        sort = "AdmissionDate";
    }
    else if (sort != "admissiondate" & sort != "name" & sort != "type" & sort != "lastname")
    {
        return BadRequest("Niepoprawny parametr");
    }

    List<Object> listQ = new List<Object>();
    using (var client = new SqlConnection("Data Source = db-mssql; Initial Catalog = s18445; Integrated Security = True"))
    using (var com = new SqlCommand())
    {
        com.Connection = client;
        com.CommandText = $"SELECT Name, Type, AdmissionDate, LastName FROM Animal, Owner WHERE Animal.IdOwner = Owner.IdOwner ORDER BY {sort}";
        client.Open();
        var dr = com.ExecuteReader();
        while (dr.Read())
        {
            var animal = new
            {
                Name = dr["Name"].ToString(),
                Type = dr["Type"].ToString(),
                AdmissionDate = DateTime.Parse(dr["AdmissionDate"].ToString()),
                LastName = dr["LastName"].ToString()
            };
            listQ.Add(animal);
        }
    }
    if (listQ.Count != 0)
    {
        return Ok(listQ);
    }
    else
    {
        return NotFound("Animal not Found");
    }

}
*/
