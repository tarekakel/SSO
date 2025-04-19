using Domain.Interfaces.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser, IAuditableEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        // Additional properties
        public bool IsActive { get; set; } = true;  // Default to true
        public bool IsDeleted { get; set; } = false; // Default to false
        public string? CreatedBy { get; set; } // User who created the record
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Record creation date
        public string? UpdatedBy { get; set; } // User who updated the record
        public DateTime? UpdatedDate { get; set; } // Record update date

        public string? DisplayName { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string? PreferredLanguage { get; set; }
    }
}
