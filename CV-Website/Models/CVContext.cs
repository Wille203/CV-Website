using CV_Website.Models;
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

        public DbSet<Project> Project { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
            .HasOne(p => p.Creator)
            .WithMany()
            .HasForeignKey(p => p.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Users) 
                .WithMany(u => u.Project)  
                .UsingEntity(j => j.ToTable("ProjectUsers"));


            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Alice", Password = "password123", Address = "Drottninggatan 10", Email = "alice@example.com", Private = false, PhoneNumber = "+46701234567" },
                new User { UserId = 2, Name = "Bob", Password = "securepassword", Address = "Storgatan 5", Email = "bob@example.com", Private = true, PhoneNumber = "0701234567" },
                new User { UserId = 3, Name = "Charlie", Password = "charlie123", Address = "Kungsgatan 18", Email = "charlie@example.com", Private = false, PhoneNumber = "+46761234567" },
                new User { UserId = 4, Name = "Dave", Password = "dave456", Address = "Östra Långgatan 22", Email = "dave@example.com", Private = false, PhoneNumber = "0761234567" },
                new User { UserId = 5, Name = "Eve", Password = "eve789", Address = "Västra Vallgatan 4", Email = "eve@example.com", Private = true, PhoneNumber = "+46731234567" },
                new User { UserId = 6, Name = "Frank", Password = "frank101", Address = "Kyrkogatan 8", Email = "frank@example.com", Private = false, PhoneNumber = "0731234567" }
            );


            modelBuilder.Entity<CV>().HasData(
                new CV { CVId = 1, UserId = 1 },
                new CV { CVId = 2, UserId = 2 },
                new CV { CVId = 3, UserId = 3 },
                new CV { CVId = 4, UserId = 4 },
                new CV { CVId = 5, UserId = 5 },
                new CV { CVId = 6, UserId = 6 }
            );

            modelBuilder.Entity<Skills>().HasData(
                new Skills { SkillsId = 1, Name = "C#" },
                new Skills { SkillsId = 2, Name = "ASP.NET" },
                new Skills { SkillsId = 3, Name = "JavaScript" },
                new Skills { SkillsId = 4, Name = "SQL" },
                new Skills { SkillsId = 5, Name = "Python" },
                new Skills { SkillsId = 6, Name = "React" },
                new Skills { SkillsId = 7, Name = "Docker" },
                new Skills { SkillsId = 8, Name = "Kubernetes" },
                new Skills { SkillsId = 9, Name = "AWS" },
                new Skills { SkillsId = 10, Name = "Machine Learning" }
            );
            

            modelBuilder.Entity<Education>().HasData(
                new Education { EducationId = 1, Name = "Bachelor in Computer Science" },
                new Education { EducationId = 2, Name = "Master in Software Engineering" },
                new Education { EducationId = 3, Name = "Diploma in Web Development" },
                new Education { EducationId = 4, Name = "PhD in Artificial Intelligence" },
                new Education { EducationId = 5, Name = "Bachelor in Data Science" },
                new Education { EducationId = 6, Name = "Master in Machine Learning" }
            );

            modelBuilder.Entity<Experience>().HasData(
                new Experience { ExperienceId = 1, Name = "Software Developer at XYZ Corp" },
                new Experience { ExperienceId = 2, Name = "Full Stack Developer at ABC Inc" },
                new Experience { ExperienceId = 3, Name = "Data Analyst at DataWorks" },
                new Experience { ExperienceId = 4, Name = "DevOps Engineer at Cloudify" },
                new Experience { ExperienceId = 5, Name = "Backend Developer at SecureSoft" },
                new Experience { ExperienceId = 6, Name = "Frontend Developer at BrightIdeas" }
            );

            modelBuilder.Entity<Message>().HasData(
                new Message { MessageId = 1, SenderId = 1, SenderName = "Alice", ReceiverId = 2, MessageText = "Hello Bob, how are you?", Read = false },
                new Message { MessageId = 2, SenderId = 2, SenderName = "Bob", ReceiverId = 1, MessageText = "Hi Alice! I'm good, thank you!", Read = true },
                new Message { MessageId = 3, SenderId = 1, SenderName = "Alice", ReceiverId = 3, MessageText = "Hello Charlie, nice to meet you!", Read = false },
                new Message { MessageId = 4, SenderId = 3, SenderName = "Charlie", ReceiverId = 1, MessageText = "Hi Alice, great to connect!", Read = false },
                new Message { MessageId = 5, SenderId = 2, SenderName = "Bob", ReceiverId = 3, MessageText = "Hey Charlie, are you available for a call?", Read = true },
                new Message { MessageId = 6, SenderId = 3, SenderName = "Charlie", ReceiverId = 2, MessageText = "Hi Bob, yes I am available. Let's talk!", Read = false },
                new Message { MessageId = 7, SenderId = 4, SenderName = "Dave", ReceiverId = 5, MessageText = "Eve, I need your help with the project.", Read = false },
                new Message { MessageId = 8, SenderId = 5, SenderName = "Eve", ReceiverId = 4, MessageText = "Sure Dave, let me know the details.", Read = true },
                new Message { MessageId = 9, SenderId = 6, SenderName = "Frank", ReceiverId = 1, MessageText = "Alice, your portfolio is impressive!", Read = false },
                new Message { MessageId = 10, SenderId = 1, SenderName = "Alice", ReceiverId = 4, MessageText = "Dave, what do you think about the design?", Read = true },
                new Message { MessageId = 11, SenderId = 4, SenderName = "Dave", ReceiverId = 1, MessageText = "Alice, I love it! Great job.", Read = false },
                new Message { MessageId = 12, SenderId = 3, SenderName = "Charlie", ReceiverId = 5, MessageText = "Eve, can you send me the updated files?", Read = false }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, Title = "Personal Portfolio", Description = "A portfolio website to showcase my projects.", Information = "Demonstrates my skills and previous works.", CreatorId = 1 },
                new Project { ProjectId = 2, Title = "Task Manager App", Description = "A web application to manage tasks.", Information = "Tracks and manages daily tasks effectively.", CreatorId = 2 },
                new Project { ProjectId = 3, Title = "E-Commerce Platform", Description = "An online platform for buying and selling products.", Information = "Enables secure online transactions.", CreatorId = 3 },
                new Project { ProjectId = 4, Title = "AI Assistant", Description = "A chatbot powered by AI.", Information = "Provides personalized assistance to users.", CreatorId = 4 },
                new Project { ProjectId = 5, Title = "Fitness Tracker", Description = "An app to monitor fitness activities.", Information = "Helps users track their workouts and progress.", CreatorId = 5 },
                new Project { ProjectId = 6, Title = "Online Learning Platform", Description = "A platform for online courses.", Information = "Offers a variety of courses across domains.", CreatorId = 6 }
            );

            modelBuilder.Entity<CV>()
                .HasMany(c => c.Skills)
                .WithMany(s => s.CV)
                .UsingEntity(j => j.HasData(
                    new { CVId = 1, SkillsId = 1 },
                    new { CVId = 1, SkillsId = 2 },
                    new { CVId = 2, SkillsId = 3 },
                    new { CVId = 3, SkillsId = 4 },
                    new { CVId = 4, SkillsId = 5 },
                    new { CVId = 5, SkillsId = 6 },
                    new { CVId = 6, SkillsId = 7 }
                ));

            modelBuilder.Entity<CV>()
                .HasMany(c => c.Education)
                .WithMany(e => e.CV)
                .UsingEntity(j => j.HasData(
                    new { CVId = 1, EducationId = 1 },
                    new { CVId = 2, EducationId = 2 },
                    new { CVId = 3, EducationId = 3 },
                    new { CVId = 4, EducationId = 4 },
                    new { CVId = 5, EducationId = 5 },
                    new { CVId = 6, EducationId = 6 }
                ));

            modelBuilder.Entity<CV>()
                .HasMany(c => c.Experience)
                .WithMany(e => e.CV)
                .UsingEntity(j => j.HasData(
                    new { CVId = 1, ExperienceId = 1 },
                    new { CVId = 2, ExperienceId = 2 },
                    new { CVId = 3, ExperienceId = 3 },
                    new { CVId = 4, ExperienceId = 4 },
                    new { CVId = 5, ExperienceId = 5 },
                    new { CVId = 6, ExperienceId = 6 }
                ));

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Users)
                .WithMany(u => u.Project)
                .UsingEntity(j => j.HasData(
                    new { ProjectId = 1, UsersUserId = 1 },
                    new { ProjectId = 1, UsersUserId = 2 },
                    new { ProjectId = 2, UsersUserId = 3 },
                    new { ProjectId = 3, UsersUserId = 4 },
                    new { ProjectId = 4, UsersUserId = 5 },
                    new { ProjectId = 5, UsersUserId = 6 }
                ));




        }
    }
}
