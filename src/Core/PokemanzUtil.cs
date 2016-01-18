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
		//TODO: Pokemon stat calculator
		//Stat Research is derived from: http://bulbapedia.bulbagarden.net/wiki/Statistic
		

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

		//TODO: add rest of types
		public static int DamageEffectiveness(PokemonType, Moves PokemonType)
		{
			int normalAttack = 0;
			int fightingAttack = 1;
			int flyingAttack = 2;

			PokemonType Normal = 0;
			PokemonType Fight = 1;
			PokemonType Flying = 2;

			float[,] typeChart = new float[3, 3] {
			{1, 1, 1,} ,   /*  Normal */
			{2, 1, 0.5f} ,   /*  Fight */
			{1, 2, 1}   /*  Flying */
			};

			int attackModifier = typeChart[PokemonType, Moves PokemonType];

			return attackModifier;
		}

		//Catchrate formula

		public static int CatchRate(int hpMax, int hpCurrent, int ballCatchRateModifer, int statusConditionModifier)
		{
			int catchRate = max(((3 * hpMax) - (2 * hpCurrent)) * ( ballCatchRateModifer / (3 * hpMax), 1)) + statusConditionModifier;
			return catchRate;
		}

		public static int ShakeProbability(int catchRate)
		{
			int shakeProbability;
		if (catchRate = Enumerable.Range(0, 1)){
				return shakeProbability = 63;
			}
		else if (catchRate == 2)
			{
				return shakeProbability = 75;
			}
			else if (catchRate == 3)
			{
				return shakeProbability = 84;
			}
			else if (catchRate == 4)
			{
				return shakeProbability = 90;
			}
			else if (catchRate == 5)
			{
				return shakeProbability = 95;
			}
			else if (catchRate = Enumerable.Range(6, 7))
			{
				return shakeProbability = 103;
			}
			else if (catchRate = Enumerable.Range(8, 10))
			{
				return shakeProbability = 113;
			}
			else if (catchRate = Enumerable.Range(11, 15))
			{
				return shakeProbability = 126;
			}
			else if (catchRate = Enumerable.Range(16, 20))
			{
				return shakeProbability = 134;
			}
			else if (catchRate = Enumerable.Range(21, 30))
			{
				return shakeProbability = 149;
			}
			else if (catchRate = Enumerable.Range(31, 40))
			{
				return shakeProbability = 160;
			}
			else if (catchRate = Enumerable.Range(41, 50))
			{
				return shakeProbability = 169;
			}
			else if (catchRate = Enumerable.Range(51, 60))
			{
				return shakeProbability = 177;
			}
			else if (catchRate = Enumerable.Range(61, 80))
			{
				return shakeProbability = 191;
			}
			else if (catchRate = Enumerable.Range(81, 100))
			{
				return shakeProbability = 201;
			}
			else if (catchRate = Enumerable.Range(101, 120))
			{
				return shakeProbability = 211;
			}
			else if (catchRate = Enumerable.Range(121, 140))
			{
				return shakeProbability = 220;
			}
			else if (catchRate = Enumerable.Range(141, 160))
			{
				return shakeProbability = 227;
			}
			else if (catchRate = Enumerable.Range(161, 180))
			{
				return shakeProbability = 234;
			}
			else if (catchRate = Enumerable.Range(181, 200))
			{
				return shakeProbability = 240;
			}
			else if (catchRate = Enumerable.Range(201, 220))
			{
				return shakeProbability = 246;
			}
			else if (catchRate = Enumerable.Range(221, 240))
			{
				return shakeProbability = 251;
			}
			else if (catchRate = Enumerable.Range(241, 254))
			{
				return shakeProbability = 253;
			}
			else if (catchRate == 255)
			{
				return shakeProbability = 255;
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(shakeProbability));
			}
		}

		//If return true, pokemon is caught, if return false, pokemon is not caught
		public static bool ShakeCheck(int shakeProbability)
		{
			for (int numberOfTries = 0; numberOfTries < 4; numberOfTries++)
			{
				int shakeCheck = GetRandomNumber(0, 255);

				if (shakeProbability >= shakeCheck)
				{
					return true;
				}
			}
			return false;
		}
	}
}

