namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbUser
    {
        public TbUser()
        {
            UserPermissions = new HashSet<UserPermission>();
        }

        public string IdU { get; set; } = null!;
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public int? IdTt { get; set; }
        public bool? Kh { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public bool? Vip { get; set; }
        /// <summary>
        /// 4
        /// </summary>
        public int? Group { get; set; }
        public string? Weburl { get; set; }
        public byte? Doituong { get; set; }
        public string? Address { get; set; }
        public DateTime? Ngaydangky { get; set; }
        public string? NickSkype { get; set; }
        public string? Signal { get; set; }
        public double? TaiKhoan { get; set; }
        public byte? Iosstatus { get; set; }
        public double? TaiKhoanThat { get; set; }
        public string? Ip { get; set; }
        public int? NumUpdateDaily { get; set; }
        public DateTime? LastVisited { get; set; }
        public string? NickYahoo { get; set; }
        public int? Point { get; set; }
        public int? GioiTinh { get; set; }
        public string? Avatar { get; set; }
        public string? Gps { get; set; }
        public string? Scriptchat { get; set; }
        public bool? IsSendEmail { get; set; }
        public bool? DailySendEmail { get; set; }
        public byte? Langid { get; set; }
        public DateTime? Dateupdateavatar { get; set; }
        public byte? IsKiemduyet { get; set; }
        public string? Appleid { get; set; }
        public bool? IsReady { get; set; }
        public string? UserVerify { get; set; }
        public bool? IsOnline { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }

}
