using Microsoft.EntityFrameworkCore;
namespace Hogwarts.Models
{
    public class SolicitudContext : DbContext
    { 
        public SolicitudContext(DbContextOptions<SolicitudContext> options):base(options)
        {
        }
        public DbSet<Solicitud> Solicitud { get; set; }
    }
}