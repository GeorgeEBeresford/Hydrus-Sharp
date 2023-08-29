using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HydrusSharp.Models.ClientMappings
{
    [Table("current_mappings_10")]
    public class CurrentMappings10 : ICurrentMapping
    {
        [Key]
        [Column("tag_id", Order = 0)]
        public int TagId { get; set; }

        [Key]
        [Column("hash_id", Order = 1)]
        public int HashId { get; set; }
    }
}