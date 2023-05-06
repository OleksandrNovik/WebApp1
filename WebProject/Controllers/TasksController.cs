using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
    public class TasksController : BaseController
    {
        public IActionResult General()
        {
            return View("GeneralTasks");
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
