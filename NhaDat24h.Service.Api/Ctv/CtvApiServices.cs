using NhaDat24h.Common;
using NhaDat24h.DataAccess.Entities;
using NhaDat24h.DataDto.Ctv;
using NhaDat24h.DataDto.Users;

namespace NhaDat24h.Service.Api.Ctv
{
    public class CtvApiServices : ApiServiceBase, ICtvApiServices
    {

        public ResponseBase<int> InsertCtv(CtvInsertDataDto param)
        {
            var response = Post<CtvInsertDataDto, int>("Ctv/insert", param);
            return response;
        }
        public ResponseBase<int> UpdateCtv(CtvEditDto param)
        {
            var response = Post<CtvEditDto, int>("Ctv/update", param);
            return response;
        }
        public ResponseBase<string> UpdateCtvId(UpdateImageIdCtvDto param)
        {
            var response = Post<UpdateImageIdCtvDto, string>("Ctv/update-ctvimage", param);
            return response;
        }
        public ResponseBase<string> UpdateStatusCtv(UpdateStatusCtvDto param)
        {
            var response = Put<UpdateStatusCtvDto, string>("Ctv/update-status", param);
            return response;
        }
		public ResponseBase<string> UpdateDepartmentCtv(UpdateDepartmentCtvDto param)
		{
			var response = Put<UpdateDepartmentCtvDto, string>("Ctv/ctv-department", param);
			return response;
		}
		public ResponseBase<string> AddNewDepartmentCtv(UpdateDepartmentCtvDto param)
		{
			var response = Post<UpdateDepartmentCtvDto, string>("Ctv/ctv-department", param);
			return response;
		}
        public ResponseBase<string> UpdateCtvLonLat(CtvUpdateLonLatDataDto param)
        {
            var response = Put<CtvUpdateLonLatDataDto, string>("Ctv/update-lonlat", param);
            return response;
        }
        public ResponseBase<int> InsertCountertimeOnline(CounterTimeOnlineDto param)
        {
            var response = Post<CounterTimeOnlineDto, int>("Ctv/insert-countertimeonline", param);
            return response;
        }
        public ResponseBase<List<DepartmentCtvDto>> GetListDepartmentCtv(int IdCompany,int IdUser)
		{
			var response = Get<List<DepartmentCtvDto>>("Ctv/list-department"
                , new KeyValuePair<string, object>("IdCompany", IdCompany)
				, new KeyValuePair<string, object>("IdUser", IdUser));
			return response;
		}
		public ResponseBase<CtvEditDto> GetCtv(int idCtv)
		{
			var response = Get<CtvEditDto>("user/ctv"
                , new KeyValuePair<string, object>("idCtv", idCtv));
			return response;
		}
		public ResponseBase<string> DeleteCtv(int idUser, int idCtv, string description)
		{
			var response = Delete<string>("ctv/ctv"
				, new KeyValuePair<string, object>("idUser", idUser)
				, new KeyValuePair<string, object>("idCtv", idCtv)
                , new KeyValuePair<string, object>("description", description)
                );
			return response;
		}
		public ResponseBase<string> LockCtv(int idUser, int idCtv, string description)
		{
			var response = Delete<string>("ctv/lock-ctv"
				, new KeyValuePair<string, object>("idUser", idUser)
				, new KeyValuePair<string, object>("idCtv", idCtv)
                , new KeyValuePair<string, object>("description", description)
                );
			return response;
		}
        public ResponseBase<List<int>> GetCountCtvByStatus()
        {
            var response = Get<List<int>>("ctv/count-ctv-by-status");
            return response;
        }
        public ResponseBase<HRReportDto> GetHRReport(DateTime DateStart, DateTime DateEnd, int IdCompany)
        {
            var response = Get<HRReportDto>("ctv/hr-report"
                , new KeyValuePair<string, object>("DateStart", DateStart)
                , new KeyValuePair<string, object>("DateEnd", DateEnd)
                , new KeyValuePair<string, object>("IdCompany", IdCompany));
            return response;
        }

        public ResponseBase<List<CtvInCompanyDto>> GetListCtvByIdCompany(int IdCtv)
        {
            var response = Get<List<CtvInCompanyDto>>("ctv/ctv-in-company"
                , new KeyValuePair<string, object>("IdCtv", IdCtv));
            return response;
        }

        public ResponseBase<CtvSearchDto> GetListCtv(int idUser, int? idctv, string? searchkey, int? status, int? idCompany, int? idDepartment,
            int? numdayoff, int pageSize, int pageIndex)
        {
            var response = Get<CtvSearchDto>("ctv/list-ctv"
                , new KeyValuePair<string, object>("idUser", idUser)
                , new KeyValuePair<string, object>("idctv", idctv)
                , new KeyValuePair<string, object>("searchkey", searchkey)
                , new KeyValuePair<string, object>("status", status)
                , new KeyValuePair<string, object>("idDepartment", idDepartment)
                , new KeyValuePair<string, object>("numdayoff", numdayoff)
                , new KeyValuePair<string, object>("idCompany", idCompany)
                , new KeyValuePair<string, object>("pageSize", pageSize)
                , new KeyValuePair<string, object>("pageIndex", pageIndex));
            return response;
        }
        public ResponseBase<List<CtvTopTimeOnlineDto>> GetTopCtvTimeOnline(int idCompany = 0, int period = -30, int pageIndex = 1, int pageSize = 20)
        {
            var response = Get<List<CtvTopTimeOnlineDto>>("ctv/list-top-ctv-onltime"
                , new KeyValuePair<string, object>("period", period)
                , new KeyValuePair<string, object>("idCompany", idCompany)
                , new KeyValuePair<string, object>("pageSize", pageSize)
                , new KeyValuePair<string, object>("pageIndex", pageIndex));
            return response;
        }
    }
}
