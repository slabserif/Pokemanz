using Pokemanz.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemanz.ConsoleApp
{
	class Program
	{
		static Pokemon ChoosePokemon()
		{
			IPokemonRepository repository = PokemonExcelRepository.Create();
			while (true)
			{
				Console.WriteLine("Choose a Pokemon\n\n");
				Console.WriteLine("Type 'b' for Bulbasuar\n");
				Console.WriteLine("Type 'c' for Charmander\n");
				Console.WriteLine("Type 's' for Squirtle\n");
				string name = Console.ReadLine();
				Console.WriteLine();
				Console.WriteLine();


				switch (name)
				{
					case "b":
						name = "Bulbasuar";
						break;
					case "c":
						name = "Charmander";
						break;
					case "s":
						name = "squirtle";
						break;
					default:
						Console.WriteLine("\nInvaild input, please try again.\n");
						continue;
				}

				Pokemon starter = repository.GetPokemonByName(name);

				Program.PrintStats(starter);

				Console.WriteLine("\nDo you want to choose " + starter.Name + "?\n'y' for yes, 'n' for no.");
				string pokechoice = Console.ReadLine();
				switch (pokechoice)
				{
					case "y":
						return starter;
					default:
						continue;
				}
			}
		}


		static void Main(string[] args)
		{
			//ChooseStarter()
			DoBattle();

		}

		public static void ChooseStarter()
		{
			Pokemon starter = ChoosePokemon();
			Console.WriteLine("Starter Pokemon: " + starter.Name);
			Player newPlayer = new Player();
			newPlayer.AssignPokemon(starter, 1);
			Console.WriteLine("Pokemon Assigned: " + newPlayer.playerPokemonList[0].Name);
			Console.WriteLine(" ");
			IPokemonRepository repository = PokemonExcelRepository.Create();
			Pokemon randomPokemon = repository.GetRandom();
			Console.WriteLine("Random Pokemon: " + randomPokemon.Name);
			Console.ReadLine();
		}

		public static void DoBattle()
		{

			IPokemonRepository pokemonRepository = PokemonExcelRepository.Create();
			IMoveRepository moveRepository = MoveExcelRepository.Create();
			Pokemon playerPokemon = pokemonRepository.GetRandom();
			Move move1 = moveRepository.GetRandom();
			playerPokemon.AssignMove(move1, 1);
			Pokemon wildPokemon = pokemonRepository.GetRandom();
			Move move2 = moveRepository.GetRandom();
			wildPokemon.AssignMove(move2, 1);

			Player player = new Player();
			player.playerPokemonList[0] = playerPokemon;
			Player theWild = new Player();
			theWild.playerPokemonList[0] = wildPokemon;
			Battle battle = new Battle(player, theWild);


			while (true)
			{
				string playerActionString = Console.ReadLine();
				PlayerAction playerAction;
				if (!Enum.TryParse(playerActionString, out playerAction))
				{
					Console.WriteLine("Bad choice");
					continue;
				}

				switch (playerAction)
				{
					case PlayerAction.Fight:
						string playerPokemonMove = Console.ReadLine();
						int moveSlot = int.Parse(playerPokemonMove);
						bool DoesMoveHavePp = BattleUtil.CheckIfMoveHasPp(playerPokemon, moveSlot);
						if (!DoesMoveHavePp)
						{
							Console.WriteLine("No more PP");
							continue; //Not a continue? 
						}
						battle.Fight(moveSlot);
						break;
					case PlayerAction.Pokemon:
						string chosenPokemon = Console.ReadLine();
						int chosenSlot = int.Parse(chosenPokemon);
						battle.PlayerSwitchPokemon(chosenSlot);
						break;
					case PlayerAction.Item:
						string chosenItem = Console.ReadLine();
						break;
					case PlayerAction.Run:
						battle.Run();
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(playerAction));
				}

				if (battle.CheckIfBattleOver())
				{
					Console.WriteLine();
					break;
				}
				bool isPlayerPokemonDead = battle.isCurrentPokemonDead(playerPokemon);
				bool isEnemyPokemonDead = battle.isCurrentPokemonDead(wildPokemon);
				if (isPlayerPokemonDead)
				{
					string chosenPokemon = Console.ReadLine();
					int chosenSlot = int.Parse(chosenPokemon);
					battle.PlayerSwitchPokemon(chosenSlot);
				}
				//if (isEnemyPokemonDead)
				//{
				//	battle.PlayerSwitchPokemon(theWild);
				//}
			}
		}

		public static void PrintStats(Pokemon pokemon)
		{
			Console.WriteLine("Name: " + pokemon.Name);
			Console.WriteLine("Type 1: " + pokemon.Type1);
			Console.WriteLine("Level: " + pokemon.GetLevel() + "\n");

			Console.WriteLine("Hp: " + pokemon.Hp);
			Console.WriteLine("Attack: " + pokemon.Attack);
			Console.WriteLine("Defense: " + pokemon.Defense);
			Console.WriteLine("Special Attack: " + pokemon.SpAttack);
			Console.WriteLine("Special Defense: " + pokemon.SpDefense);
			Console.WriteLine("Speed: " + pokemon.Speed);
			Console.WriteLine("Sprite Row: " + PokemanzUtil.GetFrontSpriteRow(pokemon.Id));
			Console.WriteLine("Sprite Col: " + PokemanzUtil.GetFrontSpriteCol(pokemon.Id));
		}

	}
}