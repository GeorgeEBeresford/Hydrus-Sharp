using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HydrusSharp.Models.Client
{
    [Table("tag_parents")]
    public class TagParent
    {
        [Key]
        [Column("child_tag_id", Order = 0)]
        public int ChildTagId { get; set; }

        [Key]
        [Column("parent_tag_id", Order = 1)]
        public int ParentTagId { get; set; }
    }
}