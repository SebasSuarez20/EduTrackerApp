using EduTrackerServer.CapaDTO;
using EduTrackServer.CapaBase;
using EduTrackServer.CapaDatos.Model;
using EduTrackServer.CapaDatos.Models;
using EduTrackServer.CapaDatos.Views;
using EduTrackServer.CapaLogica.Interface;
using EduTrackServer.CapaLogica.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SistemaTickets.Data;
using SistemaTickets.Repository;
using SistemaTickets.Services;
using System.Text;

namespace SistemaTickets.Extensions
{
    public static class applicationExtensions
    {

       public static void AddCorsApplication(this IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy("Dev", app =>
                {
                    app.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader().
                       AllowCredentials();
                });
            });
        }

       public static void ConfigurationConnection(this IServiceCollection service,IConfiguration configuration) {

            string strlConnection = configuration.GetConnectionString("connectionDefault");
            service.AddDbContext<appDbContext>(options => options.UseMySql(strlConnection, ServerVersion.AutoDetect(strlConnection)));
        }

       public static void AddJwtApplication(this IServiceCollection service,IConfiguration configuration) {

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false
                };
            });

        }

       public static void ComprenssionData(this IServiceCollection service)
        {
            service.AddResponseCompression(opt =>
            {
                opt.Providers.Add<BrotliCompressionProvider>();
                opt.Providers.Add<GzipCompressionProvider>();
            });
        }


       public static void FileServerApplication( WebApplication app,IConfiguration configuration)
        {

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(configuration["pathFile:path"]),
                RequestPath = "/files",
                EnableDirectoryBrowsing = true
            });
        }

        public static void ApplicationServices (this IServiceCollection service, IConfiguration configuration)
        {

            #region Service
            service.AddTransient<IUser,UserService>();
            service.AddTransient<IStudentWithSubjects, SubjectWithStudentService>();
            #endregion

            #region DbHandler
            service.AddTransient<IDbHandler<Users>, repositoryServices<Users>>();
            service.AddTransient<IDbHandler<StudentWithSubjects>, repositoryServices<StudentWithSubjects>>();
            service.AddTransient<IDbHandler<ResponseModelSp<string>>, repositoryServices<ResponseModelSp<string>>>();
            service.AddTransient<IDbHandler<TeacherDTO>, repositoryServices<TeacherDTO>>();
            service.AddTransient<IDbHandler<StudentWithSubjects>, repositoryServices<StudentWithSubjects>>();
            service.AddTransient<IDbHandler<ConsultInformationStudentView>, repositoryServices<ConsultInformationStudentView>>();
            #endregion

            #region Views
            service.AddTransient<IDbHandler<UsersAllForLoginViews>, repositoryServices<UsersAllForLoginViews>>();
            #endregion
        }


    }
}
