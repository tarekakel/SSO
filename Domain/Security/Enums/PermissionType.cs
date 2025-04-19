using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Security.Enums
{
    public enum PermissionType
    {
        /// <summary>
        /// Represents the entire system level permission (e.g., a complete application or subsystem).
        /// </summary>
        System = 1,

        /// <summary>
        /// Represents a module (a logical section like "User Management", "Reporting").
        /// </summary>
        Module = 2,

        /// <summary>
        /// Represents a UI page or screen (or API resource).
        /// </summary>
        Page = 3,

        /// <summary>
        /// Represents a specific action (e.g., Create, Edit, Delete).
        /// </summary>
        Action = 4,

        /// <summary>
        /// Represents a button control in the UI that should be secured.
        /// </summary>
        Button = 5,

        /// <summary>
        /// Represents an API endpoint or method-level permission.
        /// </summary>
        Api = 6
    }
}
