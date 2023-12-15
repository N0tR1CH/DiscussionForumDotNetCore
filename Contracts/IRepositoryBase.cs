using System.Linq.Expressions;

namespace DiscussionForum.Contracts;

public interface IRepositoryBase<T>
    where T : class
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveAsync();
}
