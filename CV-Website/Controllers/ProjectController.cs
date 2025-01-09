using System.Security.Claims;
using CV_Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NuGet.Packaging.Signing;
using Project = CV_Website.Models.Project;

namespace CV_Website.Controllers
{
    public class ProjectController : Controller
    {
        private CVContext _context;

        public ProjectController(CVContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateProject()
        {
            Project project = new Project();

            return View(project);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            project.CreatorId = userId;

            _context.Add(project);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditProject(int Id)
        {
            var project = _context.Project.Find(Id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            if (project == null)
            {
                return NotFound();
            }
            if (project.CreatorId != userId)
            {
                return Forbid();
            }

            return View("EditProject", project);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditProject(int projectId, Project updatedProject)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedProject);
            }
            if (projectId != updatedProject.ProjectId)
            {
                return BadRequest();
            }

            var project = _context.Project.FirstOrDefault(u => u.ProjectId == projectId);
            if (project == null)
            {
                return NotFound();
            }

            project.Title = updatedProject.Title;
            project.Description = updatedProject.Description;
            project.Information = updatedProject.Information;

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ListProject()
        {
            IQueryable<Project> ProjectList = from project in _context.Project select project;

            return View(ProjectList.ToList());
        }

        // om personen inte är inloggad ska privata profiler tas bort!!
        public IActionResult ProjectPage(int Id)
        {
            var project = _context.Project
                .Include(p => p.Users)
                .Include(p => p.Creator)
                .FirstOrDefault(u => u.ProjectId == Id);

            if (project == null)
            {
                return NotFound();
            }

            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["LoggedInUserId"] = loggedInUserId;

            // om personen inte är inloggad ska privata profiler tas bort!!
            if (!User.Identity?.IsAuthenticated ?? false)
            {
                project.Users = project.Users.Where(u => !u.Private).ToList();
            }

            return View(project);
        }

        public IActionResult LeaveProject(int id)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (loggedInUserId == null)
            {
                return Unauthorized();
            }

            var project = _context.Project.Include(p => p.Users).FirstOrDefault(p => p.ProjectId == id);
            var user = _context.Users.FirstOrDefault(u => u.Id == loggedInUserId);

            if (user != null && project?.Users.Contains(user) == true)
            {
                project.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("ProjectPage", new { id = id });
        }

        public IActionResult JoinProject(int id)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (loggedInUserId == null)
            {
                return Unauthorized();
            }

            var project = _context.Project.Include(p => p.Users).FirstOrDefault(p => p.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            if (!project.Users.Any(u => u.Id == loggedInUserId))
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == loggedInUserId);
                if (user != null)
                {
                    project.Users.Add(user);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("ProjectPage", new { id = id });
        }
    }
}
