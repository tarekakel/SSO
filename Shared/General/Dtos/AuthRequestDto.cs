using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.General.Dtos
{
    public record RegisterRequestDto(
     string Email,
     string Password,
     string FirstName,
     string LastName,
     string confirm
 );
    public record LoginRequestDto(
          string Email,
          string Password,
          bool Remember
);



}
