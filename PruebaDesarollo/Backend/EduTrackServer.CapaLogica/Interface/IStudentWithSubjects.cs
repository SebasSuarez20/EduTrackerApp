using EduTrackerServer.CapaDTO;
using EduTrackServer.CapaDatos.Model;
using EduTrackServer.CapaDatos.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaLogica.Interface
{
    public interface IStudentWithSubjects
    {

        public Task<ResponseModel<int>> VerificationSubjectForUser();
        public Task<ResponseModel<int>> InsertAndUpdateInformationStudentWithSubject(List<StudentWithSubjects> items);
        public Task<ResponseModel<int>> DeleteInformationForStudent(int id);
        public Task<ResponseModel<IEnumerable<TeacherDTO>>> GetInformationOfTeacher();
        public Task<ResponseModel<dynamic>> GetInformationForStudentAndAlls(string? name = null);

        public Task<ResponseModel<dynamic>> GetInformationForUser();
    }
}
