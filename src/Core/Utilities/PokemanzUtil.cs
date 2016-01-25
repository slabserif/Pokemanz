using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public static class PokemanzUtil
	{
		private static Random random { get; } = new Random();

		internal static bool IsOdd(int value)
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

		internal static int GetRandomNumber(int minValue, int maxValue)
		{
			return PokemanzUtil.random.Next(minValue, maxValue);
		}

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

