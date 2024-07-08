using Microsoft.EntityFrameworkCore;

namespace QuizApp.Data.Context
{
    public class QuizDbContext:DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options):base(options)
        {
            
        }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
