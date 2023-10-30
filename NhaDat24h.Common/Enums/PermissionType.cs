using System.ComponentModel;

namespace NhaDat24h.Common.Enums
{
	public enum PermissionType
	{
		[Description("Quyền xem danh sách nhân viên")]
		ListCtv = 12,
		[Description("Quyền xóa nhân viên")]
		DeleteCtv = 13,
		[Description("Quyền khóa nhân viên")]
		LockCtv = 14,
        [Description("Quyền Duyệt vị trí")]
        AcceptPosition = 15,
        [Description("Quyền duyệt quyền")]
        AcceptPermission = 16,
        [Description("Quyền xem thống kê")]
        Statistics = 17
    }
}
