using HydrusSharp.Models.ClientMaster;

namespace HydrusSharp.Models.ViewModel
{
    public class FileInfoViewModel
    {
        public int HashId { get; set; }
        public long Size { get; set; }
        public string MimeType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int? Duration { get; set; }
        public int? FrameCount { get; set; }
        public bool HasAudio { get; set; }
        public TagViewModel[] Tags { get; set; }
    }
}