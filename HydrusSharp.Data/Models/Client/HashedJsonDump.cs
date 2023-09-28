using System;

namespace HydrusSharp.Data.Models.Client
{
    public class HashedJsonDump
    {
        public byte[] Hash { get; set; }

        public int DumpType { get; set; }

        public int Version { get; set; }

        public string Dump { get; set; }

        public string HashedString => BitConverter.ToString(Hash).Replace("-", "").ToLowerInvariant();
    }
}