using DiscussionForum.Contracts;
using DiscussionForum.Models;

namespace DiscussionForum.Repositories;

public class ForumRepository : RepositoryBase<Forum>, IForumRepository
{
    public ForumRepository(AppDbContext context)
        : base(context) { }
}
