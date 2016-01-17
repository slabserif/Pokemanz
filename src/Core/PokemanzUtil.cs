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

		//METHOD: PokemonStatCalc();
		//Runs a stat calculation 5 times, once for each stat (attack, defence, spattack, spdefense, speed)
		//Runs an Hp calculation 

		//Needs to receive 
		//--int dv from DvRandomizer()
		//--Level of current pokemon
		//--Base Stats from tab delimited for pokemon
		//--Count of total Evs earned between last level up and new level up
		//--New moves if any (NOT MVP)
		//--Evolution (NOT MVP)
		//If hits level 100, cannot level anymore (NOT MVP)

		//QUESTIONS:
		//--Is the stat calculation meant to add on to an existing stat, or replace it?
		

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
	}
}
