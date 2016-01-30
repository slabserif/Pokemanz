using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
    public class Battle
    {

		//TODO: add corner cases for priority such as move with priority effects 
		public static Pokemon GetPokemonGoesFirst(Pokemon playerPokemon, Pokemon opponentPokemon, string playerAction) //evaluated per turn
		{
			int playerPokemonLevel = playerPokemon.GetLevel();
			int opponentPokemonLevel = opponentPokemon.GetLevel();
			int playerSpeed = playerPokemon.Speed.GetValue(playerPokemonLevel);
			int opponentSpeed = opponentPokemon.Speed.GetValue(opponentPokemonLevel);

			if (playerAction == "pokemon" || playerAction == "item")
			{
				return opponentPokemon;
			}

			if (playerAction == "run")
			{
				return playerPokemon;
			}

			if (playerSpeed == opponentSpeed)
			{
				int randomFirst = PokemanzUtil.GetRandomNumber(0, 2);
				if (randomFirst == 1)
				{
					return playerPokemon;
				}
			}
			else if (playerSpeed > opponentSpeed)
			{
				return playerPokemon;
			}
			return opponentPokemon;
		}

		public Pokemon PlayerSwitchPokemon(Pokemon chosenPokemon)
		{
			Pokemon playerPokemon = chosenPokemon;
			return playerPokemon;
		}



		/*public static bool PlayerOutofPokemon(Player player) //This would be executed every time a player's pokemon faints.
		{
			for (int i = 0; i < player.playerPokemonList.Length; i++) 
			{
				if (player.playerPokemonList[i].hpModifier >= player.playerPokemonList[i].Hp) //HELP: trying to get hp
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

		static void Main(string[] args)
		{
			Player newPlayer = new Player();
			Pokemon playerPokemon = newPlayer.playerPokemonList[0];

			IPokemonRepository repository = PokemonExcelRepository.Create();
			Pokemon opponentPokemon = repository.GetRandomPokemon();

			//TODO: player chooses an action
			string playerAction = Console.ReadLine();
			switch (playerAction) //Decision details based on menu item selected
			{
				case "attack":
					string playerPokemonMove = Console.ReadLine(); //HELP: turn into move
					break;
				case "pokemon":
					string chosenPokemon = Console.ReadLine(); //HELP: turn into pokemon
					break;
				case "item":
					string chosenItem = Console.ReadLine();
					break;
				default:
					break;
						}
			
			Pokemon pokemonGoesFirst = GetPokemonGoesFirst(playerPokemon, opponentPokemon, playerAction);

			switch (playerAction)
			{
				case "run":
					BattleUtil.CheckIfEscapeSuccess(playerPokemon, opponentPokemon);
					break;
				case "attack":
					BattleUtil.PokemonAttacks(attackingPokemon, defendingPokemon, activeMove);
					break;
				case "pokemon":
					PlayerSwitchPokemon(chosenPokemon);
					break;
				case "item":
					//TODO: use item method
					break;
			}
		}
	}
}
