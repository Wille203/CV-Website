using Microsoft.AspNetCore.Mvc;
using CV_Website.Models;
using Microsoft.AspNetCore.Mvc.Filters;
namespace CV_Website.Controllers
{
    public class BaseController : Controller
    {
        private readonly CVContext _context;

        public BaseController(CVContext context)
        {
            _context = context;
        }

        protected void SetUnreadMessageCount(int userId)
        {
            var unreadMessagesCount = _context.Messages
                .Count(m => m.ReceiverId == userId && !m.Read);
 

            ViewData["TotalUnreadMessages"] = unreadMessagesCount;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //MÅSTE BYTA UT TILL RIKTIGT ID
            SetUnreadMessageCount(1); 
            base.OnActionExecuting(context);
        }
    }
}
