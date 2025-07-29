using EduTrackServer.CapaLogica;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaDatos.Views
{
    public class UsersAllForLoginViews
    {
        [Required]
        public int? Idcontrol { get; set; }

        [Required(ErrorMessage ="Error: Campo requerido Identificacion.")]
        [StringLength(100,ErrorMessage ="Error: la longitud max de identificacion es 100 (Identificacion)")]
        public string? Identification {  get; set; }
        [Required(ErrorMessage = "Error: Campo requerido Identificacion.")]
        [StringLength(255, ErrorMessage = "Error: la longitud max de identificacion es 255 (Password)")]
        public string? Password { get; set; }
        public string? Rol { get; set; }

    }
}
