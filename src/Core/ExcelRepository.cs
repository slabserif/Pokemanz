using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public abstract class ExcelRepository<T> where T : class, new()
	{
		protected static Random random = new Random();
		protected List<T> itemList { get; }
		protected ExcelRepository(List<T> itemList)
		{
			this.itemList = itemList;
		}


		public static Dictionary<string, PropertyInfo> GetProperties()
		{
			Dictionary<string, PropertyInfo> pokemonProperties = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
			List<PropertyInfo> propertyInfoList = typeof(T).GetTypeInfo().DeclaredProperties.ToList();
			foreach (PropertyInfo propertyInfo in propertyInfoList)
			{
				CustomAttributeData attribute = propertyInfo.CustomAttributes.FirstOrDefault(ca => ca.AttributeType == typeof(PokemonPropertyAttribute));

				string name = null;
				if (attribute != null)
				{
					CustomAttributeTypedArgument typedArgument = attribute.ConstructorArguments.First();
					name = typedArgument.Value?.ToString();
				}
				if (name == null)
				{
					name = propertyInfo.Name;
				}
				pokemonProperties.Add(name, propertyInfo);
			}
			return pokemonProperties;
		}

		public static IEnumerable<T> ParseFile(Func<string, PropertyInfo, object> parseCell, Func<T, T> endParse, string resourceName)
		{
			Dictionary<string, PropertyInfo> properties = ExcelRepository<T>.GetProperties();
			Stream pokedexStream = typeof(MoveExcelRepository).GetTypeInfo().Assembly.GetManifestResourceStream(resourceName);
			using (TextReader textReader = new StreamReader(pokedexStream))
			{
				string pokedexText = textReader.ReadToEnd();
				string[] rows = pokedexText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
				string[] columns = rows.First().Split('\t');
				foreach (string row in rows.Skip(1))
				{
					T item = new T();
					string[] cells = row.Split('\t');
					for (int i = 0; i < columns.Length; i++)
					{
						string column = columns[i];
						string cell = cells[i];
						if (string.IsNullOrWhiteSpace(cell))
						{
							continue;
						}
						PropertyInfo propertyInfo;
						if (properties.TryGetValue(column, out propertyInfo))
						{
							object cellValue = parseCell(cell, propertyInfo);
							propertyInfo.SetValue(item, cellValue);
						}
					}
					yield return endParse(item);
				}
			}
		}


		public T GetRandom()
		{
			int randomIndex = ExcelRepository<Pokemon>.random.Next(0, this.itemList.Count);
			return this.itemList[randomIndex];
		}
	}
}
