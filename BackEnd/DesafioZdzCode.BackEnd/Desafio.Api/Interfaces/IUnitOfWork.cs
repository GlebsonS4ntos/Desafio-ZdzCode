namespace Desafio.Api.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryCategory RepositoryCategory { get; }
        IRepositoryProduct RepositoryProduct { get; }

        Task Commit();
    }
}
