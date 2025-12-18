using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    /// <summary>
    /// репозиторий пользователей 
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// получить пользователя по Email
        /// </summary>
        Task<User?> GetByEmailAsync(string email);
    }
}
