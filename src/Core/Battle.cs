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
		private List<BattleTurn> Turns { get; } = new List<BattleTurn>();

		public Battle(Player player1, Player player2)
		{
			this.player1State = new PlayerState(player1);
			this.player2State = new PlayerState(player2);
		}

		public void PlayerSwitchPokemon(int pokemonSlot)
		{
			this.player1State.ActivePokemon = this.player1State.Player.playerPokemonList[pokemonSlot];
			this.Turns.Add(new PokemonSwitchTurn(pokemonSlot));
			this.DoAIMove();
		}

		//public void EnemySwitchPokemon(Player enemyPlayer)
		//{
		//	int nextPokemon = enemyPlayer.playerPokemonList[player2State.ActivePokemon + 1];//HELP: get partyslot # of active pokemon
		//	this.player2State.ActivePokemon = this.player2State.Player.playerPokemonList[nextPokemon];
		//}

		public void Fight(int moveSlot)
		{
			bool player1GoesFirst = BattleUtil.DoesPokemonAttackFirst(this.player1State.ActivePokemon, this.player2State.ActivePokemon);

			int player1PartySlot = this.player1State.Player.playerPokemonList.IndexOf(this.player1State.ActivePokemon); //HELP Why is Index of not working?
			int player2PartySlot = this.player2State.Player.playerPokemonList.IndexOf(this.player2State.ActivePokemon); //HELP Why is Index of not working?

			if (player1GoesFirst)
			{
				this.Attack(this.player1State.ActivePokemon, this.player2State.ActivePokemon, moveSlot);
				bool isDead = this.isCurrentPokemonDead(this.player2State.ActivePokemon);
				this.Turns.Add(new FightTurn(player1PartySlot, moveSlot, isDead));
				if (!isDead)
				{
					this.DoAIMove();//TODO: make enemy pokemon actually attack
				}
			}
			else
			{
				this.DoAIMove();
				bool isDead = this.isCurrentPokemonDead(this.player2State.ActivePokemon); //HELP: condense duplicates?
				if (!isDead)
				{
					this.Attack(this.player1State.ActivePokemon, this.player2State.ActivePokemon, moveSlot);
					this.Turns.Add(new FightTurn(player1PartySlot, moveSlot, isDead));
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
				this.Turns.Add(new RunTurn(true));
				player1State.Condition = PlayerCondition.Escaped;
			}
			else
			{
				this.Turns.Add(new RunTurn(false));
				this.player1State.EscapeAttemptCounter++; //TODO: Remove this method of counting escapes since we could do it with the BattleTurn
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

		public int CalcEscapeAttempts() //Replacement for current escapseCounter method
		{
			int escapeCounter = 0; //Should always return at least 1 when counting current Run action
			foreach (BattleTurn index in this.Turns.Reverse<index >()) //Run through list from most recent to oldest
			{
				if (this.Turns[index] == this.Turns.OfType<RunTurn>)
				{
					escapeCounter++;
				}
				else
				{
					return escapeCounter; //Should always return at least 1 when counting current Run action
				}
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
				ResetEscapeCounter(playerState); 
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
		/*public void RewardExperience(PlayerState notFainted, PlayerState fainted)
		{
			int expGain = PokemanzUtil.ExpGain(fainted.ActivePokemon, fainted.Player.PType, pokemonUsed);
			Pokemon.AddExperience(expGain);
		}*/

		private void DoAIMove()
		{
			//TODO Get random move
		}

		public static bool PlayerOutofPokemon(Player player)
		{
			for (int i = 0; i < player.playerPokemonList.Length; i++)
			{
				if (player.playerPokemonList[i].HpModifier >= player.playerPokemonList[i].Hp.GetValue(player.playerPokemonList[i].GetLevel())) 
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

	public abstract class BattleTurn
	{
		public bool IsFirstPlayer { get; } 

	}

	public class FightTurn : BattleTurn
	{
		public int PartySlot { get; }
		public int MoveID { get; } //Needed? If a move was not used, how do we not have this store anything?
		public bool Fainted { get; } //Needed? Other methods alreayd handle this
		public FightTurn(int attackingPartySlot, int moveID, bool defendingFainted)
		{
			this.PartySlot = attackingPartySlot;
			this.MoveID = MoveID;
			this.Fainted = defendingFainted;
				
		}
	}

	public class PokemonSwitchTurn : BattleTurn
	{
		//public int OldPartySlot { get; }
		public int NewPartySlot { get; }
		public PokemonSwitchTurn(int newPartySlot)
		{
			this.NewPartySlot = newPartySlot;
		}
	}

	public class ItemUseTurn : BattleTurn //TODO: place an "Add class object" into the battle itself when an item is used
	{
		public int ItemId { get; }
		public ItemUseTurn(int itemId)
		{
			this.ItemId = itemId;
		}
	}

	public class RunTurn : BattleTurn
	{
		public bool Successful { get; }

		public RunTurn(bool successful)
		{

			this.Successful = successful;
		}
	}
}
