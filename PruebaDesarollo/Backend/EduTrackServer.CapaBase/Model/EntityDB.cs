using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaBase.Model
{
    public abstract class EntityDB
    {
        [Key]
        public int? Idcontrol { get; set; }
        [StringLength(50,ErrorMessage ="Minimo longitud dentro del campo username")]
        public string? Username { get; set; }
        public DateTime? Entrydate { get; set; }
    }
}
