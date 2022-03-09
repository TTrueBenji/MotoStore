using Microsoft.AspNetCore.Mvc;

namespace MotoStore.Controllers
{
    public class AccountController : Controller
    {
        // GET
        public IActionResult Register()
        {
            return View();
        }
    }
}