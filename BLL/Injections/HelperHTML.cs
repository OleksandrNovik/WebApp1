namespace BLL.Injections
{
	public static class HelperHTML
	{
		public static string GenerateUnorderedList(string value)
		{
			var rawLinesList = value
					.Split(new string[] { "\n\t", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			var listItems = string.Join("", rawLinesList
										.Select(line => line.Contains('-') ? $"<li class=\"description-list-item\">{line.Replace('-', ' ')}</li>" : $"<div>{line}</div>"));
			return $"<ul style=\"padding: 0;\">{listItems}</ul>";
		}
	}
}
