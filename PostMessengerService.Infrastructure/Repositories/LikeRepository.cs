using Microsoft.EntityFrameworkCore;
using PostMessengerService.Domain.Models;
using PostMessengerService.Infrastructure.Data;

namespace PostMessengerService.Infrastructure.Repositories;

public class LikeRepository : ILikeRepository<LikeModel>
{
    private readonly PostDbContext _context;


    private bool _disposed;

    public LikeRepository(PostDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LikeModel>> GetEntityListAsync()
    {
        return await _context.Likes.ToListAsync();
    }

    public async Task<LikeModel> GetEntityByIdAsync(int likeId)
    {
        var like = await _context.Likes.SingleOrDefaultAsync(x => x.LikeId == likeId);
        if (like == null)
            throw new ArgumentNullException(nameof(like), "Like was null");
        return like;
    }


    public void PostEntity(LikeModel like)
    {
        _context.Likes.Add(like);
    }

    public void DeleteEntity(int likeId)
    {
        var like = _context.Likes.Find(likeId);
        if (like == null)
            throw new ArgumentNullException(nameof(like), "Like was null");
        _context.Likes.Remove(like);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<LikeModel> GetEntityByUsernameAndPostIdAsync(string username, int postId)
    {
        var like = await _context.Likes.SingleOrDefaultAsync(x => (x.Username == username) & (x.PostId == postId));
        return like;
    }

    public async Task<IEnumerable<LikeModel>> GetEntityListAsyncByPostId(int postId)
    {
        return await _context.Likes.Where(x => x.PostId == postId).ToListAsync();
    }

    public void DeleteAllEntitiesByPostId(int postId)
    {
        _context.Likes.RemoveRange(_context.Likes.Where(x => x.PostId == postId));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();
        _disposed = true;
    }
}