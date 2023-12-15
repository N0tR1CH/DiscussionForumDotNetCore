using System.Linq.Expressions;
using DiscussionForum.Contracts;
using DiscussionForum.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForum.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : class
{
    protected AppDbContext AppDbContext { get; set; }

    public RepositoryBase(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
    }

    public IQueryable<T> FindAll() => AppDbContext.Set<T>().AsNoTracking();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
        AppDbContext.Set<T>().Where(expression).AsNoTracking();

    public Task CreateAsync(T entity) => AppDbContext.Set<T>().AddAsync(entity).AsTask();

    public void Update(T entity) => AppDbContext.Set<T>().Update(entity);

    public void Delete(T entity) => AppDbContext.Set<T>().Remove(entity);

    public async Task SaveAsync() => await AppDbContext.SaveChangesAsync();
}
