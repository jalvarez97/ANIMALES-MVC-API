using AnimalesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AnimalesAPI.Controllers
{
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AnimalController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("ObtenerTodosLosAnimales")]
        public List<Animal> ObtenerTodosLosAnimales()
        {
            List<Animal> animales = new List<Animal>();

            using (SqlConnection connection = 
                new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT IdAnimal, NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento FROM Animal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Animal animal = new Animal
                            {
                                IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                                NombreAnimal = reader["NombreAnimal"].ToString(),
                                Raza = reader["Raza"].ToString(),
                                RIdTipoAnimal = Convert.ToInt32(reader["RIdTipoAnimal"]),
                                FechaNacimiento = (reader["FechaNacimiento"] != DBNull.Value)
                                                    ? Convert.ToDateTime(reader["FechaNacimiento"]) : (DateTime?)null
                            };
                            animales.Add(animal);
                        }
                    }
                }
            }

            return animales;
        }

        [HttpGet]
        [Route("ObtenerAnimalPorId/{idAnimal}")]
        public Animal ObtenerAnimalPorId(int idAnimal)
        {
            Animal animal = null;

            using (SqlConnection connection = 
                new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT IdAnimal, NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento FROM Animal WHERE IdAnimal = @IdAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAnimal", idAnimal);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            animal = new Animal
                            {
                                IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                                NombreAnimal = reader["NombreAnimal"].ToString(),
                                Raza = reader["Raza"].ToString(),
                                RIdTipoAnimal = Convert.ToInt32(reader["RIdTipoAnimal"]),
                                FechaNacimiento = (reader["FechaNacimiento"] != DBNull.Value) ? Convert.ToDateTime(reader["FechaNacimiento"]) : (DateTime?)null
                            };
                        }
                    }
                }
            }

            return animal;
        }

        [HttpGet]
        [Route("ObtenerTodosLosTiposAnimales")]
        public List<TipoAnimal> ObtenerTodosLosTiposAnimales()
        {
            List<TipoAnimal> tiposAnimales = new List<TipoAnimal>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "SELECT IdTipoAnimal, TipoDescripcion FROM TipoAnimal";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TipoAnimal tipoAnimal = new TipoAnimal
                            {
                                IdTipoAnimal = Convert.ToInt32(reader["IdTipoAnimal"]),
                                TipoDescripcion = reader["TipoDescripcion"].ToString()
                            };
                            tiposAnimales.Add(tipoAnimal);
                        }
                    }
                }
            }

            return tiposAnimales;
        }


        [HttpPost]
        [Route("InsertarAnimal")]
        public void InsertarAnimal(Animal animal)
        {
            using (SqlConnection connection =
                new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string query = "INSERT INTO Animal (NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento) " +
                               "VALUES (@NombreAnimal, @Raza, @RIdTipoAnimal, @FechaNacimiento)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                    command.Parameters.AddWithValue("@Raza", animal.Raza);
                    command.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                    command.Parameters.AddWithValue("@FechaNacimiento", (object)animal.FechaNacimiento ?? DBNull.Value);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
