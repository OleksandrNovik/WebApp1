namespace BLL.Educational_entities.Education
{
	public class Assignment
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public ICollection<Work>? StudentWorks { get; set; }
	}
}
