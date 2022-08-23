using Microsoft.AspNetCore.Mvc;

namespace expenseTracker.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
