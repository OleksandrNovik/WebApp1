using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels.Create_Models
{
    public class AssignmentCreateModel
    {
        public int AssignmentId { get; set; }
        public int TopicId { get; set; }

        [Required(ErrorMessage = "Назва завдання обов'язкова")]
        public string Name { get; set; }

        [UIHint("MultilineText")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Завдання має бути вказаним")]
        [UIHint("MultilineText")]
        [MinLength(10, ErrorMessage = "Завдання має бути інформативнішим")]
        public string MainTask { get; set; }

        [UIHint("BooleanCheckbox")]
        public bool IsStarterCode { get; set; }
        public string? StarterCode { get; set; }

        public string? ProgramingLanguages { get; set; }

        [UIHint("MultilineText")]
        public string? LinkForInfo { get; set; }

        public string? SelectedTheme { get; set; }
    }
}
