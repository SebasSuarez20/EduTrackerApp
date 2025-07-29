using EduTrackServer.CapaBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaDatos.Model
{
    public class StudentWithSubjects : EntityDB
    {

        public int? IdFkStudent { get; set; }
        public int? IdFkSubject { get; set; }
        public int? IdFkSubjectAssignation { get; set; }


        [ForeignKey("IdFkStudent")]
        public virtual Students? Students { get; set; }

        [ForeignKey("IdFkSubject")]
        public virtual Subjects? Subjects { get; set; }
        public bool? Enabled { get; set; }

    }
}
