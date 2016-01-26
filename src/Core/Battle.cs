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

		public void pokemonBattle()
		{
			Player newPlayer = new Player();
			Pokemon playerPokemon = newPlayer.playerPokemonList[0];

			IPokemonRepository repository = PokemonExcelRepository.Create();
			Pokemon opponentPokemon = repository.GetRandomPokemon();

			//TODO: player chooses an action
			BattleUtil.GetPlayerAction(playerPokemon, opponentPokemon); //gets player action to run after playerGoesFirst() has been executed
			
			bool playerGoesFirst = BattleUtil.GetPlayerGoesFirst(playerPokemon, opponentPokemon); //maybe change this to an int type? Easier to pass into a turn method. 0 == player, 1 == opponent.

			//execute turn
		}
	}
}
