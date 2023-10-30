using Microsoft.AspNetCore.Http;
using NhaDat24h.DataAccess.Entities;

namespace NhaDat24h.DataDto.RealEstates
{
    public class DepositREUpdateParam
    {
        public int IdDeposit { get; set; }
        public string Name { get; set; }
        public int IdRE { get; set; }
        public decimal DepositValue { get; set; }
        public decimal TotalValue { get; set; }

        public DateTime DepositDate { get; set; }
        public DateTime PaymentDeadline { get; set; }


        public string? BuyerName { get; set; } = null!;
        public string? BuyerPhone { get; set; } = null!;
        public string? BuyerIdNum { get; set; } = null!;
        public int IdUserRequest { get; set; }
        public string? Detail { get; set; }
        public string? ContractImg { get; set; }
        public byte Status { get; set; }
        public int IdTypeRE { get; set; }
        public decimal? Bonus { get; set; }
    }
    public class REForDepositDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
    public class DepositIndexModel
    {
        public List<REForDepositDto> ListRE { get; set; }
        public List<DepositREDto> ListDeposit { get; set; }
    }
    public class ModelDeposit
    {
        public List<DepositREDto> ListDeposit { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

    }
    public class DepositREDto
    {
        public int Id { get; set; }
        public int IdRE { get; set; }
        public string? REImg { get; set; }
        public string? NameRE { get; set; }
        public string? Name { get; set; }
        public string? TypeRE { get; set; }
        public DateTime DepositDate { get; set; }
        public DateTime PaymentDeadline { get; set; }
        public int Ngayhethan
        {
            get
            {
                TimeSpan span = PaymentDeadline.Subtract(DepositDate);
                return span.Days;
            }
        }
        public double HetHan_curent
        {
            get
            {
                TimeSpan curent = PaymentDeadline.Subtract(System.DateTime.Now);
                return curent.Days;
            }
        }

        public double Percen
        {
            get
            {
                if (HetHan_curent > 0)
                {
                    var ccc = HetHan_curent * 100 / Ngayhethan;
                    return 100 - Math.Round(ccc, 0);
                }
                else
                {
                    return 100;
                }
            }
        }
        public byte Status { get; set; }
        public decimal DepositValue { get; set; }
        public decimal TotalValue { get; set; }
        public int Total { get; set; }
        public decimal? Bonus { get; set; }
        public bool Selected(byte value)
        {
            if (value == Status)
                return true;
            else
                return false;
        }
        public int IdTypeRE { get; set; }
        public string? Detail { get; set; }
        public string? ContractImg { get; set; }
        public List<string> Imgs
        {
            get
            {
                if (ContractImg != null)
                {
                    return ContractImg.Split(";").ToList();
                }
                else
                {
                    return null;
                }
            }
        }

    }
    public class AddDepositForm
    {
        public List<REForDepositDto> ListRE { get; set; }
        public int IdDeposit { get; set; }
        public string Name { get; set; }
        public int IdRE { get; set; }
        public DateTime DepositDate { get; set; }
        public DateTime PaymentDeadline { get; set; }
        public decimal DepositValue { get; set; }
        public decimal TotalValue { get; set; }
        public decimal? Bonus { get; set; }
        public string Detail { get; set; }
        public List<IFormFile> Contract { get; set; }
        public byte Status { get; set; }
        public bool Selected(byte value)
        {
            if (value == Status)
                return true;
            else
                return false;
        }
        public int IdTypeRE { get; set; }
        public int IdUser { get; set; }
        public string? ContractImg { get; set; }

        public List<string> Imgs
        {
            get
            {
                if (ContractImg != null)
                {
                    return ContractImg.Split(";").ToList();
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public class UpdateStatusDeponsitParam
    {
        public int IdUserRequest { get; set; }
        public int Id { get; set; }
        public byte Status { get; set; }
        //public string? Description { get; set; }
    }

    public class EditDepositForm
    {
        public List<REForDepositDto> ListRE { get; set; }
        public int IdDeposit { get; set; }
        public string Name { get; set; }
        public int IdRE { get; set; }
        public DateTime DepositDate { get; set; }
        public DateTime PaymentDeadline { get; set; }
        public decimal DepositValue { get; set; }
        public decimal TotalValue { get; set; }
        public decimal? Bonus { get; set; }
        public string Detail { get; set; }
        public List<IFormFile> Contract { get; set; }

        public byte Status { get; set; }

        public bool Selected(byte value)
        {
            if (value == Status)
                return true;
            else
                return false;
        }
        public int IdTypeRE { get; set; }
        public int IdUser { get; set; }
        public string? ContractImg { get; set; }
        public List<string> Imgs
        {

            get
            { 
                if(ContractImg != null)
                {
                    return ContractImg.Split(";").ToList();
                }
                else
                {
                    return null;
                } 
            }    
               


        }

    }
}
