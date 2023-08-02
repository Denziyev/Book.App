using Book.Service.Dtos.Accounts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Book.Service.Validations.Accounts
{
    public class LoginDtoValidation:AbstractValidator<LoginDto>
    {
        public LoginDtoValidation()
        {
            RuleFor(x => x.Username)
              .NotNull().NotEmpty()
              .MinimumLength(8)
              .MaximumLength(25);

            RuleFor(x => x.Password)
         .NotEmpty().NotNull()
         .MinimumLength(8)
         .MaximumLength(20);
        }
    }
}
