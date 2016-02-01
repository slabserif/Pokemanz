using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public interface IPokemonRepository
	{
		Pokemon GetPokemonByName(string name);

		Pokemon GetPokemonById(int id);

		Pokemon GetRandom();

		List<Pokemon> GetAllPokemon();

	}
}
