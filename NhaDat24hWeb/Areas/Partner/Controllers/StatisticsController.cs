using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using NhaDat24h.Common.Configuration;
using NhaDat24h.CommonCode;
using NhaDat24h.Service.Api.Company;
using NhaDat24h.Service.Api.Ctv;
using NhaDat24h.Service.Api.RealEstates;
using NhaDat24h.Service.Api.Users;
using NhaDat24h.Service;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NhaDat24h.DataDto.Statistics;
using NhaDat24h.DataDto.User;
using NhaDat24h.DataDto.Authen;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using NhaDat24h.Common;
using System.Web.WebPages;
using NhaDat24h.DataDto.Ctv;
using NhaDat24h.Common.Enums;

namespace NhaDat24hWeb.Areas.Partner.Controllers
{
    [Area("Partner")]
    public class StatisticsController : Controller
    {
        private IUsersApiServices _usersApiServices;
        private ICtvApiServices _ctvApiServices;
        private ICompanyApiServices _companyApiServices;
        private IRealEstateApiServices _reApiServices;
        private ISessionManager _sessionManager;
        public StatisticsController(IUsersApiServices usersApiServices, IRealEstateApiServices reApiServices,
            ISessionManager sessionManager, ICtvApiServices ctvApiServices, ICompanyApiServices companyApiServices)
        {
            _usersApiServices = usersApiServices;
            _sessionManager = sessionManager;
            _ctvApiServices = ctvApiServices;
            _companyApiServices = companyApiServices;
            _reApiServices = reApiServices;
        }
        [Route("business-intelligence-report")]
        public IActionResult Index(DateTime StartDate, DateTime EndDate, int IdCompany = 0)
        {

            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var Permission = _companyApiServices.GetPositionCtv(UserId);
            var ctvPoPer = _usersApiServices.GetUserPositionPermission(UserId);

            if (!ctvPoPer.Data.Contains(((int)PermissionType.Statistics)))
            {
                return Json(ErrorCodeMessage.NoPermission.Value);
            }
            var result = new ResponseBase<HRReportDto>();
            if (Permission.Data.Min(x => x.IdPositionDepartment)==1)
            {
                result = _ctvApiServices.GetHRReport(StartDate, EndDate, 0);
            }
            else
            {
                result = _ctvApiServices.GetHRReport(StartDate, EndDate, (int)Permission.Data.Min(x => x.IdCompany));
            }
            result.Data.IdCompany = IdCompany;
            result.Data.StartDate = StartDate;
            result.Data.EndDate = EndDate.AddDays(1);
            var result2 = _reApiServices.GetREReport(StartDate, EndDate);
            StatisticDto model = new StatisticDto()
            {
                HrReport = result.Data,
                REReport = result2.Data,
                ListCompany = _companyApiServices.GetListCompanyByCtv(UserId).Data
            };
            return View(model);
        }
        [Route("search-Statistics")]
        public IActionResult SearchStatistics(DateTime StartDate, DateTime EndDate, int IdCompany = 0)
        {
            ViewBag.ListCompany = _companyApiServices.GetListCompanyByCtv(20).Data;

            var result = _ctvApiServices.GetHRReport(StartDate, EndDate, IdCompany);
            result.Data.IdCompany = IdCompany;
            result.Data.StartDate = StartDate;
            result.Data.EndDate = EndDate.AddDays(1);
      
            var result2 = _reApiServices.GetREReport(StartDate, EndDate);
            StatisticDto model = new StatisticDto()
            {
                HrReport = result.Data,
                REReport = result2.Data
            };

            return PartialView("~/Areas/Partner/Views/Statistics/StatisticPartial.cshtml", model);
        }

        // danh sachs nhan su
        [Route("export-list-user-report")]
        public IActionResult ExportListUserReport()
        {
            var idcompany = 0;
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var Permission = _companyApiServices.GetPositionCtv(UserId);

            var listcompany = _companyApiServices.GetListCompanyByCtv(UserId).Data;
            if (Permission.Data.Min(x => x.IdPositionDepartment)>1)
            {
                idcompany = listcompany.FirstOrDefault().Id;
            }
            var report = _usersApiServices.GetHrReport(new CtvSgoGroupRequestDto()
            {

                IdCompany = idcompany,
                StartDate = new DateTime(2023, 6, 1).ToString("yyyy/MM/dd"),
                EndDate = DateTime.Now.ToString("yyyy/MM/dd"),
                PageIndex = 1,
                PageSize = 40

            }).Data;


            var model = new ModelUserReport()
            {
                StartDate = new DateTime(2023, 6, 1),
                EndDate = DateTime.Now,
                i = 0,
                data = report,
                ListCompany = listcompany

            };
            model.data.ForEach(x => x.i = 1);
            return View(model);
        }

        [Route("search-export-list-user-report")]
        public IActionResult SearchExportListUserReport(int IdCompany, DateTime startDate, DateTime endDate, int pageIndex)
        {
            var report = _usersApiServices.GetHrReport(new CtvSgoGroupRequestDto()
            {
                IdCompany = IdCompany,
                StartDate = startDate.ToString("yyyy/MM/dd"),
                EndDate = endDate.AddDays(1).ToString("yyyy/MM/dd"),
                PageIndex = pageIndex,
                PageSize = 40

            }).Data;
            var model = new ModelUserReport();
            model.StartDate = startDate;
            model.EndDate = endDate;
            model.data = report;
            model.data.ForEach(x => x.i = (40 * (pageIndex - 1) + 1));

            //var list = report.Where(x => x.Key.IdCompany == IdCompany).SingleOrDefault()?.Value;
            //var key = new GetHrReportKey()
            //{
            //    IdCompany = IdCompany,
            //    Company = report.Where(x => x.Key.IdCompany == IdCompany)
            //                    .Select(x => x.Key).FirstOrDefault().Company
            //};
            if (model.data.Count() > 0)
            {
                return PartialView("~/Areas/Partner/Views/Statistics/ExportListUserReportPartial.cshtml", model);
            }
            else
            {
                return Json("");
            }
        }

        [Route("load-more-list-user-report")]
        public IActionResult LoadmoreListUserReport(int IdCompany, DateTime startDate, DateTime endDate, int pageIndex)
        {
            var report = _usersApiServices.GetHrReport(new CtvSgoGroupRequestDto()
            {
                IdCompany = IdCompany,
                StartDate = startDate.ToString("yyyy/MM/dd"),
                EndDate = endDate.AddDays(1).ToString("yyyy/MM/dd"),
                PageIndex = pageIndex,
                PageSize = 40

            }).Data;
            var model = new GetHrReportData();

            model.i = (40 * (pageIndex - 1) + 1);
            model.Value = report.Where(x => x.Key.IdCompany == IdCompany).SingleOrDefault()?.Value;
            model.Key = new GetHrReportKey()
            {
                IdCompany = IdCompany,
                Company = report.Where(x => x.Key.IdCompany == IdCompany)
                                .Select(x => x.Key).FirstOrDefault().Company
            };
            if (model.Value.Count() > 0)
            {
                return PartialView("~/Areas/Partner/Views/Statistics/ExportListUserReportLoadMoreCompanyPartial.cshtml", model);
            }
            else
            {
                return Json("");
            }
        }

        [Route("export-list-user-report-excell")]
        public IActionResult ExportListUserReportExcell(int idCompany, DateTime startDate, DateTime endDate)
        {

            try
            {
                var report = _usersApiServices.PostHrReportExcel(new CtvSgoGroupRequestDto()
                {
                    IdCompany = idCompany,
                    StartDate = startDate.ToString("yyyy/MM/dd"),
                    EndDate = endDate.AddDays(1).ToString("yyyy/MM/dd"),
                    PageSize = 1000,
                    PageIndex = 1
                });

                MemoryStream ms = new MemoryStream();
                report.CopyTo(ms);
                return File(ms.ToArray(), "application/vnd.ms-excel", "Nhansu_sgolandxlsx.xlsx");

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //  tông hơp so luong theo phong ban
        [Route("export-summary-list-user-report")]
        public IActionResult ExportSummaryListReport()
        {
            var idcompany = 0;
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var Permission = _companyApiServices.GetPositionCtv(UserId);

            var listcompany = _companyApiServices.GetListCompanyByCtv(UserId).Data;
            if (Permission.Data.Min(x => x.IdPositionDepartment)>1)
            {
                idcompany = listcompany.FirstOrDefault().Id;
            }

            var report = _usersApiServices.GetHrReportSynthesis(new GHrReportSynthesisRequestDto()
            {
                IdCompany = idcompany,
                StartDate = new DateTime(2023, 6, 1).ToString("dd/MM/yyyy"),
                EndDate = DateTime.Now.ToString("dd/MM/yyyy"),
                PageIndex = 1,
                PageSize = 40

            }).Data;

            var model = new HrReportSynthesisModel()
            {
                StartDate = new DateTime(2023, 6, 1),
                EndDate = DateTime.Now,
                data = report,
                ListCompany = listcompany
            };
            model.data.ForEach(x => x.i = 1);
            return View(model);
        }


        [Route("search-summary-list-user-report")]
        public IActionResult SearchExportSummaryListReport(int IdCompany, DateTime startDate, DateTime endDate, int pageIndex)
        {

            var report = _usersApiServices.GetHrReportSynthesis(new GHrReportSynthesisRequestDto()
            {
                IdCompany = IdCompany,
                StartDate = startDate.ToString("dd/MM/yyyy"),
                EndDate = endDate.AddDays(1).ToString("dd/MM/yyyy"),
                PageIndex = pageIndex,
                PageSize = 40

            }).Data;

            var model = new HrReportSynthesisModel();
            model.StartDate = startDate;
            model.EndDate = endDate;
          //  model.i = (40 * (pageIndex - 1) + 1);
            model.data = report;
            model.data.ForEach(x => x.i = (40 * (pageIndex - 1) + 1));


            if (model.data.Count() > 0)
            {
                return PartialView("~/Areas/Partner/Views/Statistics/ExportSummaryListReportPartial.cshtml", model);
            }
            else
            {
                return Json("");
            }
        }



        [Route("load-more-summary-list-user-report")]
        public IActionResult LoadmoreSummaryReport(int IdCompany, DateTime startDate, DateTime endDate, int pageIndex)
        {
            var report = _usersApiServices.GetHrReportSynthesis(new GHrReportSynthesisRequestDto()
            {
                IdCompany = IdCompany,
                StartDate = startDate.ToString("dd/MM/yyyy"),
                EndDate = endDate.AddDays(1).ToString("dd/MM/yyyy"),
                PageIndex = pageIndex,
                PageSize = 40

            }).Data;
            var model = new GetHrReportSynthesisData();

            model.i = (40 * (pageIndex - 1) + 1);
            model.Value = report.Where(x => x.Key.IdCompany == IdCompany).SingleOrDefault()?.Value;
            model.Key = new GetHrReportSynthesisKey()
            {
                IdCompany = IdCompany,
                Company = report.Where(x => x.Key.IdCompany == IdCompany)
                                .Select(x => x.Key).FirstOrDefault().Company
            };
            if (model.Value.Count() > 0)
            {
                return PartialView("~/Areas/Partner/Views/Statistics/ExportSummaryListUserReportLoadMoreCompanyPartial.cshtml", model);
            }
            else
            {
                return Json("");
            }
        }

        [Route("export-Summary-list-report-excell")]
        public IActionResult ExportSummaryListReportExcell(int idCompany, DateTime startDate, DateTime endDate)
        {
            try
            {
                var report = _usersApiServices.PostHrReportSynthesisExcel(new GHrReportSynthesisRequestDto()
                {
                    IdCompany = idCompany,
                    StartDate = startDate.ToString("dd/MM/yyyy"),
                    EndDate = endDate.AddDays(1).ToString("dd/MM/yyyy"),
                    PageSize = 1000,
                    PageIndex = 1
                });

                MemoryStream ms = new MemoryStream();
                report.CopyTo(ms);
                return File(ms.ToArray(), "application/vnd.ms-excel", "TongHopNhanSu_sgolandxlsx.xlsx");
                //"application/vnd.ms-word
                //.txt : text/plain
                //.pdf : application/pdf
                //.png : image/png => các file ảnh khác tương tự
                //.zip : application/zip
                //.csv : text/csv

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // ket qua kinh doanh
        [Route("export-business-report")]
        public IActionResult ExportBusinessReport()
        {
            var idcompany = 0;
            var UserId = _sessionManager.GetLoginAdminFromSessionAdmin().Id;
            var Permission = _companyApiServices.GetPositionCtv(UserId);

            var listcompany = _companyApiServices.GetListCompanyByCtv(UserId).Data;
            if (Permission.Data.Min(x => x.IdPositionDepartment)>1)
            {
                idcompany = listcompany.FirstOrDefault().Id;
            }
            var report = _usersApiServices.GetBusinessReport(new ReportBusinessRequestDto()
            {
                IdCompany = idcompany,
                StartDate = new DateTime(2023, 6, 1).ToString("dd/MM/yyyy"),
                EndDate = DateTime.Now.ToString("dd/MM/yyyy"),
                PageIndex = 1,
                PageSize = 40
            }).Data;
            var model = new HrReportBusinessModel()
            {
                StartDate = new DateTime(2023, 6, 1),
                EndDate = DateTime.Now,
                data = report,
                ListCompany = listcompany

            };
            return View(model);
        }


        [Route("search-export-business-report")]
        public IActionResult SearchExportBusinessReport(int IdCompany, DateTime startDate, DateTime endDate, int pageIndex)
        {

            var report = _usersApiServices.GetBusinessReport(new ReportBusinessRequestDto()
            {
                IdCompany = IdCompany,
                StartDate = startDate.ToString("dd/MM/yyyy"),
                EndDate = endDate.ToString("dd/MM/yyyy"),
                PageIndex = pageIndex,
                PageSize = 40

            }).Data;

            var model = new HrReportBusinessModel();
            model.StartDate = startDate;
            model.EndDate = endDate;
            model.i = (40 * (pageIndex - 1) + 1);
            model.data = report;

            if (model.data.Count() > 0)
            {
                return PartialView("~/Areas/Partner/Views/Statistics/ExportBusinessReportPartial.cshtml", model);
            }
            else
            {
                return Json("");
            }

        }

        [Route("load-more-export-business-report")]
        public IActionResult LoadmoreBusinessReport(int IdCompany, DateTime startDate, DateTime endDate, int pageIndex)
        {

            var report = _usersApiServices.GetBusinessReport(new ReportBusinessRequestDto()
            {
                IdCompany = IdCompany,
                StartDate = startDate.ToString("dd/MM/yyyy"),
                EndDate = endDate.ToString("dd/MM/yyyy"),
                PageIndex = pageIndex,
                PageSize = 40

            }).Data;
            var model = new GetReportBusinessData();

            model.i = (40 * (pageIndex - 1) + 1);
            model.Value = report.Where(x => x.Key.IdCompany == IdCompany).SingleOrDefault()?.Value;
            model.Key = new GetReportBusinessKey()
            {
                IdCompany = IdCompany,
                Company = report.Where(x => x.Key.IdCompany == IdCompany)
                                .Select(x => x.Key).FirstOrDefault().Company
            };
            if (model.Value.Count() > 0)
            {
                return PartialView("~/Areas/Partner/Views/Statistics/ExportBusinessReportLoadMoreCompanyPartial.cshtml", model);
            }
            else
            {
                return Json("");
            }
        }

        [Route("export-business-report-excell")]
        public IActionResult ExportBusinessReportExcell(int idCompany, DateTime startDate, DateTime endDate)
        {
            try
            {
                var report = _usersApiServices.PostBusinessReportExcel(new ReportBusinessRequestDto()
                {
                    IdCompany = idCompany,
                    StartDate = startDate.ToString("dd/MM/yyyy"),
                    EndDate = endDate.ToString("dd/MM/yyyy"),
                    PageSize = 1000,
                    PageIndex = 1
                });
                MemoryStream ms = new MemoryStream();
                report.CopyTo(ms);
                return File(ms.ToArray(), "application/vnd.ms-excel", "BaoCaoKetQuaKinhDoanh_sgolandxlsx.xlsx");
                //"application/vnd.ms-word
                //.txt : text/plain
                //.pdf : application/pdf
                //.png : image/png => các file ảnh khác tương tự
                //.zip : application/zip
                //.csv : text/csv
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }


}

