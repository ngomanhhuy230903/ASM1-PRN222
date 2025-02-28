using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HuynmHE176493.Web.Filters
{
    public class AdminAuthFilter : ActionFilterAttribute // Kế thừa từ ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int? userRole = context.HttpContext.Session.GetInt32("UserRole");

            // Kiểm tra nếu không đăng nhập hoặc role không phải 0 (Admin) hoặc 1 (Staff)
            if (!userRole.HasValue || (userRole != 0 && userRole != 1))
            {
                context.HttpContext.Response.Cookies.Append("ErrorMessage", "Bạn không có quyền truy cập trang này.");
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
        }
    }
}