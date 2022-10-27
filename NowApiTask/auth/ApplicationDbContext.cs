using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static NowApiTask.Auth.ApplicationDbContext;

namespace NowApiTask.Auth
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany
             (e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
 
        public class ApplicationUser : IdentityUser
        {
     
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string device { get; set; }
            public string ipaddress { get; set; }
            
        }


    }
   
   
}
