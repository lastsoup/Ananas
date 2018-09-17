using Microsoft.AspNetCore.Mvc;

namespace Ananas.Web.Mvc.Examples.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
       public IActionResult Index(string uid)
        {
             ViewData["UID"]=uid;
            return View();
        }

    }
}
