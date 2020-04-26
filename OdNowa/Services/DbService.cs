using Microsoft.AspNetCore.Mvc;
using OdNowa.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OdNowa.Services
{
    public class DbService : IDbService
    {
        public void DodajZwierze(AnimalsRequest request)
        {

            using (var client = new SqlConnection("Data Source = db-mssql; Initial Catalog = s18445; Integrated Security = True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                client.Open();

                com.CommandText = "Insert into Animal (Name, Type, AdmissionDate, IdOwner) " +
                    "Values (@Name, @Type, @AdmissionDate, @IdOwner)";
                //com.Parameters.AddWithValue("IdAnimal", request.IdAnimal);
                com.Parameters.AddWithValue("Name", request.Name);
                com.Parameters.AddWithValue("Type", request.Type);
                com.Parameters.AddWithValue("AdmissionDate", request.AdmissionDate);
                com.Parameters.AddWithValue("IdOwner", request.IdOwner);

                var dr = com.ExecuteReader();

                com.CommandText = "Insert into Procedure ( Name, Description)" +
                    "Values ( @NameProcedure, @Description)";
                //com.Parameters.AddWithValue("IdProcedure", request.IdProcedure);
                //com.Parameters.AddWithValue("NameProcedure", request.NameProcedure);
                //com.Parameters.AddWithValue("Description", request.Description);

                //var fr = com.ExecuteReader();
            }

        }

        public List<AnimalsResponse> PobierzDaneZwierzat(string sortBy)
        {
            List<AnimalsResponse> list = new List<AnimalsResponse>();

            using (var client = new SqlConnection("Data Source = db-mssql; Initial Catalog = s18445; Integrated Security = True"))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                client.Open();
                //usunalem sortowanie
                com.CommandText = $"SELECT Name, Type, AdmissionDate, LastName FROM Animal, Owner WHERE Animal.IdOwner = Owner.IdOwner ORDER By {sortBy}";


                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var animal = new AnimalsResponse();
                    {
                        animal.Name = dr["Name"].ToString();
                        animal.Type = dr["Type"].ToString();
                        animal.AdmissionDate = DateTime.Parse(dr["AdmissionDate"].ToString());
                        animal.LastName = dr["LastName"].ToString();
                    };
                    list.Add(animal);
                }
            }

            return list;
        }

    }


}
