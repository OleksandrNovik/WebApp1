using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace WebProject.Controllers
{
    /// <summary>
    /// Базовий контролер, який створено для задання стандартної культури для вебсайту
    /// </summary>
    public class BaseController : Controller
    {
        //TODO: задати для авторизованого користувача ту культуру, яку вибрав він
        public override void OnActionExecuted(ActionExecutedContext context) 
        {
            base.OnActionExecuted(context);
            var culture = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
