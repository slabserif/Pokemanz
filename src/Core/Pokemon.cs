using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
    public abstract class Pokemon
    {
        public string name { get; set; }
        public int pdexNumber { get; set; }

        public string type1 { get; set; }
        public int level { get; set; }

        public int hp { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int spAttack { get; set; }
        public int spDefense { get; set; }
        public int speed { get; set; }
        
    }

    public class Bulbasuar : Pokemon
    {
        public Bulbasuar()
        {
            name = "Bulbasuar";
            type1 = "Grass";
            pdexNumber = 1;
            level = 5;
            hp = 21;
            attack = 12;
            defense = 11;
            spAttack = 11;
            spDefense = 13;
            speed = 11;

        }
    }

    public class Charmander : Pokemon
    {
        public Charmander()
        {
            name = "Charmander";
            type1 = "Fire";
            pdexNumber = 4;
            level = 5;
            hp = 20;
            attack = 12;
            defense = 10;
            spAttack = 10;
            spDefense = 11;
            speed = 13;

        }
    }


    public class Squirtle : Pokemon
    {
        public Squirtle()
        {
            name = "Squirtle";
            type1 = "Water";
            pdexNumber = 7;
            level = 5;
            hp = 20;
            attack = 12;
            defense = 13;
            spAttack = 9;
            spDefense = 12;
            speed = 10;

        }
    }
}
