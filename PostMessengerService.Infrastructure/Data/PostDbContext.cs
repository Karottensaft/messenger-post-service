using Microsoft.EntityFrameworkCore;
using PostMessengerService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostMessengerService.Infrastructure.Data
{
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
}
