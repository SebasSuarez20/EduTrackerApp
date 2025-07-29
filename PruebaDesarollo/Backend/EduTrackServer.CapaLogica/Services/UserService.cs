

using EduTrackServer.CapaBase;
using EduTrackServer.CapaBase.Utils;
using EduTrackServer.CapaDatos.Model;
using EduTrackServer.CapaDatos.Models;
using EduTrackServer.CapaDatos.Views;
using EduTrackServer.CapaLogica.Interface;
using Microsoft.Extensions.Configuration;
using System.Dynamic;
using System.Text.Json;
using System.Web;

namespace SistemaTickets.Services
{
    public class UserService : IUser
    {

        private readonly IDbHandler<UsersAllForLoginViews> _dbHandlerUser;
        private readonly IConfiguration _config;
       

        public UserService(IDbHandler<UsersAllForLoginViews> dbHandlerUser,IConfiguration config) {
        
             _dbHandlerUser = dbHandlerUser;
            _config = config;
            GenerateClassActive.KEY_JWT = _config.GetSection("JWT:Key")?.Value ?? "";
            GenerateClassActive.KEY_AES = _config.GetSection("AES:Key")?.Value ?? "";
            GenerateClassActive.KEY_IV = _config.GetSection("AES:Iv")?.Value ?? "";
      }

        public async Task<ResponseModel<string>> GetConnectionForLoginWithUser(string model)
        {
            try
            {
                dynamic response = new ExpandoObject();

                string decrypted = GenerateClassActive.Decrypt(model);

                UsersAllForLoginViews descrypt = JsonSerializer.Deserialize<UsersAllForLoginViews>(decrypted) ?? new UsersAllForLoginViews();

                IEnumerable<UsersAllForLoginViews> result = await _dbHandlerUser.GetAllAsyncForAllWithClouse(new UsersAllForLoginViews { Identification = descrypt.Identification, Password = descrypt.Password });

                if (result.Any())
                {
                    response.id = result.FirstOrDefault()?.Idcontrol;
                    response.identification = result.FirstOrDefault()?.Identification;
                    response.rol = result.FirstOrDefault()?.Rol;
                    response.token = GenerateClassActive.GenerateToken(result.FirstOrDefault()?.Rol.ToString() ?? "", result.FirstOrDefault()?.Identification ?? "", result.FirstOrDefault()?.Idcontrol!.ToString());


                    return await Task.FromResult(new ResponseModel<string>
                    {
                        DataContent = GenerateClassActive.Encrypt(JsonSerializer.Serialize(response)),
                        Status = System.Net.HttpStatusCode.OK,
                        Message = "Se creo correctamente la informacion"
                    });
                }

                throw new Exception();
            }catch(Exception ex)
            {
                throw new Exception("Error: no se encontro informacion relacionada con el usuario.");
            }
        }
    }
}
