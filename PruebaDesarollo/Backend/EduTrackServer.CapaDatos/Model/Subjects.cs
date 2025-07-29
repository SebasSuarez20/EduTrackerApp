using EduTrackServer.CapaBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaDatos.Model
{
    public class Subjects : EntityDB
    {

        [Required(ErrorMessage = "Error: campo requerido (Nombre de la materia)")]
        public string? Name {  get; set; }

        [Required(ErrorMessage = "Error: campo requerido (Estado de la materia)")]
        public bool? Enabled { get; set; }
        
    }
}
