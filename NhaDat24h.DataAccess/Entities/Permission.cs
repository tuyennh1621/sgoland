using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            UserPermissions = new HashSet<UserPermission>();
        }

        public int Id { get; set; }
        public int Code { get; set; }
        public string Description { get; set; } = null!;
        public bool Deleted { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
