using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Security.Dtos
{

    public record SystemDto(Guid? Id, string? Name, string? Description);

    public record CreateSystemDto(string Name, string? Description);

    public record UpdateSystemDto(Guid Id, string Name, string? Description);

    public record ModuleDto(Guid Id, string Name, Guid SystemId);

    public record CreateModuleDto(string Name, Guid SystemId);

    public record UpdateModuleDto(Guid Id, string Name, Guid SystemId);



    public record PageDto(Guid Id, string Name, Guid ModuleId);

    public record CreatePageDto(string Name, Guid ModuleId);

    public record UpdatePageDto(Guid Id, string Name, Guid ModuleId);



    public record PermissionDto(Guid Id, string Code, string Description, Guid PageId);

    public record CreatePermissionDto(string Code, string Description, Guid PageId);

    public record UpdatePermissionDto(Guid Id, string Code, string Description, Guid PageId);


}
