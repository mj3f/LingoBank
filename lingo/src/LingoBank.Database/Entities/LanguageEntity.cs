using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LingoBank.Database.Entities
{
    [Table("Languages")]
    public class LanguageEntity
    {
        [Key]
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set;}
        
        [ForeignKey("Users")]
        public string UserId { get; set; }
        
        public ICollection<PhraseEntity> Phrases { get; set; }
    }
}