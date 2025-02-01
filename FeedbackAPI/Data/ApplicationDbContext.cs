using FeedbackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedbackAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Feedback> Feedbacks { get; set;  }

    }
}
