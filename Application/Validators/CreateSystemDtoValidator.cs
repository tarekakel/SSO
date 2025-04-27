using FluentValidation;
using Shared.Security.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateSystemDtoValidator: AbstractValidator<CreateSystemDto>
    {
        public CreateSystemDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");
        }
    }
}
