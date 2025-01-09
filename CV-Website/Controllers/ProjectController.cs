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
    public class ProjectController : BaseController
    {
        private CVContext _context;

        public ProjectController(CVContext context) : base(context)
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
        //lägg till inlogad användare istllet för hårdkodningen
        [HttpPost]
        [Authorize]
        public IActionResult CreateProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return View(project);
            }
            // ej testad kod
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int loggedInUserId = int.Parse(userId);
            //project.CreatorId = 2;

            project.CreatorId = loggedInUserId; // denna rad 
           
            _context.Add(project);
            _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            
            

        }


        //auth kolla användare som skapat är den inloggade???
        [HttpGet]
        [Authorize]
        public IActionResult EditProject(int Id)
        {

            
            var project = _context.Project.Find(Id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int loggedInUserId = int.Parse(userId);
            if (project == null)
            {
                return NotFound();
            }
            if (project.CreatorId != loggedInUserId)
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

            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["LoggedInUserId"] = loggedInUserId;

            if (!User.Identity.IsAuthenticated)// ej testad
            {
                project.Users = project.Users.Where(u => !u.Private).ToList();
            }

            if (project == null)
            {
                return NotFound();
            }


            return View(project);
        }
        [Authorize]
        public IActionResult LeaveProject(int id)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(loggedInUserId);
            var project = _context.Project.Include(p => p.Users).FirstOrDefault(p => p.ProjectId == id);
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user != null && project.Users.Contains(user))
            {
                
                project.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("ProjectPage", new { id = id });
        }
        [Authorize]
        public IActionResult JoinProject(int id)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(loggedInUserId);
            var project = _context.Project.Include(p => p.Users).FirstOrDefault(p => p.ProjectId == id);
            
            if (!project.Users.Any(u => u.UserId == userId))
            {
                
                var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    project.Users.Add(user);
                    _context.SaveChanges(); 
                }
            }
            return RedirectToAction("ProjectPage", new { id = id });
        }
        [Authorize]
        public IActionResult DeleteProject(int id)
        {
            var project = _context.Project
            .Include(p => p.Users)
            .FirstOrDefault(p => p.ProjectId == id);

            if (project != null)
            {
                _context.Project.Remove(project);
                _context.SaveChanges();
            }

            return RedirectToAction("Project", "ListProject");
        }
    }
}
