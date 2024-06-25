using WebApplication1.Model;

namespace WebApplication1.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> GetUsersExceptAsync(Guid id);
    }
}
