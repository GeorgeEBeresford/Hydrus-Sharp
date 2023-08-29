using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HydrusSharp.Models.ClientMaster
{
    [Table("namespaces")]
    public class Namespace
    {
        [Column("namespace_id")]
        public int NamespaceId { get; set; }

        [Column("namespace")]
        public string Value { get; set; }

        public virtual List<Tag> Tags { get; set; }
    }
}