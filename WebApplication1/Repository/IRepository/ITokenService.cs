namespace WebApplication1.Repository.IRepository
{
    public interface ITokenService
    {
        void GenerateTokenSetCookies(Guid userId, HttpResponse response);
    }
}
