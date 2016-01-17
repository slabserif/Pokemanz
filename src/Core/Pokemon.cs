using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public class Pokemon
	{
		public string Name { get; private set; }
		public int Id { get; private set; }
		[PokemonProperty("Type 1")]
		public PokemonType Type1 { get; private set; }
		[PokemonProperty("Type 2")]
		public PokemonType Type2 { get; private set; }
		public Stat Hp { get; private set; }
		public Stat Attack { get; private set; }
		public Stat Defense { get; private set; }
		public Stat SpAttack { get; private set; }
		public Stat SpDefense { get; private set; }
		public Stat Speed { get; private set; }
		public int Experience { get; private set; }
		public PokemonExpType ExpType { get; private set; }
		public int BaseExpGiven { get; private set; }

		public bool AddExperience(int expEarned)
		{
			int levelBefore = GetLevel();
			this.Experience += expEarned;
			int levelAfter = GetLevel();
			return levelAfter > levelBefore;
		}

		public int GetLevel()
		{
			switch (this.ExpType)
			{
				case PokemonExpType.MedFast:
					const double oneThird = 1.0 / 3.0;
					return (int)Math.Pow(this.Experience, oneThird);
				case PokemonExpType.MedSlow:
					int level = 2;
					while (true)
					{
						int lvlCubed = (int)Math.Pow(level, 3);
						int expForLevelUp = (((6 / 5) * (lvlCubed)) - (15 * (level * level)) + (100 * level)) - 140;
						if (expForLevelUp > this.Experience)
						{
							return level - 1;
						}
						if (expForLevelUp == this.Experience)
						{
							return level;
						}
						level++;
					}
				default:
					throw new ArgumentOutOfRangeException(nameof(this.ExpType));
			}
		}
	}

	public class Stat
	{
		public int BaseValue { get; }
		public int DeterminentValue { get; }
		public int StatExpValue { get; private set; }

		public Stat(int baseValue)
		{
			this.BaseValue = baseValue;
			this.DeterminentValue = PokemanzUtil.GetRandomNumber(0, 16);
		}

		public Stat(int baseValue, int determinantValue)
		{
			this.BaseValue = baseValue;
			this.DeterminentValue = determinantValue;
		}


		protected int CalcStat(int level)
		{
			double newStat = Math.Floor(((this.BaseValue + this.DeterminentValue) * 2) + ((Math.Sqrt(this.StatExpValue) / 4) * level) / 100);
			return (int)newStat;
		}

		public virtual int GetValue(int level)
		{
			int statValue = CalcStat(level);
			statValue += 5;
			return statValue;
		}
	}

	public sealed class HealthStat : Stat
	{
		public HealthStat(int baseValue, int dvAttack, int dvDefense, int dvSpecial, int dvSpeed) 
			: base(baseValue, HealthStat.GetHpDeterminent(dvAttack, dvDefense, dvSpecial, dvSpeed))
		{
		}
		

		public override int GetValue(int level)
		{
			int statValue = CalcStat(level);
			statValue += level + 10;
			return statValue;
		}

		//Determinant Value for hp is unique because it is calculated based on the results of the other stat dvs.
		private static int GetHpDeterminent(int dvAttack, int dvDefense, int dvSpecial, int dvSpeed)
		{
			int dvHp = 0;
			if (PokemanzUtil.IsOdd(dvAttack))
			{
				dvHp += 8;
			}
			if (PokemanzUtil.IsOdd(dvDefense))
			{
				dvHp += 4;
			}
			if (PokemanzUtil.IsOdd(dvSpecial))
			{
				dvHp += 1;
			}
			if (PokemanzUtil.IsOdd(dvSpeed))
			{
				dvHp += 2;
			}
			return dvHp;
		}

	}


	public class PokemonPropertyAttribute : Attribute
	{
		public string Name { get; set; }
		public PokemonPropertyAttribute(string name)
		{
			this.Name = name;
		}
	}

	public enum PokemonType
	{
		Grass,
		Water,
		Fire,
		Flying,
		Electric,
		Normal,
		Poison
	}

	public enum PokemonExpType
	{
		MedSlow,
		MedFast
	}

}