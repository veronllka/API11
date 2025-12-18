using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Interfaces
{
    /// <summary>
    /// базовая абстракция CRUD операций для сущности
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// получить все записи
        /// </summary>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// получить запись по Id
        /// </summary>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// добавить запись
        /// </summary>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// обновить запись
        /// </summary>
        Task UpdateAsync(T entity);

        /// <summary>
        /// удалить запись по Id
        /// </summary>
        Task DeleteAsync(int id);
    }
}
