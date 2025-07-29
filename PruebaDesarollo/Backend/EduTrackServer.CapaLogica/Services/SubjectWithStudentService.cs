using EduTrackerServer.CapaDTO;
using EduTrackServer.CapaBase;
using EduTrackServer.CapaDatos.Model;
using EduTrackServer.CapaDatos.Views;
using EduTrackServer.CapaLogica.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EduTrackServer.CapaLogica.Services
{
    public class SubjectWithStudentService : IStudentWithSubjects
    {

        private readonly IDbHandler<ResponseModelSp<string>> _dbHandler;
        private readonly IDbHandler<TeacherDTO> _dbHandlerTeacherDTO;
        private readonly IDbHandler<StudentWithSubjects> _dbHandlerStudentSubject;
        private readonly IDbHandler<ConsultInformationStudentView> _dbHandlerConsultInformationStudentView;
        private readonly IHttpContextAccessor _Icontext;
        private int IdFkUser = -1;

        public SubjectWithStudentService(IDbHandler<ResponseModelSp<string>> dbHandler,IDbHandler<TeacherDTO> dbHandlerTeacherDTO,
            IHttpContextAccessor Icontext,IDbHandler<StudentWithSubjects> dbHandlerStudentSubject,
            IDbHandler<ConsultInformationStudentView> dbHandlerConsultInformationStudentView
            )
        {
            _dbHandler = dbHandler;
            _dbHandlerTeacherDTO = dbHandlerTeacherDTO;
            _dbHandlerStudentSubject = dbHandlerStudentSubject;
            _dbHandlerConsultInformationStudentView = dbHandlerConsultInformationStudentView;
            _Icontext = Icontext;

            IdFkUser = authorizeServices.GetIdentityFk(_Icontext);
        }

        public async Task<ResponseModel<int>> InsertAndUpdateInformationStudentWithSubject(List<CapaDatos.Model.StudentWithSubjects> items)
        {
            try
            {

                int? result = null;

                if (items.Count() >= 1 || items.Count() <= 3)
                {
                    foreach (var item in items)
                    {
                        item.IdFkStudent = IdFkUser;
                    }

                    var respSp = await _dbHandler.GetAllAsyncSp("Sp_InsertUpdateStudentsWithSubjects", new ResponseModelSp<string>() { MetaData = JsonSerializer.Serialize(items) });

                    return await Task.FromResult(
                       new ResponseModel<int>()
                       {
                           DataContent = 1,
                           Status = System.Net.HttpStatusCode.OK,
                           Message = "Sucess: Se obtuvo la informacion adecuadamente."
                       }
                      );

                }
                else
                {
                    throw new Exception("Error: Excedió los ítems permitidos. Solo se permiten 3 ítems por estudiante.");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseModel<int>> DeleteInformationForStudent(int id)
        {
            try
            {
                int result = await _dbHandlerStudentSubject.DeleteAsync(new StudentWithSubjects { IdFkStudent = IdFkUser,IdFkSubject = id});

                if(result ==  -1 ) throw new Exception("Error: no se elimino correctamente");

                return await Task.FromResult(
                   new ResponseModel<int>()
                   {
                       DataContent = result,
                       Status = System.Net.HttpStatusCode.OK,
                       Message = "Sucess: Se obtuvo la informacion adecuadamente."
                   }
                  );

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseModel<IEnumerable<TeacherDTO>>> GetInformationOfTeacher()
        {
            try
            {
                var Sp = await _dbHandlerTeacherDTO.GetCodeAsyncAll("Sp_TeacherAll");

                return await Task.FromResult(
                     new ResponseModel<IEnumerable<TeacherDTO>>()
                     {
                         DataContent = Sp,
                         Status = System.Net.HttpStatusCode.OK,
                         Message = "Sucess: Se obtuvo la informacion adecuadamente."
                     }
                    );
            }catch(Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<ResponseModel<int>> VerificationSubjectForUser()
        {
            try
            {
                IEnumerable<StudentWithSubjects> resp = (await _dbHandlerStudentSubject.GetAllAsyncForAllWithClouse(new StudentWithSubjects { IdFkStudent = IdFkUser })).ToList();
                return await Task.FromResult(
                 new ResponseModel<int>()
                 {
                     DataContent = resp.Count(),
                     Status = System.Net.HttpStatusCode.OK,
                     Message = "Sucess: Se obtuvo la informacion adecuadamente."
                 }
                );

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseModel<dynamic>> GetInformationForStudentAndAlls(string? name = null)
        {
            try
            {
                dynamic respDynamic = new ExpandoObject();

                

                var Response = await _dbHandlerConsultInformationStudentView.
                    GetAllAsyncForAllWithClouse(name != null ? new ConsultInformationStudentView { Name = name } : null);

                if(name is null)
                {
                    respDynamic.listSelect = Response.Where(s=>s.IdFkStudent == IdFkUser).ToList();
                }
                else
                {
                    respDynamic.header = Response.Where(s => s.IdFkStudent == IdFkUser).ToList();
                    respDynamic.items = Response.Where(s=> s.IdFkStudent != IdFkUser).DistinctBy(s=>s.NameStudent);
                }

                    return await Task.FromResult(
                         new ResponseModel<dynamic>()
                         {
                             DataContent = respDynamic,
                             Status = System.Net.HttpStatusCode.OK,
                             Message = "Sucess: Se obtuvo la informacion adecuadamente."
                         }
                        );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public async Task<ResponseModel<dynamic>> GetInformationForUser()
        {
            try
            {
                var resp = (await _dbHandlerConsultInformationStudentView.GetAllAsyncForAllWithClouse(new ConsultInformationStudentView { IdFkStudent = IdFkUser }));

                return await Task.FromResult(
                          new ResponseModel<dynamic>()
                          {
                              DataContent = resp.DistinctBy(s=>s.Name).ToList(),
                              Status = System.Net.HttpStatusCode.OK,
                              Message = "Sucess: Se obtuvo la informacion adecuadamente."
                          }
                         );
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
