using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Menu
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Text { get; set; }
        public string? Description { get; set; }
        public string? Links { get; set; }
        public int? Stt { get; set; }
        public int? Langid { get; set; }
        public int? TabId { get; set; }
        public int? PageId { get; set; }
        public int? Idwo { get; set; }
        public bool? Show { get; set; }
        public string? Keyword { get; set; }
    }
}
