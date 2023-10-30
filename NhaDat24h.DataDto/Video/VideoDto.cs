namespace NhaDat24h.DataDto.Video
{
    public class VideoTelerik
    {
        public string? title { get; set; }
        public string? source { get; set; }
        public string? poster { get; set; }
    }
    public class VideoDataDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public string? UrlVideo { get; set; }
        public string? UrlImage { get; set; }
        public int IdCtv { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string? Hashtag { get; set; }
        public string? FullName { get; set; }
        public int CountView { get; set; }
        public int Total { get; set; }



        public bool Selected(byte value)
        {
            if (value == Status)
                return true;
            else
                return false;
        }

        public string Nametype()
        {
            if (Type == 1)
                return "Đào tạo hội nhập";

            if (Type == 2)
                return "Đào tạo chuyên sâu";

            if (Type == 3)
                return "Đào tạo Phần mềm";

            return "";
        }

        public DateTime? LastUpdate { get; set; }
    }
    public class Video
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public string? UrlVideo { get; set; }
        public string? UrlImage { get; set; }
        public int IdCtv { get; set; }
        public int CountView { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
        public string? Hashtag { get; set; }
        public string? FullName { get; set; }

        public bool Selected(byte value)
        {
            if (value == Type)
                return true;
            else
                return false;
        }


    }
    public class ListVideoModel
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public List<int> Permission { get; set; }
        public List<VideoDataDto> ListVideo { get; set; }
    }
    public class VideoAddModel
    {
        public int IdUser { get; set; }
        public string[] ImgAllowedExtensions { get; set; }
        public string[] VideoAllowedExtensions { get; set; }
        public Video Video { get; set; }
        public bool IsLimited { get; set; }
    }
    public class VideoUpdateDataDto
    {
        public int Id { get; set; }
        public string? Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public byte? Type { get; set; }
        public string? Hashtag { get; set; }

        public string? UrlVideo { get; set; }
        public string? UrlImage { get; set; }
    }
    public class VideoSearchDataDto
    {

        public string? Title { get; set; }
        public int? IdCtv { get; set; }
        public byte? Status { get; set; }
        public byte? Type { get; set; }
        public DateTime? DateCreate { get; set; }
        public string? Hashtag { get; set; }
        public string? FullName { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
    public class VideoUpdateUrlDto
    {
        public int Id { get; set; }
        public int Style { get; set; }
        public string? Url { get; set; }
    }

    public class VideoEditDto
    {
        public int Id { get; set; }
        public string? Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Detail { get; set; }
        public byte? Type { get; set; }
        public string? Hashtag { get; set; }
    }
    public class UpdateStatusVideoDto
    {
        public int Id { get; set; }
        public byte Status { get; set; }
    }

    public class VideoLandEduDataDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public byte? Type { get; set; }
        public string? Detail { get; set; }
        public string? UrlVideo { get; set; }
        public string? UrlImage { get; set; }
        public DateTime? DateCreate { get; set; }
        public int CountView { get; set; }
        public string? FullName { get; set; }
        public DateTime? LastUpdate { get; set; }

    }

    public class VideoLandEdu
    {
        public Dictionary<string, int>? TypeVideo { get; set; }
        public List<VideoLandEduDataDto>? Listvideo { get; set; }
        public ListByTypeVideoModel ListByType { get; set; }
        public List<TypeVideoDto>? ListType { get; set; }
        public int Total { get; set; }
    }

    public class ListVideoEdu
    {
        public Dictionary<string, int>? TypeVideo { get; set; }
        public List<VideoLandEduDataDto>? Listvideo { get; set; }
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

    }

    public class PlayListVideoDto
    {
        public int? idVideos { get; set; }
        public VideoPlaySingle Video { get; set; }
        public List<VideoInPlayList>? ListByType { get; set; }
        public List<TypeVideoDto>? ListType { get; set; }
    }
    public class ListByTypeVideoModel
    {
        public int Totalidtype { get; set; }
        public int CurentPage { get; set; }
        public int Type { get; set; }
        public List<VideoInPlayList> PlayList { get; set; }
        public int Totalpage { get; set; }
    }

    public class Modelnew
    {
        public ListByTypeVideoModel ListVideoModel { get; set; }
    }
   
    public class VideoInPlayList
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string UrlImage { get; set; }
        public string UrlVideo { get; set; }
        public string FullName { get; set; }
        public DateTime? DateCreate { get; set; }
        public int CountView { get; set; }
        public string? Detail { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
    public class TypeVideoDto
    {
        public TypeVideoDto(int idType, string nameType)
        {
            IdType = idType;
            NameType = nameType;
        }

        public int IdType { get; set; }
        public string NameType { get; set; }
    }
    public class VideoPlaySingle
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string UrlVideo { get; set; } = null!;
        public string UrlVideoCached { get; set; } = null!;
        public string UrlImage { get; set; } = null!;
        public int IdCtv { get; set; }
        public int CountView { get; set; }
        public DateTime DateCreate { get; set; }
        public byte Type { get; set; }
        public string? Hashtag { get; set; }
        public string? Detail { get; set; }
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public string? Position { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
