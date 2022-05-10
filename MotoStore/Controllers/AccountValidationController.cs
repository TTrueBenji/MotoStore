using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MotoStore.Controllers
{
    public class AccountValidationController : Controller
    {
        private readonly StoreApplicationContext _db;

        public AccountValidationController(StoreApplicationContext db)
        {
            _db = db;
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckUserName(string userName)
        {
            var isExist = _db.Users.Any(u => u.UserName == userName);
            return Json(!isExist);
        }
    }
}