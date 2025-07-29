using EduTrackServer.CapaDatos.Model;
using EduTrackServer.CapaDatos.Views;
using EduTrackServer.CapaLogica.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduTrackServer.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUser _service;

        public UserController(IUser service)
        {
            _service = service;
        }

        [HttpGet("LoggedWithUser")]

        public async Task<IActionResult> LoggedWithUser(string data)
        {
            try
            {
               return Ok(await _service.GetConnectionForLoginWithUser(data));
            }
            catch (Exception ex)
            {
               return NotFound(new ResponseModel<DBNull>
               {
                   DataContent = null,
                   Error = ex.Message,
                   Status = System.Net.HttpStatusCode.NotFound
               });
            }
        }


    }
}
