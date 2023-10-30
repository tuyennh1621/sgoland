using NhaDat24h.Common;
using NhaDat24h.DataAccess.Entities;
using NhaDat24h.DataDto.Company;
using NhaDat24h.DataDto.Ctv;
using NhaDat24h.DataDto.RealEstates;
using NhaDat24h.DataDto.Users;
using NhaDat24h.DataDto.Video;
using System;
using System.Collections.Generic;

namespace NhaDat24h.Service.Api.RealEstates
{
    public class RealEstateApiServices : ApiServiceBase, IRealEstateApiServices
    {
        public ResponseBase<int> InsertREProject(REPjInsertDto data)
        {
            var response = Post<REPjInsertDto, int>("RealEstate/REPj", data);
            return response;
        }
        public ResponseBase<int> InsertREResidence(REReInsertDto data)
        {
            var response = Post<REReInsertDto, int>("RealEstate/RERe", data);
            return response;
        }
        public ResponseBase<List<REForDepositDto>> GetListREForDeposit()
        {
            var response = Get<List<REForDepositDto>>("RealEstate/list-re-for-deposit");
            return response;
        }
        public ResponseBase<List<DepositREDto>> SearchListDepositRE(string? Name, byte? IdType, byte? Status, string? StartDate,
                                                string? EndDate, int PageIndex, int PageSize)
        {
            var response = Get<List<DepositREDto>>("RealEstate/search-deposit",
                new KeyValuePair<string, object>("Name", Name),
                new KeyValuePair<string, object>("IdType", IdType),
                new KeyValuePair<string, object>("Status", Status),
                new KeyValuePair<string, object>("StartDate", StartDate),
                new KeyValuePair<string, object>("EndDate", EndDate),
                new KeyValuePair<string, object>("PageIndex", PageIndex),
                new KeyValuePair<string, object>("PageSize", PageSize)
                );
            return response;
        }
        public ResponseBase<int> InsertDefaulDepositRE(int IdUser)
        {
            var response = Post<int, int>("RealEstate/defaul-deposit-re", IdUser);
            return response;
        }

        public ResponseBase<int> UpdateDepositRE(DepositREUpdateParam param)
        {
            var response = Put<DepositREUpdateParam, int>("RealEstate/deposit-re", param);
            return response;
        }

        public ResponseBase<bool> DeleteDepositRE(int IdUserRequest, int IdDeposit)
        {
            var response = Delete<bool>("RealEstate/deposit-re",
                new KeyValuePair<string, object>("IdUserRequest", IdUserRequest),
                new KeyValuePair<string, object>("IdDeposit", IdDeposit));
            return response;
        }
        public ResponseBase<bool> RemoveDepositRE(int IdUserRequest, int IdDeposit)
        {
            var response = Delete<bool>("RealEstate/remove-deposit-re",
                new KeyValuePair<string, object>("IdUserRequest", IdUserRequest),
                new KeyValuePair<string, object>("IdDeposit", IdDeposit));
            return response;
        }
        public ResponseBase<string> InsertMultiDoc(REDocDto data)
        {
            var response = Post<REDocDto, string>("RealEstate/multi-doc", data);
            return response;
        }
        public ResponseBase<int> InsertDoc(DocumentInsertDto data)
        {
            var response = Post<DocumentInsertDto, int>("RealEstate/doc", data);
            return response;
        }
        public ResponseBase<List<TbLoainha>> GetREType()
        {
            var response = Get<List<TbLoainha>>("RealEstate/Re-type");
            return response;
        }
        public ResponseBase<List<TbTinhthanh>> GetListProvince()
        {
            var response = Get<List<TbTinhthanh>>("RealEstate/list-province");
            return response;
        }
        public ResponseBase<List<ProvinceDto>> GetListProvinceOnPj(byte Style)
        {
            var response = Get<List<ProvinceDto>>("RealEstate/list-province-on-pj"
                , new KeyValuePair<string, object>("Style", Style));
            return response;
        }
        public ResponseBase<List<TbQuan>> GetListDistrict()
        {
            var response = Get<List<TbQuan>>("RealEstate/list-district");
            return response;
        }
        public ResponseBase<List<TbQuan>> GetListDistrictByIdProvince(int IdProvince)
        {
            var response = Get<List<TbQuan>>("RealEstate/list-district-by-province",
                new KeyValuePair<string, object>("IdProvince", IdProvince));
            return response;
        }
        public ResponseBase<List<TbPhuong>> GetListWards()
        {
            var response = Get<List<TbPhuong>>("RealEstate/list-wards");
            return response;
        }
        public ResponseBase<List<TbPhuong>> GetListWardsByIdDistrict(int IdDistrict)
        {
            var response = Get<List<TbPhuong>>("RealEstate/list-wards-by-district",
                new KeyValuePair<string, object>("IdDistrict", IdDistrict));
            return response;
        }
        public ResponseBase<List<WardsDto>> GetListWardsByMultiDistrict(List<int> IdDistrict)
        {
            var response = Post<List<int>, List<WardsDto>>("RealEstate/list-wards-by-multi-district", IdDistrict);
            return response;
        }
        public ResponseBase<List<TbKv>> GetListStreet()
        {
            var response = Get<List<TbKv>>("RealEstate/list-street");
            return response;
        }
        public ResponseBase<List<TbKv>> GetListStreetByIdWards(int IdWards)
        {
            var response = Get<List<TbKv>>("RealEstate/list-street-by-wards",
                new KeyValuePair<string, object>("IdWards", IdWards));
            return response;
        }
        public ResponseBase<List<DocumentDto>> GetListDocumentByIdRE(int IdRE, int DocType)
        {
            var response = Get<List<DocumentDto>>("RealEstate/list-doc",
                new KeyValuePair<string, object>("IdRE", IdRE),
                new KeyValuePair<string, object>("DocType", DocType));
            return response;
        }
        public ResponseBase<List<RealEstateDto>> GetListREByCtv(int IdCtv, int pageIndex, int pageSize)
        {
            var response = Get<List<RealEstateDto>>("RealEstate/list-re",
                new KeyValuePair<string, object>("IdCtv", IdCtv),
                new KeyValuePair<string, object>("pageIndex", pageIndex),
                new KeyValuePair<string, object>("pageSize", pageSize));
            return response;
        }
        public ResponseBase<List<RealEstateDto>> GetTopREinProvince(int IdProvince, int PageIndex, int PageSize, int Style)
        {
            var response = Get<List<RealEstateDto>>("RealEstate/top-re-province",
                new KeyValuePair<string, object>("IdProvince", IdProvince),
                new KeyValuePair<string, object>("PageIndex", PageIndex),
                new KeyValuePair<string, object>("PageSize", PageSize),
                new KeyValuePair<string, object>("Style", Style));
            return response;
        }
        public ResponseBase<List<RealEstateDto>> GetListSaveRE(int IdCtv, int pageIndex, int pageSize)
        {
            var response = Get<List<RealEstateDto>>("RealEstate/list-save-re",
                new KeyValuePair<string, object>("IdCtv", IdCtv));
            //new KeyValuePair<string, object>("pageIndex", pageIndex),
            //new KeyValuePair<string, object>("pageSize", pageSize));
            return response;
        }

        public ResponseBase<List<RealEstateDto>> GetListNewRE(int Style, int Status, int Number, bool isNews)
        {
            var response = Get<List<RealEstateDto>>("RealEstate/list-new-re",
                new KeyValuePair<string, object>("Style", Style),
                new KeyValuePair<string, object>("Status", Status),
                new KeyValuePair<string, object>("Number", Number),
                new KeyValuePair<string, object>("isNews", isNews));
            return response;
        }


        public ResponseBase<RealEstateGetOneDto> GetREById(int IdRE)
        {
            var response = Get<RealEstateGetOneDto>("RealEstate/re",
                new KeyValuePair<string, object>("IdRE", IdRE));
            return response;
        }
        public ResponseBase<int> UpdateRE(UpdateREParam param)
        {
            var response = Put<UpdateREParam, int>("RealEstate/re", param);
            return response;
        }
        public ResponseBase<string> UpdateStatusRE(UpdateStatusREParam param)
        {
            var response = Put<UpdateStatusREParam, string>("RealEstate/status-re", param);
            return response;
        }

        public ResponseBase<string> DeleteDoc(int idDoc, int IdCtvRequest)
        {
            var response = Delete<string>("RealEstate/doc",
                new KeyValuePair<string, object>("idDoc", idDoc),
                new KeyValuePair<string, object>("IdCtvRequest", IdCtvRequest));
            return response;
        }
        public ResponseBase<string> DeleteDocUrl(string url, int IdCtvRequest)
        {
            var response = Delete<string>("RealEstate/remove-doc",
                new KeyValuePair<string, object>("url", url),
                new KeyValuePair<string, object>("IdCtvRequest", IdCtvRequest));
            return response;
        }
        public ResponseBase<List<DocumentType>> GetListDocType()
        {
            var response = Get<List<DocumentType>>("RealEstate/list-doctype");
            return response;
        }
        public ResponseBase<List<CountDocType>> GetCountDocType(int IdRE)
        {
            var response = Get<List<CountDocType>>("RealEstate/count-doctype",
                new KeyValuePair<string, object>("IdRE", IdRE));
            return response;
        }
        public ResponseBase<bool> CheckDocExisting(string Url)
        {
            var response = Get<bool>("RealEstate/check-doc-existing",
                new KeyValuePair<string, object>("Url", Url));
            return response;
        }
        public ResponseBase<int> InsertSaveRE(SaveREInsertDto param)
        {
            var response = Post<SaveREInsertDto, int>("RealEstate/save-re", param);
            return response;
        }
        public ResponseBase<bool> DeleteSaveRE(int IdCtv, int IdRE)
        {
            var response = Delete<bool>("RealEstate/save-re",
                new KeyValuePair<string, object>("IdCtv", IdCtv),
                new KeyValuePair<string, object>("IdRE", IdRE));
            return response;
        }
        public ResponseBase<string> DeleteRE(int IdUser, int IdRE)
        {
            var response = Delete<string>("RealEstate/re",
                new KeyValuePair<string, object>("IdUser", IdUser),
                new KeyValuePair<string, object>("IdRE", IdRE));
            return response;
        }
        public ResponseBase<List<RealEstateDto>> SearchListRE(SearchListREParam param)
        {
            var response = Post<SearchListREParam, List<RealEstateDto>>("RealEstate/search-re", param);
            return response;
        }
        public ResponseBase<REReportDto> GetREReport(DateTime DateStart, DateTime DateEnd)
        {
            var response = Get<REReportDto>("RealEstate/re-report"
                , new KeyValuePair<string, object>("DateStart", DateStart)
                , new KeyValuePair<string, object>("DateEnd", DateEnd));
            return response;
        }

        public ResponseBase<int> InsertContactCtvRE(ContactCtvREInsertDto param)
        {
            var response = Post<ContactCtvREInsertDto, int>("RealEstate/contact-ctv-re", param);
            return response;
        }
        public ResponseBase<bool> RemoveContactCtvRE(int Id)
        {
            var response = Delete<bool>("RealEstate/remove-contact-ctv-re"
                , new KeyValuePair<string, object>("Id", Id));
            return response;
        }
        public Stream DownloadZipFolder(DownloadZipParam param)
        {
            var response = DownLoadFilePost("RealEstate/dowload-zip", param);
            return response.Result;
        }

        public ResponseBase<string> UpdateStatusDeposit(UpdateStatusDeponsitParam param)
        {
            var response = Put<UpdateStatusDeponsitParam, string>("RealEstate/status-deposit", param);
            return response;
        }

        public ResponseBase<DepositREUpdateParam> GetDeposit(int IdDeposit)
        {
            var response = Get<DepositREUpdateParam>("RealEstate/get-deposit-id"
                , new KeyValuePair<string, object>("Id", IdDeposit));
            return response;
        }
    }
}
