
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using NhaDat24h.Common;
using NhaDat24h.Common.Configuration;
using NhaDat24h.CommonCode;
using NhaDat24h.DataDto.RealEstates;
using NhaDat24h.DataDto.Users;
using NhaDat24h.Service;
using NhaDat24h.Service.Api.Company;
using NhaDat24h.Service.Api.Ctv;
using NhaDat24h.Service.Api.RealEstates;
using NhaDat24h.Service.Api.Users;

namespace NhaDat24hWeb.Areas.Partner.Controllers
{
    [Area("Partner")]
    public class LandController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUsersApiServices _usersApiServices;
        private ICtvApiServices _ctvApiServices;
        private FirestoreDb _firestore;
        private string projectId;
        private ICompanyApiServices _companyApiServices;
        private IRealEstateApiServices _reApiServices;
        private readonly IMyTypedClientServices _client;
        private ISessionManager _sessionManager;
        public LandController(IHttpContextAccessor httpContextAccessor, IUsersApiServices usersApiServices, IRealEstateApiServices reApiServices,
            ISessionManager sessionManager, ICtvApiServices ctvApiServices, ICompanyApiServices companyApiServices, IMyTypedClientServices client)
        {
            _httpContextAccessor = httpContextAccessor;
            _usersApiServices = usersApiServices;
            _sessionManager = sessionManager;
            _ctvApiServices = ctvApiServices;
            _companyApiServices = companyApiServices;
            _reApiServices = reApiServices;
            _client = client;

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", AppConfigs.Directorio);
            projectId = "gostay-1ae07";
            _firestore = FirestoreDb.Create(projectId);
        }
        [Route("land")]
        public IActionResult Index(int IdProvince, int IdDistrict, int IdWards, string Address, string Searchkey,
            int IdType, int minPrice, int maxPrice, int pageIndex = 1, int pageSize = 10)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var ctv = _ctvApiServices.GetCtv(User.Id);
            if (ctv.Data.IdcongTy == 5)
            {
                return Json(ErrorCodeMessage.NoPermission.Value);
            }
            if (Searchkey != null)
            {
                Searchkey = Searchkey.RemoveUnicode().Replace(" ", string.Empty).ToLower();
            }

            var p = new SearchListREParam()
            {
                IdProvince = IdProvince,
                IdDistrict = new List<int>(),
                IdWards = new List<int>(),
                Address = Address,
                SearchKey = Searchkey,
                IdType = IdType,
                minPrice = minPrice,
                maxPrice = maxPrice,
                pageIndex = pageIndex,
                pageSize = pageSize,
                Style = 1
            };

            var searchResult = _reApiServices.SearchListRE(new SearchListREParam()
            {
                IdUserRequest = User.Id,
                IdProvince = (p.IdProvince == 0) ? null : p.IdProvince,
                IdDistrict = new List<int>(),
                IdWards = new List<int>(),
                Address = p.Address,
                SearchKey = p.SearchKey,
                IdType = (p.IdType == 0) ? null : p.IdType,
                minPrice = null,
                maxPrice = null,
                pageIndex = p.pageIndex,
                pageSize = p.pageSize,
                Style = 1
            });

            var permission = _usersApiServices.GetUserPermission(User.Id).Data;
            var provinces = _reApiServices.GetListProvinceOnPj(1).Data;
            ModelListRE model = new ModelListRE()
            {
                Param = p,
                realEstateDtos = searchResult.Data,
                Provinces = provinces,
                Permission = permission.Select(x=>x.Code).ToList(),
                IdCtv = User.Id,
                PageIndex = 1,
                PageSize = 10
            };
            return View(model);
        }


        [Route("properties")]
        public IActionResult Properties(int IdProvince, int IdDistrict, int IdWards, string Address, string Searchkey,
            int IdType, int minPrice, int maxPrice, int pageIndex = 1, int pageSize = 10)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();

            if (Searchkey != null)
            {
                Searchkey = Searchkey.RemoveUnicode().Replace(" ", string.Empty).ToLower();
            }


            var p = new SearchListREParam()
            {
                IdProvince = IdProvince,
                IdDistrict = new List<int>(),
                IdWards = new List<int>(),
                Address = Address,
                SearchKey = Searchkey,
                IdType = IdType,
                minPrice = minPrice,
                maxPrice = maxPrice,
                pageIndex = pageIndex,
                pageSize = pageSize,
                Style = 2,
            };



            var searchResult = _reApiServices.SearchListRE(new SearchListREParam()
            {
                IdUserRequest = User.Id,
                IdProvince = (p.IdProvince == 0) ? null : p.IdProvince,
                IdDistrict = new List<int>(),
                IdWards = new List<int>(),
                Address = p.Address,
                SearchKey = p.SearchKey,
                IdType = (p.IdType == 0) ? null : p.IdType,
                minPrice = null,
                maxPrice = null,
                pageIndex = p.pageIndex,
                pageSize = p.pageSize,
                Style = 2,
            });

            ViewBag.ListTypeRE = _reApiServices.GetREType().Data;

            var permission = _usersApiServices.GetUserPermission(User.Id).Data;
            var provinces = _reApiServices.GetListProvinceOnPj(2).Data;

            ModelListRE model = new ModelListRE()
            {
                Param = p,
                realEstateDtos = searchResult.Data,
                Permission = permission.Select(x => x.Code).ToList(),
                Provinces = provinces,
                IdCtv = User.Id,
                PageIndex = 1,
                PageSize = 10
            };
            return View(model);
        }

        [Route("detail-properties")]
        public IActionResult DetailProperties(int IdRE)
        {
            var Model = _reApiServices.GetREById(IdRE);

            ViewBag.ListImage = Model.Data.ListDocuments.Where(x => x.IdType == 16).ToList();
            ViewBag.ListImageSD = Model.Data.ListDocuments.Where(x => x.IdType == 19).ToList();
            return View(Model.Data);
        }

        [Route("search-landPj")]
        public IActionResult SearchREPj(int? IdProvince, string? strlistdistrict, string? strlistwards, string? Address, string? Searchkey,
            int? IdType, int? minPrice, int? maxPrice, decimal? minArg, decimal? maxArg, int pageIndex, int pageSize, byte style, int? Status)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var permission = _usersApiServices.GetUserPermission(User.Id).Data;

            if (Searchkey != null)
            {
                Searchkey = Searchkey.RemoveUnicode().Replace(" ", string.Empty).ToLower();
            }

            var listdistrict = strlistdistrict?.Split(',')?.Select(Int32.Parse)?.ToList();

            var listwards = strlistwards?.Split(',')?.Select(Int32.Parse)?.ToList();


            var p = new SearchListREParam()
            {
                IdProvince = IdProvince,
                IdDistrict = listdistrict,
                IdWards = listwards,
                Address = Address,
                SearchKey = Searchkey,
                IdType = IdType,
                minPrice = minPrice,
                maxPrice = maxPrice,
                minArg = minArg,
                maxArg = maxArg,
                Status = Status,
                pageIndex = pageIndex,
                pageSize = pageSize,
                Style = style,
            };

            var searchResult = _reApiServices.SearchListRE(new SearchListREParam()
            {
                IdUserRequest = User.Id,
                IdProvince = (p.IdProvince == 0) ? null : p.IdProvince,
                IdDistrict = p.IdDistrict,
                IdWards = p.IdWards,
                Address = p.Address,
                SearchKey = p.SearchKey,
                IdType = (p.IdType == 0) ? null : p.IdType,
                minPrice = p.minPrice,
                maxPrice = p.maxPrice,
                minArg = p.minArg,
                maxArg = p.maxArg,
                Status = p.Status,
                pageIndex = p.pageIndex,
                pageSize = p.pageSize,
                Style = style,
            });
            ModelListRE model = new ModelListRE()
            {
                Param = p,
                realEstateDtos = searchResult.Data,
                Permission = permission.Select(x => x.Code).ToList(),
                IdCtv = User.Id,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            if (style == 1)
            {
                return PartialView("~/Areas/Partner/Views/Land/ListREPartial.cshtml", model);
            }
            else
            {
                return PartialView("~/Areas/Partner/Views/Land/ListPropertiesParrtial.cshtml", model);
            }



        }
        [Route("detail")]
        public IActionResult Detail(int IdRE)
        {
            var Model = _reApiServices.GetREById(IdRE);
            return View(Model.Data);
        }

        [Route("download-zipfile")]
        public IActionResult DownloadZipFile(string url, int type)
        {
            var temp1 = url.LastIndexOf("/");
            var temp2 = url.Remove(temp1);
            var temp3 = temp2.LastIndexOf("/");
            var temp4 = temp2.Remove(temp3);
            try
            {                
                var report = _reApiServices.DownloadZipFolder(new DownloadZipParam()
                {
                    FolderName=type.ToString(),
                    FolderPath=temp4,
                });

                MemoryStream ms = new MemoryStream();
                report.CopyTo(ms);
                return File(ms.ToArray(), "application/zip", $"{type}.zip");

            }
            catch (Exception ex)
            {
                return Json(temp4 + ex.Message);
            }
        }
        [Route("detailre")]
        public IActionResult DetailRe()
        {
            return View();
        }

        [Route("land/event")]
        public IActionResult landevent()
        {
            return View();
        }

        [Route("add-new-landpj")]
        public IActionResult AddNewLandPj(int idR = 0)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin();
            var checkPermission = _usersApiServices.CheckUserPermision(new CheckPermisionParam()
            {
                IdUser = UserId.Id,
                Permission = new int[] { 101 },
            });
            if (checkPermission.Data == false)
            {
                return Json("Ban khong co quyen truy cap (No permission)");
            }

            ViewBag.ListProvince = _reApiServices.GetListProvince().Data;

            AddNewLandDto model = new AddNewLandDto()
            {
                Email = UserId.Email,
                Phone = UserId.Phone,
                Manager = UserId.Fullname,
            };
            var d = new Dictionary<int, int>() { };

            if (idR > 0)
            {
                var land = _reApiServices.GetREById(idR);
                if (land.Data != null)
                {

                    model.IdR = idR;
                    model.Name = land.Data.Name;
                    model.Email = land.Data.Email;
                    model.Phone = land.Data.Phone;

                    model.Manager = land.Data.Manager;
                    model.IdProvince = land.Data.IdProvince;
                    model.IdDistrict = land.Data.IdDistrict;
                    model.IdWard = land.Data.IdWard;

                    model.Address = land.Data.Address;
                    model.IdType = land.Data.IdType;
                    model.ImplementCompany = land.Data.ImplementCompany;

                    model.OfferPrice = land.Data.OfferPrice;
                    model.S = land.Data.S;
                    model.Detail = land.Data.Detail;

                    model.SelectedContactList = land.Data.ListContactCtvRE?.Select(x => x.Id).ToList();


                    ViewBag.ListDistrict = _reApiServices.GetListDistrictByIdProvince(land.Data.IdProvince).Data;
                    ViewBag.ListWard = _reApiServices.GetListWardsByIdDistrict(land.Data.IdDistrict).Data;

                }
            }
            else
            {

                ViewBag.ListDistrict = _reApiServices.GetListDistrictByIdProvince(5).Data;
            }
            ViewBag.ListCTVInCompany = _ctvApiServices.GetListCtvByIdCompany(UserId.Id).Data;
            // ViewBag.REtype = _reApiServices.GetREType().Data;


            //ViewBag.ListWards = _reApiServices.GetListWards().Data;
            //ViewBag.ListStreet = _reApiServices.GetListStreet().Data;

            return View(model);
        }
        [Route("add-new-landre")]
        public IActionResult AddNewLandRe(int idR = 0)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin();
            var checkPermission = _usersApiServices.CheckUserPermision(new CheckPermisionParam()
            {
                IdUser = UserId.Id,
                Permission = new int[] { 101 },
            });
            if (checkPermission.Data == false)
            {
                return Json("Ban khong co quyen truy cap (No permission)");
            }
            ViewBag.ListProvince = _reApiServices.GetListProvince().Data;
            AddNewLandDto model = new AddNewLandDto()
            {
                Email = UserId.Email,
                Phone = UserId.Phone,
                Manager = UserId.Fullname,
            };

            if (idR > 0)
            {
                var land = _reApiServices.GetREById(idR);
                if (land.Data != null)
                {
                    model.IdR = idR;
                    model.Name = land.Data.Name;
                    model.Phone = land.Data.Phone;

                    model.Manager = land.Data.Manager;
                    model.IdProvince = land.Data.IdProvince;
                    model.IdDistrict = land.Data.IdDistrict;
                    model.IdWard = land.Data.IdWard;

                    model.Address = land.Data.Address;
                    model.IdType = land.Data.IdType;

                    model.OfferPrice = land.Data.OfferPrice;
                    model.LastPrice = land.Data.LastPrice;
                    model.S = land.Data.S;
                    model.Detail = land.Data.Detail;

                    model.FrontLenght = land.Data.FrontLenght;
                    model.Lenght = land.Data.Lenght;
                    model.EntranceLenght = land.Data.EntranceLenght;

                    model.BonusPercent = land.Data.BonusPercent;
                    model.Source = land.Data.Source;
                    model.SourcePhone = land.Data.SourcePhone;

                    model.NumberFloor = land.Data.NumberFloor;
                    model.NumberBedRoom = land.Data.NumberBedRoom;
                    model.NumberWc = land.Data.NumberWc;
                    model.NumberBalcony = land.Data.NumberBalcony;

                    model.OwnerCCCD = land.Data.OwnerCCCD;
                    model.OwnerName = land.Data.OwnerName;

                    model.DirectionHouse = land.Data.DirectionHouse;

                    model.listAnhBDS = land.Data.ListDocuments.Where(x => x.IdType == 16).ToList();
                    model.listAnhSD = land.Data.ListDocuments.Where(x => x.IdType == 19).ToList();

                    ViewBag.ListDistrict = _reApiServices.GetListDistrictByIdProvince(land.Data.IdProvince).Data;
                    ViewBag.ListWard = _reApiServices.GetListWardsByIdDistrict(land.Data.IdDistrict).Data;

                }
            }
            else
            {
                ViewBag.ListDistrict = _reApiServices.GetListDistrictByIdProvince(5).Data;
            }
            return View(model);
        }
        [Route("Add-Doc-To-Land")]
        public IActionResult AddDocToLan(int idr)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var docform = new DocFileUpload()
            {
                IdUser = UserId,
                IdRE = idr,
                AllowedExtensions = new string[] { "jpg", "jpeg", "pdf", "docx", "doc", "xlsx", "xls", "zip", "png", "mp4", "pptx", "ppt", "mov" },
                IsLimited = false,
                listDoc = new List<CountDocType>(),
                listLinkbanghang = new List<DocumentDto>()
            };


            var ListDocUpdated = _reApiServices.GetCountDocType(idr).Data;

            foreach (var item in ListDocUpdated)
            {
                docform.listDoc.Add(new CountDocType() { CountType = item.CountType, IdType = item.IdType });
            }

            docform.listLinkbanghang = _reApiServices.GetListDocumentByIdRE(idr, 15).Data;
            docform.listLinkbanghang.ForEach(x => x.IdClient = UserId);

            return PartialView("~/Areas/Partner/Views/Land/AddDocumentLand.cshtml", docform);

        }

        [Route("add-new-landpj-submit")]
        public IActionResult AddNewLandPjSubmit(AddNewLandDto data)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var checkPermission = _usersApiServices.CheckUserPermision(new CheckPermisionParam()
            {
                IdUser = UserId,
                Permission = new int[] { 101 },
            });
            if (checkPermission.Data == false)
            {
                return Json("Ban khong co quyen truy cap (No permission)");
            }
            if (data.IdR > 0)
            {
                var REDto = new UpdateREParam()
                {
                    IdRE = data.IdR,
                    IdUserRequest = UserId,
                    data = new RealEstateUpdateDto()
                    {
                        IdUser = UserId,
                        Name = data.Name,
                        IdProvince = data.IdProvince,
                        IdDistrict = data.IdDistrict,
                        IdType = data.IdType,
                        IdWard = data.IdWard,
                        Address = data.Address,
                        Email = data.Email,
                        Phone = data.Phone,
                        ImplementCompany = data.ImplementCompany,
                        Manager = data.Manager,

                        OfferPrice = data.OfferPrice * data.cbodvGiaLo,
                        S = data.S,
                        Detail = data.Detail,
                        SelectedContactList = data.SelectedContactList,
                    }

                };
                var UpdateRE = _reApiServices.UpdateRE(REDto);
                return RedirectToAction("AddDocToLan", new { idr = data.IdR });
            }
            else
            {
                try
                {
                    var REDto = new REPjInsertDto()
                    {
                        IdUser = UserId,
                        Name = data.Name,
                        IdType = data.IdType,
                        IdProvince = data.IdProvince,
                        IdDistrict = data.IdDistrict,
                        IdStreet = 1,
                        IdWard = data.IdWard,
                        Address = data.Address,
                        ImplementCompany = data.ImplementCompany,
                        Manager = data.Manager,
                        Email = data.Email,
                        Phone = data.Phone,
                        OfferPrice = data.OfferPrice * data.cbodvGiaLo,
                        S = data.S,
                        Detail = data.Detail,
                        SelectedContactList = data.SelectedContactList,
                    };
                    var insertRE = _reApiServices.InsertREProject(REDto);
                    if (!insertRE.IsSuccessful)
                    {
                        return Json(insertRE.Message);
                    }
                    return RedirectToAction("AddDocToLan", new { idr = insertRE.Data });
                }
                catch(Exception ex)
                {
                    return Json(ex.Message);
                }
                
            }

        }

        [Route("add-link-bang-hang")]
        [HttpGet]
        public IActionResult addlinkbanghang(int idR, string name, string link)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;

            DocumentInsertDto doc = new DocumentInsertDto()
            {
                IdType = 15,
                Name = name,
                Url = link,
                IdRE = idR,
                IdUserRequest = UserId,
                FileType = "link",
                FileSize = 0,
            };

            var insertdoc = _reApiServices.InsertDoc(doc);
            var Model = _reApiServices.GetListDocumentByIdRE(idR, 15).Data;
            return PartialView("~/Areas/Partner/Views/Shared/showlistFiles.cshtml", Model);

        }


        [Route("land/add-new-landre-submit")]
        [HttpPost]
        public IActionResult AddNewLandReSubmit(AddNewLandDto data)
        {
            string strMess = "";
            int idr = 0;
            ViewBag.ListProvince = _reApiServices.GetListProvince().Data;
            //           ViewBag.ListDistrict = _reApiServices.GetListDistrict().Data;
            //            ViewBag.ListWards = _reApiServices.GetListWards().Data;
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var checkPermission = _usersApiServices.CheckUserPermision(new CheckPermisionParam()
            {
                IdUser = UserId,
                Permission = new int[] { 101 },
            });
            if (checkPermission.Data == false)
            {
                return Json("Ban khong co quyen truy cap (No permission)");
            }

            if (data.IdR > 0)
            {
                var REDto = new UpdateREParam()
                {
                    IdRE = data.IdR,
                    IdUserRequest = UserId,
                    data = new RealEstateUpdateDto()
                    {
                        IdUser = UserId,
                        Name = data.Name,
                        IdProvince = data.IdProvince,
                        IdDistrict = data.IdDistrict,
                        IdType = data.IdType,
                        IdWard = data.IdWard,
                        Address = data.Address,
                        Phone = data.Phone,
                        Manager = data.Manager,
                        OfferPrice = data.OfferPrice,
                        S = data.S,
                        DirectionHouse = data.DirectionHouse,
                        Detail = data.Detail,
                        FrontLenght = data.FrontLenght,
                        Lenght = data.Lenght,
                        EntranceLenght = data.EntranceLenght,
                        NumberFloor = data.NumberFloor,
                        NumberBedRoom = data.NumberBedRoom,
                        NumberWc = data.NumberWc,
                        NumberBalcony = data.NumberBalcony,
                        LastPrice = data.LastPrice,
                        BonusPercent = data.BonusPercent,
                        Source = data.Source,
                        SourcePhone = data.SourcePhone,
                        OwnerName = data.OwnerName,
                        OwnerCCCD = data.OwnerCCCD,
                    }
                };
                var UpdateRE = _reApiServices.UpdateRE(REDto);
                idr = data.IdR;
                strMess = UpdateRE.Message;
            }
            else
            {
                var REDto = new REReInsertDto()
                {
                    IdUser = UserId,
                    Name = data.Name,
                    IdType = data.IdType,
                    IdProvince = data.IdProvince,
                    IdDistrict = data.IdDistrict,
                    IdStreet = 1,
                    IdWard = data.IdWard,
                    Address = data.Address,
                    ImplementCompany = data.ImplementCompany,
                    Manager = data.Manager,
                    Email = data.Email,
                    Phone = data.Phone,
                    S = data.S,
                    Detail = data.Detail,
                    FrontLenght = data.FrontLenght,
                    Lenght = data.Lenght,
                    EntranceLenght = data.EntranceLenght,
                    NumberFloor = data.NumberFloor,
                    NumberBedRoom = data.NumberBedRoom,
                    NumberWc = data.NumberWc,
                    NumberBalcony = data.NumberBalcony,
                    BonusPercent = data.BonusPercent,
                    BonusMoney = data.BonusMoney,
                    DirectionHouse = data.DirectionHouse,
                    Source = data.Source,
                    SourcePhone = data.SourcePhone,
                    LinkFb = data.LinkFb,
                    SellerPhone = data.SellerPhone,
                    OwnerName = data.OwnerName,
                    OwnerCCCD = data.OwnerCCCD,
                    OfferPrice = data.OfferPrice,
                    LastPrice = data.LastPrice,
                };
                var insertRE = _reApiServices.InsertREResidence(REDto);
                if (insertRE.Data == 0)
                {
                    return Json("Có lỗi xảy ra, vui lòng thử lại");
                }
                idr = insertRE.Data;
                strMess = insertRE.Message;
            }

            if (idr > 0 && data.AnhBDS != null)
            {
                UploadImagesResponse temp = new UploadImagesResponse();
                string[] charsToRemove = new string[] { "@", "[", "]", "'" };
                temp = _client.PostFileREEndGetData(data.AnhBDS, idr, UserId, 16);
                foreach (var c in charsToRemove)
                {
                    temp.data = temp.data.Replace(c, string.Empty);
                    temp.size = temp.size.Replace(c, string.Empty);
                    temp.type = temp.type.Replace(c, string.Empty);

                }
                var url = temp.data.Split(",");
                var size = temp.size.Split(",");
                var type = temp.type.Split(",");

                string[] urlimg = new string[url.Length];

                for (int i = 0; i < url.Length; i++)
                {

                    var fileName = Path.GetFileNameWithoutExtension(url[i]);
                    urlimg[i] = $"/uploads/SGOland/ctv/{UserId}/bds/{idr}/{16}/" + fileName + Path.GetExtension(url[i]).Replace("\"", "");
                    DocumentInsertDto doc = new DocumentInsertDto()
                    {
                        IdType = 16,
                        Name = fileName,
                        Url = urlimg[i],
                        IdRE = idr,
                        IdUserRequest = UserId,
                        FileType = "." + type[i].Replace("\"", ""),
                        FileSize = decimal.Parse(size[i]),
                    };

                    var insertdoc = _reApiServices.InsertDoc(doc);
                }
            }
            if (idr > 0 && data.AnhSD != null)
            {
                UploadImagesResponse temp = new UploadImagesResponse();
                string[] charsToRemove = new string[] { "@", "[", "]", "'" };
                temp = _client.PostFileREEndGetData(data.AnhSD, idr, UserId, 19);
                foreach (var c in charsToRemove)
                {
                    temp.data = temp.data.Replace(c, string.Empty);
                    temp.size = temp.size.Replace(c, string.Empty);
                    temp.type = temp.type.Replace(c, string.Empty);

                }
                var url = temp.data.Split(",");
                var size = temp.size.Split(",");
                var type = temp.type.Split(",");

                string[] urlimg = new string[url.Length];

                for (int i = 0; i < url.Length; i++)
                {

                    var fileName = Path.GetFileNameWithoutExtension(url[i]);
                    urlimg[i] = $"/uploads/SGOland/ctv/{UserId}/bds/{idr}/{19}/" + fileName + Path.GetExtension(url[i]).Replace("\"", "");
                    DocumentInsertDto doc = new DocumentInsertDto()
                    {
                        IdType = 19,
                        Name = fileName,
                        Url = urlimg[i],
                        IdRE = idr,
                        IdUserRequest = UserId,
                        FileType = "." + type[i].Replace("\"", ""),
                        FileSize = decimal.Parse(size[i]),
                    };

                    var insertdoc = _reApiServices.InsertDoc(doc);
                }
            }
            return Json(strMess);
        }

        [Route("edit-land")]
        public IActionResult EditLandPrimary()
        {
            return View();
        }

        [Route("showlistFiles")]
        [HttpGet]
        public IActionResult showlistFiles(int idR, int idType)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;

            var Model = _reApiServices.GetListDocumentByIdRE(idR, idType).Data;

            Model.ForEach(x => x.IdClient = UserId);

            return PartialView("~/Areas/Partner/Views/Shared/showlistFiles.cshtml", Model);
        }

        [Route("saveRe")]
        [HttpGet]
        public void SaveReSubmit(int IdRE)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var action = _reApiServices.InsertSaveRE(new SaveREInsertDto()
            {
                IdRE = IdRE,
                IdCtv = UserId,
                Type = 1,
            });
        }

        [Route("unsaveRe")]
        [HttpGet]
        public void UnSaveReSubmit(int IdCtv, int IdRE)
        {
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            IdCtv = UserId;
            var action = _reApiServices.DeleteSaveRE(IdCtv, IdRE);
        }

        [Route("deleteRe")]
        [HttpGet]
        public IActionResult DeleteRE(int IdRE)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var result = _reApiServices.DeleteRE(User, IdRE);

            return Json(result.Message);
        }

        [Route("update-Status-RE")]
        [HttpGet]
        public IActionResult updateStatusRE(int IdR)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            UpdateStatusREParam update = new UpdateStatusREParam()
            {
                IdRE = IdR,
                IdUserRequest = User.Id,
                Status = 10
            };
            var result = _reApiServices.UpdateStatusRE(update);
            try
            {

                string strEmailist = _companyApiServices.GetEmailOfSuperior(IdR).Data;
                var timeTmp = DateTime.Now;
                var time = ((DateTimeOffset)timeTmp).ToUnixTimeSeconds().ToString();

                DocumentReference colRf = _firestore.Collection("sgoland-pheduyet").Document(System.Guid.NewGuid().ToString());
                Dictionary<string, object> value = new Dictionary<string, object>()
                {
                            {"Emailcc", strEmailist},
                            {"Email", User.Email },
                            {"Content", "Xin chào: " + User.Fullname +", bạn đã gửi phê duyệt dự án ID:"+IdR +" thành công. Bạn sẽ nhận được thông báo khi dự án được phê duyệt hoặc không phê duyệt. Xin chân thành cảm ơn." },
                            {"Time", time },
                };

                colRf.CreateAsync(value);
            }
            catch
            {
            }

            return Json(result.Code);
        }

        [Route("update-Status-RE2")]
        [HttpGet]
        public IActionResult UpdateStatusRE2(int IdRE, byte Status, string Descriptions, string idUserDuan)
        {
            var UserID = _sessionManager.GetLoginAdminFromSessionAdmin();
            UpdateStatusREParam update = new UpdateStatusREParam()
            {
                IdRE = IdRE,
                IdUserRequest = UserID.Id,
                Status = Status,
                Description = Descriptions
            };
            var result = _reApiServices.UpdateStatusRE(update);
            try
            {

                string strEmailist = _companyApiServices.GetEmailOfSuperior(IdRE).Data;
                var UserDuAn = _usersApiServices.CheckUserByID(idUserDuan);
                var timeTmp = DateTime.Now;
                var time = ((DateTimeOffset)timeTmp).ToUnixTimeSeconds().ToString();
                var positionValue = HttpContext.Session.GetString("Position");
                string position = "";
                if (!string.IsNullOrEmpty(positionValue))
                {
                    position = JsonConvert.DeserializeObject<string>(positionValue);
                }
                DocumentReference colRf = _firestore.Collection("sgoland-pheduyet").Document(System.Guid.NewGuid().ToString());
                Dictionary<string, object> value = new Dictionary<string, object>()
                {
                            {"Emailcc", strEmailist},
                            {"Email", UserDuAn.Data.Email },
                            {"Content", "Xin chào: " + UserDuAn.Data.Fullname + ", dự án ID: "+ IdRE +" của bạn đã: <b>"+ AppConfigs._status(Status)+"</b>, Lý do: <b>"+  Descriptions +"</b>, Phê chuẩn bởi: <b>"+ UserID.Fullname+"</b>, chức vụ: <b>"+ position +"</b>"},
                            {"Time", time },
                };

                colRf.CreateAsync(value);
            }
            catch
            {
            }
            return Json(result.Code);
        }
    }
}
