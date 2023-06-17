using BLL.Educational_entities.Organization;
using System.ComponentModel.DataAnnotations;

namespace BLL.ViewModels.Create_Models
{
    public class CourseCreateModel
    {
        [Required (ErrorMessage = "Назва курсу - обов'язкова")]
        [RegularExpression(@"^[a-zA-Z0-9]+$|^((?![/|~`]).)*$", 
            ErrorMessage = "Назва курсу не повинна містити символи \"/|\\~`\"")]
        public string Name { get; set; }
        
        [UIHint("MultilineText")]
        public string? Description { get; set; }

        public CourseLevel Level { get; set; }
        public CourseType Type { get; set; }
        
        [UIHint("BooleanCheckbox")]
        public bool IsPublic { get; set; }

        [UIHint("MultilineText")]
        public string? Additional { get; set; }
    }
}
