using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public class Player
	{
		public string Name { get; private set; }
		public int Id { get; private set; }
		public GenderType Gender { get; private set; }
		public int BadgeCount { get; private set; }
		public int MoneyCount { get; private set; }
		public int PokedexCount { get; private set; }
		public PlayerType PType { get; private set; }

		public Pokemon[] playerPokemonList { get; private set; } = new Pokemon[6];

		public void AssignPokemon(Pokemon playerPokemon, int partySlotNumber)
		{
			this.playerPokemonList[partySlotNumber - 1] = playerPokemon;
		}

		public Player(string name, PlayerType playerType, GenderType gender)
		{
			this.Name = name;
			this.Gender = gender;
			this.PType = playerType;
		}
	}

	public enum PlayerType
	{
		You,
		Wild,
		Trainer
	}

	public enum GenderType
	{
		Male,
		Female
	}

}
