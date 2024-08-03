namespace Net.Microservices.CleanArchitecture.Core.Application
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}