using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HydrusSharp.Models.ClientMaster
{
    [Table("subtags")]
    public class Subtag
    {
        [Column("subtag_id")]
        public int SubtagId { get; set; }

        [Column("subtag")]
        public string Value { get;set; }

        public virtual List<Tag> Tags { get; set; }
    }
}