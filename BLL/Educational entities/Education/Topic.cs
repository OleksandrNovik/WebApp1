namespace BLL.Educational_entities.Education
{
	public class Topic
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public ICollection<Task>? Tasks { get; set; }
	}
}
