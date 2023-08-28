using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HydrusSharp.Models.Client
{
    [Table("json_dumps_hashed")]
    public class HashedJsonDump
    {
        [Column("hash")]
        public byte[] Hash { get; set; }

        [Column("dump_type")]
        public int DumpType { get; set; }

        [Column("version")]
        public int Version { get; set; }

        [Key]
        [Column("dump")]
        public string Dump { get; set; }

        [NotMapped]
        public string HashedString => BitConverter.ToString(Hash).Replace("-", "").ToLowerInvariant();
    }
}