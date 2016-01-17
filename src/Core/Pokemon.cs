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
        public int BaseValue { get; private set; }
        public int DeterminentValue { get; private set; }
        public int StatExpValue { get; private set; }

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

    public class HealthStat : Stat
    {
        public override int GetValue(int level)
        {

            int statValue = CalcStat(level);
            statValue += level + 10;
            return statValue;
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