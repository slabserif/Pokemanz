using Pokemanz.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemanz.ConsoleApp
{
	//QUESTIONS
	//Where to put "this.thing"
	//How to choose a random class so i can print it
	//How do i return a pokemon based on user choice and print it?
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
			Pokemon starter = ChoosePokemon();
			Console.WriteLine("Starter Pokemon: " + starter.Name);
			Console.ReadLine();

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