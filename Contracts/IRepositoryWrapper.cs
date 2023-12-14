namespace DiscussionForum.Contracts;

public interface IRepositoryWrapper
{
    ICategoryRepository Category { get; }
    IForumRepository Forum { get; }
    void Save();
}
