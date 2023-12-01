using AnimalesMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using TiendaAnimales.Models;

namespace TiendaAnimales.Controllers
{
    public class AnimalesController : Controller
    {
        private readonly HttpClient _client;

        public AnimalesController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7275/"); // URL base de la API
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Mensaje = "¡Valor pasado por viewbag!";

            List<Animal> lstAnimals = new List<Animal>();
            
            // Realizar la solicitud GET a la API
            HttpResponseMessage response = await _client.GetAsync("ObtenerTodosLosAnimales");

            if (response.IsSuccessStatusCode)
            {
                lstAnimals = await response.Content.ReadFromJsonAsync<List<Animal>>();
            }
            else
            {
                // Manejar el error si la solicitud no fue exitosa
                // Por ejemplo, mostrar un mensaje de error o manejar el flujo de la aplicación
                ViewBag.Error = "No se pudieron obtener los animales desde la API.";
            }

            HttpResponseMessage responseTipo = await _client.GetAsync("ObtenerTodosLosTiposAnimales");
            var tiposAnimales = await responseTipo.Content.ReadFromJsonAsync<List<TipoAnimal>>();

           
            Random random = new Random();
            int numeroAleatorio = random.Next(1, tiposAnimales.Count); 


            ViewBag.EjemploBlabla = "El " + tiposAnimales[numeroAleatorio].TipoDescripcion + " es tu animal de la suerte!";

            return View(lstAnimals);
        }

        public async Task<IActionResult> Crear()
        {
            await CargarComboTipoAnimal();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Animal nuevoAnimal)
        {
            if (ModelState.IsValid)
            {
                // Serializar el nuevoAnimal a JSON
                string json = JsonConvert.SerializeObject(nuevoAnimal);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realizar la solicitud POST a la API para insertar el nuevo animal
                HttpResponseMessage response = await _client.PostAsync("InsertarAnimal", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Redirigir a la acción "Index" si la inserción fue exitosa
                }
                else
                {
                    // Manejar el error si la solicitud no fue exitosa
                    // Por ejemplo, mostrar un mensaje de error o manejar el flujo de la aplicación
                    ViewBag.Error = "No se pudo insertar el nuevo animal en la API.";
                }
            }
            await CargarComboTipoAnimal();


            return View(nuevoAnimal);
        }        


        private async Task CargarComboTipoAnimal()
        {
            List<TipoAnimal> tiposAnimales =  new List<TipoAnimal>();
            // Realizar la solicitud GET a la API
            HttpResponseMessage response = await _client.GetAsync("ObtenerTodosLosTiposAnimales");

            if (response.IsSuccessStatusCode)
            {
                tiposAnimales = await response.Content.ReadFromJsonAsync<List<TipoAnimal>>(); 
            }
            else
            {
                // Manejar el error si la solicitud no fue exitosa
                // Por ejemplo, mostrar un mensaje de error o manejar el flujo de la aplicación
                ViewBag.Error = "No se pudieron obtener los animales desde la API.";
            }

            // Agregar la opción predeterminada "Seleccionar..."
            tiposAnimales.Insert(0, new TipoAnimal { IdTipoAnimal = 0, TipoDescripcion = "Seleccionar..." });

            ViewBag.TiposAnimales = new SelectList(tiposAnimales, "IdTipoAnimal", "TipoDescripcion");

        }
    }
}
