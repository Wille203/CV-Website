using CV_Website.Models;
using Microsoft.AspNetCore.Mvc;

namespace CV_Website.Controllers
{
    public class CVController : Controller
    {
        private CVContext _context;

        public CVController(CVContext context) 
        {
            _context = context;
        }

       
    }
}
