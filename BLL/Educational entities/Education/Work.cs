using BLL.Person_entities.UserFolder;

namespace BLL.Educational_entities.Education
{
	public enum WorkStatus
	{
		NotComplited,
		Complited,
		Evaluated
	}
	public class Work
	{
		public int Id { get; set; }
		public WorkStatus Status { get; set; }
		public Assignment OnTask { get; set; }
		public User WorkAuthor { get; set; }
		public Mark? Assessment { get; set; }
		public string Code { get; set; }
	}
}
