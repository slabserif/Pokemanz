﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public static class BattleUtil
	{

		/*public void UseItem(string chosenItem) //HELP
		{
			if (chosenItem == typeof(PokeBall){
				CatchPokemonUtil.GetCatchRate(hpMax, hpCurrent, ballCatchRateModifer, statusConditionModifier);
				GetShakeProbability(catchRate);
				ShakeCheck(shakeProbability);
			}
		}*/

		public static int CalculatePokemonDamage(Pokemon attackingPokemon, Pokemon defendingPokemon, int moveSlot) 
	{
			bool hitSuccess = CheckIfHit(attackingPokemon, moveSlot); 

			if (hitSuccess)
			{
				float sameTypeAttackBonus = SameTypeAttackBonus(attackingPokemon, moveSlot);
				int damageRandomizationModifier = GetDamageRandomizationModifier();
				float attackTypeModifier = DamageEffectiveness(attackingPokemon.Type1, defendingPokemon.Type1);
				int critical = CriticalHit();
				int modifier = GetDamageModifier(damageRandomizationModifier, attackTypeModifier, (int)sameTypeAttackBonus, critical); //TODO: sameTypeAttackBonus should be a float

				int damage = CalculateDamage(attackingPokemon, defendingPokemon, moveSlot, modifier);
				return damage;
			}
			return 0;
		}

		//TODO: connect SameTypeAttackBonus() to int sameAttackTypeBonus
		private static int GetDamageModifier(int damageRandomizationModifier, float attackTypeModifier, int sameTypeAttackBonus, int critical)
		{
			//TODO: add "other" variable to equation to account for held items
			int modifier = sameTypeAttackBonus * (int)attackTypeModifier * critical * damageRandomizationModifier;
			return modifier;
		}
		private static int CalculateDamage(Pokemon attackingPokemon, Pokemon defendingPokemon, int moveSlot, int modifier)
		{
			int attackingPokemonLevel = attackingPokemon.GetLevel();
			int defendingPokemonLevel = defendingPokemon.GetLevel();
			int attackStat;
			int defenseStat;

			if (attackingPokemon.Moves[moveSlot].Category == MoveCategory.Special)
			{
				attackStat = attackingPokemon.SpAttack.GetValue(attackingPokemonLevel);
				defenseStat = defendingPokemon.SpDefense.GetValue(defendingPokemonLevel);
			}
			else
			{
				attackStat = attackingPokemon.Attack.GetValue(attackingPokemonLevel);
				defenseStat = defendingPokemon.Defense.GetValue(defendingPokemonLevel);
			}

			int damage = ((2 * attackingPokemonLevel + 10 / 250) * (attackStat / defenseStat) * attackingPokemon.Moves[moveSlot].BasePower + 2) * modifier;
			return damage;
		}

		public static bool CheckIfEscapeSuccess(Pokemon playerPokemon, Pokemon opponentPokemon, int escapeCounter)
		{
			int playerSpeed = playerPokemon.Speed.GetValue(playerPokemon.GetLevel());
			int opponentSpeed = playerPokemon.Speed.GetValue(playerPokemon.GetLevel());
			opponentSpeed = (opponentSpeed/4) % 256; 
			if (opponentSpeed == 0)
			{
				return true;
			}

			int checkEscape = ((playerSpeed * 32) / opponentSpeed) + (30 * escapeCounter);
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
				return false;
			}
		}

		public static bool CheckStatusFreeze()
		{
			int checkForRelease = PokemanzUtil.GetRandomNumber(0, 101);
			return checkForRelease >= 20;
		}

		public static bool CheckStatusParalyzed()
		{
			int checkForCanAttack = PokemanzUtil.GetRandomNumber(0, 101);
			return checkForCanAttack >= 75;
		}

		public static int GetSleepTurns()
		{ 
			int sleepNumTurns = PokemanzUtil.GetRandomNumber(1, 8);
			return sleepNumTurns;
		}

		public static bool CheckIfAwake(int sleepNumTurns, int turnsSinceSleep)
		{
			if (sleepNumTurns >= turnsSinceSleep)
			{
				return sleepNumTurns >= turnsSinceSleep;
			}
			return false;
		}

		//TODO: Write method of halving attack of burned pokemon 
		public static int PoisonOrBurnDamage(Pokemon pokemon)
		{
			int hpMax = pokemon.Hp.GetValue(pokemon.GetLevel());
			int damage = hpMax * (1 / 8);
			return damage;
		}

		public static int BadlyPoisonedDamage(Pokemon pokemon, int turnNum)
		{
			int hpMax = pokemon.Hp.GetValue(pokemon.GetLevel());
			int damage = (int)(hpMax * (turnNum / 16.0));
			return damage;
		}


		private static float DamageEffectiveness(PokemonType attackType, PokemonType defenseType)
		{

			float[,] typeChart = new float[17, 17] {
			{1, 1, 1, 1, 1, 0.5f, 1, 0, 0.5f, 1, 1, 1, 1, 1, 1, 1, 1 } , // Normal
			{2, 1, 0.5f, 0.5f, 1, 2, 0.5f, 0, 2, 1, 1, 1, 1, 0.5f, 2, 1, 2} ,  // Fighting
			{1, 2, 1, 1, 1, 0.5f, 2, 1, 0.5f, 1, 1, 2, 0.5f, 1, 1, 1, 1} ,  // Flying
			{1, 1, 1, 0.5f, 0.5f, 0.5f, 1, 0.5f, 0, 1, 1, 2, 1, 1, 1, 1, 1 },  // Poison
			{1, 1, 0, 2, 1, 2, 0.5f, 1, 2, 2, 1, 0.5f, 2, 1, 1, 1, 1},  // Ground
			{1, 0.5f, 2, 1, 0.5f, 1, 2, 1, 0.5f, 2, 1, 1, 1, 1, 2, 1, 1},  // Rock
			{1, 0.5f, 0.5f, 0.5f, 1, 1, 1, 0.5f, 0.5f, 0.5f, 1, 2, 1, 2, 1, 1, 2,},  // Bug
			{0, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 0.5f }, // Ghost
			{1, 1, 1, 1, 1, 2, 1, 1, 0.5f, 0.5f, 0.5f, 1, 0.5f, 1, 2, 1, 1 }, // Steel
			{1, 1, 1, 1, 1, 0.5f, 2, 1, 2, 0.5f, 0.5f, 2, 1, 1, 2, 0.5f, 1 }, //Fire
			{1, 1, 1, 1, 2, 2, 1, 1, 1, 2, 0.5f, 0.5f, 1, 1, 1, 0.5f, 1 }, //Water
			{1, 1, 0.5f, 0.5f, 2, 2, 0.5f, 1, 0.5f, 0.5f, 2, 0.5f, 1, 1, 1, 0.5f, 1}, // Grass
			{1, 1, 2, 1, 0, 1, 1, 1, 1, 1, 2, 0.5f, 0.5f, 1, 1, 0.5f, 1 }, // Electric
			{1, 2, 1, 2, 1, 1, 1, 1, 0.5f, 1, 1, 1, 1, 0.5f, 1, 1, 0 }, // Psychic
			{1, 1, 2, 1, 2, 1, 1, 1, 0.5f, 0.5f, 0.5f, 2, 1, 1, 0.5f, 2, 1 }, // Ice
			{1, 1, 1, 1, 1, 1, 1, 1, 0.5f, 1, 1, 1, 1, 1, 1, 2, 1 }, //Dragon
			{1, 0.5f, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 0.5f}, // Dark
			};

			float attackTypeModifier = typeChart[(int)attackType, (int)defenseType];

			return attackTypeModifier;
		}

		//TODO: integer percentage?
		private static int GetDamageRandomizationModifier()
		{
			int damageRandomizationModifier = PokemanzUtil.GetRandomNumber(217, 256);
			damageRandomizationModifier = ((damageRandomizationModifier * 100) / 255) / 100;
			return damageRandomizationModifier;
		}

		private static float SameTypeAttackBonus(Pokemon pokemon, int moveSlot)
		{
			if (pokemon.Type1 == pokemon.Moves[moveSlot].Type)
			{
				return 1.5f;
			}
			else
			{
				return 1;
			}
		}

		private static int CriticalHit()
		{
			//TODO: add extra critical hit stages based on high-crit ratio specific moves and held items
			int checkForCritical = PokemanzUtil.GetRandomNumber(0, 1001);
			checkForCritical /= 100;
			if (checkForCritical >= 6.25)
			{
				return 2;
			}
			return 1;
		}


		private static bool CheckIfHit(Pokemon pokemon, int moveSlot)
		{
			float accuracyBase = pokemon.Moves[moveSlot].Accuracy / 100;
			float p = accuracyBase * (pokemon.Accuracy / pokemon.Evasion);
			return p > 1;
		}


		//TODO: add corner cases for priority such as move with priority effects 
		public static bool DoesPokemonAttackFirst(Pokemon pokemon1, Pokemon pokemon2) 
		{

			int playerPokemonLevel = pokemon1.GetLevel();
			int opponentPokemonLevel = pokemon2.GetLevel();
			int playerSpeed = pokemon1.Speed.GetValue(playerPokemonLevel);
			int opponentSpeed = pokemon2.Speed.GetValue(opponentPokemonLevel);
			

			if (playerSpeed == opponentSpeed)
			{
				int randomFirst = PokemanzUtil.GetRandomNumber(0, 2);
				if (randomFirst == 1)
				{
					return true;
				}
			}
			else if (playerSpeed > opponentSpeed)
			{
				return true;
			}
			return false;
		}

		public static bool CheckIfMoveHasPp(Pokemon pokemon, int moveSlot) 
		{
			bool outOfPP = pokemon.Moves[moveSlot].PpModifier == pokemon.Moves[moveSlot].PP;
			if (outOfPP)
			{
				return false;
			}
			return true;
		}
	}
}
