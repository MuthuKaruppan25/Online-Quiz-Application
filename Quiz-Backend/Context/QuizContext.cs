using Microsoft.EntityFrameworkCore;
using Quiz.Models;

namespace Quiz.Contexts
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Attender> Attenders { get; set; } = null!;
        public DbSet<QuizData> Quizzes { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<QuizAttempt> QuizAttempts { get; set; } = null!;
        public DbSet<Answers> Answers { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>()
                .HasKey(u => u.guid);

            // Admin (1-1 with User)
            modelBuilder.Entity<Admin>()
                .HasKey(a => a.guid);

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.user)
                .WithOne(u => u.admin)
                .HasForeignKey<Admin>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Attender (1-1 with User)
            modelBuilder.Entity<Attender>()
                .HasKey(a => a.guid);

            modelBuilder.Entity<Attender>()
                .HasOne(a => a.user)
                .WithOne(u => u.attender)
                .HasForeignKey<Attender>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // QuizData (many quizzes created by admin)
            modelBuilder.Entity<QuizData>()
                .HasKey(q => q.Id);

            modelBuilder.Entity<QuizData>()
                .HasOne(q => q.admin)
                .WithMany(a => a.quizDatas)
                .HasForeignKey(q => q.adminId);

            modelBuilder.Entity<QuizData>()
                .HasOne(q => q.category)
                .WithMany(c => c.quizDatas)
                .HasForeignKey(q => q.categoryId);

            // Question (each question belongs to one quiz)
            modelBuilder.Entity<Question>()
                .HasKey(q => q.guid);

            modelBuilder.Entity<Question>()
                .HasOne(q =>q.quiz)
                .WithMany(q => q.questions)
                .HasForeignKey(k => k.QuizId);  

            // QuizAttempt
            modelBuilder.Entity<QuizAttempt>()
                .HasKey(a => a.guid);

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(a => a.attender)
                .WithMany(at => at.quizAttempts)
                .HasForeignKey(a => a.AttemptorId);

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(a => a.quizData)
                .WithMany(q => q.quizAttempts)
                .HasForeignKey(a => a.QuizId);

            // Answers
            modelBuilder.Entity<Answers>()
                .HasKey(ans => ans.guid);

            modelBuilder.Entity<Answers>()
                .HasOne(ans => ans.question)
                .WithMany()
                .HasForeignKey(ans => ans.QuestionId);

            modelBuilder.Entity<Answers>()
                .HasOne(ans => ans.attempt)
                .WithMany(a => a.answers)
                .HasForeignKey(ans => ans.quizAttemptId);

            // Category
            modelBuilder.Entity<Category>()
                .HasKey(c => c.guid);
        }
    }
}
