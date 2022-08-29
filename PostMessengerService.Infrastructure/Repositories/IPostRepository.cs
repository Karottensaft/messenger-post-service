namespace PostMessengerService.Infrastructure.Repositories;

internal interface IPostRepository<T> : IDisposable
    where T : class
{
    Task<IEnumerable<T>> GetEntityListAsync();

    Task<T> GetEntityByIdAsync(int postId);

    void PostEntity(T post);

    void DeleteEntity(int postId);

    Task<IEnumerable<T>> GetEntityListAsyncByUserId(string username);
}