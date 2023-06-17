namespace BLL.ViewModels
{
    public class AssignmentViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string MainTask { get; set; }
        public string? StarterCode { get; set; }
        public string? ProgramingLanguages { get; set; }

        public int AssessmentId { get; set; }
        public string? SelectedLanguage { get; set; }
        public string? SelectedEditorTheme { get; set; }
        public string? UserCode { get; set; } 

        public string? Links { get; set; }
    }
}
