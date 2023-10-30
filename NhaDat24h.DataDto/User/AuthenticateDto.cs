namespace NhaDat24h.DataDto.Authen
{
    public class AuthenticateRequest
    {
        public int UserId { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
    public class AuthenticateResponse
    {
        //public User? User { get; set; }
        public string Token { get; set; }
        public string Status { get; set; }
    }
    public class CtvSgoGroupRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }


    }
    public class PermissionDto
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Description { get; set; } = null!;
    }
    public class AuthorityModel
    {
        public int IdCtv { get; set; }
        public string NameCtv { get; set; }
        public List<PermissionDto> ListPermission { get; set; }
        public List<int>? listpermissionSelected { get; set; }
    }
}
