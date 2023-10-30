using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NhaDat24h.Common.Configuration;
using NhaDat24h.CommonCode;
using NhaDat24h.DataDto.Users;
using NhaDat24h.DataDto.Video;
using NhaDat24h.Service;
using NhaDat24h.Service.Api.Users;
using NhaDat24h.Service.Api.Videos;
using System.Text;
using System.Text.RegularExpressions;
using static NhaDat24hWeb.Areas.Partner.Controllers.UploadController;

namespace NhaDat24hWeb.Areas.Partner.Controllers
{
    [Area("Partner")]
    public partial class VideoController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IVideoApiServices _videoApiServices;
        private IUsersApiServices _usersApiServices;
        private readonly IMyTypedClientServices _client;
        private ISessionManager _sessionManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public class FileResult
        {
            public bool uploaded { get; set; }
            public string fileUid { get; set; }
        }
        public VideoController(IHttpContextAccessor httpContextAccessor, IVideoApiServices videoApiServices,
            ISessionManager sessionManager, IMyTypedClientServices client, IWebHostEnvironment webHostEnvironment, IUsersApiServices userApiServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _sessionManager = sessionManager;
            _videoApiServices = videoApiServices;
            _client = client;
            _webHostEnvironment = webHostEnvironment;
            _usersApiServices = userApiServices;
        }

        [Route("video")]
        public IActionResult Index()
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin();
            var checkPermission = _usersApiServices.CheckUserPermision(new CheckPermisionParam()
            {
                IdUser = UserId.Id,
                Permission = new int[] { 702 },
            });
            if (checkPermission.Data == false)
            {
                return Json("Ban khong co quyen truy cap (No permission)");
            }
            var data = new ListVideoModel()
            {
                ListVideo = _videoApiServices.SearchVideo(new VideoSearchDataDto()
                {
                    IdCtv = null,
                    Title = "",
                    Status = null,
                    Type = null,
                    DateCreate = null,
                    Hashtag = "",
                    FullName = "",
                    pageIndex = 1,
                    pageSize = 10

                }).Data,
                PageIndex = 1,
                PageSize = 10,
                Permission = new List<int>() { 102 }
            };
            return View(data);
        }
        [Route("video/search")]
        public IActionResult SearchListVideo(string hashtag, int? idctv, byte? status, byte? type, int pageIndex, int pageSize)
        {

            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin();
            var checkPermission = _usersApiServices.CheckUserPermision(new CheckPermisionParam()
            {
                IdUser = UserId.Id,
                Permission = new int[] { 702 },
            });
            if (checkPermission.Data == false)
            {
                return Json("Ban khong co quyen truy cap (No permission)");
            }
            var data = new ListVideoModel()
            {
                ListVideo = _videoApiServices.SearchVideo(new VideoSearchDataDto()
                {
                    IdCtv = idctv,
                    Title = "",
                    Status = status,
                    Type = type,
                    DateCreate = null,
                    Hashtag = hashtag,
                    FullName = "",
                    pageIndex = pageIndex,
                    pageSize = pageSize

                }).Data,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Permission = new List<int>() { 102 }
            };
            return PartialView("~/Areas/Partner/Views/Video/ListVideoPartial.cshtml", data);
        }

        [Route("video/add")]
        public IActionResult AddVideo()
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin();
            var checkPermission = _usersApiServices.CheckUserPermision(new CheckPermisionParam()
            {
                IdUser = UserId.Id,
                Permission = new int[] { 701 },
            });
            if (checkPermission.Data == false)
            {
                return Json("Ban khong co quyen truy cap (No permission)");
            }
            var video = _videoApiServices.CheckAndGetVideo(UserId.Id).Data;
            var model = new VideoAddModel()
            {
                IdUser = UserId.Id,
                ImgAllowedExtensions = new string[] { "jpg", "jpeg", "png" },
                VideoAllowedExtensions = new string[] { "mp4", "mov" },
                Video = video,
                IsLimited = false
            };
            return View(model);
        }
        [Route("video/submit")]
        public IActionResult SubmitUpdateVideo(VideoAddModel data)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin();
            var checkPermission = _usersApiServices.CheckUserPermision(new CheckPermisionParam()
            {
                IdUser = UserId.Id,
                Permission = new int[] { 701 },
            });
            if (checkPermission.Data == false)
            {
                return Json("Ban khong co quyen truy cap (No permission)");
            }
            var videoupdate = new VideoUpdateDataDto()
            {
                Id = data.Video.Id,
                Title = data.Video.Title,
                Description = data.Video.Description,
                Detail = data.Video.Detail,
                Hashtag = data.Video.Hashtag,
                Type = data.Video.Type
            };

            var update = _videoApiServices.UpdateVideo(videoupdate);
            if (update.Data >= 0)
            {
                data.Video = _videoApiServices.CheckAndGetVideo(UserId.Id).Data;
            }

            var dataindex = new ListVideoModel()
            {
                ListVideo = _videoApiServices.SearchVideo(new VideoSearchDataDto()
                {
                    IdCtv = null,
                    Title = "",
                    Status = null,
                    Type = null,
                    DateCreate = null,
                    Hashtag = "",
                    FullName = "",
                    pageIndex = 1,
                    pageSize = 10

                }).Data,
                PageIndex = 1,
                PageSize = 10,
                Permission = new List<int>() { 102 }
            };
            return View("~/Areas/Partner/Views/Video/Index.cshtml", dataindex);
        }
        [Route("video/playlist")]
        public IActionResult Playlist()
        {
            return View(GetVideos());
        }
        [Route("video/read")]
        public IActionResult Videos_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetVideos().ToDataSourceResult(request));
        }
        [Route("video/player")]
        public IActionResult Player(string Title, string Source, string Poster)
        {
            var model = new VideoTelerik()
            {
                title = Title,
                source = Source,
                poster = Poster
            };
            return PartialView("~/Areas/Partner/Views/Video/Player.cshtml", model);
        }
        private static IEnumerable<VideoTelerik> GetVideos()
        {
            List<VideoTelerik> videos = new List<VideoTelerik>();
            return videos;
        }

        [Route("uploadfilevideo")]
        public async Task<ActionResult> UploadFile_Video(IEnumerable<IFormFile> files, string metaData, int IdUser, int IdVideo, int Style)
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(metaData));

            JsonSerializer serializer = new JsonSerializer();
            ChunkMetaData chunkData;
            using (StreamReader streamReader = new StreamReader(ms))
            {
                chunkData = (ChunkMetaData)serializer.Deserialize(streamReader, typeof(ChunkMetaData));
            }

            string path = String.Empty;
            // The Name of the Upload component is "files"
            var exten = Path.GetExtension(chunkData.FileName);
            var filename = Regex.Replace(Path.GetFileNameWithoutExtension(chunkData.FileName), "[^a-zA-Z0-9]", "-");

            //check exists;
            var urlcheck = $"/uploads/SGOland/ctv/" + IdUser.ToString() + $"/video/{IdVideo}/" + filename + exten;

            FileResult fileBlob = new FileResult();
            if (files != null)
            {
                path = Path.Combine(AppConfigs.FullPath, $"{IdUser}/video/{IdVideo}/", filename + exten);

                DirectoryInfo directory = new DirectoryInfo($"{AppConfigs.FullPath}{IdUser}/video/{IdVideo}/");

                if (!directory.Exists)
                {
                    directory.Create();
                }


                if (System.IO.File.Exists(path) && chunkData.ChunkIndex == 0)
                {
                    try
                    {
                        System.IO.File.Delete(path);
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }



                foreach (var file in files)
                {
                    AppendToFileCompress(path, file);
                }
            }


            fileBlob.uploaded = chunkData.TotalChunks - 1 <= chunkData.ChunkIndex;
            fileBlob.fileUid = chunkData.UploadUid;
            if (fileBlob.uploaded == true)
            {
                var videoupdate = new VideoUpdateUrlDto()
                {
                    Id = IdVideo,
                    Style = Style,
                    Url = urlcheck
                };
                var update = _videoApiServices.UpdateUrlVideo(videoupdate);
            }
            return Json(fileBlob);
        }
        public void AppendToFileCompress(string fullPath, IFormFile content)
        {
            try
            {
                using (FileStream stream = new FileStream(fullPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    content.CopyTo(stream);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        [Route("video/deletevideo")]
        public IActionResult DeleteVideo(int Id)
        {

            var User = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var result = _videoApiServices.DeleteVideo(Id);

            return Json("1");
        }

        [Route("video/editvideo")]
        public IActionResult Editvideo(int IdCtv, int IdVideo)
        {

            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var video = _videoApiServices.GetVideo(IdVideo).Data;
            var vb = new VideoAddModel();

            vb.IdUser = UserId;
            vb.ImgAllowedExtensions = new string[] { "jpg", "jpeg", "png" };
            vb.VideoAllowedExtensions = new string[] { "mp4", "mov" };
            vb.IsLimited = false;
            vb.Video = video;

            return View(vb);
        }

        [Route("video/changestatus")]
        [HttpGet]
        public IActionResult ChangeVideoStatus(int Id, byte Status)
        {
            var result = _videoApiServices.UpdateStatusVideo(new UpdateStatusVideoDto()
            {
                Id = Id,
                Status = Status
            });

            return Json("1");
        }

        [Route("video/edu")]
        public IActionResult Landvideoedu()
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var Video = _videoApiServices.GetVideoLandEdu(1, 4).Data;
            var ListbyType = _videoApiServices.GetListVideoByType(0, 1, 10).Data;
            Video.ListByType = ListbyType;
            Video.ListType = new List<TypeVideoDto>()
            {
                new TypeVideoDto(0,"Tất cả"),
                new TypeVideoDto(1,"Đào tạo hội nhập"),
                new TypeVideoDto(2,"Đào tạo chuyên sâu"),
                new TypeVideoDto(3,"Đào tạo phần mềm"),
            };
            var model = new ListVideoEdu()
            {
                Listvideo = Video.Listvideo,
                TypeVideo = Video.TypeVideo,
                Total = Video.Total,
                PageIndex = 1,
                PageSize = 4
            };
            return View(model);
        }

        [Route("video/Listvideoedu")]
        public IActionResult Listvideoedu(int pageIndex, int pageSize)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var Video = _videoApiServices.GetVideoLandEdu(pageIndex, pageSize).Data;
            var ListbyType = _videoApiServices.GetListVideoByType(0, 1, 10).Data;
            Video.ListByType = ListbyType;
            Video.ListType = new List<TypeVideoDto>()
            {
                new TypeVideoDto(0,"Tất cả"),
                new TypeVideoDto(1,"Đào tạo hội nhập"),
                new TypeVideoDto(2,"Đào tạo chuyên sâu"),
                new TypeVideoDto(3,"Đào tạo phần mềm"),
            };
            var model = new ListVideoEdu()
            {
                Listvideo = Video.Listvideo,
                TypeVideo = Video.TypeVideo,
                Total = Video.Total,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            return PartialView("~/Areas/Partner/Views/Video/ListvideoeduPartial.cshtml", model);
        }



        [Route("video/list-by-type")]
        public IActionResult GetListVideoByType(int IdType, int curentpage = 1)
        {
            var ListbyType = _videoApiServices.GetListVideoByType(IdType, curentpage, 3).Data;
            ListbyType.CurentPage = curentpage;
            ListbyType.Totalpage = (ListbyType.Totalidtype - 1) / 3 + 1;
            ListbyType.Type = IdType;
            return PartialView("~/Areas/Partner/Views/Video/ListByTypePartial.cshtml", ListbyType);
        }

        [Route("video/Get-List-Video-By-Type-content")]
        public IActionResult GetListVideoByType_content(int IdType, int curentpage = 1)
        {
            var ListbyType = _videoApiServices.GetListVideoByType(IdType, curentpage, 3).Data;
            ListbyType.CurentPage = curentpage;
            ListbyType.Totalpage = (ListbyType.Totalidtype - 1) / 3 + 1;
            ListbyType.Type = IdType;

            return PartialView("~/Areas/Partner/Views/Video/ListByTypePartial_content.cshtml", ListbyType);
        }


        [Route("video/show-play-model-videos")]
        public IActionResult showPlayModelVideos(int IDVideos, int IdType)
        {

            var vd = _videoApiServices.GetVideoByidVideos(IDVideos).Data;

            var model = new PlayListVideoDto();
            model.Video = vd;
            var ListbyType = _videoApiServices.GetListVideoByType(IdType, 1, 3).Data;


            //model.ListByType = ListbyType;
            model.ListType = new List<TypeVideoDto>()
            {
                new TypeVideoDto(0,"Tất cả"),
                new TypeVideoDto(1,"Đào tạo hội nhập"),
                new TypeVideoDto(2,"Đào tạo chuyên sâu"),
                new TypeVideoDto(3,"Đào tạo phần mềm"),
            };
            return PartialView("~/Areas/Partner/Views/Video/PlayListPartial.cshtml", model);
        }

        [Route("video/play-single-videos")]
        public IActionResult playSingleVideos(int IDVideo)
        {
            var model = _videoApiServices.GetVideoByidVideos(IDVideo).Data;

            return PartialView("~/Areas/Partner/Views/Video/playSingleVideos.cshtml", model);
        }
        [Route("video/count-view")]
        public void SetCountViewVideo(int idVideo)
        {
            var mode = _videoApiServices.GetVideoCoutView(idVideo).Data;
        }
    }
}