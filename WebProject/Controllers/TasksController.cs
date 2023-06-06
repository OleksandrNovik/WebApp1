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
        public IActionResult CodeEditor(int assignmentID)
        {
            var assignment = SampleData.sampleTasks.FirstOrDefault(assign => assign.Id == assignmentID);
            if (assignment == default(Assignment))
            {
                return Error();
            }
            return View("CodeEditor", assignment);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
