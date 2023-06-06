using System.Linq;
using System.Text;

namespace WebProject
{
	public static class HelperHTML
	{
		public static string GenerateUnorderedList(string value)
		{
			// Виділяю усі лінії з тексту
			var rawLinesList = value
					.Split(new string[] { "\n\t", "\n" }, StringSplitOptions.RemoveEmptyEntries);
			// Далі кожну лінію об'єдную назад у спільну строку
			var listItems = string.Join("", rawLinesList
										// Дивлюся на перші 3 символи лінії
										.Select(line => line
										.Where((ch, index) => index < 3)
										//Чи присутнє там '-', як символ списку (перечислення)
										.Select(ch => ch == '-')
										.Aggregate(false, (cond, res) => res || cond) ? 
										//ЯКщо так, то додаю це як елемент списку
										$"<li class=\"description-list-item\">{line.Replace('-', ' ')}</li>" 
										//Ні - додаю просто як блок
										: $"<div style=\"margin: 12px 0;\">{line}</div>"));

			return $"<ul style=\"padding: 0;\">{listItems}</ul>";
		}
        public static string GenerateCodeLines(string input)
        {
			// Виділяю усі лінії з введеного тексту
            var rawLinesList = input
                    .Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            
			// Вибираю серед строк ті, де декілька разів підряд попадає $, що є символом початку коду
			/*
				Наприклад це може бути такий код:
				$value = 1
				$print(value + 1)
				$func(value)
			 */
			StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < rawLinesList.Count - 1; i++)
			{
				if (rawLinesList[i].Contains('$') && rawLinesList[i + 1].Contains('$'))
				{
					builder.Append(rawLinesList[i]);
					builder.Append("\n");
					builder.Append(rawLinesList[i + 1].Replace('$', ' '));
					rawLinesList[i] = builder.ToString();
					rawLinesList.RemoveAt(i + 1);
					i--;
				}
				builder.Clear();
			}
			var selectedCode = string.Join("\n", rawLinesList
											// Буру кожну лінію розділену "\n"
											.Select(line => line
											// Дивлюся на перші 3 символи
											.Where((ch, index) => index < 3)
											// Якщо серед них є знак $
											.Select(ch => ch == '$')
											.Aggregate(false, (cond, res) => res || cond) ?
											// То створюю блок під код та видліяю його спочатку та в кінці <div></div>
											new string ("<div class=\"code-piece\">" + string.Join("", line
															// Серед якого вибираю окремі лінії коду
															.Split('\n', StringSplitOptions.RemoveEmptyEntries)
															// Записую їх в окремий абзац
															.Select(l => $"<p class=\"code-line\">{l.Replace('$', ' ')}</p>")
															) + "</div>") : line
															)
				);
			return selectedCode;
        }
    }
}
