using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ananas.Web.Mvc.Examples.Models;
using Ananas.Web.Mvc.Extensions;
using Ananas.Web.Mvc.Models;
using ControllerBase = Ananas.Web.Mvc.Base.ControllerBase;

namespace Ananas.Web.Mvc.Examples.Controllers
{
    public class HomeController : ControllerBase
    {
        
        public IActionResult Index(string uid)
        {
            UserInfo user = new UserFactory().FirstOne("select * from Ananas_User where sName='qingtang166@163.com'");
            NLogHelper.LogWriter("4324",Request.HttpContext.Connection.RemoteIpAddress.ToString()); 
            ViewModel mode = new BaseList().GetCurrentModel(this.HttpContext, user);
            mode.ViewUser=user;
            return View(mode);
            
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
