using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HydrusSharp.Models.ClientMappings
{
    [Table("current_mappings_9")]
    public class CurrentMappings9 : ICurrentMapping
    {
        [Key]
        [Column("tag_id", Order = 0)]
        public int TagId { get; set; }

        [Key]
        [Column("hash_id", Order = 1)]
        public int HashId { get; set; }
    }
}