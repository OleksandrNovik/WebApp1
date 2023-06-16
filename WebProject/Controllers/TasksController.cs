using BLL.Educational_entities.Education;
using BLL.ViewModels;
using BLL.ViewModels.Create_Models;
using DAL.Data;
using Microsoft.AspNetCore.Authorization;
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
                _logger.LogError("Помилка! Завдання типу Assignment за id = {0} не знайдено!", assignmentID);
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
        [HttpGet]
        [Authorize(Roles = "Mentor, Admin")]
        public IActionResult CreateTask(int topicId)
        {
            ViewData["TopicId"] = topicId;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Mentor, Admin")]
        public async Task<IActionResult> CreateTask(AssignmentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var topic = await _dbContext
                    .Topics
                    .Include(t => t.Tasks)
                    .FirstOrDefaultAsync(t => t.Id == model.TopicId);

                if (topic == null)
                {
                    return NotFound();
                }
                var assignment = new Assignment
                {
                    Name = model.Name,
                    Description = model.Description,
                    LinkForInfo = model.LinkForInfo,
                    MainTask = model.MainTask,
                    ProgramingLanguages = model.ProgramingLanguages,
                    OnTopic = topic
                };
                if (topic.Tasks != null)
                {
                    topic.Tasks.Add(assignment);
                }
                await _dbContext.Assignments.AddAsync(assignment);
                await _dbContext.SaveChangesAsync();
                if (model.IsStarterCode)
                {
                    return RedirectToAction("Edit", new { assignmentID = assignment.Id, isEditCode = true });

                }
                return RedirectToAction("CodeEditor", new { assignmentID = assignment.Id });
            }
            return View(model);
        }
        /// <summary>
        /// Представлення редагування завдання з кодом
        /// </summary>
        /// <param name="assignmentID"> id завдання з кодом</param>
        /// <returns> Форма редагування завдання з кодом </returns>
        [Authorize(Roles = "Mentor, Admin")]
        public async Task<IActionResult> Edit(int assignmentID, bool isEditCode = false)
        {
            if (User.Identity == null)
            {
                return RedirectToAction("LogIn", "Account");
            }
            var currentUser = await _dbContext
                .Users
                .Include(u => u.Info)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var thisAssignment = await _dbContext
                .Assignments
                .FirstOrDefaultAsync(a => a.Id == assignmentID);
            _logger.LogInformation($"Пошук завдання з кодом за id = {assignmentID}.");
            if (thisAssignment == null || currentUser == null)
            {
                _logger.LogError($"Помилка в {nameof(TasksController)}, метод {nameof(Edit)}! Не отримано необхідні дані!");
                return NotFound();
            }
            _logger.LogInformation($"Завдання з кодом за id = {assignmentID} знайдено та сформовано на його основі модель.");
            var model = new AssignmentCreateModel
            {
                AssignmentId = thisAssignment.Id,
                Name = thisAssignment.Name,
                Description = thisAssignment.Description,
                MainTask = thisAssignment.MainTask,
                ProgramingLanguages = thisAssignment.ProgramingLanguages,
                LinkForInfo = thisAssignment.LinkForInfo,
                SelectedTheme = currentUser.Info.EditorTheme,
                StarterCode = thisAssignment.StarterCode
            };
            if (isEditCode)
            {
                ViewData["Title"] = "Задати стартовий код";
                ViewData["CodeEdit"] = true;
			}
            else
            {
                ViewData["Title"] = "Редагувати завдання";
                ViewData["CodeEdit"] = false;
            }
			return View(model);
        }
        /// <summary>
        /// Перехід з форми редагування завдання з кодом на 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Mentor, Admin")]
        public async Task<IActionResult> Edit (AssignmentCreateModel model)
        { 
            if (ModelState.IsValid)
            {
                var assignment = await _dbContext
                    .Assignments
                    .FirstOrDefaultAsync(a => a.Id == model.AssignmentId);

                if (assignment == null)
                {
                    return NotFound();
                }
                assignment.Name = model.Name;
                assignment.Description = model.Description;
                assignment.MainTask = model.MainTask;
                assignment.LinkForInfo = model.LinkForInfo;
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("CodeEditor", new { assignmentID = assignment.Id });
            }
            ViewData["Title"] = "Редагувати завдання";
            ViewData["CodeEdit"] = false;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Mentor, Admin")]
        public async Task<IActionResult> SaveStarterCode(string starterCode, int taskId)
        {
            var thisTask = await _dbContext
                .Assignments
                .FirstOrDefaultAsync(a => a.Id == taskId);
            
            _logger.LogInformation($"Пошук завдання за id = {taskId}, для заміни стартового коду.");
            if (thisTask == null)
            {
                _logger.LogError($"Помилка в {nameof(TasksController)}, метод {nameof(SaveStarterCode)}! Завдання за id = {taskId} не знайдено!");
                return Json(new { success = false });
            }
            thisTask.StarterCode = starterCode;
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Замінено стартовий код для завдання id = {taskId}.");
            return Json(new { success = true });
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
