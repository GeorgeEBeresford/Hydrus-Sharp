using HydrusSharp.Core.Enums;

namespace HydrusSharp.Data.Models.Client
{
    public class NamedJsonDump
    {
        public int DumpTypeId { get; set; }

        public string DumpName { get; set; }

        public int Version { get; set; }

        public int Timestamp { get; set; }

        public string Dump { get;set; }

        public SerialisableType DumpType => (SerialisableType)DumpTypeId;
    }
}