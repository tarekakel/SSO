using Domain.Interfaces.Generic;
using Domain.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Security
{
    public class Permission : IAuditableEntity
    {
        public Guid Id { get; set; }
        public required string Code { get; set; } // e.g. "User.Create", "Report.View"
        public required string Name { get; set; }
        public string? Description { get; set; }
        public PermissionType Type { get; set; } // Enum: Page, Action, Button
        public Guid? PageId { get; set; }
        public Page? Page { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }

}
