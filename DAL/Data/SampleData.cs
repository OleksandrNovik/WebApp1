using BLL.Educational_entities.Education;
using BLL.Educational_entities.Organization;

namespace DAL.Data
{
    public class SampleData
    {
        public static List<Assignment> sampleTasks = new List<Assignment>
        {
            new Assignment { Id = 1, Name = "FirstTask", Description = "This is the first assignment!"},
            new Assignment { Id = 2, Name = "2"},
            new Assignment { Id = 3, Name = "Assignment 3", Description="DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD"},
        };
        public static List<Courses> sampleCourses = new List<Courses>
        {
            new Courses { Id = 1, Name = "First course", Description = "This is first course"},
            new Courses { Id = 2, Name = "Python Course", Description = "1111111111111111111 PYTHON 1111111111111111111"},
            new Courses {Id = 3, Name = "History Course", Description = "Historical course about Ukraine"},
            new Courses {Id = 4, Name = "Test course"}
        };
    }
}
