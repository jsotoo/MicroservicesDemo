namespace Net.Microservices.CleanArchitecture.Core.Application
{
    public interface IPasswordGeneratorService
    {
        string GenerateRandomPassword();
    }
}
