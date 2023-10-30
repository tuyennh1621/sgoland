using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Song
    {
        public int SongId { get; set; }
        public string? SongName { get; set; }
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public int? SongType { get; set; }
        public int? SongSingerId { get; set; }
        public int? SongAuthorId { get; set; }
        public string? SongLyric { get; set; }
        public int? SongListenCount { get; set; }
        public DateTime? SongPublishedDate { get; set; }
        public bool? Vn { get; set; }
        public int? MemberId { get; set; }
        public int? SongCount { get; set; }
        public DateTime? Ngaysinh { get; set; }
    }
}
