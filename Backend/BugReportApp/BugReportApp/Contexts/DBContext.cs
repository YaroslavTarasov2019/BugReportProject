using Microsoft.EntityFrameworkCore;
using BugReportApp.ModelDB;

namespace BugReportApp.Contexts
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }


        public DbSet<UserData> userData { get; set; }
        public DbSet<Project> project { get; set; }
        public DbSet<BugReport> bugReport { get; set; }
        public DbSet<UserProject> userProject { get; set; }
        public DbSet<BugReportUpdateHistory> bugReportUpdateHistory { get; set; }
        public DbSet<TestersTask> testersTask { get; set; }
        public DbSet<FileData> fileData { get; set; }

     /*   protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
   
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=YAROSLAV\\MSSQLSERVER01;Initial Catalog=BugReport;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }*/

    }
}

