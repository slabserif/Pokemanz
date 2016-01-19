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
		public HealthStat Hp { get; private set; }
		public Stat Attack { get; private set; }
		public Stat Defense { get; private set; }
		public Stat SpAttack { get; private set; }
		public Stat SpDefense { get; private set; }
		public Stat Speed { get; private set; }
		public int Experience { get; private set; }
		public PokemonExpType ExpType { get; private set; }
		public int BaseExpGiven { get; private set; }
		public int BaseCatchRate { get; private set; }

		public int MoveSlot1 { get; private set; }
		public int MoveSlot2 { get; private set; }
		public int MoveSlot3 { get; private set; }
		public int MoveSlot4 { get; private set; }

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
		public int EffortValue { get; private set; }

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

		public override string ToString()
		{
			return $"Base Value: {this.BaseValue}, Determinent Value: {this.DeterminentValue}, Effort Value: {this.EffortValue}";
		}

		protected int CalcStat(int level)
		{
			double newStat = Math.Floor(((this.BaseValue + this.DeterminentValue) * 2) + ((Math.Sqrt(this.EffortValue) / 4) * level) / 100);
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
		private static int GetHpDeterminent(int dvAttack, int dvDefense, int dvSpecialAttack, int dvSpeed)
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
			if (PokemanzUtil.IsOdd(dvSpecialAttack))
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
		Poison,
		Ground,
		Rock,
		Ghost,
		Steel,
		Dark,
		Bug,
		Ice,
		Dragon,
		Fighting,
		Psychic
	}

	public enum PokemonExpType
	{
		MedSlow,
		MedFast
	}

	public class Moves
	{
		public string Name { get; private set; }
		public int Id { get; private set; }
		public PokemonType Type { get; private set; }
		public MoveCategory Category { get; private set; }
		public int PP { get; private set; }
		public int BasePower { get; private set; }
		public int Accuracy { get; private set; }

	}

	public enum MoveCategory
	{
		Physical,
		Special,
		Status
	}

	public class PokeBall
	{
		public float BallCatchRateModifer
		{
			get
			{
				switch (this.Type)
				{
					case PokeballType.PokeBall:
						return 1;
					case PokeballType.GreatBall:
						return 1.5f;
					case PokeballType.UltraBall:
						return 2;
					default:
						throw new ArgumentOutOfRangeException(nameof(this.Type));
				}
			}
		}
		public PokeballType Type { get; set; }
	}

	public enum PokeballType
	{
		PokeBall,
		GreatBall,
		UltraBall
	}

	//TODO: inherited enum class for types of Status conditions?
}