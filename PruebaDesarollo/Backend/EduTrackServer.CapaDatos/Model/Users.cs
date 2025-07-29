using EduTrackServer.CapaBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaDatos.Models
{
    public class Users : EntityDB
    {

        [Required]
        [StringLength(100, ErrorMessage = "Minimo longitud 100 dentro del campo identification.")]
        public string? Identification {  get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Minimo longitud 255 dentro del campo password.")]
        public string? Password { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "Minimo longitud 1 dentro del campo rol.")]
        public string? Rol { get; set; }
        public bool? Enabled { get; set; }
    }
}
