using Microsoft.EntityFrameworkCore;
using PostMessengerService.Domain.Models;
using PostMessengerService.Infrastructure.Data;

namespace PostMessengerService.Infrastructure.Repositories
{
    public class PostRepository
    {
        private readonly PostDbContext _context;


        private bool _disposed;

        public PostRepository(PostDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostModel>> GetEntityListAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<PostModel> GetEntityByIdAsync(int postId)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(x => x.PostId == postId);
            if (post == null)
                throw new ArgumentNullException(nameof(post), "Post was null");
            return post;
        }


        public void PostEntity(PostModel post)
        {
            _context.Posts.Add(post);
        }


        public void UpdateEntity(PostModel post)
        {
            _context.Entry(post).State = EntityState.Modified;
        }

        public void DeleteEntity(int postId)
        {
            var post = _context.Posts.Find(postId);
            if (post == null)
                throw new ArgumentNullException(nameof(post), "Post was null");
            _context.Posts.Remove(post);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<PostModel>> GetEntityListAsyncByUserId(string username)
        {
            return await _context.Posts.Where(x => x.Username == username).ToListAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }
    }
}
