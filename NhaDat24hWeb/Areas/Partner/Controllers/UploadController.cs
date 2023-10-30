using Google.Cloud.Firestore;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using NhaDat24h.Common;
using NhaDat24h.Common.Configuration;
using NhaDat24h.CommonCode;
using NhaDat24h.DataAccess.Entities;
using NhaDat24h.DataDto.RealEstates;
using NhaDat24h.Service;
using NhaDat24h.Service.Api.Company;
using NhaDat24h.Service.Api.Ctv;
using NhaDat24h.Service.Api.RealEstates;
using NhaDat24h.Service.Api.Users;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.Pipes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using static Azure.Core.HttpHeader;
using static System.Net.WebRequestMethods;

namespace NhaDat24hWeb.Areas.Partner.Controllers
{
    [Area("Partner")]

	
	public partial class UploadController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IRealEstateApiServices _reApiServices;
        private readonly IMyTypedClientServices _client;
        private ISessionManager _sessionManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public class ChunkMetaData
        {
            public string UploadUid { get; set; }
            public string FileName { get; set; }
            public string RelativePath { get; set; }
            public string ContentType { get; set; }
            public long ChunkIndex { get; set; }
            public long TotalChunks { get; set; }
            public long TotalFileSize { get; set; }
        }
        
        public class FileResult
        {
            public bool uploaded { get; set; }
            public string fileUid { get; set; }
        }
        public UploadController(IHttpContextAccessor httpContextAccessor,  IRealEstateApiServices reApiServices,
            ISessionManager sessionManager, IMyTypedClientServices client, IWebHostEnvironment webHostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _sessionManager = sessionManager;
            _reApiServices = reApiServices;
            _client = client;
            _webHostEnvironment = webHostEnvironment;
        }
        #region RE

        [Route("upload-docRE")]
        public IActionResult Index(DocFileUpload model)
        {
            if (model.AllowedExtensions == null)
            {
                model = new DocFileUpload()
                {
                    AllowedExtensions = new string[] { "jpg", "jpeg", "pdf","docx", "doc", "xls", "xlsx", "zip","png", "pptx", "ppt","mp4","mov" },
                    IsLimited = false
                };
            }

            return PartialView("~/Areas/Partner/Views/Land/AddDocumentLand.cshtml",model);
        }



        public ActionResult ChunkUpload()
        {
            return View();
        }

		public byte[] Compress(byte[] data)
		{
			using (var compressedStream = new MemoryStream())
			{
				using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
				{
					zipStream.Write(data, 0, data.Length);
					zipStream.Close();
					return compressedStream.ToArray();
				}
			}
		}

		public byte[] Decompress(byte[] data)
		{
			using (var compressedStream = new MemoryStream(data))
			{
				using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
				{
					using (var resultStream = new MemoryStream())
					{
						zipStream.CopyTo(resultStream);
						return resultStream.ToArray();
					}
				}
			}
		}
		public byte[] ReadFully(Stream input)
		{
			byte[] buffer = new byte[16 * 1024];
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
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

		public void AppendToFile(string fullPath, IFormFile content)
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

      

        [Route("uploadfile")]
        public async Task<ActionResult> UploadFile(IEnumerable<IFormFile> files, string metaData, int IdUser, int IdRE, int IdType)
        {

            //if (metaData == null)
            //{
            //    return await Save(files);
            //}

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
            var urlcheck = $"/uploads/SGOland/ctv/" + IdUser.ToString() + $"/bds/{IdRE}/{IdType}/" + filename + exten;
            var check = _reApiServices.CheckDocExisting(urlcheck).Data;
            if (check) 
            {
                if (files != null)
                {
                    path = Path.Combine(AppConfigs.FullPath, $"{IdUser}/bds/{IdRE}/{IdType}/", filename + exten);

                    DirectoryInfo directory = new DirectoryInfo($"{AppConfigs.FullPath}{IdUser}/bds/{IdRE}/{IdType}/");

                    if (!directory.Exists)
                    {
                        directory.Create();
                    }

                    foreach (var file in files)
                    {
                        AppendToFileCompress(path, file);
                    }
                }

                FileResult fileBlob = new FileResult();
                fileBlob.uploaded = chunkData.TotalChunks - 1 <= chunkData.ChunkIndex;
                fileBlob.fileUid = chunkData.UploadUid;

                if (fileBlob.uploaded == true)
                {
                    var insertdoc = UploadDoc(Path.GetFileNameWithoutExtension(chunkData.FileName), filename + exten, IdUser, IdRE, IdType, exten, (decimal)chunkData.TotalFileSize);
                }

                return Json(fileBlob);
            }
            return Json("File đã tồn tại trong mục này.");


        }

        [Route("ClientremoveDeleteFiles")]
        public void ClientDeleteFiles(int fileID, string fileType, string url, int IdRE, int IdType, string fullName)
        {
            DeleteFiles(fileID, fileType, url, _sessionManager.GetLoginAdminFromSessionAdmin().Id, IdRE, IdType, fullName);
        }

        [Route("removeDeleteFiles")]
        public void DeleteFiles(int fileID,string FileType, string url, int IdUser, int IdRE, int IdType, string fullName)
        {
            string exten = "";
            string filename = "";
            
            if(!string.IsNullOrEmpty(fullName))
            {
                exten = Path.GetExtension(fullName);
                filename = Regex.Replace(Path.GetFileNameWithoutExtension(fullName.Replace("&amp;", "&")), "[^a-zA-Z0-9]", "-");
            }

            if (fileID > 0)
            {
                _reApiServices.DeleteDoc(fileID, IdUser);
            }
            else if(!string.IsNullOrEmpty(url))
            {
                _reApiServices.DeleteDocUrl(url, IdUser);
            }
            else {
                _reApiServices.DeleteDocUrl("/uploads/SGOland/ctv/"+ IdUser + "/bds/"+ IdRE + "/"+ IdType + "/"+ filename + exten, IdUser);
            }


            if (FileType!="link")
            {
                string physicalPath = "";

                if (!string.IsNullOrEmpty(url))
                {
                    physicalPath = AppConfigs.RootPath+ url;
                }
                else
                {
                    physicalPath = Path.Combine(AppConfigs.FullPath, $"{IdUser}/bds/{IdRE}/{IdType}/", filename + exten);
                }
                
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }   
        }
        [Route("removefile")]
        public ActionResult Chunk_Upload_Remove(string[] fileNames, int IdUser , int IdRE ,int IdType)
        {
            //The parameter of the Remove action must be called "fileNames"

            try
            {
                if (fileNames != null)
                {
                    foreach (var fullName in fileNames)
                    {
                        DeleteFiles(0,"","", IdUser, IdRE, IdType, fullName);
                    }

                }
                return Content("");
            }
            catch(Exception exx)
            {
                return Content(exx.Message.ToString());
            }
            
            // Return an empty string to signify success
            
        }
        [Route("save-file")]
        public async Task<ActionResult> Savefile(IEnumerable<IFormFile> files, string metaData, int IdUser, int IdRE, int IdType)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
                    var physicalPath = Path.Combine(AppConfigs.FullPath, $"{IdUser}/bds/{IdRE}/{IdType}/", fileName);

                    using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
        public ResponseBase<int> UploadDoc(string DisplayName,string fileName, int IdUser, int IdRE, int IdType, string FileType, decimal FileSize)
        {
            var urls = $"/uploads/SGOland/ctv/" + IdUser.ToString() + $"/bds/{IdRE}/{IdType}/" + fileName;

            DocumentInsertDto doc = new DocumentInsertDto()
            {
                IdType = IdType,
                Name = DisplayName,
                Url = urls,
                IdRE = IdRE,
                IdUserRequest = IdUser,
                FileType = FileType.ToLower(),
                FileSize = FileSize,
            };

            var insertdoc = _reApiServices.InsertDoc(doc);

            return insertdoc;
        }
        #endregion

    }
}