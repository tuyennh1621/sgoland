using NhaDat24h.Common;
using NhaDat24h.DataDto.Company;
using NhaDat24h.DataDto.Ctv;
using NhaDat24h.DataDto.Video;

namespace NhaDat24h.Service.Api.Videos
{
    public interface IVideoApiServices
    {
        public ResponseBase<List<VideoDataDto>> SearchVideo(VideoSearchDataDto param);
        public ResponseBase<int> UpdateVideo(VideoUpdateDataDto param);
        public ResponseBase<Video> CheckAndGetVideo(int IdCtv);
        public ResponseBase<int> UpdateUrlVideo(VideoUpdateUrlDto param);
        public ResponseBase<string> DeleteVideo(int Id);
        public ResponseBase<Video> GetVideo(int IdVideo);

        public ResponseBase<string> UpdateStatusVideo(UpdateStatusVideoDto param);
        public ResponseBase<VideoLandEdu> GetVideoLandEdu(int pageIndex, int pageSize);
        public ResponseBase<ListByTypeVideoModel> GetListVideoByType(int idType, int pageIndex, int pageSize);

        public ResponseBase<VideoPlaySingle> GetVideoByidVideos(int idVideos);

        public ResponseBase<VideoPlaySingle> GetVideoCoutView(int IdVideo);
        


    }
}
