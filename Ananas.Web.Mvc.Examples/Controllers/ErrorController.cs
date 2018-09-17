using Microsoft.AspNetCore.Mvc;

namespace Ananas.Web.Mvc.Examples.Controllers
{
  public class ErrorController : Controller
  {
      public IActionResult Index()
      {
          return View();
      }
      public IActionResult About()
      {
          return View();
      }
      public IActionResult Contact()
      {
          return View();
      }
      public IActionResult Error()
      {
          return View();
      }
  }
}