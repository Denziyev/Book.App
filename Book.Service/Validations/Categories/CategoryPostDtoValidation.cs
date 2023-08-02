using Book.Service.Dtos.Categories;
using FluentValidation;


namespace Book.Service.Validations.Categories
{
    public class CategoryPostDtoValidation:AbstractValidator<CategoryPostDto>
    {
       public CategoryPostDtoValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name can not be empty")
                .NotNull().WithMessage("Name can not be null")
                .MinimumLength(5)
                .MaximumLength(30);
            
        }
    }
}
