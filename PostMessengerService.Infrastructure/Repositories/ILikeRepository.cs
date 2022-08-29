namespace PostMessengerService.Infrastructure.Repositories;

internal interface ILikeRepository<T> : IDisposable
    where T : class
{
    Task<IEnumerable<T>> GetEntityListAsync();

    Task<T> GetEntityByIdAsync(int likeId);

    void PostEntity(T like);

    public void DeleteEntity(int likeId);

    Task<T> GetEntityByUsernameAndPostIdAsync(string username, int postId);

    Task<IEnumerable<T>> GetEntityListAsyncByPostId(int postId);

    void DeleteAllEntitiesByPostId(int postId);
}