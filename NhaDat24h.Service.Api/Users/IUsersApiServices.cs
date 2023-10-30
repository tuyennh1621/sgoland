using NhaDat24h.Common;
using NhaDat24h.DataAccess.Entities;
using NhaDat24h.DataDto.Authen;
using NhaDat24h.DataDto.User;
using NhaDat24h.DataDto.Users;
using System.Web.Mvc;

namespace NhaDat24h.Service.Api.Users
{
    public interface IUsersApiServices
    {
        public ResponseBase<int> InsertUser(UserInsertDataDto param);
        public ResponseBase<string> UpdateUser(UserInsertDataDto param);
        public ResponseBase<string> UpdateAvatarUser(UpdateAvatarUserDto param);
        public ResponseBase<TbUser> CheckUserByID(string Id);
        public ResponseBase<bool> CheckUserPermision(CheckPermisionParam param);
        public ResponseBase<List<PermissionDto>> GetUserPermission(int Userid);
        public ResponseBase<List<PermissionDto>> GetListPermission();
        public ResponseBase<List<int>> GetUserPositionPermission(int IdUser);

        public ResponseBase<string> SetUserPermission(SetPermisionParam param);
        public TbUser CheckUserByEmail(string email);
        public TbUser CheckUserByPhone(string phoneNumber);
        public TbUser? CheckUserByAccount(string email, string password);
        public ResponseBase<CheckRegisterOutput> CheckEmailExisting(string email);
        public ResponseBase<CheckRegisterOutput> CheckPhoneExisting(string phone);
        public ResponseBase<CheckRegisterOutput> CheckRefidExisting(int refid);
        public TbUser RegisterUserPhone(TbUser user);
        public ResponseBase<List<UserSearchOutputDto>> GetListCtv(int idUser, int? idctv, string? searchkey, int? status, decimal? department, int idCompany,
             int pageSize, int pageIndex);
        public ResponseBase<List<UserSearchOutputDto>> GetListPartner(int idCtv, int pageSize, int pageIndex);
        public ResponseBase<TbUser> ChangeOnlineStatus(ChangeOnOffUser Param);
        public ResponseBase<string> UpdatePasswordUser(UpdatePasswordUserParam param);

        ResponseBase<List<GetHrReportData>> GetHrReport(CtvSgoGroupRequestDto request);

        public ResponseBase<List<GetHrReportSynthesisData>>GetHrReportSynthesis(GHrReportSynthesisRequestDto request);

        ResponseBase<List<GetReportBusinessData>> GetBusinessReport(ReportBusinessRequestDto request);

        public Stream PostHrReportSynthesisExcel(GHrReportSynthesisRequestDto parram);
        public Stream PostHrReportExcel(CtvSgoGroupRequestDto parram);
        public Stream PostBusinessReportExcel(ReportBusinessRequestDto parram);
        public Stream GetListCtvReportExcel(SearchCtvParam request);
    }
}
