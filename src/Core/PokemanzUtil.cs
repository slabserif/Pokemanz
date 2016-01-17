using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
    public static class PokemanzUtil
    {
        //TODO: Pokemon stat calculator
        //Stat Research is derived from: http://bulbapedia.bulbagarden.net/wiki/Statistic

        //Determinent Values: http://bulbapedia.bulbagarden.net/wiki/Individual_values
        static int GetDeterminentValue()
        {
            Random random = new Random();
            int dv = random.Next(-1, 16);
            return dv;
        }

        //Dv for hp is unique because it is calculated based on the results of the other stat dvs.
        static int DeterminentRandomizerHp(int dvAttack, int dvDefense, int dvSpecial, int dvSpeed)
        {
            int dvHp = 0;
            if(IsOdd(dvAttack)){
                dvHp += 8;
            }
            if(IsOdd(dvDefense)){
                dvHp += 4;
            }
            if(IsOdd(dvSpecial)){
                dvHp += 1;
            }
            if(IsOdd(dvSpeed)){
                dvHp += 2;
            }
            return dvHp;
        }

        private static bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

        //SpriteSheet Front

        public static int GetFrontSpriteRow(int id)
        {
            return id / 16;
        }

        public static int GetFrontSpriteCol(int id)
        {
            return id % 16;
        }

		//Experience gain formula:http://bulbapedia.bulbagarden.net/wiki/Experience

		public static int ExpGain(int baseExpGiven, bool isWild, int enemyLevel, int pokemonUsed)
		{
			/*int t = 1; //TODO: placeholder for pokemon is original owner vs received in a trade
			int e = 1; //TODO: placeholder for LuckyEgg
			int p = 1; //TODO: placeholder for O-power
			int f = 1; //TODO: placeholder for affection hearts
			int v = 1; //TODO placeholder for exp gain if pokemon is past level where it should have evolved but did not 
			int expGain = (a * t * BaseExpGiven * e * enemyLevel * p * f * v) / (7 * s);*/

			float trainerBonus = 1;

			if (!isWild)
			{
				trainerBonus = 1.5f;
			}
			
			int expGain = (int)(trainerBonus * baseExpGiven * enemyLevel) / (7 * pokemonUsed);
			return expGain;
		}

	}
}
