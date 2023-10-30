using System.ComponentModel.DataAnnotations;

namespace NhaDat24h.DataAccess.Interface
{
    public interface IBaseEntity
    {
        [Key]
        int Id { get; set; }
        int Deleted { get; set; }

    }
}