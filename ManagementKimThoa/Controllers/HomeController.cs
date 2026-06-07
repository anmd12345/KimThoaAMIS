using System.Text.Json;
using ManagementKimThoa.Constants;
using ManagementKimThoa.DTOs.User;
using ManagementKimThoa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementKimThoa.Controllers;


public class HomeController : Controller
{

    [Route(RouteConstant.Index)]
    [AllowAnonymous]
    public IActionResult Index()
    {
        var userJson = HttpContext.Session.GetString(SessionConstant.CurrentUser);

        if (!string.IsNullOrEmpty(userJson))
        {
            var currentUser = JsonSerializer.Deserialize<UserSession>(userJson);
            ViewBag.currentUser = currentUser;
        }

        return View();
    }

}

