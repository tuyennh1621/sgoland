using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;

namespace NhaDat24h.DataDto.Company
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }

        public string? Avatar { get; set; }
        public IFormFile fileAvt { get; set; }
        public bool SelectedCompany(int value)
        {
            if (value == Id)
                return true;
            else
                return false;
        }
    }
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? IdSuperiors { get; set; }
        public int? IdCompany { get; set; }
        public string? CompanyName { get; set; }
        public byte? Level { get; set; }
        public byte? Class { get; set; }
        public string? Description { get; set; }

    }
    public class PositionDepartmentDto
    {
        public int Id { get; set; }
        public int IdPosition { get; set; }
        public int IdDepartment { get; set; }
        public string? Description { get; set; }
        public string? DepartmentName { get; set; }
        public string? CompanyName { get; set; }
        public int? IdSuperior { get; set; }
    }
    public class CompanyInsertParam
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
    }
    public class CompanyUpdateAvatarParam
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
    public class CompanyUpdateParam
    {
        public int Id { get; set; }
        public int IdUserRequest { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
    }
    public class DepartmentInsertParam
    {
        public string Name { get; set; } = null!;
        public int? IdSuperiors { get; set; }
        public int? IdCompany { get; set; }
        public string? Description { get; set; }
        public byte? Level { get; set; }
    }
    public class DepartmentUpdateParam
    {
        public int Id { get; set; }
        public int IdUserRequest { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
    public class PositonDepartmentInsertParam
    {
        public int IdPosition { get; set; }
        public int IdDepartment { get; set; }
        public string? Description { get; set; }
    }
    public class CtvPositonInsertParam
    {
        public int IdCtv { get; set; }
        public int IdUser { get; set; }
        public List<int> IdPositionDepartment { get; set; }
    }
    public class GetDepartmentInCompanyByCtvParam
    {
        public int IdCtv { get; set; }
        public List<int>? IdCompany { get; set; }
    }
    public class StructureDepartmentDto
    {
        public int IdDepartment { get; set; }
        public int IdCompany { get; set; }
        public string? Description { get; set; }
        public string? DepartmentName { get; set; }

        public List<DepartmentDto> ListDepartment { get; set; }

    }
    public class StructurePositionDto
    {
        public int IdDepartment { get; set; }
        public int IdPositionDep { get; set; }
        public string? Description { get; set; }
        public string? DepartmentName { get; set; }

        public List<PositionDepartmentDto> ListPositionExiting { get; set; }
        public List<PositionDto> ListPosition { get; set; }

    }
    public class PositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte Class { get; set; }
        public string? Description { get; set; }
    }
    public class GetStillPositionInDepartmentParam
    {
        public int IdCtv { get; set; }
        public List<int> IdDepartment { get; set; }
    }
    public class CtvPositionDepartmentDto
    {
        public int Id { get; set; }
        public int IdCtv { get; set; }
        public int IdPositionDepartment { get; set; }
        public int? IdCompany { get; set; }
        public int? IdDepartment { get; set; }
        public int? IdPosition { get; set; }
        public byte? ClassPosition { get; set; }
        public bool Apply { get; set; }

    }
}
