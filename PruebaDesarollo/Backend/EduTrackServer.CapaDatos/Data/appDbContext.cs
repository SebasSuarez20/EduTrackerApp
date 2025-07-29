using EduTrackServer.CapaDatos.Model;
using EduTrackServer.CapaDatos.Models;
using EduTrackServer.CapaDatos.Views;
using Microsoft.EntityFrameworkCore;
using EduTrackerServer.CapaDTO;

namespace SistemaTickets.Data
{
    public class appDbContext : DbContext
    {

        public appDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersAllForLoginViews> UsersAllForLoginViews { get; set; }
        public DbSet<ResponseModelSp<string>> ResponseModelSp { get; set; }
        public DbSet<TeacherDTO> TeacherDTO { get; set; }
        public DbSet<StudentWithSubjects> StudentWithSubjects { get; set; }
        public DbSet<ConsultInformationStudentView> ConsultInformationStudentView { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersAllForLoginViews>().HasNoKey();
            modelBuilder.Entity<ResponseModelSp<string>>().HasNoKey();
            modelBuilder.Entity<TeacherDTO>().HasNoKey();
            modelBuilder.Entity<ConsultInformationStudentView>().HasNoKey();
        }

    }
}
