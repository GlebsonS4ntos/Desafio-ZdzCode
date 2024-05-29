namespace Desafio.Api.Entities
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
