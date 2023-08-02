using Book.Service.Dtos.Accounts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Book.Service.Validations.Accounts
{
    public class RegisterDtoValidation : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidation()
        {
            RuleFor(x => x.UserName)
                .NotNull().NotEmpty()
                .MinimumLength(8)
                .MaximumLength(25);
            //RuleFor(x => x).Custom((x, context) =>
            //{
            //    Regex regex = new Regex("\"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\\\.[a-zA-Z]{2,}(\\\\.[a-zA-Z]{2,})?$\"");
            //    if (!regex.IsMatch(x.Email))
            //    {
            //        context.AddFailure("Email", "Email is not valid");
            //    }
            //});

            RuleFor(x => x.Password)
                .NotEmpty().NotNull()
                .MinimumLength(8)
                .MaximumLength(20);

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure("Confirmpassword", "Passwords doesnt match each other");
                }
            });
        }
    }
}
