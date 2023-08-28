using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HydrusSharp.Models.Client
{
    [Table("options")]
    public class OptionCollection
    {
        [Key]
        [Column("options")]
        public string Value { get; set; }
    }
}