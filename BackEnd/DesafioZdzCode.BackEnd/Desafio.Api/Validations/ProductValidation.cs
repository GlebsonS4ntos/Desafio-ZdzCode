using Desafio.Api.Dtos.Product;
using FluentValidation;

namespace Desafio.Api.Validations
{
    public class ProductValidation : AbstractValidator<ProductDto>
    {
        public ProductValidation() 
        {
            RuleFor(p => p.Name).NotNull().WithMessage("O Nome do Produto n pode ser nulo.");

            RuleFor(p => p.Name).NotEmpty().When(p => p.Name != null).WithMessage("O Nome do Produto n pode ser vazio.")
                .MinimumLength(2).WithMessage("O Nome do Produto deve ter no minimo 2 caracteres.");

            RuleFor(p => p.Description).NotNull().WithMessage("A Descrição do Produto n pode ser nulo.");

            RuleFor(p => p.Description).NotEmpty().When(p => p.Description != null).WithMessage("A Descrição do Produto n pode ser vazio.")
                .MinimumLength(5).WithMessage("A Descrição do Produto deve ter no minimo 5 caracteres.");

            RuleFor(p => p.Price).GreaterThan(0.0m).WithMessage("O Preço do Produto deve ser maior que 0");

            RuleFor(p => p.ImageUrl).NotNull().WithMessage("O Produto deve conter uma Imagem.");

            RuleFor(p => p.ImageUrl).NotEmpty().When(p => p.ImageUrl != null).WithMessage("O Produto deve conter uma Imagem.")
                .MinimumLength(10).WithMessage("O Produto deve conter uma Imagem.");

            RuleFor(p => p.CategoryId).NotNull().WithMessage("O Produto precisa conter uma Categoria.");

            RuleFor(p => p.CategoryId).NotEmpty().When(p => p.CategoryId != null).WithMessage("O Produto precisa conter uma Categoria.");

            RuleFor(p => p.Quantity).GreaterThanOrEqualTo(0).WithMessage("O Produto deve ter 0 ou mais itens.");
        }
    }
}
