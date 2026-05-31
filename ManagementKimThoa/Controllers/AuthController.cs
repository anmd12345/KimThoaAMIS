using Microsoft.AspNetCore.Mvc;


namespace ManagementKimThoa.Controllers
{
    public class AuthController : Controller
    {
        [Route("/dang-nhap")]
        public IActionResult Login()
        {
            return View();
        }
    }
}

