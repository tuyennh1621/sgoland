namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblctvonline
    {
        public Tblctvonline()
        {
            CtvDepartments = new HashSet<CtvDepartment>();
        }

        public int Id { get; set; }
        public string? FullName { get; set; }
        public int? IdcongTy { get; set; }
        public string? Socmtnd { get; set; }
        public string? Ngaysinh { get; set; }
        public string? Diachi { get; set; }
        public int? IdQuan { get; set; }
        public int? IdKv { get; set; }
        public DateTime? DateIn { get; set; }
        public int? Intlevel { get; set; }
        public string? Mobile { get; set; }
        public string? Gioithieu { get; set; }
        public int? Refid { get; set; }
        public string? Frontimageid { get; set; }
        public string? Backimageid { get; set; }
        public byte Status { get; set; }
        public decimal DepartmentId { get; set; }
        public string? Avatar2 { get; set; }
        public string? CvUrl { get; set; }
        public string? SearchKey { get; set; }
        public string? Position { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<CtvDepartment> CtvDepartments { get; set; }
    }
}
