using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CV_Website.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.EducationId);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    ExperienceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.ExperienceId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillsId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Private = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "CVs",
                columns: table => new
                {
                    CVId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVs", x => x.CVId);
                    table.ForeignKey(
                        name: "FK_CVs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Messages_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CVEducation",
                columns: table => new
                {
                    CVId = table.Column<int>(type: "int", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVEducation", x => new { x.CVId, x.EducationId });
                    table.ForeignKey(
                        name: "FK_CVEducation_CVs_CVId",
                        column: x => x.CVId,
                        principalTable: "CVs",
                        principalColumn: "CVId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CVEducation_Education_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Education",
                        principalColumn: "EducationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CVExperience",
                columns: table => new
                {
                    CVId = table.Column<int>(type: "int", nullable: false),
                    ExperienceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVExperience", x => new { x.CVId, x.ExperienceId });
                    table.ForeignKey(
                        name: "FK_CVExperience_CVs_CVId",
                        column: x => x.CVId,
                        principalTable: "CVs",
                        principalColumn: "CVId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CVExperience_Experience_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "Experience",
                        principalColumn: "ExperienceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CVSkills",
                columns: table => new
                {
                    CVId = table.Column<int>(type: "int", nullable: false),
                    SkillsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CVSkills", x => new { x.CVId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_CVSkills_CVs_CVId",
                        column: x => x.CVId,
                        principalTable: "CVs",
                        principalColumn: "CVId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CVSkills_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "SkillsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => new { x.ProjectId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Education",
                columns: new[] { "EducationId", "Name" },
                values: new object[,]
                {
                    { 1, "Bachelor in Computer Science" },
                    { 2, "Master in Software Engineering" },
                    { 3, "Diploma in Web Development" },
                    { 4, "PhD in Artificial Intelligence" },
                    { 5, "Bachelor in Data Science" },
                    { 6, "Master in Machine Learning" }
                });

            migrationBuilder.InsertData(
                table: "Experience",
                columns: new[] { "ExperienceId", "Name" },
                values: new object[,]
                {
                    { 1, "Software Developer at XYZ Corp" },
                    { 2, "Full Stack Developer at ABC Inc" },
                    { 3, "Data Analyst at DataWorks" },
                    { 4, "DevOps Engineer at Cloudify" },
                    { 5, "Backend Developer at SecureSoft" },
                    { 6, "Frontend Developer at BrightIdeas" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillsId", "Name" },
                values: new object[,]
                {
                    { 1, "C#" },
                    { 2, "ASP.NET" },
                    { 3, "JavaScript" },
                    { 4, "SQL" },
                    { 5, "Python" },
                    { 6, "React" },
                    { 7, "Docker" },
                    { 8, "Kubernetes" },
                    { 9, "AWS" },
                    { 10, "Machine Learning" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Email", "Name", "Password", "PhoneNumber", "Private" },
                values: new object[,]
                {
                    { 1, "Drottninggatan 10", "alice@example.com", "Alice", "password123", "+46701234567", false },
                    { 2, "Storgatan 5", "bob@example.com", "Bob", "securepassword", "0701234567", true },
                    { 3, "Kungsgatan 18", "charlie@example.com", "Charlie", "charlie123", "+46761234567", false },
                    { 4, "Östra Långgatan 22", "dave@example.com", "Dave", "dave456", "0761234567", false },
                    { 5, "Västra Vallgatan 4", "eve@example.com", "Eve", "eve789", "+46731234567", true },
                    { 6, "Kyrkogatan 8", "frank@example.com", "Frank", "frank101", "0731234567", false }
                });

            migrationBuilder.InsertData(
                table: "CVs",
                columns: new[] { "CVId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "MessageId", "MessageText", "Read", "ReceiverId", "SenderId", "SenderName" },
                values: new object[,]
                {
                    { 1, "Hello Bob, how are you?", false, 2, 1, "Alice" },
                    { 2, "Hi Alice! I'm good, thank you!", true, 1, 2, "Bob" },
                    { 3, "Hello Charlie, nice to meet you!", false, 3, 1, "Alice" },
                    { 4, "Hi Alice, great to connect!", false, 1, 3, "Charlie" },
                    { 5, "Hey Charlie, are you available for a call?", true, 3, 2, "Bob" },
                    { 6, "Hi Bob, yes I am available. Let's talk!", false, 2, 3, "Charlie" },
                    { 7, "Eve, I need your help with the project.", false, 5, 4, "Dave" },
                    { 8, "Sure Dave, let me know the details.", true, 4, 5, "Eve" },
                    { 9, "Alice, your portfolio is impressive!", false, 1, 6, "Frank" },
                    { 10, "Dave, what do you think about the design?", true, 4, 1, "Alice" },
                    { 11, "Alice, I love it! Great job.", false, 1, 4, "Dave" },
                    { 12, "Eve, can you send me the updated files?", false, 5, 3, "Charlie" }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "CreatorId", "Description", "Information", "Title" },
                values: new object[,]
                {
                    { 1, 1, "A portfolio website to showcase my projects.", "Demonstrates my skills and previous works.", "Personal Portfolio" },
                    { 2, 2, "A web application to manage tasks.", "Tracks and manages daily tasks effectively.", "Task Manager App" },
                    { 3, 3, "An online platform for buying and selling products.", "Enables secure online transactions.", "E-Commerce Platform" },
                    { 4, 4, "A chatbot powered by AI.", "Provides personalized assistance to users.", "AI Assistant" },
                    { 5, 5, "An app to monitor fitness activities.", "Helps users track their workouts and progress.", "Fitness Tracker" },
                    { 6, 6, "A platform for online courses.", "Offers a variety of courses across domains.", "Online Learning Platform" }
                });

            migrationBuilder.InsertData(
                table: "CVEducation",
                columns: new[] { "CVId", "EducationId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 }
                });

            migrationBuilder.InsertData(
                table: "CVExperience",
                columns: new[] { "CVId", "ExperienceId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 }
                });

            migrationBuilder.InsertData(
                table: "CVSkills",
                columns: new[] { "CVId", "SkillsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 4, 5 },
                    { 5, 6 },
                    { 6, 7 }
                });

            migrationBuilder.InsertData(
                table: "ProjectUsers",
                columns: new[] { "ProjectId", "UsersUserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 4, 5 },
                    { 5, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CVEducation_EducationId",
                table: "CVEducation",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_CVExperience_ExperienceId",
                table: "CVExperience",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_CVs_UserId",
                table: "CVs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CVSkills_SkillsId",
                table: "CVSkills",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CreatorId",
                table: "Project",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_UsersUserId",
                table: "ProjectUsers",
                column: "UsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CVEducation");

            migrationBuilder.DropTable(
                name: "CVExperience");

            migrationBuilder.DropTable(
                name: "CVSkills");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "CVs");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
