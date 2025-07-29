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
    public class SubjectsAssignation : EntityDB
    {

        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Day { get; set; }

        public TimeSpan? HoursInitial { get; set; }

        public TimeSpan? HoursFinal { get; set; }

        public int? FkIdControlSubject { get; set; }


        public bool? Enabled { get; set; }

        public int? Idx { get; set; }

    }
}
