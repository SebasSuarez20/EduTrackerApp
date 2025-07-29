using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaDatos.Model
{
    internal class SubjectsWithTeacher
    {

        [Required]
        public int IdFkTeacher { get; set; }
        public int IdFkSubject { get; set; }

        [ForeignKey("IdFkTeacher")]
        public virtual Teachers Teachers { get; set; }

        [ForeignKey("IdFkSubject")]
        public virtual Subjects Subjects { get; set; }
        public bool? Enabled { get; set; }
    }
}
