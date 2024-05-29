using Desafio.Api.Data;
using Desafio.Api.Entities;
using Desafio.Api.Interfaces;

namespace Desafio.Api.Repositories
{
    public class RepositoryCategory : Repository<Category>, IRepositoryCategory
    {
        public RepositoryCategory(DesafioContext context) : base(context)
        {
        }
    }
}
