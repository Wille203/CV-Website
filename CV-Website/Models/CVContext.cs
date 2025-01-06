using Microsoft.EntityFrameworkCore;

namespace CV_Website.Models
{
    public class CVContext : DbContext
    {
        public CVContext(DbContextOptions<CVContext> options) : base(options)
        {
        }

        public DbSet<CV> CVs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Reciver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReciverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Alice", Password = "password123", Address = "123 Wonderland St", Email = "alice@example.com", Private = false },
                new User { UserId = 2, Name = "Bob", Password = "securepassword", Address = "456 Nowhere Ave", Email = "bob@example.com", Private = true },
                new User { UserId = 3, Name = "Charlie", Password = "charlie123", Address = "789 Cloud Ln", Email = "charlie@example.com", Private = false }
    );

            modelBuilder.Entity<CV>().HasData(
                new CV { CVId = 1, UserId = 1 },
                new CV { CVId = 2, UserId = 2 },
                new CV { CVId = 3, UserId = 3 }
            );

            modelBuilder.Entity<Skills>().HasData(
                new Skills { SkillsId = 1, Name = "C#" },
                new Skills { SkillsId = 2, Name = "ASP.NET" },
                new Skills { SkillsId = 3, Name = "JavaScript" },
                new Skills { SkillsId = 4, Name = "SQL" }
            );

            modelBuilder.Entity<Education>().HasData(
                new Education { EducationId = 1, Name = "Bachelor in Computer Science" },
                new Education { EducationId = 2, Name = "Master in Software Engineering" },
                new Education { EducationId = 3, Name = "Diploma in Web Development" }
            );

            modelBuilder.Entity<Experience>().HasData(
                new Experience { ExperienceId = "1", Name = "Software Developer at XYZ Corp" },
                new Experience { ExperienceId = "2", Name = "Full Stack Developer at ABC Inc" },
                new Experience { ExperienceId = "3", Name = "Data Analyst at DataWorks" }
            );

            modelBuilder.Entity<Message>().HasData(
                new Message { MessageId = 1, SenderId = 1, SenderName = "Alice", ReciverId = 2, MessageText = "Hello Bob, how are you?", Read = false },
                new Message { MessageId = 2, SenderId = 2, SenderName = "Bob", ReciverId = 1, MessageText = "Hi Alice! I'm good, thank you!", Read = true },
                new Message { MessageId = 3, SenderId = 1, SenderName = "Alice", ReciverId = 3, MessageText = "Hello Charlie, nice to meet you!", Read = false },
                new Message { MessageId = 4, SenderId = 3, SenderName = "Charlie", ReciverId = 1, MessageText = "Hi Alice, great to connect!", Read = false },
                new Message { MessageId = 5, SenderId = 2, SenderName = "Bob", ReciverId = 3, MessageText = "Hey Charlie, are you available for a call?", Read = true },
                new Message { MessageId = 6, SenderId = 3, SenderName = "Charlie", ReciverId = 2, MessageText = "Hi Bob, yes I am available. Let's talk!", Read = false }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, Title = "Personal Portfolio", Description = "A portfolio website to showcase my projects." },
                new Project { ProjectId = 2, Title = "Task Manager App", Description = "A web application to manage tasks efficiently." },
                new Project { ProjectId = 3, Title = "E-Commerce Platform", Description = "An online platform for buying and selling products." }
            );
        }
    }
}
