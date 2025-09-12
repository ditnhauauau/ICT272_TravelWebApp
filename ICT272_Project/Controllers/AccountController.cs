using Microsoft.AspNetCore.Mvc;

namespace ICT272_Project.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
