using DiscussionForum.Contracts;
using DiscussionForum.Models;

namespace DiscussionForum.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private AppDbContext _context;
    private ICategoryRepository _category;
    private IForumRepository _forum;

    public RepositoryWrapper(AppDbContext context)
    {
        _context = context;
    }

    public ICategoryRepository Category
    {
        get
        {
            if (_category == null)
            {
                _category = new CategoryRepository(_context);
            }
            return _category;
        }
    }

    public IForumRepository Forum
    {
        get
        {
            if (_forum == null)
            {
                _forum = new ForumRepository(_context);
            }
            return _forum;
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
