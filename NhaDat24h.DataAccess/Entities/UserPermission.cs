using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class UserPermission
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdPermision { get; set; }
        public bool Deleted { get; set; }

        public virtual Permission IdPermisionNavigation { get; set; } = null!;
        public virtual TbUser IdUserNavigation { get; set; } = null!;
    }
}
