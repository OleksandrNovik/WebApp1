using BLL.Educational_entities.Education;
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
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(assign => assign.Id == assignmentID);
            if (assignment == default(Assignment))
            {
                _logger.LogInformation("Помилка! Завдання типу Assignment за id = {0} не знайдено!", assignmentID);
                return Error();
            }
            _logger.LogInformation("Завдання типу Assignment за id = {0} знайдено та передано представленню.", assignmentID);
            
            //TODO: Після того як додав авторизацію зробити таке
            //var currentUser = await _dbContext.Users
            //    .Include(u => u.Info)
            //    .FirstOrDefaultAsync(user => user.UserName == User.Identity.Name);
            //if (currentUser == null)
            //{
            //    return NotFound();
            //}
            //ViewData["EditorTheme"] = currentUser.Info.EditorTheme;

            return View(assignment);
        }

   //     [HttpPost]
   //     public async Task<IActionResult> SaveChanges(int assignmentId, string selectedTheme, string editor, string language)
   //     {
   //         var currentUser = await _dbContext.Users
   //             .Include(u => u.Info)
   //             .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

   //         currentUser.Info.EditorTheme = selectedTheme;

   //         var studentWork = await _dbContext.StudentWorks
   //             .FirstOrDefaultAsync(w => w.WorkAuthor == currentUser);

   //         if (studentWork == null)
   //         {
   //             var assingment = await _dbContext.Assignments.FirstOrDefaultAsync();
   //             if (assingment == null)
   //             {
   //                 return Json(new { success = false });
   //             }
			//	studentWork = new Work
   //             {
   //                 Code = editor,
   //                 ProgramingLanguage = language,
   //                 OnTask = assingment,
   //                 Status = WorkStatus.NotComplited,
   //                 WorkAuthor = currentUser,
   //                 SubmitDate = DateTime.Now,
   //                 UserId = currentUser.Id
			//	};
			//	await _dbContext.StudentWorks.AddAsync(studentWork);
			//}
   //         else
   //         {
   //             studentWork.Code = editor;
   //             studentWork.ProgramingLanguage = language;
   //         }
   //         await _dbContext.SaveChangesAsync();
   //         return Json(new { success = true });
   //     }
        public IActionResult Error()
        {
            return View();
        }
    }
}
