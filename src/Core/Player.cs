﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core.compiler
{
	public class Player
	{
		public string Name { get; private set; }
		public int Id { get; private set; }
		public bool Gender { get; private set; }
		public int badgeCount { get; private set; }
		public int moneyCount { get; private set; }
		public int pokedexCount { get; private set; }

		public List<Pokemon> playerPokemonList { get; private set; } = new List<Pokemon>(6);

		public void AssignPokemon(Pokemon playerPokemon, int partySlotNumber)
		{
			this.playerPokemonList[partySlotNumber - 1] = playerPokemon;
		}
	}
}
