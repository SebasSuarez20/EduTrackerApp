using EduTrackServer.CapaDatos.Model;
using EduTrackServer.CapaDatos.Models;
using EduTrackServer.CapaDatos.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTrackServer.CapaLogica.Interface
{
    public interface IUser
    {

        public Task<ResponseModel<string>> GetConnectionForLoginWithUser(string model);

    }
}
