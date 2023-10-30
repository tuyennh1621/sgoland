using NhaDat24h.Common;
using NhaDat24h.DataDto.Ctv;
using NhaDat24h.DataDto.Users;

namespace NhaDat24h.Service.Api.Ctv
{
    public interface ICtvApiServices
    {
        public ResponseBase<int> InsertCtv(CtvInsertDataDto param);
        public ResponseBase<int> UpdateCtv(CtvEditDto param);
        public ResponseBase<string> UpdateCtvId(UpdateImageIdCtvDto param);
        public ResponseBase<string> UpdateStatusCtv(UpdateStatusCtvDto param);
        public ResponseBase<List<DepartmentCtvDto>> GetListDepartmentCtv(int IdCompany, int IdUser);
        public ResponseBase<string> UpdateDepartmentCtv(UpdateDepartmentCtvDto param);
        public ResponseBase<CtvEditDto> GetCtv(int idCtv);
        public ResponseBase<string> DeleteCtv(int idUser, int idCtv, string description);
        public ResponseBase<string> LockCtv(int idUser, int idCtv, string description);
        public ResponseBase<string> AddNewDepartmentCtv(UpdateDepartmentCtvDto param);
        public ResponseBase<List<int>> GetCountCtvByStatus();
        public ResponseBase<HRReportDto> GetHRReport(DateTime DateStart, DateTime DateEnd, int IdCompany);
        public ResponseBase<List<CtvInCompanyDto>> GetListCtvByIdCompany(int IdCtv);
        public ResponseBase<string> UpdateCtvLonLat(CtvUpdateLonLatDataDto param);
        public ResponseBase<int> InsertCountertimeOnline(CounterTimeOnlineDto param);
        public ResponseBase<List<CtvTopTimeOnlineDto>> GetTopCtvTimeOnline(int idCompany = 0, int period = -30, int pageIndex = 1, int pageSize = 20);


        public ResponseBase<CtvSearchDto> GetListCtv(int idUser, int? idctv, string? searchkey, int? status, int? idCompany, int? idDepartment,
            int? numdayoff, int pageSize, int pageIndex);
    }
}
