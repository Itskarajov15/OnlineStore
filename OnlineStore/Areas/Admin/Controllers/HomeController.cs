using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}