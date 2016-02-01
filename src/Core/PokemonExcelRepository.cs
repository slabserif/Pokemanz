using Pokemanz.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public class PokemonExcelRepository : ExcelRepository<Pokemon>, IPokemonRepository
	{
		public static PokemonExcelRepository Create()
		{
			List<Pokemon> pokemonList = PokemonExcelRepository.ParseFile().ToList();
			return new PokemonExcelRepository(pokemonList);
		}


		protected static IEnumerable<Pokemon> ParseFile()
		{
			PropertyInfo healthStatPropertyInfo = null;
			int healthStatValue = 0;



			Func<string, PropertyInfo, object> parseCell = (cell, propertyInfo) =>
			{
				if (propertyInfo.PropertyType == typeof(int))
				{
					return int.Parse(cell);
				}
				else if (propertyInfo.PropertyType == typeof(PokemonType))
				{
					return Enum.Parse(typeof(PokemonType), cell);
				}
				else if (propertyInfo.PropertyType == typeof(PokemonExpType))
				{
					return Enum.Parse(typeof(PokemonExpType), cell);
				}
				else if (propertyInfo.PropertyType == typeof(Stat))
				{
					int baseValue = int.Parse(cell);
					return new Stat(baseValue);
				}
				else if (propertyInfo.PropertyType == typeof(HealthStat))
				{
					healthStatPropertyInfo = propertyInfo;
					healthStatValue = int.Parse(cell);
					return null;
				}
				else
				{
					return cell;
				}
			};

			Func<Pokemon, Pokemon> endParse = (pokemon) =>
			{
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
				return pokemon;
			};

			return ExcelRepository<Pokemon>.ParseFile(parseCell, endParse, "Core.compiler.resources.pokedex.txt");
		}
		
		private PokemonExcelRepository(List<Pokemon> pokemonList) : base(pokemonList)
		{

		}

		public List<Pokemon> GetAllPokemon()
		{
			return this.itemList;
		}

		public Pokemon GetPokemonById(int id)
		{
			foreach (Pokemon pokemon in this.itemList)
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
			foreach (Pokemon pokemon in this.itemList)
			{
				if (string.Equals(name, pokemon.Name, StringComparison.OrdinalIgnoreCase))
				{
					return pokemon;
				}
			}
			throw new Exception($"Pokemon with the name '{name}' doesnt exist in the universe.");
		}



	}
}
