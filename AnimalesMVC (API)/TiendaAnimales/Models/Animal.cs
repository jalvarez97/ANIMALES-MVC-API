using System.ComponentModel.DataAnnotations;

namespace TiendaAnimales.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }

        [Required(ErrorMessage = "El campo nombre animal es obligatorio.")]
        [Display(Name = "Nombre de animal")]
        [StringLength(50, ErrorMessage = "El nombre de animal introducido no puede exceder los 50 caracteres.")]
        public string NombreAnimal { get; set; }

       
        [StringLength(50, ErrorMessage = "El campo Raza no puede exceder los 50 caracteres.")]
        public string Raza { get; set; }

        [Required(ErrorMessage = "Es obligatorio seleccionar un tipo de animal.")]
        [Display(Name = "Tipo de animal")]
        [Range(1, int.MaxValue, ErrorMessage = "Se debe seleccionar un tipo de animal.")]
        public int RIdTipoAnimal { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }
    }
}
