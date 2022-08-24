using Microsoft.EntityFrameworkCore;
using PostMessengerService.Infrastructure.Data;

namespace PostMessengerService.Infrastructure.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly PostDbContext _context;

        private bool _disposed;

        public UnitOfWork(DbContextOptions<PostDbContext> options)
        {
            _context = new PostDbContext(options);
            PostRepository = new PostRepository(_context);
            CommentRepository = new CommentRepository(_context);
            LikeRepository = new LikeRepository(_context);
        }

        public PostRepository PostRepository { get; }

        public CommentRepository CommentRepository { get; }

        public LikeRepository LikeRepository { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
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
