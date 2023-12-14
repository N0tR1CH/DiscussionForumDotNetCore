using DiscussionForum.Contracts;
using DiscussionForum.Repositories;

namespace DiscussionForum.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureRepositoryWrapper(this IServiceCollection services) =>
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
}
