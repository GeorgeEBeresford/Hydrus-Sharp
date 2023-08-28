using HydrusSharp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HydrusSharp.Models.Client
{
    [Table("json_dumps_named")]
    public class NamedJsonDump
    {
        [Key]
        [Column("dump_type", Order = 0)]
        public int DumpTypeId { get; set; }

        [Column("dump_name")]
        public string DumpName { get; set; }

        [Column("version")]
        public int Version { get; set; }

        [Key]
        [Column("timestamp", Order = 1)]
        public int Timestamp { get; set; }

        [Column("dump")]
        public string Dump { get; set; }

        [NotMapped]
        public SerialisableType DumpType => (SerialisableType)DumpTypeId;
    }
}