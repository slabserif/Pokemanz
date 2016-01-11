using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
    public static class PokemanzUtil
    {
        //TODO: Pokemon stat calculator
        //Stat Research is derived from: http://bulbapedia.bulbagarden.net/wiki/Statistic

        //METHOD: PokemonStatCalc();
        //Runs a stat calculation 5 times, once for each stat (attack, defence, spattack, spdefense, speed)
        //Runs an Hp calculation 

        //Needs to receive 
        //--int dv from DvRandomizer()
        //--Level of current pokemon
        //--Base Stats from tab delimited for pokemon
        //--Count of total Evs earned between last level up and new level up
        //--New moves if any (NOT MVP)
        //--Evolution (NOT MVP)
        //If hits level 100, cannot level anymore (NOT MVP)

        //QUESTIONS:
        //--BaseStats found online seem too high and lack clarity. Are these stats assuming the pokemon is at level 1, 0, 100, or what? 
        //--Is the stat calculation meant to add on to an existing stat, or replace it?
        //--How do Exp points fit into this and what determines how much exp is needed per level?

        //Dv randomizer is used in generating the pokemon's initial stats, as well as making levelling up produce diverse stats between two of the same pokemon at the same level.
        //Dv reserach is derived from: http://bulbapedia.bulbagarden.net/wiki/Individual_values
        static int DvRandomizer(int min, int max)
        {
            Random random = new Random();
            int dv = random.Next(0, 16);
            return dv;
        }


        //TODO: write method to loop through each stat type and assign a new value
        public static int LevelUpBaseStat(int pokemon.basestat, int dv)
        {
            decimal newStat = ((((basestat + dv) * 2) + ((Math.Sqrt(Ev) / 4) * level) / 100);
            if (pokemon.basestat == hp)
            {
                newStat += level + 10;
            }
            else
            {
                newStat += 5;
            }
            newStat = Math.Floor(newStat);
            int levelUpBasestat = Decimal.ToInt32(newStat);
            return levelUpBasestat;
        }


        //TODO Calculate exp needed to gain next level (based on exp gain type for pokemon)
        //Research derived from: http://bulbapedia.bulbagarden.net/wiki/Experience

        public static int NextLevelExp(int pokemon.level, string pokemon.expType)
        {
            int lvlCubed = Math.Pow(pokemon.level, 3);
            int nextExp = 0;
            switch (pokemon.expType)
            {
                case "MedFast":
                    nextExp = lvlCubed;
                    return nextExp;
                case "MedSlow":
                    nextExp = ((6 / 5) * (lvlCubed)) - (15 * (pokemon.level * pokemon.level)) + (100 * pokemon.level)) -140;
                    return nextExp;
            }
        }
    }
}
