using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
    public static class PokemanzUtil
    {

        public static Pokemon GetPokemonFromName(string name)
        {
            switch (name)
            {
                case "b":
                    return new Bulbasuar();
                case "c":
                    return new Charmander();
                case "s":
                    return new Squirtle();
                default:
                    return null;
            }
        }

    }
}
