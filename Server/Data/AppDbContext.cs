using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MudBlazorUICRUDApp.Shared.Models;

namespace MudBlazorUICRUDApp.Server.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
