using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LingoBank.Database.Entities
{
    [Table("Users")]
    public class ApplicationUser : IdentityUser
    {
        public List<LanguageEntity> Languages { get; set; }
        
        [Required]
        public string Role { get; set; }
    }
}