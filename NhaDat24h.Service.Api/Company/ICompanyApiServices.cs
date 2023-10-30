using NhaDat24h.Common;
using NhaDat24h.DataDto.Company;
using NhaDat24h.DataDto.Ctv;

namespace NhaDat24h.Service.Api.Company
{
    public interface ICompanyApiServices
    {
        public ResponseBase<List<CompanyDto>> GetListCompanyByCtv(int IdCtv);
        public ResponseBase<List<DepartmentDto>> GetDepartmentInCompanyByCtv(GetDepartmentInCompanyByCtvParam param);
        public ResponseBase<List<DepartmentDto>> GetDepartmentInCompany(List<int> IdCompany);
        public ResponseBase<List<PositionDepartmentDto>> GetStillPositionInDepartment(GetStillPositionInDepartmentParam param);
        public ResponseBase<List<PositionDto>> GetListPosition(int IdUserRequest, int IdCompany);
        public ResponseBase<List<CtvPositionDepartmentDto>> GetPositionCtv(int IdCtv);

        public ResponseBase<int> InsertCompany(CompanyInsertParam param);
        public ResponseBase<int> UpdateAvatarCompany(CompanyUpdateAvatarParam param);
        public ResponseBase<int> UpdateCompany(CompanyUpdateParam param);
        public ResponseBase<int> InsertDepartment(DepartmentInsertParam param);
        public ResponseBase<int> UpdateDepartment(DepartmentUpdateParam param);
        public ResponseBase<int> SoftDeleteDepartment(int Id, int IdUserRequest);
        public ResponseBase<int> InsertPositionDepartment(PositonDepartmentInsertParam param);
        public ResponseBase<string> InsertCtvPosition(CtvPositonInsertParam param);
        public ResponseBase<int> DeletePositionDepartment(int Id, int IdUser);
        public ResponseBase<int> DeleteCtvPosition(int Id, int IdUser);
        public ResponseBase<string> GetEmailOfSuperior(int IdDepartment);
        public ResponseBase<bool> IsSuperior(int IdUser, int IdCtv);

        public ResponseBase<List<PositionDepartmentDto>> GetStillPositionInCompanyLowLevel(int IdCompany);


    }
}
