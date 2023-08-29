using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HydrusSharp.Models.ClientMaster
{
    [Table("hashes")]
    public class Hash
    {
        [Key]
        [Column("hash_id")]
        public int HashId { get; set; }

        [Column("hash")]
        public byte[] HashBytes { get; set; }

        [NotMapped]
        public string HashString => BitConverter.ToString(HashBytes).Replace("-", "").ToLowerInvariant();
    }
}