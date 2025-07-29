using EduTrackServer.CapaBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaDatos.Model
{
    public class Students : EntityDB
    {
        [Required(ErrorMessage ="Error: campo requerido (Identification)")]
        public string? Identification { get; set; }
        [Required(ErrorMessage = "Error: campo requerido (Primer Nombre)")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Error: campo requerido (Segundo nombre)")]
        public string? FirstSurname { get; set; }
        [Required(ErrorMessage = "Error: campo requerido (Email)")]
        [EmailAddress]
        public string? Email { get; set; }

        public bool Enabled { get; set; }
    }
}
