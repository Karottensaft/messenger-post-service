namespace PostMessengerService.Infrastructure.Repositories;

internal interface ICommentRepository<T> : IDisposable
    where T : class
{
    Task<IEnumerable<T>> GetEntityListAsync();

    Task<T> GetEntityByIdAsync(int commentId);

    void PostEntity(T comment);

    void DeleteEntity(int commentId);

    Task<IEnumerable<T>> GetEntityListByPostIdAsync(int postId);

    void DeleteAllEntitiesByPostId(int postId);
}