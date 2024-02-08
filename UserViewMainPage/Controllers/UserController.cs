using Microsoft.AspNetCore.Mvc;

namespace UserViewMainPage.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
