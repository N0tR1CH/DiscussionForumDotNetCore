using DiscussionForum.Contracts;
using DiscussionForum.Models;

namespace DiscussionForum.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context)
        : base(context) { }
}
