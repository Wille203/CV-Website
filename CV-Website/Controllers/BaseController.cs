using Microsoft.AspNetCore.Mvc;
using CV_Website.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
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

        protected int? GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //MÅSTE BYTA UT TILL RIKTIGT ID
            if(GetCurrentUserId() != null)
            {
                SetUnreadMessageCount(GetCurrentUserId().Value);
                base.OnActionExecuting(context);
            }
            ViewBag.CurrentUserId = GetCurrentUserId();

        }
    }
}
