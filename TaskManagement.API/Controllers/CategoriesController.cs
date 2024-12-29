using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Dtos;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repository.Contract;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDtos = _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpPost("CreateCategory")]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.AddAsync(category);

            var createdCategoryDto = _mapper.Map<CategoryDto>(category);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategoryDto.Id }, createdCategoryDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest("Category ID mismatch.");
            }

            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found.");
            }

            _mapper.Map(categoryDto, existingCategory);
            await _categoryRepository.UpdateAsync(existingCategory);

            var updatedCategoryDto = _mapper.Map<CategoryDto>(existingCategory);
            return Ok(updatedCategoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            await _categoryRepository.DeleteAsync(id);
            return Ok("Category deleted successfully.");
        }
    }
}
