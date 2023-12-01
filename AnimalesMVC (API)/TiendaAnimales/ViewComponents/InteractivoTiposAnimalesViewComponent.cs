using Microsoft.AspNetCore.Mvc;

namespace TiendaAnimales.ViewComponents
{
    public class InteractivoTiposAnimalesViewComponent : ViewComponent
    {
        Dictionary<string, string> tiposAnimal = new Dictionary<string, string>();

        public IViewComponentResult Invoke()
        {
            Dictionary<string, string> tiposAnimal = new Dictionary<string, string>() {   
                {
                    "Peces", "Los peces son animales acuáticos que se mantienen en peceras como mascotas decorativas."
                },
                {
                    "Elefantes", "Los elefantes son mamíferos de gran tamaño con trompas largas y colmillos," +
                    " y a menudo se encuentran en manadas en hábitats salvajes."
                },
                {
                    "Jirafas", "Las jirafas son animales con cuellos largos y patrones únicos en su pelaje, " +
                    "generalmente se encuentran en zonas de sabana."
                },
                {
                    "Leones", "Los leones son grandes felinos que viven en grupos llamados manadas, son depredadores " +
                    "principales y cazan en grupo."
                },
                {
                    "Tigres", "Los tigres son felinos grandes y poderosos que tienen rayas en su pelaje y son animales " +
                    "solitarios que cazan de forma individual."
                },
                {
                    "Osos", "Los osos son mamíferos omnívoros de gran tamaño que viven en diferentes hábitats, desde " +
                    "bosques hasta regiones árticas."
                },
                {
                    "Canguros", "Los canguros son marsupiales que se caracterizan por sus saltos largos y potentes, " +
                    "y a menudo se encuentran en Australia."
                }
            };
            return View(tiposAnimal);
        }
    }
}
