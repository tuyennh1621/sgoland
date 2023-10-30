using NhaDat24h.Common;
using NhaDat24h.DataAccess.Entities;
using NhaDat24h.DataDto.Authen;
using NhaDat24h.DataDto.User;
using NhaDat24h.DataDto.Users;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NhaDat24h.Service.Api.Users
{
    public class UsersApiServices : ApiServiceBase, IUsersApiServices
    {
        public ResponseBase<int> InsertUser(UserInsertDataDto param)
        {
            var response = Post<UserInsertDataDto, int>("User/insert", param);
            return response;
        }
        public ResponseBase<string> UpdateUser(UserInsertDataDto param)
        {
            var response = Post<UserInsertDataDto, string>("User/update", param);
            return response;
        }
        public ResponseBase<string> UpdateAvatarUser(UpdateAvatarUserDto param)
        {
            var response = Post<UpdateAvatarUserDto, string>("User/update-avatar", param);
            return response;
        }

        public ResponseBase<TbUser> CheckUserByID(string Id)
        {
            var response = Get<TbUser>("user/check-user-by-id", new KeyValuePair<string, object>("Id", Id));
            return response;
        }

        public ResponseBase<TbUser> ChangeOnlineStatus(ChangeOnOffUser Param)
        {
            var response = Put<ChangeOnOffUser, TbUser>("user/onoff", Param);
            return response;
        }


        //author
        public ResponseBase<bool> CheckUserPermision(CheckPermisionParam param)
        {
            var response = Post<CheckPermisionParam, bool>("user/check-user-permision", param);
            return response;
        }
        public ResponseBase<List<PermissionDto>> GetUserPermission(int Userid)
        {
            var response = Get<List<PermissionDto>>("user/user-permission"
                , new KeyValuePair<string, object>("Userid", Userid));
            return response;
        }
        public ResponseBase<List<int>> GetUserPositionPermission(int IdUser)
        {
            var response = Get<List<int>>("user/position-permission"
                , new KeyValuePair<string, object>("IdUser", IdUser));
            return response;
        }
        public ResponseBase<List<PermissionDto>> GetListPermission()
        {
            var response = Get<List<PermissionDto>>("user/list-permission");
            return response;
        }
        public ResponseBase<string> SetUserPermission(SetPermisionParam param)
        {
            var response = Post<SetPermisionParam, string>("user/user-permission", param);
            return response;
        }

        //login
        public TbUser CheckUserByEmail(string email)
        {
            var response = Get<TbUser>("user/check-user-by-email", new KeyValuePair<string, object>("email", email));
            return response.Data;
        }
        public TbUser CheckUserByPhone(string phoneNumber)
        {
            var response = Get<TbUser>("user/check-user-by-phone", new KeyValuePair<string, object>("phoneNumber", phoneNumber));
            return response.Data;
        }
        public TbUser? CheckUserByAccount(string email, string password)
        {
            var response = Get<TbUser?>("user/check-user-by-account"
                , new KeyValuePair<string, object>("email", email)
                , new KeyValuePair<string, object>("password", password));
            return response.Data;
        }
        public ResponseBase<CheckRegisterOutput> CheckEmailExisting(string email)
        {
            var response = Get<CheckRegisterOutput>("user/check-email-existing"
                , new KeyValuePair<string, object>("email", email));
            return response;
        }
        public ResponseBase<CheckRegisterOutput> CheckPhoneExisting(string phone)
        {
            var response = Get<CheckRegisterOutput>("user/check-phone-existing"
                , new KeyValuePair<string, object>("phone", phone));
            return response;
        }
        public ResponseBase<CheckRegisterOutput> CheckRefidExisting(int refid)
        {
            var response = Get<CheckRegisterOutput>("user/check-refid-existing"
                , new KeyValuePair<string, object>("refid", refid));
            return response;
        }
        public TbUser RegisterUserPhone(TbUser user)
        {
            var response = Post<TbUser, TbUser>("user/register-user-phone", user);
            return response.Data;
        }

        public ResponseBase<List<UserSearchOutputDto>> GetListCtv(int idUser, int? idctv, string? searchkey, int? status, decimal? department, int idCompany,
             int pageSize, int pageIndex)
        {
            var response = Get<List<UserSearchOutputDto>>("user/list-ctv"
                , new KeyValuePair<string, object>("idUser", idUser)
                , new KeyValuePair<string, object>("idctv", idctv)
                , new KeyValuePair<string, object>("searchkey", searchkey)
                , new KeyValuePair<string, object>("status", status)
                , new KeyValuePair<string, object>("department", department)
                , new KeyValuePair<string, object>("idCompany", idCompany)
                , new KeyValuePair<string, object>("pageSize", pageSize)
                , new KeyValuePair<string, object>("pageIndex", pageIndex));
            return response;
        }
        public ResponseBase<List<UserSearchOutputDto>> GetListPartner(int idCtv, int pageSize, int pageIndex)
        {
            var response = Get<List<UserSearchOutputDto>>("user/list-partner"
                , new KeyValuePair<string, object>("idCtv", idCtv), new KeyValuePair<string, object>("pageSize", pageSize), new KeyValuePair<string, object>("pageIndex", pageIndex));
            return response;
        }

        public ResponseBase<string> UpdatePasswordUser(UpdatePasswordUserParam param)
        {
            var response = Put<UpdatePasswordUserParam, string>("user/change-password", param);
            return response;
        }


        public ResponseBase<List<GetHrReportData>> GetHrReport(CtvSgoGroupRequestDto request)
        {
            var response = Post<CtvSgoGroupRequestDto, List<GetHrReportData>>("Statistics/hr-report", request);
            return response;
        }
        public ResponseBase<List<GetReportBusinessData>> GetBusinessReport(ReportBusinessRequestDto request)
        {
            var response = Post<ReportBusinessRequestDto, List<GetReportBusinessData>>("Statistics/business-report", request);
            return response;
        }

        public ResponseBase<List<GetHrReportSynthesisData>> GetHrReportSynthesis(GHrReportSynthesisRequestDto request)
        {

            var response = Post<GHrReportSynthesisRequestDto, List<GetHrReportSynthesisData>>("Statistics/hr-report-synthesis", request);
            return response;
        }
      
        public Stream PostHrReportExcel(CtvSgoGroupRequestDto parram)
        {
            var response = DownLoadFilePost("Statistics/hr-report-excel", parram);
            return response.Result;
        }   

        public Stream PostHrReportSynthesisExcel(GHrReportSynthesisRequestDto parram)
        {
            var response = DownLoadFilePost("Statistics/hr-report-synthesis-excel", parram);
            return response.Result;
        }
        public Stream PostBusinessReportExcel(ReportBusinessRequestDto parram)
        {
            var response = DownLoadFilePost("Statistics/business-report-excel", parram);
            return response.Result;
        }

        public Stream GetListCtvReportExcel(SearchCtvParam request)
        {
            var response = DownLoadFilePost("ctv/list-search-excel", request);
            return response.Result;
        }

    }
}
