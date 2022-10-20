using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICommonService commonService;

        public BaseController(ICommonService commonService)
        {
            this.commonService = commonService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.Categories = this.commonService.GetCategories();

            base.OnActionExecuting(context);
        }
    }
}
