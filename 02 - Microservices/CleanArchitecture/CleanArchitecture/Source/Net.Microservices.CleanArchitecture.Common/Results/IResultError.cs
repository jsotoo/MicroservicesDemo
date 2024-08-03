namespace Net.Microservices.CleanArchitecture.Common
{
    public interface IResultError
    {
        string Error { get; }
        string Code { get; }
    }
}