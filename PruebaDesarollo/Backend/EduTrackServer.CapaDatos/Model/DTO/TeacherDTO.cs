using System.ComponentModel.DataAnnotations;

namespace EduTrackerServer.CapaDTO
{
    public class TeacherDTO
    {

        [Required]
        public int? IdxSubjectAssignation { get; set; }

        [Required]
        public int? IdxSubject { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Day { get; set; }
        [Required]
        public TimeSpan? HoursInitial { get; set; }
        [Required]
        public TimeSpan? HoursFinal { get; set; }
        [Required]
        public string? NameTeacher { get; set; }
        [Required]
        public int? Idx { get; set; }



    }
}
