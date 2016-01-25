using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public class BattleUtil
	{
		
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

		//TODO: Write method of halving attack of burned pokemon 
		public static int PoisonOrBurnDamage(int hpMax)
		{
			int damage = hpMax * (1 / 8);
			return damage;
		}

		public static int BadlyPoisonedDamage(int hpMax, int turnNum)
		{
			int damage = (int)(hpMax * (turnNum / 16.0));
			return damage;
		}


		public static float DamageEffectiveness(PokemonType attackType, PokemonType defenseType)
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
		public static int GetDamageRandomizationModifier()
		{
			int damageRandomizationModifier = PokemanzUtil.GetRandomNumber(217, 256);
			damageRandomizationModifier = ((damageRandomizationModifier * 100) / 255) / 100;
			return damageRandomizationModifier;
		}

		//TODO: Get move type?
		public static float SameTypeAttackBonus(Pokemon pokemon, Move move)
		{
			if (pokemon.Type1 == move.Type)
			{
				return 1.5f;
			}
			else
			{
				return 1;
			}
		}

		public static int CriticalHit()
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

		//TODO: connect SameTypeAttackBonus() to int sameAttackTypeBonus
		public static int GetDamageModifier(int damageRandomizationModifier, float attackTypeModifier, int sameAttackTypeBonus, int critical)
		{
			//TODO: add "other" variable to equation to account for held items
			int modifier = sameAttackTypeBonus * (int)attackTypeModifier * critical * damageRandomizationModifier;
			return modifier;
		}

		//TODO: Get level? Help with getting classes
		//public static int CalculateDamage(int modifier, int level, int attack, int defense, MoveCategory category, int basePower, int attack, int pokemonDefense, int pokemonSpAttack, int pokemonSpDefense)
		public static int CalculateDamage(Pokemon attackingPokemon, Pokemon defendingPokemon, Move move, int modifier)
		{
			int attackingPokemonLevel = attackingPokemon.GetLevel();
			int defendingPokemonLevel = defendingPokemon.GetLevel();
			int attackStat;
			int defenseStat;

			if (move.Category == MoveCategory.Special)
			{
				attackStat = attackingPokemon.SpAttack.GetValue(attackingPokemonLevel);
				defenseStat = defendingPokemon.SpDefense.GetValue(defendingPokemonLevel);
			}
			else
			{
				attackStat = attackingPokemon.Attack.GetValue(attackingPokemonLevel);
				defenseStat = defendingPokemon.Defense.GetValue(defendingPokemonLevel);
			}

			int damage = ((2 * attackingPokemonLevel + 10 / 250) * (attackStat / defenseStat) * move.BasePower + 2) * modifier;
			return damage;
		}

		//TODO: Method for calculating current accuracy of pokemon in a battle in case its accuracy has been affected by moves used against it. Called "Stat Modifiers" in http://bulbapedia.bulbagarden.net/wiki/Accuracy
		public static bool CheckForMiss(float pokemonAccuracy, float pokemonEvasion, float moveAccuracy)
		{
			float accuracyBase = moveAccuracy / 100;
			float p = accuracyBase * (pokemonAccuracy / pokemonEvasion);
			return p > 1;
		}
	}
}
