using NhaDat24h.Common;
using NhaDat24h.DataAccess.Entities;
using NhaDat24h.DataDto.Company;
using NhaDat24h.DataDto.Ctv;
using NhaDat24h.DataDto.Video;
using System.Collections.Generic;

namespace NhaDat24h.Service.Api.Videos
{
    public class VideoApiServices : ApiServiceBase, IVideoApiServices
    {
		public ResponseBase<List<VideoDataDto>> SearchVideo(VideoSearchDataDto param)
        {
			var response = Post<VideoSearchDataDto,List<VideoDataDto>>("Video/search",param);
			return response;
		}
        public ResponseBase<int> UpdateVideo(VideoUpdateDataDto param)
        {
            var response = Put<VideoUpdateDataDto, int>("Video/update", param
                );
            return response;
        }
        public ResponseBase<int> UpdateUrlVideo(VideoUpdateUrlDto param)
        {
            var response = Put<VideoUpdateUrlDto, int>("Video/update-url", param
                );
            return response;
        }
        public ResponseBase<Video> CheckAndGetVideo(int IdCtv)  
        {
            var response = Get<Video>("Video/check-and-get"
                , new KeyValuePair<string, object>("IdCtv", IdCtv));
            return response;
        }


        public ResponseBase<string> DeleteVideo(int Id)
        {
            var response = Delete<string>("Video/video"

                , new KeyValuePair<string, object>("id", Id)
                );
            return response;
        }

        public ResponseBase<Video> GetVideo(int IdVideo)
        {
            var response = Get<Video>("Video/get"
                , new KeyValuePair<string, object>("Id", IdVideo));
            return response;
        }

        public ResponseBase<string> UpdateStatusVideo(UpdateStatusVideoDto param)
        {
            var response = Put<UpdateStatusVideoDto, string>("Video/update-status", param);
            return response;
        }

       
        public ResponseBase<VideoLandEdu> GetVideoLandEdu(int pageIndex, int pageSize)
        {
            var response = Get<VideoLandEdu>("Video/GetVideoLandEdu",  
                new KeyValuePair<string, object>("pageIndex", pageIndex)
                , new KeyValuePair<string, object>("pageSize", pageSize));
            return response;
        }
        public ResponseBase <ListByTypeVideoModel> GetListVideoByType(int idType, int pageIndex, int pageSize)
        {
            var response = Get<ListByTypeVideoModel>("Video/list-by-type",
                new KeyValuePair<string, object>("idType", idType),
                new KeyValuePair<string, object>("pageIndex", pageIndex)
                , new KeyValuePair<string, object>("pageSize", pageSize));
            return response;
        }

       public ResponseBase<VideoPlaySingle> GetVideoByidVideos(int idVideos)
       {
            var response = Get<VideoPlaySingle>("Video/list-by-idVideos",
              new KeyValuePair<string, object>("idVideos", idVideos));
            return response;
       }

        public ResponseBase<VideoPlaySingle> GetVideoCoutView(int IdVideo)
        {
            var response = Put<int, VideoPlaySingle>("Video/countview-video", IdVideo);
            return response;
        }
    }
}
