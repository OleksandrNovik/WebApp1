using BLL.Educational_entities.Education;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebProject.Controllers
{
    public class TasksController : BaseController
    {
		private readonly ILogger<TasksController> _logger;
        private readonly DbContextProject _dbContext;

		public TasksController(ILogger<TasksController> logger, DbContextProject dbContext) 
        {
			_logger = logger;
            _dbContext = dbContext;
		}
        public IActionResult Index()
        {
            return View(_dbContext.Assignments.ToList());
        }
        public async Task<IActionResult> CodeEditor(int assignmentID)
        {
            if (User.Identity == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(assign => assign.Id == assignmentID);
            if (assignment == default(Assignment))
            {
                _logger.LogInformation("Помилка! Завдання типу Assignment за id = {0} не знайдено!", assignmentID);
                return Error();
            }
            _logger.LogInformation("Завдання типу Assignment за id = {0} знайдено та передано представленню.", assignmentID);

            //TODO: Після того як додав авторизацію зробити таке
            var currentUser = await _dbContext.Users
                .Include(u => u.Info)
                .FirstOrDefaultAsync(user => user.UserName == User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound();
            }
            var currentStudWork = await _dbContext
                .StudentWorks
                .Include(w => w.OnTask)
                .Where(w => w.UserId == currentUser.Id)
                .FirstOrDefaultAsync(w => w.OnTask.Id == assignmentID);
            
            return View(new AssignmentViewModel 
            { 
                AssessmentId = assignmentID,
                Description = assignment.Description,
                MainTask = assignment.MainTask,
                ProgramingLanguages = assignment.ProgramingLanguages,
                SelectedEditorTheme = currentUser.Info.EditorTheme,
                StarterCode = assignment.StarterCode,
                UserCode = currentStudWork?.Code,
                SelectedLanguage = currentStudWork?.ProgramingLanguage,
                Name = assignment.Name,
            });

        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
