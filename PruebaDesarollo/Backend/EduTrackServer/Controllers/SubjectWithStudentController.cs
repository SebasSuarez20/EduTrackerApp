using EduTrackServer.CapaDatos.Model;
using EduTrackServer.CapaLogica.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduTrackServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectWithStudentController : Controller
    {

        private readonly IStudentWithSubjects _service;

        public SubjectWithStudentController(IStudentWithSubjects service)
        {
            _service = service;
        }

        [HttpPost("InsertInformationStudentWithSubject")]

        public async Task<IActionResult> InsertInformationStudentWithSubject([FromBody] List<StudentWithSubjects> items)
        {
            try
            {
                return Ok(await _service.InsertAndUpdateInformationStudentWithSubject(items));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<DBNull>
                {
                     Error = ex.Message,
                     Status = System.Net.HttpStatusCode.BadRequest
                });
            }
        }

        [HttpGet("GetAllInformationTeacher")]

        public async Task<IActionResult> GetAllInformationTeacher()
        {
            try
            {
                return Ok(await _service.GetInformationOfTeacher());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<DBNull>
                {
                    Error = ex.Message,
                    Status = System.Net.HttpStatusCode.BadRequest
                });
            }
        }


        [HttpDelete("DeleteInformationStudentWithSubject")]

        public async Task<IActionResult> DeleteInformationStudentWithSubject(int id)
        {
            try
            {
                return Ok(await _service.DeleteInformationForStudent(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<DBNull>
                {
                    Error = ex.Message,
                    Status = System.Net.HttpStatusCode.BadRequest
                });
            }
        }



        [HttpGet("VerificationSubjectForUser")]

        public async Task<IActionResult> VerificationSubjectForUser()
        {
            try
            {
                return Ok(await _service.VerificationSubjectForUser());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<DBNull>
                {
                    Error = ex.Message,
                    Status = System.Net.HttpStatusCode.BadRequest
                });
            }
        }

        [HttpGet("GetInformationForStudentAndAlls")]

        public async Task<IActionResult> GetInformationForStudentAndAlls(string? name = null)
        {
            try
            {
                return Ok(await _service.GetInformationForStudentAndAlls(name));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<DBNull>
                {
                    Error = ex.Message,
                    Status = System.Net.HttpStatusCode.BadRequest
                });
            }
        }

        [HttpGet("GetInformationUser")]

        public async Task<IActionResult> GetInformationUser()
        {
            try
            {
                return Ok(await _service.GetInformationForUser());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<DBNull>
                {
                    Error = ex.Message,
                    Status = System.Net.HttpStatusCode.BadRequest
                });
            }
        }

    }
}
