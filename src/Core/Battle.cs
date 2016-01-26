using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
    public class Battle
    {
		//Moved to Pokemon class and Move class for now
		/*public class battlingPokemon
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

		public class battlingPokemonMoves
		{
			public int currentPP { get; private set; }
			public int currentBasePower { get; private set; }
		}*/

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
			return playerGoesFirst == 1;
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
				return true; 
			}

			int checkEscape = ((speed * 32) / opposingSpeed) + (30 * timesAttempted);
			if (checkEscape > 255)
			{
				return true; 
			}

			int checkForEscape = PokemanzUtil.GetRandomNumber(0, 256);
			if (checkForEscape < checkEscape)
			{
				return true; 
			}
			else
			{
				timesAttempted++; //TODO: is this actually going to increment?
								  //TODO: players turn is over?
				return false;
			}
		}

		/*public static bool PlayerOutofPokemon(Player player) //This would be executed every time a player's pokemon faints.
		{
			for (int i = 0; i < player.playerPokemonList.Length; i++) 
			{
				if (player.playerPokemonList[i].hpModifier == 0)
				{
					continue;
				}
				else
				{
					return false;
				}
			}
			return true;
		}*/

		public enum PlayerActionType
		{
			Fight,
			Bag,
			PKMN,
			Run
		}

		public void PokemonAttacks(Pokemon attackingPokemon, Pokemon defendingPokemon)
		{
			Move move = attackingPokemon.Moves[0]; //TODO: get actual used move
			float moveAccuracy = 1.0f; //TODO: Dummy data. Remove once excel implemented
			bool checkForMiss = BattleUtil.CheckForMiss(attackingPokemon.Accuracy, attackingPokemon.Evasion, moveAccuracy); //Move excel repository needed

			if (checkForMiss)
			{
				int damage = BattleUtil.CalculateDamage(attackingPokemon, defendingPokemon, Move move, int modifier); //HELP Move move and int modifier?
				defendingPokemon.hpModifier += damage; //Best way? BaseHp never gets touched and a modifier is compared to it. OR Just modify the HP directly?
			}
			//TODO: End turn
		}


		public static void pokemonBattle()
		{
			Player newPlayer = new Player();
			Pokemon playerPokemon = newPlayer.playerPokemonList[0];

			IPokemonRepository repository = PokemonExcelRepository.Create();
			Pokemon opponentPokemon = repository.GetRandomPokemon();

			//TODO: player chooses an action

			bool playerGoesFirst = GetPlayerGoesFirst(playerPokemon, opponentPokemon); //maybe change this to an int type? Easier to pass into a turn method. 0 == player, 1 == opponent.

			//execute turn
		}
	}
}
