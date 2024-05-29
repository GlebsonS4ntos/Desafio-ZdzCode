using Desafio.Api.Data;
using Desafio.Api.Entities;
using Desafio.Api.Interfaces;

namespace Desafio.Api.Repositories
{
    public class RepositoryProduct : Repository<Product>, IRepositoryProduct 
    {
        public RepositoryProduct(DesafioContext context) : base(context)
        {
        }
    }
}
