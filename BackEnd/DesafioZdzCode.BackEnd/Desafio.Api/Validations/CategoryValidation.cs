using Desafio.Api.Dtos.Category;
using FluentValidation;

namespace Desafio.Api.Validations
{
    public class CategoryValidation : AbstractValidator<CategoryDto>
    {
        public CategoryValidation() 
        { 
            RuleFor(c => c.Name).NotNull().WithMessage("O Nome da Categoria n pode ser nulo.");

            RuleFor(c => c.Name).NotEmpty().When(c => c.Name != null).WithMessage("O Nome da Categoria n pode ser vazio.")
                .MinimumLength(2).WithMessage("O Nome da Categoria deve ter no minimo 2 caracteres.");
        }
    }
}
