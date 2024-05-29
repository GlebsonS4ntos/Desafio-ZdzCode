using AutoMapper;
using Desafio.Api.Dtos.Category;
using Desafio.Api.Entities;
using Desafio.Api.Interfaces;
using Desafio.Api.Validations;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.RepositoryCategory.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] Guid id)
        {
            var category = await _unitOfWork.RepositoryCategory.GetByIdAsync(id);
            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CategoryDto cat)
        {
            var validator = new CategoryValidation();
            var result = await validator.ValidateAsync(cat);

            if (!result.IsValid)
            {
                var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(erros);
            }

            var category = _mapper.Map<Category>(cat);

            await _unitOfWork.RepositoryCategory.AddAsync(category);
            await _unitOfWork.Commit();

            return Created();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategoryByIdAsync([FromRoute] Guid id, [FromBody] CategoryDto cat)
        {
            var category = await _unitOfWork.RepositoryCategory.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var validator = new CategoryValidation();
            var result = await validator.ValidateAsync(cat);

            if (!result.IsValid) 
            { 
                var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(erros);
            }

            _mapper.Map(cat, category);

            _unitOfWork.RepositoryCategory.Update(category);
            await _unitOfWork.Commit();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid id) 
        {
            var category = await _unitOfWork.RepositoryCategory.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _unitOfWork.RepositoryCategory.Delete(category);
            await _unitOfWork.Commit();

            return NoContent();
        }
    }
}
