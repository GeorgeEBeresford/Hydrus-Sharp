using HydrusSharp.Core.Enums;

namespace HydrusSharp.Data.Models.Client
{
    public class JsonDump
    {
        public int DumpTypeId { get; set; }

        public int Version { get; set; }

        public string Dump { get; set; }

        public SerialisableType DumpType => (SerialisableType)DumpTypeId;
    }
}