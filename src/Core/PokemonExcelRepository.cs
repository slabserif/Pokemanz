using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public class PokemonExcelRepository : IPokemonRepository
	{
		public static PokemonExcelRepository Create()
		{
			List<Pokemon> pokemonList = PokemonExcelRepository.ParseFile().ToList();
			return new PokemonExcelRepository(pokemonList);
		}

		public static Dictionary<string, PropertyInfo> GetPokemonProperties()
		{
			Dictionary<string, PropertyInfo> pokemonProperties = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
			List<PropertyInfo> propertyInfoList = typeof(Pokemon).GetTypeInfo().DeclaredProperties.ToList();
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

		public static IEnumerable<Pokemon> ParseFile()
		{
			try
			{
				Dictionary<string, PropertyInfo> pokemonProperties = PokemonExcelRepository.GetPokemonProperties();
				List<Pokemon> pokemonList = new List<Pokemon>();
				Stream pokedexStream = typeof(PokemonExcelRepository).GetTypeInfo().Assembly.GetManifestResourceStream("Core.compiler.resources.pokedex.txt");
				using (TextReader textReader = new StreamReader(pokedexStream))
				{
					string pokedexText = textReader.ReadToEnd();
					string[] rows = pokedexText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
					string[] columns = rows.First().Split('\t');
					foreach (string row in rows.Skip(1))
					{
						Pokemon pokemon = new Pokemon();
						pokemonList.Add(pokemon);
						PropertyInfo healthStatPropertyInfo = null;
						int healthStatValue = 0;
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
							if (pokemonProperties.TryGetValue(column, out propertyInfo))
							{
								if (propertyInfo.PropertyType == typeof(int))
								{
									propertyInfo.SetValue(pokemon, int.Parse(cell));
								}
								else if (propertyInfo.PropertyType == typeof(PokemonType))
								{
									propertyInfo.SetValue(pokemon, Enum.Parse(typeof(PokemonType), cell));
								}
								else if (propertyInfo.PropertyType == typeof(PokemonExpType))
								{
									propertyInfo.SetValue(pokemon, Enum.Parse(typeof(PokemonExpType), cell));
								}
								else if (propertyInfo.PropertyType == typeof(Stat))
								{
									int baseValue = int.Parse(cell);
										Stat stat;
										stat = new Stat(baseValue);
										propertyInfo.SetValue(pokemon, stat);

								}
								else if (propertyInfo.PropertyType == typeof(HealthStat))
								{
									healthStatPropertyInfo = propertyInfo;
									healthStatValue = int.Parse(cell);
								}
								else
								{
									propertyInfo.SetValue(pokemon, cell);
								}
							}
							else
							{
								throw new Exception($"Found no property matching the column '{column}'");
							}
						}
						if (healthStatValue == 0 || healthStatPropertyInfo == null)
						{
							throw new Exception("No health stat set for this pokemon");
						}
						HealthStat healthStat = new HealthStat(healthStatValue,
							pokemon.Attack.DeterminentValue,
							pokemon.Defense.DeterminentValue,
							pokemon.SpAttack.DeterminentValue,
							pokemon.Speed.DeterminentValue);
						healthStatPropertyInfo.SetValue(pokemon, healthStat);
					}
					return pokemonList;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Pokedex file failed to parse, see inner exception", ex);
			}
		}

		private Random random { get; }
		private List<Pokemon> pokemonList { get; }
		private PokemonExcelRepository(List<Pokemon> pokemonList)
		{
			this.pokemonList = pokemonList;
			this.random = new Random();
		}

		public List<Pokemon> GetAllPokemon()
		{
			return pokemonList;
		}

		public Pokemon GetPokemonById(int id)
		{
			foreach (Pokemon pokemon in pokemonList)
			{
				if (id == pokemon.Id)
				{
					return pokemon;
				}
			}
			throw new Exception($"Pokemon with the pokedex number '{id}' doesnt exist in the universe.");
		}

		public Pokemon GetPokemonByName(string name)
		{
			foreach (Pokemon pokemon in pokemonList)
			{
				if (string.Equals(name, pokemon.Name, StringComparison.OrdinalIgnoreCase))
				{
					return pokemon;
				}
			}
			throw new Exception($"Pokemon with the name '{name}' doesnt exist in the universe.");
		}

		public Pokemon GetRandomPokemon()
		{
			int randomIndex = this.random.Next(0, pokemonList.Count);

			return pokemonList[randomIndex];
		}



	}
}
