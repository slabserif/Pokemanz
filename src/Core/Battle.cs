using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public class Battle
	{

		private PlayerState player1State { get; set; }
		private PlayerState player2State { get; set; }

		public Battle(Player player1, Player player2)
		{
			this.player1State = new PlayerState(player1);
			this.player2State = new PlayerState(player2);
		}


		public void PlayerSwitchPokemon(int pokemonSlot)
		{
			this.player1State.ActivePokemon = this.player1State.Player.playerPokemonList[pokemonSlot];
			this.DoAIMove();
		}

		public void Fight(int moveSlot)
		{
			bool player1GoesFirst = BattleUtil.DoesPokemonAttackFirst(this.player1State.ActivePokemon, this.player2State.ActivePokemon);
			if (player1GoesFirst)
			{
				this.Attack(this.player1State.ActivePokemon, this.player2State.ActivePokemon, moveSlot);
				bool isDead = this.isCurrentPokemonDead(this.player2State.ActivePokemon);
				if (!isDead)
				{
					this.DoAIMove();
				}
			}
			else
			{
				this.DoAIMove();
				bool isDead = this.isCurrentPokemonDead(this.player2State.ActivePokemon); //HELP: condense duplicates?
				if (!isDead)
				{
					this.Attack(this.player1State.ActivePokemon, this.player2State.ActivePokemon, moveSlot);
				}
			}
		}

		private void Attack(Pokemon attackingPokemon, Pokemon defendingPokemon, int moveSlot)
		{
			Move chosenMove = attackingPokemon.Moves[moveSlot];
			int damage = BattleUtil.CalculatePokemonDamage(attackingPokemon, defendingPokemon, chosenMove);
			defendingPokemon.hpModifier += damage;
		}

		public void Run()
		{
			bool ifSuccess = BattleUtil.CheckIfEscapeSuccess(this.player1State.ActivePokemon, this.player2State.ActivePokemon);
			if (ifSuccess)
			{
				player1State.Condition = PlayerCondition.Escaped;
			}
			else
			{
				this.DoAIMove();
			}
		}

		public bool isCurrentPokemonDead(Pokemon pokemon)
		{
			if (pokemon.hpModifier >= pokemon.Hp.GetValue(pokemon.GetLevel()))
			{
				return true;
			}
			return false;
		}

		private bool CheckBattleOverPlayer(PlayerState playerState)
		{
			//TODO check if active pokemon dead
			bool outOfPokemon = PlayerOutofPokemon(playerState.Player);
			if (outOfPokemon)
			{
				playerState.Condition = PlayerCondition.OutOfPokemon;
			}
			switch (playerState.Condition)
			{
				case PlayerCondition.Escaped:
					return true;
				case PlayerCondition.OutOfPokemon:
					return true;
				case PlayerCondition.Normal:
					return false;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public bool CheckIfBattleOver()
		{
			bool player1Over = CheckBattleOverPlayer(player1State);
			bool player2Over = CheckBattleOverPlayer(player2State);
			if (player1Over || player2Over)
			{
				return true;
			}
			return false;
		}

		private void DoAIMove()
		{
			//TODO Get random move
		}

		public static bool PlayerOutofPokemon(Player player) //This would be executed every time a player's pokemon faints.
		{
			for (int i = 0; i < player.playerPokemonList.Length; i++)
			{
				if (player.playerPokemonList[i].hpModifier >= player.playerPokemonList[i].Hp.GetValue(player.playerPokemonList[i].GetLevel())) //HELP: trying to get hp
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


		private class PlayerState
		{
			public Player Player { get; set; }
			public Pokemon ActivePokemon { get; set; }
			public PlayerCondition Condition { get; set; }
			public PlayerState(Player player)
			{
				this.Player = player;
				this.ActivePokemon = player.playerPokemonList[0];
			}
		}

	}
	public enum PlayerCondition
	{
		Normal,
		Escaped,
		OutOfPokemon
	}

	public enum PlayerAction
	{
		Fight,
		Pokemon,
		Item,
		Run
	}
}
