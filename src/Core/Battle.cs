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
			int damage = BattleUtil.CalculatePokemonDamage(attackingPokemon, defendingPokemon, moveSlot);
			attackingPokemon.Moves[moveSlot].PpModifier++;
			defendingPokemon.HpModifier += damage;
		}

		public void Run()
		{
			int escapeCounter = this.player1State.EscapeAttemptCounter;
			bool ifSuccess = BattleUtil.CheckIfEscapeSuccess(this.player1State.ActivePokemon, this.player2State.ActivePokemon, escapeCounter);
			if (ifSuccess)
			{
				player1State.Condition = PlayerCondition.Escaped;
			}
			else
			{
				this.player1State.EscapeAttemptCounter++;
				this.DoAIMove();
			}
		}

		// HELP: Get player1's most recent Action?????????
		// A var in the CheckIfEscapeSuccess Method is an int of how many escape attempts have been made in a row
		// This method is trying to check to see if the player previously tried to Run, but this turn did Fight,Item,or Pokemon.
		// If the player did another action besides run, their EscapeAttemptsCounter value needs to be reset to 1.
		private void ResetEscapeCounter(PlayerState playerState)
		{
			if (this.player1State.Action != PlayerAction.Run)
			{
				this.player1State.EscapeAttemptCounter = 1;
			}
		}

		public bool isCurrentPokemonDead(Pokemon pokemon)
		{
			if (pokemon.HpModifier >= pokemon.Hp.GetValue(pokemon.GetLevel()))
			{
				return true;
			}
			return false;
		}

		private bool CheckBattleOverPlayer(PlayerState playerState)
		{
				ResetEscapeCounter(playerState); //HELP: is this a good spot for this? 
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

		//HELP
		public void RewardExperience(Pokemon activePokemon, Pokemon faintedPokemon)
		{
			int expGain = PokemanzUtil.ExpGain(Pokemon faintedPokemon, bool isWild, int pokemonUsed);
			Pokemon.AddExperience(expGain);
		}

		private void DoAIMove()
		{
			//TODO Get random move
		}

		public static bool PlayerOutofPokemon(Player player)
		{
			for (int i = 0; i < player.playerPokemonList.Length; i++)
			{
				if (player.playerPokemonList[i].HpModifier >= player.playerPokemonList[i].Hp.GetValue(player.playerPokemonList[i].GetLevel())) //HELP: trying to get hp
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
			public int EscapeAttemptCounter { get; set; }
			public PlayerCondition Condition { get; set; }
			public PlayerAction Action { get; set; }
			public PlayerState(Player player)
			{
				this.Player = player;
				this.ActivePokemon = player.playerPokemonList[0];
				this.EscapeAttemptCounter = 1;
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
