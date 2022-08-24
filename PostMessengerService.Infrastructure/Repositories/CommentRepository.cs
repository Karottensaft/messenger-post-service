using Microsoft.EntityFrameworkCore;
using PostMessengerService.Domain.Models;
using PostMessengerService.Infrastructure.Data;

namespace PostMessengerService.Infrastructure.Repositories
{
    public class CommentRepository
    {
        private readonly PostDbContext _context;


        private bool _disposed;

        public CommentRepository(PostDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommentModel>> GetEntityListAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<CommentModel> GetEntityByIdAsync(int commentId)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(x => x.CommentId == commentId);
            if (comment == null)
                throw new ArgumentNullException(nameof(comment), "Comment was null");
            return comment;
        }

        public void PostEntity(CommentModel comment)
        {
            _context.Comments.Add(comment);
        }

        public void DeleteEntity(int commentId)
        {
            var comment = _context.Comments.Find(commentId);
            if (comment == null)
                throw new ArgumentNullException(nameof(comment), "Comment was null");
            _context.Comments.Remove(comment);
        }

        public void UpdateEntity(CommentModel comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<CommentModel>> GetEntityListByPostIdAsync(int postId)
        {
            return await _context.Comments.Where(x => x.PostId == postId).ToListAsync();
        }

        public void DeleteAllEntitiesByPostId(int postId)
        {
            _context.Comments.RemoveRange(_context.Comments.Where(x => x.PostId == postId));
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
