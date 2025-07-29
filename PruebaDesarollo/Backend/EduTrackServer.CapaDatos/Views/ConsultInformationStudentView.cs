using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaDatos.Views
{
    public class ConsultInformationStudentView
    {

         public int? IdxSubjectStudent { get; set; }
         public int? IdxSubjects { get; set; }
         public string? Name { get; set; }
         public int? IdxAssignation { get; set; }
         public int? IdFkStudent { get; set; }

         public TimeSpan? HoursInitial { get; set; } 
         public TimeSpan? HoursFinal { get; set; }
         public string? Day { get; set; }

         public int? Idx { get; set; }

        public string? NameStudent { get; set; }

         public bool? Enabled { get; set; }
    }
}
