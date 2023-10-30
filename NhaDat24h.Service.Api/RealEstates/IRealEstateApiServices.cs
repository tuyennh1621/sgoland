
using NhaDat24h.Common;
using NhaDat24h.DataAccess.Entities;
using NhaDat24h.DataDto.RealEstates;
using NhaDat24h.DataDto.Users;
using NhaDat24h.DataDto.Video;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NhaDat24h.Service.Api.RealEstates
{
    public interface IRealEstateApiServices
    {
        public ResponseBase<int> InsertREProject(REPjInsertDto data);
        public ResponseBase<int> InsertREResidence(REReInsertDto data);
        public ResponseBase<string> InsertMultiDoc(REDocDto data);
        public ResponseBase<int> InsertDoc(DocumentInsertDto data);
        public ResponseBase<List<TbLoainha>> GetREType();
        public ResponseBase<List<TbTinhthanh>> GetListProvince();
        public ResponseBase<List<ProvinceDto>> GetListProvinceOnPj(byte Style);

        public ResponseBase<List<TbQuan>> GetListDistrict();
        public ResponseBase<List<TbPhuong>> GetListWards();
        public ResponseBase<List<TbKv>> GetListStreet();
        public ResponseBase<List<DocumentDto>> GetListDocumentByIdRE(int IdRE, int DocType);
        public ResponseBase<List<TbKv>> GetListStreetByIdWards(int IdWards);
        public ResponseBase<List<TbPhuong>> GetListWardsByIdDistrict(int IdDistrict);
        public ResponseBase<List<WardsDto>> GetListWardsByMultiDistrict(List<int> IdDistrict);

        public ResponseBase<List<TbQuan>> GetListDistrictByIdProvince(int IdProvince);
        public ResponseBase<List<RealEstateDto>> GetListREByCtv(int IdCtv, int pageIndex, int pageSize);
        public ResponseBase<List<RealEstateDto>> GetTopREinProvince(int IdProvince, int PageIndex, int PageSize, int Style);

        public ResponseBase<RealEstateGetOneDto> GetREById(int IdRE);
        public ResponseBase<int> UpdateRE(UpdateREParam param);
        public ResponseBase<string> DeleteDoc(int idDoc, int IdCtvRequest);
        public ResponseBase<string> DeleteDocUrl(string url, int IdCtvRequest);
        public ResponseBase<List<DocumentType>> GetListDocType();
        public ResponseBase<List<CountDocType>> GetCountDocType(int IdRE);

        public ResponseBase<bool> CheckDocExisting(string Url);
        public ResponseBase<int> InsertSaveRE(SaveREInsertDto param);
        public ResponseBase<bool> DeleteSaveRE(int IdCtv, int IdRE);
        public ResponseBase<List<RealEstateDto>> SearchListRE(SearchListREParam param);
        public ResponseBase<string> DeleteRE(int IdUser, int IdRE);
        public ResponseBase<string> UpdateStatusRE(UpdateStatusREParam param);
        public ResponseBase<List<RealEstateDto>> GetListSaveRE(int IdCtv, int pageIndex, int pageSize);
        public ResponseBase<REReportDto> GetREReport(DateTime DateStart, DateTime DateEnd);
        public ResponseBase<List<RealEstateDto>> GetListNewRE(int Style, int Status, int Number, bool isNews);
        public ResponseBase<int> InsertContactCtvRE(ContactCtvREInsertDto param);
        public ResponseBase<bool> RemoveContactCtvRE(int Id);

        public ResponseBase<int> InsertDefaulDepositRE(int IdUser);
        public ResponseBase<int> UpdateDepositRE(DepositREUpdateParam param);
        public ResponseBase<bool> DeleteDepositRE(int IdUserRequest, int IdDeposit);
        public ResponseBase<bool> RemoveDepositRE(int IdUserRequest, int IdDeposit);
        public ResponseBase<List<REForDepositDto>> GetListREForDeposit();
        public ResponseBase<List<DepositREDto>> SearchListDepositRE(string? Name, byte? IdType, byte? Status, string? StartDate,
                                        string? EndDate, int PageIndex, int PageSize);
        public Stream DownloadZipFolder(DownloadZipParam param);

        public ResponseBase<string> UpdateStatusDeposit(UpdateStatusDeponsitParam param);

        public ResponseBase<DepositREUpdateParam> GetDeposit(int IdDeposit);

    }
}
