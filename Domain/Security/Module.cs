using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Security
{
    public class Module : IAuditableEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid SystemId { get; set; }
        public SystemEntity? System { get; set; }
        public ICollection<Page> Pages { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }

}
