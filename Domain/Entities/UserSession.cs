using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserSession : IAuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign key to ApplicationUser
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public string IPAddress { get; set; } = string.Empty;
        public string Browser { get; set; } = string.Empty;
        public string? OperatingSystem { get; set; }
        public string? Location { get; set; }

        public DateTime LoginTime { get; set; } = DateTime.UtcNow;
        public DateTime? LogoutTime { get; set; }

        public string? SessionToken { get; set; } // Optional, if using session tokens
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }

}
