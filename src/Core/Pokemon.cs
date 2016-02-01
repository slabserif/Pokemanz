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

		//HELP
		//Changed "private set" ? 
		//do these belong on class Pokemon?
		//How do you add initial values?
		public int hpModifier { get; set; }
		public int attackModifier { get; set; }
		public int defenseModifier { get; set; }
		public int spAttackModifier { get; set; }
		public int spDefenseModifier { get; set; }
		public int speedModifier { get; set; }
		public int Accuracy { get; set; }
		public int Evasion { get; set; }
		public int statusCondition { get; set; }

		public List<Move> Moves { get; private set; } = new List<Move>(new Move[4]);
		
		public void AssignMove(Move move, int slotNumber)
		{
			this.Moves[slotNumber - 1] = move;
		}
		
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


		//todo connect with Pokemon class & dont pass in level 
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
		Normal = 0,
		Fighting = 1,
		Flying = 2,
		Poison = 3,
		Ground = 4,
		Rock = 5,
		Bug = 6,
		Ghost = 7,
		Steel = 8,
		Fire = 9,
		Water = 10,
		Grass = 11,
		Electric = 12,
		Psychic = 13,
		Ice = 14, 
		Dragon = 15,
		Dark = 16
	}

	public enum PokemonExpType
	{
		MedSlow,
		MedFast
	}

	public class Move
	{
		public string Name { get; private set; }
		public int Id { get; private set; }
		public PokemonType Type { get; private set; }
		public MoveCategory Category { get; private set; }
		public int PP { get; private set; }
		public int BasePower { get; private set; }
		public int Accuracy { get; private set; }

		//HELP
		//Changed "private set" ? 
		//do these belong on class Move?
		//How do you add initial values?
		public int ppModifier { get; set; }
		public int basePowerModifier { get; set; }

		//TODO
		public static Move GetRandom()
		{
			return new Move
			{
				Accuracy = 1,
				BasePower = 1,
				Category = MoveCategory.Physical,
				Name = "Name",
				PP = 1,
				Type = PokemonType.Fire
			};
		}
	}

	public enum MoveCategory
	{
		None, //HELP is this needed?
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
					case PokeballType.MasterBall:
						return 255;
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
		UltraBall,
		MasterBall
	}

	public class StatusConditions
	{
		public float StatusConditionModifier
		{
			get
			{
				switch (this.Type)
				{
					case StatusType.Paralyze:
					case StatusType.Poison:
					case StatusType.Burn:
						return 5;
					case StatusType.Sleep:
					case StatusType.Freeze:
						return 10;
					default:
						throw new ArgumentOutOfRangeException(nameof(this.Type));
				}
			}
		}
		public StatusType Type { get; set; }
	}

	public enum StatusType
	{
		Paralyze,
		Sleep,
		Burn,
		Freeze,
		Poison
	}
}