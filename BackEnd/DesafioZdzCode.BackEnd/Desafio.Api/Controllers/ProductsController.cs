using AutoMapper;
using Desafio.Api.Dtos.Product;
using Desafio.Api.Entities;
using Desafio.Api.Interfaces;
using Desafio.Api.Validations;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _unitOfWork.RepositoryProduct.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute] Guid id)
        {
            var product = await _unitOfWork.RepositoryProduct.GetByIdAsync(id);
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductDto prod)
        {
            var validator = new ProductValidation();
            var result = await validator.ValidateAsync(prod);

            if (!result.IsValid)
            {
                var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(erros);
            }

            var product = _mapper.Map<Product>(prod);

            await _unitOfWork.RepositoryProduct.AddAsync(product);
            await _unitOfWork.Commit();

            return Created();
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProductByIdAsync([FromRoute] Guid id, [FromBody] ProductDto prod)
        {
            var product = await _unitOfWork.RepositoryProduct.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var validator = new ProductValidation();
            var result = await validator.ValidateAsync(prod);

            if (!result.IsValid)
            {
                var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(erros);
            }

            _mapper.Map(prod, product);

            _unitOfWork.RepositoryProduct.Update(product);
            await _unitOfWork.Commit();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] Guid id)
        {
            var product = await _unitOfWork.RepositoryProduct.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.RepositoryProduct.Delete(product);
            await _unitOfWork.Commit();

            return NoContent();
        }
    }
}
