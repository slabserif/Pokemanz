using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
    public class BattleUtil
    {
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
			int damageRandomizationModifier = PokemanzUtil.GetRandomNumber(217, 255); 
			damageRandomizationModifier = ((damageRandomizationModifier * 100)/255)/100;
			return damageRandomizationModifier;
		}

		//TODO: Get move type?
		public static float SameTypeAttackBonus(Pokemon Type1, Move Type)
		{
			if (Type1 = Type)
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
			int checkForCritical = PokemanzUtil.GetRandomNumber(0, 1000);
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
		public static int CalculateDamage(int modifier, int level, int attack, int defense, MoveCategory category, Move BasePower, Pokemon Attack, Pokemon Defense, Pokemon SpAttack, Pokemon SpDefense)
		{
			int attackStat = Pokemon.attack;
			int defenseStat = Pokemon.defense;

			if (category = Special)
			{
				int attackStat = Pokemon.spAttack;
				int defenseStat = Pokemon.spDefense;
			}

			int damage = ((2 * level + 10 / 250) * (attackStat / defenseStat) * Move.basePower + 2) * modifier;
			return damage;

		}
}
