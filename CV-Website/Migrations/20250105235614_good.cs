using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CV_Website.Migrations
{
    /// <inheritdoc />
    public partial class good : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Education",
                columns: new[] { "EducationId", "Name" },
                values: new object[,]
                {
                    { 1, "Bachelor in Computer Science" },
                    { 2, "Master in Software Engineering" },
                    { 3, "Diploma in Web Development" }
                });

            migrationBuilder.InsertData(
                table: "Experience",
                columns: new[] { "ExperienceId", "Name" },
                values: new object[,]
                {
                    { "1", "Software Developer at XYZ Corp" },
                    { "2", "Full Stack Developer at ABC Inc" },
                    { "3", "Data Analyst at DataWorks" }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "A portfolio website to showcase my projects.", "Personal Portfolio" },
                    { 2, "A web application to manage tasks efficiently.", "Task Manager App" },
                    { 3, "An online platform for buying and selling products.", "E-Commerce Platform" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillsId", "Name" },
                values: new object[,]
                {
                    { 1, "C#" },
                    { 2, "ASP.NET" },
                    { 3, "JavaScript" },
                    { 4, "SQL" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Email", "Name", "Password", "Private" },
                values: new object[,]
                {
                    { 1, "123 Wonderland St", "alice@example.com", "Alice", "password123", false },
                    { 2, "456 Nowhere Ave", "bob@example.com", "Bob", "securepassword", true },
                    { 3, "789 Cloud Ln", "charlie@example.com", "Charlie", "charlie123", false }
                });

            migrationBuilder.InsertData(
                table: "CVs",
                columns: new[] { "CVId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageId", "MessageText", "Read", "ReciverId", "SenderId", "SenderName" },
                values: new object[,]
                {
                    { 1, "Hello Bob, how are you?", false, 2, 1, "Alice" },
                    { 2, "Hi Alice! I'm good, thank you!", true, 1, 2, "Bob" },
                    { 3, "Hello Charlie, nice to meet you!", false, 3, 1, "Alice" },
                    { 4, "Hi Alice, great to connect!", false, 1, 3, "Charlie" },
                    { 5, "Hey Charlie, are you available for a call?", true, 3, 2, "Bob" },
                    { 6, "Hi Bob, yes I am available. Let's talk!", false, 2, 3, "Charlie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CVs",
                keyColumn: "CVId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CVs",
                keyColumn: "CVId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CVs",
                keyColumn: "CVId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Education",
                keyColumn: "EducationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Education",
                keyColumn: "EducationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Education",
                keyColumn: "EducationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Experience",
                keyColumn: "ExperienceId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Experience",
                keyColumn: "ExperienceId",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Experience",
                keyColumn: "ExperienceId",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "MessageId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ProjectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ProjectId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "ProjectId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillsId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillsId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillsId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillsId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);
        }
    }
}
