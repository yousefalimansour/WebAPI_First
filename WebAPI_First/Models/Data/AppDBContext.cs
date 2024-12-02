using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_First.Models.Data
{
    public class AppDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Empolyee> empolyees { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options)
            :base(options) 
        {
            
        }
    }
}
