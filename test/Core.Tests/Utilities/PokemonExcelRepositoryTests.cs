using Pokemanz.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Utilities
{
	public class PokemonExcelRepositoryTests
	{
		[Theory]
		[InlineData(1, "Bulbasuar", PokemonType.Grass)]
		[InlineData(7, "Squirtle", PokemonType.Water)]
		public void Test1(int expectedId, string expectedName, PokemonType expectedType)
		{
			PokemonExcelRepository repository = PokemonExcelRepository.Create();
			Pokemon pokemon = repository.GetPokemonById(expectedId);
			Assert.Equal(expectedId, pokemon.Id);
			Assert.Equal(expectedName, pokemon.Name);
			Assert.Equal(expectedType, pokemon.Type1);
		}
	}
}


