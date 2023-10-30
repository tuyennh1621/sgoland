using Microsoft.AspNetCore.Http;

namespace NhaDat24h.Service
{
    public interface IMyTypedClientServices
    {
        public UploadImagesResponse PostImgAndGetData(List<IFormFile> files, int width, string? Obj_Id, int type);
        public UploadImagesResponse UploadFileTestAsync(List<IFormFile> files);
        public UploadImagesResponse PostCvAndGetData(List<IFormFile> files, string? Obj_Id);
        public UploadImagesResponse PostFileREEndGetData(List<IFormFile> file, int IdRE, int IdUser, int IdType);
        public UploadImagesResponse PostFileGeneralEndGetData(List<IFormFile> files, string path);

    }
}
