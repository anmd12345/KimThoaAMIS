using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManagementKimThoa.Controllers
{
    public class ProvinceController : Controller
    {
        [HttpGet]
        [Route("/GetProvince")]
        public async Task<IActionResult> GetProvince(int code)
        {
            using HttpClient client = new();

            string url =
                $"https://provinces.open-api.vn/api/v2/p/{code}?depth=2";

            var response = await client.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }
    }
}

