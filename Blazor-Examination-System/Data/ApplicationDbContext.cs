using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Blazor_Examination_System.Models;

namespace Blazor_Examination_System.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<Exam> Exams { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Choice> Choices { get; set; } = null!;
        public DbSet<StudentExam> StudentExams { get; set; } = null!;
        public DbSet<StudentAnswer> StudentAnswers { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Subject
            builder.Entity<Subject>(entity =>
            {
                entity.Property(s => s.Name)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(s => s.Description)
                    .HasMaxLength(500);
            });

            // Configure Exam
            builder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.Title)
                    .HasMaxLength(300)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(1000);

                entity.HasOne(e => e.Subject)
                    .WithMany(s => s.Exams)
                    .HasForeignKey(e => e.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.SubjectId);
            });

            // Configure Question
            builder.Entity<Question>(entity =>
            {
                entity.Property(q => q.Text)
                    .HasMaxLength(2000)
                    .IsRequired();

                entity.HasOne(q => q.Exam)
                    .WithMany(e => e.Questions)
                    .HasForeignKey(q => q.ExamId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(q => q.ExamId);
            });

            // Configure Choice
            builder.Entity<Choice>(entity =>
            {
                entity.Property(c => c.Text)
                    .HasMaxLength(1000)
                    .IsRequired();

                entity.HasOne(c => c.Question)
                    .WithMany(q => q.Choices)
                    .HasForeignKey(c => c.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(c => c.QuestionId);
            });

            // === StudentExam Configuration ===
            builder.Entity<StudentExam>(entity =>
            {
                entity.HasOne(se => se.Student)
                    .WithMany(u => u.StudentExams)
                    .HasForeignKey(se => se.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(se => se.Exam)
                    .WithMany(e => e.StudentExams)
                    .HasForeignKey(se => se.ExamId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasIndex(se => new { se.StudentId, se.ExamId }).IsUnique();
            });

            // === StudentAnswer Configuration ===
            builder.Entity<StudentAnswer>(entity =>
            {
                entity.HasOne(sa => sa.StudentExam)
                    .WithMany(se => se.StudentAnswers)
                    .HasForeignKey(sa => sa.StudentExamId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sa => sa.Question)
                    .WithMany(q => q.StudentAnswers)
                    .HasForeignKey(sa => sa.QuestionId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(sa => sa.SelectedChoice)
                    .WithMany()
                    .HasForeignKey(sa => sa.SelectedChoiceId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // ============== SEED DATA WITH HasData ==============
            // Roles: static, safe for HasData
            builder.Entity<IdentityRole>().HasData(SeedData.GetRoles());

            // Users & UserRoles: seeded at runtime (password hash is dynamic)
            // See Program.cs → SeedUsersAsync method

            // Academic data: static, safe for HasData
            builder.Entity<Subject>().HasData(SeedData.GetSubjects());
            builder.Entity<Exam>().HasData(SeedData.GetExams());
            builder.Entity<Question>().HasData(SeedData.GetQuestions());
            builder.Entity<Choice>().HasData(SeedData.GetChoices());
        }
    }
}
