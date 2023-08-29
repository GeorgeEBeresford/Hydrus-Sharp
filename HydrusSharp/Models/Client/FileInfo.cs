using HydrusSharp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HydrusSharp.Models.Client
{
    [Table("files_info")]
    public class FileInfo
    {
        [Key]
        [Column("hash_id")]
        public int HashId { get; set; }

        [Column("size")]
        public long Size { get; set; }

        [Column("mime")]
        public MimeType MimeType { get; set; }

        [Column("width")]
        public int Width { get; set; }

        [Column("height")]
        public int Height { get; set; }

        [Column("duration")]
        public int? Duration { get; set; }

        [Column("num_frames")]
        public int? FrameCount { get; set; }

        [Column("has_audio")]
        public bool HasAudio { get; set; }

        [Column("num_words")]
        public int? WordCount { get; set; }
    }
}