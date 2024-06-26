﻿namespace Desafio.Api.Dtos.Product
{
    public class ProductDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
