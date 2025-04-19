using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public record RegisterRequestDto(
     string Email,
     string Password,
     string FirstName,
     string LastName
 );
    public record LoginRequestDto(
  string Email,
  string Password
);



}
