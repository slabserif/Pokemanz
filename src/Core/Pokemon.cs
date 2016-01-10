using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
    public class Pokemon
    {
        public string Name { get; set; }
        public int Id { get; set; }
        [PokemonProperty("Type 1")]
        public PokemonType Type1 { get; set; }
        [PokemonProperty("Type 2")]
        public PokemonType Type2 { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
        public int Speed { get; set; }
    }

    public class PokemonPropertyAttribute : Attribute
    {
        public string Name { get; set; }
        public PokemonPropertyAttribute(string name)
        {
            this.Name = name;
        }
    }

    public enum PokemonType
    {
        Grass,
        Water,
        Fire,
        Flying,
        Electric,
        Normal
    }
    
}