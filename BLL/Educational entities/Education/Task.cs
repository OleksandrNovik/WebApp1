using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Educational_entities.Education
{
	public class Task
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public ICollection<Work>? StudentWorks { get; set; }
	}
}
