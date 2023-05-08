using BLL.Educational_entities.Education;
using DAL.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers
{
    public class TasksController : BaseController
    {
        public IActionResult Index()
        {
            return View(SampleData.sampleTasks);
        }
        public IActionResult CodeEditorTask(int? assignmentID)
        {
            return View("CodeEditor");
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
