using Microsoft.AspNetCore.Mvc;
using ControllerBase = Ananas.Web.Mvc.Base.ControllerBase;

namespace Ananas.Web.Mvc.Examples.Controllers
{
  public class ErrorController : ControllerBase
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