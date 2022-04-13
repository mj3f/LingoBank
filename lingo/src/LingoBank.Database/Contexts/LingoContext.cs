using LingoBank.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LingoBank.Database.Contexts
{
    public class LingoContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<LanguageEntity> Languages { get; set; }
        
        public DbSet<PhraseEntity> Phrases { get; set; }

        public LingoContext(DbContextOptions options) : base(options)
        {
        }
    }
}