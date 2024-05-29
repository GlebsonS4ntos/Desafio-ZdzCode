namespace Desafio.Api.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public Guid PartentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
