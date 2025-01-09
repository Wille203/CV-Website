using CV_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CV_Website.Controllers
{
    public class AccountController : BaseController
    {
        private readonly CVContext _context;

        public AccountController(CVContext context) : base(context)
        {
            _context = context;
        }

       
    }
}
