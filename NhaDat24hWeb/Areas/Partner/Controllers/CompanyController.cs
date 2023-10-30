using Google.Type;
using Microsoft.AspNetCore.Mvc;
using NhaDat24h.Common;
using NhaDat24h.CommonCode;
using NhaDat24h.DataDto.Company;
using NhaDat24h.Service;
using NhaDat24h.Service.Api.Company;
using NhaDat24h.Service.Api.Users;

namespace NhaDat24hWeb.Areas.Partner.Controllers
{
    [Area("Partner")]
    public class CompanyController : Controller
    {

        private ICompanyApiServices _companyApiServices;
        private ISessionManager _sessionManager;
        private readonly IMyTypedClientServices _client;
        private readonly IUsersApiServices _usersApiServices;
        public CompanyController(ISessionManager sessionManager, ICompanyApiServices companyApiServices, IMyTypedClientServices client,
            IUsersApiServices usersApiServices)
        {
            _companyApiServices = companyApiServices;
            _sessionManager = sessionManager;
            _client = client;
            _usersApiServices = usersApiServices;
        }

        [Route("company")]
        public IActionResult Index()
        {
            //var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var listCompany = _companyApiServices.GetListCompanyByCtv(20);

            if (listCompany.Code == ErrorCodeMessage.NoPermission.Key)
            {
                return Json(ErrorCodeMessage.NoPermission.Value);
            }
            else
            {
                return View(listCompany.Data);
            }
        }

        [Route("company/add-company")]
        public IActionResult AddCompany(int IdCty = 0)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var message = "";
            var position = _companyApiServices.GetPositionCtv(User.Id);
            if (position.Data.Select(x => x.IdPositionDepartment).Contains(1))
            {
                var model = new CompanyDto();
                if (IdCty > 0)
                {
                    var company = _companyApiServices.GetListCompanyByCtv(20).Data.Where(x => x.Id == IdCty).SingleOrDefault();
                    return View(company);
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                message="You are not authorized to do this action";
                return Json(message);
            }
        }
        [Route("company/add-company-submit")]
        public IActionResult AddCompanySubmit(CompanyDto param)
        {
            string urlavt = "";
            string[] charsToRemove = new string[] { "@", "[", "]", "'" };
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var message = "";
            var position = _companyApiServices.GetPositionCtv(User.Id);
            if (position.Data.Select(x => x.IdPositionDepartment).Contains(1))
            {
                if (param.Id > 0)
                {
                    var updateCpn = new CompanyUpdateParam()
                    {
                        Id = param.Id,
                        Name = param.Name,
                        Email = param.Email,
                        Address = param.Address,
                        Mobile = param.Mobile,
                        Phone = param.Phone,
                        Avatar = param.Avatar,
                    };
                    if (param.fileAvt != null)
                    {
                        var list = new List<IFormFile>() { param.fileAvt };
                        var avtupload = _client.PostCvAndGetData(list, User.Id.ToString());

                        foreach (var c in charsToRemove)
                        {
                            avtupload.data = avtupload.data.Replace(c, string.Empty);
                            avtupload.size = avtupload.size.Replace(c, string.Empty);
                        }

                        var y = Path.GetFileName(avtupload.data).Replace("\"", "");
                        urlavt = $"/uploads/SGOland/ctv/" + User.Id.ToString() + "/" + y;
                        updateCpn.Avatar = urlavt;
                    }

                    var update = _companyApiServices.UpdateCompany(updateCpn);
                    message = update.Message;

                }
                else
                {
                    var insert = _companyApiServices.InsertCompany(new CompanyInsertParam()
                    {
                        Name = param.Name,
                        Address = param.Address,
                        Mobile = param.Mobile,
                        Email = param.Email,
                        Phone = param.Phone,
                    });
                    if (insert.Data == 0)
                    {
                        return View(insert.Message);
                    }
                    List<IFormFile> avt = new List<IFormFile> { param.fileAvt };
                    if (avt[0] != null)
                    {
                        var avtupload = _client.PostCvAndGetData(avt, User.Id.ToString());

                        foreach (var c in charsToRemove)
                        {
                            avtupload.data = avtupload.data.Replace(c, string.Empty);
                            avtupload.size = avtupload.size.Replace(c, string.Empty);
                        }

                        var y = Path.GetFileName(avtupload.data).Replace("\"", "");
                        urlavt = $"/uploads/SGOland/ctv/" + User.Id.ToString() + "/" + y;
                        var update = _companyApiServices.UpdateAvatarCompany(new CompanyUpdateAvatarParam()
                        {
                            Id = insert.Data,
                            Url = urlavt,
                        });
                        message = update.Message;
                    }
                }
            }
            else
            {
                message="You are not authorized to do this action";
            }
            return Json(message);
        }
        [Route("company/add-department-company")]
        public IActionResult StructureCompany(int IdCompany,int IdDepartment=0)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var position = _companyApiServices.GetPositionCtv(User.Id);
            if (position.Data.Select(x => x.IdPositionDepartment).Contains(1))
            {
                    var model = new StructureDepartmentDto();
                model.IdCompany = IdCompany;
  
                model.ListDepartment = _companyApiServices.GetDepartmentInCompany(new List<int> { IdCompany }).Data;
                if (IdDepartment>0)
                {
                    model.IdDepartment = IdDepartment;
                    model.DepartmentName = model.ListDepartment.Where(x => x.Id==IdDepartment).SingleOrDefault().Name;
                    model.Description = model.ListDepartment.Where(x => x.Id==IdDepartment).SingleOrDefault().Description;
                }
                return View(model);
            }
            else
            {
                return Json(ErrorCodeMessage.NoPermission.Value);
            }
        }

        [Route("company/get-list-position")]
        public IActionResult GetListPosition(int IdDepartment, int IdPositionDep=0)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();

            var model = new StructurePositionDto();
            model.IdDepartment=IdDepartment;
            model.ListPositionExiting = _companyApiServices.GetStillPositionInDepartment(new GetStillPositionInDepartmentParam() { 
                IdDepartment= new List<int> { IdDepartment } ,
                IdCtv = User.Id
            }).Data;
            model.ListPosition = _companyApiServices.GetListPosition(User.Id, IdDepartment).Data;
            if (IdPositionDep>0)
            {
                model.IdPositionDep = IdPositionDep;
                model.Description = model.ListPositionExiting.Where(x => x.Id==IdPositionDep).SingleOrDefault().Description;
            }
            return PartialView("~/Areas/Partner/Views/Company/AddPositionPartial.cshtml", model );
        }

        [Route("company/add-department")]
        public int AddDepartment(int IdCompany, string DepartmentName, string Description, int IdSuperior)
        {
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            int result = 0;
            var position = _companyApiServices.GetPositionCtv(User.Id);
            if (position.Data.Select(x => x.IdPositionDepartment).Contains(1))
            {
                var data = _companyApiServices.InsertDepartment(new DepartmentInsertParam()
                {
                    Name = DepartmentName,
                    IdCompany=IdCompany,
                    IdSuperiors = IdSuperior,
                    Description = Description
                });
                if (data.Code == 0)
                    return 1;
            }
            else
            {
                return 2;
            }
            return result;
        }

        [Route("company/edit-department")]
        public IActionResult EditDepartment(int IdDepartment, string DepartmentName, string Description)
        {
            string result = "";
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var position = _companyApiServices.GetPositionCtv(User.Id);
            if (position.Data.Select(x => x.IdPositionDepartment).Contains(1))
            {
                var data = _companyApiServices.UpdateDepartment(new DepartmentUpdateParam()
                {
                    Id = IdDepartment,
                    Name = DepartmentName,
                    Description= Description,
                    IdUserRequest = User.Id
                });
                result=data.Message;
            }
            else
            {
                result="You are not authorized to do this action";
            }
            return Json(result);
        }
        [Route("company/delete-department")]
        public string DeleteDepartment(int IdDepartment)
        {
            string result = "";
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var position = _companyApiServices.GetPositionCtv(User.Id);
            if (position.Data.Select(x => x.IdPositionDepartment).Contains(1))
            {
                var data = _companyApiServices.SoftDeleteDepartment(IdDepartment, User.Id);
                result = data.Message;
            }
            else
            {
                result="You are not authorized to do this action";
            }
            return result;
        }

        [Route("company/add-positiondep")]
        public IActionResult AddPositionDep(int IdPosition,int IdDepartment)
        {
            string result = "";
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var position = _companyApiServices.GetPositionCtv(User.Id);
            if (position.Data.Select(x => x.IdPositionDepartment).Contains(1))
            {

                var data = _companyApiServices.InsertPositionDepartment(new PositonDepartmentInsertParam()
                {
                    IdPosition = IdPosition,
                    IdDepartment=IdDepartment,
                });
                result=data.Code.ToString();
            }
            else
            {
                result="You are not authorized to do this action";
            }
            return Json(result);
        }
        [Route("company/delete-positiondep")]
        public IActionResult DeletePositionDep(int IdPositionDep)
        {
            string result = "";
            var User = _sessionManager.GetLoginAdminFromSessionAdmin();
            var position = _companyApiServices.GetPositionCtv(User.Id);
            if (position.Data.Select(x => x.IdPositionDepartment).Contains(1))
            {
                var data = _companyApiServices.DeletePositionDepartment(IdPositionDep, User.Id);
                result = data.Code.ToString();
            }
            else
            {
                result="You are not authorized to do this action";
            }
            return Json(result);
        }
    }
}
