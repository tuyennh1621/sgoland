using NhaDat24h.Common;
using NhaDat24h.DataAccess.Entities;
using NhaDat24h.DataDto.Company;
using NhaDat24h.DataDto.Ctv;
using System.Collections.Generic;

namespace NhaDat24h.Service.Api.Company
{
    public class CompanyApiServices : ApiServiceBase, ICompanyApiServices
    {
		public ResponseBase<List<CompanyDto>> GetListCompanyByCtv(int IdCtv)
        {
			var response = Get<List<CompanyDto>>("group/list-company"
				, new KeyValuePair<string, object>("IdCtv", IdCtv));
			return response;
		}
        public ResponseBase<List<DepartmentDto>> GetDepartmentInCompanyByCtv(GetDepartmentInCompanyByCtvParam param)
        {
            var response = Post<GetDepartmentInCompanyByCtvParam, List<DepartmentDto>>("group/list-department-company-ctv",param
                );
            return response;
        }
        public ResponseBase<List<DepartmentDto>> GetDepartmentInCompany(List<int> IdCompany)
        {
            var response = Post<List<int>,List<DepartmentDto>>("group/list-department-company", IdCompany
                );
            return response;
        }
        public ResponseBase<List<PositionDepartmentDto>> GetStillPositionInDepartment(GetStillPositionInDepartmentParam param)
        {
            var response = Post<GetStillPositionInDepartmentParam, List<PositionDepartmentDto>>("group/still-position-department", param
                );
            return response;
        }
        public ResponseBase<List<PositionDepartmentDto>> GetStillPositionInCompanyLowLevel(int IdCompany)
        {
            var response = Get<List<PositionDepartmentDto>>("group/still-position-company-lowlv"
                , new KeyValuePair<string, object>("IdCompany", IdCompany)
                );
            return response;
        }
        public ResponseBase<List<PositionDto>> GetListPosition(int IdUserRequest, int IdCompany)
        {
            var response = Get<List<PositionDto>>("group/list-position-by-ctv"
                , new KeyValuePair<string, object>("IdUserRequest", IdUserRequest)
                , new KeyValuePair<string, object>("IdCompany", IdCompany)

                );
            return response;
        }
        public ResponseBase<List<CtvPositionDepartmentDto>> GetPositionCtv(int IdCtv)
        {
            var response = Get<List<CtvPositionDepartmentDto>>("group/position"
                , new KeyValuePair<string, object>("IdCtv", IdCtv)

                );
            return response;
        }
        public ResponseBase<int> InsertCompany(CompanyInsertParam param)
        {
            var response = Post<CompanyInsertParam,int>("group/company",param);
            return response;
        }
        public ResponseBase<int> UpdateAvatarCompany(CompanyUpdateAvatarParam param)
        {
            var response = Put<CompanyUpdateAvatarParam, int>("group/avatar-company", param);
            return response;
        }
        public ResponseBase<int> UpdateCompany(CompanyUpdateParam param)
        {
            var response = Put<CompanyUpdateParam, int>("group/company", param);
            return response;
        }
        public ResponseBase<int> InsertDepartment(DepartmentInsertParam param)
        {
            var response = Post<DepartmentInsertParam, int>("group/department", param);
            return response;
        }
        public ResponseBase<int> UpdateDepartment(DepartmentUpdateParam param)
        {
            var response = Put<DepartmentUpdateParam, int>("group/department", param);
            return response;
        }
        public ResponseBase<int> SoftDeleteDepartment(int Id, int IdUserRequest)
        {
            var response = Delete<int>("group/department",
                new KeyValuePair<string, object>("Id", Id)
                , new KeyValuePair<string, object>("IdUserRequest", IdUserRequest));
            return response;
        }
        public ResponseBase<int> InsertPositionDepartment(PositonDepartmentInsertParam param)
        {
            var response = Post<PositonDepartmentInsertParam, int>("group/position-dep", param);
            return response;
        }
        public ResponseBase<string> InsertCtvPosition(CtvPositonInsertParam param)
        {
            var response = Post<CtvPositonInsertParam, string>("group/ctv-position", param);
            return response;
        }
        public ResponseBase<int> DeletePositionDepartment(int Id, int IdUser)
        {
            var response = Delete<int>("group/position-dep"
                , new KeyValuePair<string, object>("Id", Id)
                , new KeyValuePair<string, object>("IdUser", IdUser));
            return response;
        }
        public ResponseBase<int> DeleteCtvPosition(int Id, int IdUser)
        {
            var response = Delete<int>("group/ctv-position"
                , new KeyValuePair<string, object>("Id", Id)
                , new KeyValuePair<string, object>("IdUser", IdUser));
            return response;
        }
        public ResponseBase<string> GetEmailOfSuperior(int IdDepartment)
        {
            var response = Get<string>("group/email-superior"
                , new KeyValuePair<string, object>("IdDepartment", IdDepartment));
            return response;
        }
        public ResponseBase<bool> IsSuperior(int IdUser, int IdCtv)
        {
            var response = Get<bool>("group/is-superior"
                , new KeyValuePair<string, object>("IdUser", IdUser)
                , new KeyValuePair<string, object>("IdCtv", IdCtv)
                );
            return response;
        }
    }
}
