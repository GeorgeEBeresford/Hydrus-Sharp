using HydrusSharp.Core.Enums;

namespace HydrusSharp.Data.Models.Client
{
    public class FileInfo
    {
        public int HashId { get; set; }

        public long Size { get; set; }

        public MimeType MimeType { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public int? Duration { get; set; }

        public int? FrameCount { get; set; }

        public bool HasAudio { get; set; }

        public int? WordCount { get; set; }
    }
}