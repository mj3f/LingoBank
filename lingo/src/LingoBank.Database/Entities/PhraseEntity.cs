using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LingoBank.Database.Enums;

namespace LingoBank.Database.Entities
{
    [Table("Phrases")]
    public sealed class PhraseEntity
    {
        [Key]
        public string Id { get; set; }
        
        [ForeignKey("Languages")]
        public string LanguageId { get; set; }
        
        [Required]
        public string SourceLanguage { get; set; }
        
        [Required]
        public string TargetLanguage { get; set; }
        
        [Required]
        public string Text { get; set; }
        
        [Required]
        public string Translation { get; set; }
        
        public string Description { get; set; }
        
        [Required]
        public Category Category { get; set; }
    }
}