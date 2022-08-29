using Microsoft.EntityFrameworkCore;
using PostMessengerService.Domain.Models;

namespace PostMessengerService.Infrastructure.Data;

public class PostDbContext : DbContext
{
    public PostDbContext(DbContextOptions<PostDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }


    public DbSet<PostModel> Posts => Set<PostModel>();
    public DbSet<CommentModel> Comments => Set<CommentModel>();
    public DbSet<LikeModel> Likes => Set<LikeModel>();
}