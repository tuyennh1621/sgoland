using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace NhaDat24h.Service
{
    public class UploadImagesResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public string data { get; set; }
        public string size { get; set; }
        public string type { get; set; }

    }


    public class MyTypedClientServices : IMyTypedClientServices
    {
        public HttpClient Client { get; set; }
        private IHttpClientFactory _client { get; }
        public MyTypedClientServices(HttpClient client, IHttpClientFactory _client)
        {
            this._client = _client;
            client.BaseAddress = new Uri("https://localhost:7247");
            this.Client = client;
        }

        public UploadImagesResponse PostImgAndGetData(List<IFormFile> files, int width, string? Obj_Id, int type)
        {
            var httpClient = _client.CreateClient();
            var content = new MultipartFormDataContent();

            foreach (var file in files)
            {
                if (file.Length <= 0)
                    return null;

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                content.Add(new StreamContent(file.OpenReadStream())
                {
                    Headers =
                    {

                        ContentLength = file.Length,
                        ContentType = new MediaTypeHeaderValue(file.ContentType)
                    }
                }, "File", fileName);
            }
            HttpResponseMessage response = new HttpResponseMessage();

            //response = httpClient.PostAsync("https://localhost:7247/api/UploadFile/UploadFileHPLandAsync" + $"?width={width}"
            //                                            + $"&Obj_Id={Obj_Id}" + $"&type={type}", content).Result;

            response = httpClient.PostAsync("https://cdn.realtech.com.vn/api/UploadFile/UploadFileHPLand" + $"?width={width}"
                                                        + $"&Obj_Id={Obj_Id}" + $"&type={type}", content).Result;

            var json = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<UploadImagesResponse>(json);

            return obj;
        }

        public UploadImagesResponse UploadFileTestAsync(List<IFormFile> files)
        {
            var httpClient = _client.CreateClient();
            var content = new MultipartFormDataContent();

            foreach (var file in files)
            {
                if (file.Length <= 0)
                    return null;

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                content.Add(new StreamContent(file.OpenReadStream())
                {
                    Headers =
                    {
                        ContentLength = file.Length,
                        ContentType = new MediaTypeHeaderValue(file.ContentType)
                    }
                }, "File", fileName);
            }
            HttpResponseMessage response = new HttpResponseMessage();

            response = httpClient.PostAsync("https://cdn.realtech.com.vn/api/UploadFile/UploadFileTest", content).Result;

            var json = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<UploadImagesResponse>(json);

            return obj;
        }

        public UploadImagesResponse PostCvAndGetData(List<IFormFile> files, string? Obj_Id)
        {
            var httpClient = _client.CreateClient();
            var content = new MultipartFormDataContent();
            foreach (var file in files)
            {
                if (file== null || file.Length <= 0)
                    return null;
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                content.Add(new StreamContent(file.OpenReadStream())
                {
                    Headers =
                        {
                            ContentLength = file.Length,
                            ContentType = new MediaTypeHeaderValue(file.ContentType)
                        }
                }, "File", fileName);
            }
            HttpResponseMessage response = new HttpResponseMessage();
            //response = httpClient.PostAsync("https://localhost:7247/api/UploadFile/UploadFileHPLandAsync" + $"?width={width}"
            //                                            + $"&Obj_Id={Obj_Id}" + $"&type={type}", content).Result;

            response = httpClient.PostAsync("https://cdn.realtech.com.vn/api/UploadFile/UploadCvHPLand"
                    + $"?Obj_Id={Obj_Id}", content).Result;

            var json = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<UploadImagesResponse>(json);
  
            return obj;
        }

        public UploadImagesResponse PostFileREEndGetData(List<IFormFile> files, int IdRE, int IdUser, int IdType)
            {
                var httpClient = _client.CreateClient();
                var content = new MultipartFormDataContent();

                foreach (var file in files)
                {
                    if (file== null || file.Length <= 0)
                        return null;

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    content.Add(new StreamContent(file.OpenReadStream())
                    {
                        Headers =
                        {

                            ContentLength = file.Length,
                            ContentType = new MediaTypeHeaderValue(file.ContentType)
                        }
                    }, "File", fileName);
                }
                HttpResponseMessage response = new HttpResponseMessage();

            //response = httpClient.PostAsync("https://localhost:7247/api/UploadFile/UploadFileRealEstate"
            //                              + $"?Obj_Id={Obj_Id}"+$"&NamePj={NamePj}"+$"&NameType={NameType}", content).Result;

            response = httpClient.PostAsync("https://cdn.realtech.com.vn/api/UploadFile/UploadFileRealEstate"
                        + $"?IdRE={IdRE}"+$"&IdUser={IdUser}"+$"&IdType={IdType}", content).Result;

            var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<UploadImagesResponse>(json);

                return obj;
            }

        public UploadImagesResponse PostFileGeneralEndGetData(List<IFormFile> files, string path)
        {
            var httpClient = _client.CreateClient();
            var content = new MultipartFormDataContent();

            foreach (var file in files)
            {
                if (file== null || file.Length <= 0)
                    return null;

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                content.Add(new StreamContent(file.OpenReadStream())
                {
                    Headers =
                        {

                            ContentLength = file.Length,
                            ContentType = new MediaTypeHeaderValue(file.ContentType)
                        }
                }, "File", fileName);
            }
            HttpResponseMessage response = new HttpResponseMessage();

            //response = httpClient.PostAsync("https://localhost:7247/api/UploadFile/UploadFileRealEstate"
            //                              + $"?Obj_Id={Obj_Id}"+$"&NamePj={NamePj}"+$"&NameType={NameType}", content).Result;

            response = httpClient.PostAsync("https://cdn.realtech.com.vn/api/UploadFile/UploadFileGeneral"
                        + $"?path={path}", content).Result;

            var json = response.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<UploadImagesResponse>(json);

            return obj;
        }

    }
}
