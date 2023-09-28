using System;

namespace HydrusSharp.Data.Models.ClientMaster
{
    public class Hash
    {
        public int HashId { get; set; }

        public byte[] HashBytes { get; set; }

        public string HashString => BitConverter.ToString(HashBytes).Replace("-", "").ToLowerInvariant();
    }
}