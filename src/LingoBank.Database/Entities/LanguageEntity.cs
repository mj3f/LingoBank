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
        
        public ICollection<PhraseEntity> Phrases { get; set; }
    }
}