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
		public static float DamageEffectiveness(PokemonType attackType, PokemonType defenseType)
		{
			
			float[,] typeChart = new float[3, 3] {
			{1, 1, 1,} ,   
			{2, 1, 0.5f} ,   
			{1, 2, 1} 
			};

			float attackModifier = typeChart[(int)attackType, (int)defenseType];

			return attackModifier;
		}

		//Catchrate formula

		public static int CatchRate(int hpMax, int hpCurrent, int ballCatchRateModifer, int statusConditionModifier)
		{
			int catchRate = Math.Max((3 * hpMax - 2 * hpCurrent) * ballCatchRateModifer / (3 * hpMax), 1) + statusConditionModifier;
			return catchRate;
		}

		public static int ShakeProbability(int catchRate)
		{
		if (catchRate > 0 && catchRate < 1)
			{
				return 63;
			}
		else if (catchRate == 2)
			{
				return 75;
			}
			else if (catchRate == 3)
			{
				return 84;
			}
			else if (catchRate == 4)
			{
				return 90;
			}
			else if (catchRate == 5)
			{
				return 95;
			}
			else if (catchRate > 6 && catchRate < 7)
			{
				return 103;
			}
			else if (catchRate > 8 && catchRate < 10)
			{
				return 113;
			}
			else if (catchRate > 11 && catchRate < 15)
			{
				return 126;
			}
			else if (catchRate > 16 && catchRate < 20)
			{
				return 134;
			}
			else if (catchRate > 21 && catchRate < 30)
			{
				return 149;
			}
			else if (catchRate > 31 && catchRate < 40)
			{
				return 160;
			}
			else if (catchRate > 41 && catchRate < 50)
			{
				return 169;
			}
			else if (catchRate > 51 && catchRate < 60)
			{
				return 177;
			}
			else if (catchRate > 61 && catchRate < 80)
			{
				return 191;
			}
			else if (catchRate > 81 && catchRate < 100)
			{
				return 201;
			}
			else if (catchRate > 101 && catchRate < 120)
			{
				return 211;
			}
			else if (catchRate > 121 && catchRate < 140)
			{
				return 220;
			}
			else if (catchRate > 141 && catchRate < 160)
			{
				return 227;
			}
			else if (catchRate > 161 && catchRate < 180)
			{
				return 234;
			}
			else if (catchRate > 181 && catchRate < 200)
			{
				return 240;
			}
			else if (catchRate > 201 && catchRate < 220)
			{
				return 246;
			}
			else if (catchRate > 221 && catchRate < 240)
			{
				return 251;
			}
			else if (catchRate > 241 && catchRate < 254)
			{
				return 253;
			}
			else if (catchRate == 255)
			{
				return 255;
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(catchRate));
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

