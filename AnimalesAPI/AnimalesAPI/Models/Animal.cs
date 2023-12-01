namespace AnimalesAPI.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }

        public string NombreAnimal { get; set; }

        public string Raza { get; set; }

        public int RIdTipoAnimal { get; set; }

        public DateTime? FechaNacimiento { get; set; }
    }
}
