namespace Desafio.Api.Dtos.Category
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PartentCategoryId { get; set; }
    }
}