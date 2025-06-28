using DumpApplication.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DumpApplication.WebApi.Data
{                                
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> op) : base(op) 
        {
            
        }


        public DbSet<EmployeeData> EmployeeDatas { get; set; }
                   

    }
}
