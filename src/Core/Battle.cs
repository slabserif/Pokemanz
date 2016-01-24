using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
    public class Battle
    {

		public class battlingPokemon
		{
			public int currentHp { get; private set; }
			public int currentAttack { get; private set; }
			public int currentDefense { get; private set; }
			public int currentSpAttack { get; private set; }
			public int currentSpDefense { get; private set; }
			public int currentSpeed { get; private set; }
			public int currentAccuracy { get; private set; }
			public int currentEvasion { get; private set; }
			
			public int currentStatusCondition { get; private set; }
		}

		//TODO: add corner cases for priority such as move with priority effects and using an item or switching out pokemon
		public static bool GetPlayerGoesFirst(Pokemon playerPokemon, Pokemon opponentPokemon) //evaluated per turn
		{
			int playerPokemonLevel = playerPokemon.GetLevel();
			int opponentPokemonLevel = opponentPokemon.GetLevel();
			int playerSpeed = playerPokemon.Speed.GetValue(playerPokemonLevel);
			int opponentSpeed = opponentPokemon.Speed.GetValue(opponentPokemonLevel);

			if (playerSpeed == opponentSpeed)
			{
				int playerGoesFirst = PokemanzUtil.GetRandomNumber(0, 2);

				if (playerGoesFirst == 1)
				{
					return true;
				}
				else
				{
					return false;
				}

			}
			return playerSpeed > opponentSpeed;
		}


		//TODO: Think this is trying to do too much
		public static bool CheckIfEscapeSuccess(Pokemon pokemon, int opposingSpeed)
		{
			int speed = pokemon.Speed.GetValue(pokemon.GetLevel());
			int timesAttempted = 1;
			opposingSpeed /= 4; //TODO: what is 'mod 256'?
			if (opposingSpeed == 0)
			{
				return true; //escape success
			}

			int checkEscape = ((speed * 32) / opposingSpeed) + (30 * timesAttempted);
			if (checkEscape > 255)
			{
				return true; //escape success
			}

			int checkForEscape = PokemanzUtil.GetRandomNumber(0, 256);
			if (checkForEscape < checkEscape)
			{
				return true; //escape success
			}
			else
			{
				timesAttempted++; //TODO: is this actually going to increment?
								  //TODO: players turn is over?
				return false;
			}
		}

		public static bool PlayerOutofPokemon(Player pokemon)
		{
			for (int i = 0; i < playerPokemonList.Count; i++) // Loop with for.
			{
				if (playerPokemonList[i].currentHp == 0)
				{
					continue;
				}
				else
				{
					return false;
				}
			}
			return true;
		}
	}
}
