using Microsoft.EntityFrameworkCore;
using PicWithFile.Entities;

namespace PicWithFile.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
    }
}
