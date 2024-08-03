namespace Net.Microservices.CleanArchitecture.Core.Application.Queries.Base
{
    public interface IPagedQuery
    {
        public int PageSize { get; set; }
        public int Start { get; set; }
    }
}
