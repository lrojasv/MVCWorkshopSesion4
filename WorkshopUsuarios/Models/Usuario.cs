using System.ComponentModel.DataAnnotations;

namespace WorkshopUsuarios.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(20,ErrorMessage = "El nombre no debe exceder a los 20 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        [StringLength(20, ErrorMessage = "El apellido paterno no debe exceder a los 20 caracteres.")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El apellido materno es obligatorio.")]
        [StringLength(20, ErrorMessage = "El apellido materno no debe exceder a los 20 caracteres.")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria.")]
        [Range(1,120,ErrorMessage = "La edad no puede ser menor de 1 y mayor de 120.")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$",ErrorMessage = "El email ingresado es incorrecto.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El login es obligatorio.")]
        [StringLength(15, ErrorMessage = "El login no debe exceder a los 15 caracteres.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "El password es obligatorio.")]
        [StringLength(15, ErrorMessage = "El nombre no debe exceder a los 15 caracteres.")]
        public string Password { get; set; }

    }
}