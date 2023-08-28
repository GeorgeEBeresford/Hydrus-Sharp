using HydrusSharp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HydrusSharp.Models.Client
{
    [Table("json_dumps")]
    public class JsonDump
    {
        [Key]
        [Column("dump_type", Order = 0)]
        public int DumpTypeId { get; set; }

        [Column("version")]
        public int Version { get; set; }

        [Column("dump")]
        public string Dump { get; set; }

        [NotMapped]
        public SerialisableType DumpType => (SerialisableType)DumpTypeId;
    }
}