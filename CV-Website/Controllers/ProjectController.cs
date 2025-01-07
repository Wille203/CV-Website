using CV_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using NuGet.Packaging.Signing;

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
        public IActionResult CreateProject()
        {
            Project project = new Project();
            
            return View(project);

          
        }
        [HttpPost]
        public IActionResult Add(Project project)
        {
            project.CreatorId = 2;
            _context.Add(project);
            _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            
            

        }
    }
}
