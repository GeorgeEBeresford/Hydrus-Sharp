using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HydrusSharp.Models.ClientMaster
{
    [Table("tags")]
    public class Tag
    {
        [Key]
        [Column("tag_id")]
        public int TagId { get; set; }

        [Column("namespace_id")]
        public int NamespaceId { get; set; }

        [Column("subtag_id")]
        public int SubtagId { get; set; }

        [ForeignKey(nameof(NamespaceId))]
        public virtual Namespace Namespace { get; set; }

        [ForeignKey(nameof(SubtagId))]
        public virtual Subtag Subtag { get; set; }
    }
}