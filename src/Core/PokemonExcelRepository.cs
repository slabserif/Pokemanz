using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
    public class PokemonExcelRepository : IPokemonRepository
    {
        public static PokemonExcelRepository Create()
        {
            //parse file
            List<Pokemon> pokemonList = new List<Pokemon>();
            return new PokemonExcelRepository(pokemonList);
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
            foreach(Pokemon pokemon in pokemonList)
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
