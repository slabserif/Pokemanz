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
        //--Is the stat calculation meant to add on to an existing stat, or replace it?

        //Dv randomizer is used in generating the pokemon's initial stats, as well as being used in the level up formula produce diverse stats between two of the same pokemon at the same level. Dv's do not change after a pokemon has been initialized.
        //Dv reserach is derived from: http://bulbapedia.bulbagarden.net/wiki/Individual_values
        static int GetDeterminentValue()
        {
            Random random = new Random();
            int dv = random.Next(0, 16);
            return dv;
        }

        //Dv for hp is unique because it is calculated based on the results of the other stat dvs.
        static int DeterminentRandomizerHp(int dvAttack, int dvDefense, int dvSpecial, int dvSpeed)
        {
            int dvHp = 0;
            if(IsOdd(dvAttack)){
                dvHp += 8;
            }
            if(IsOdd(dvDefense)){
                dvHp += 4;
            }
            if(IsOdd(dvSpecial)){
                dvHp += 1;
            }
            if(IsOdd(dvSpeed)){
                dvHp += 2;
            }
            return dvHp;
        }

        private static bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

            //TODO Calculate exp needed to gain next level (based on exp gain type for pokemon)
        //Research derived from: http://bulbapedia.bulbagarden.net/wiki/Experience


        //TODO Battle win results.
        //Add exp to pokemon who fought
        //variable pokemon.expGiven is a column in the repository
        //variable winPokemon is the pokemon you are using
        //variable enemyPokemon is the pokemon you have defeated
        //variable winPOkemon.nextExp is from NextLevelExp() and is the exp needed to level up
        //variable winPokemon.currentExp
        public static void PokemonDefeatExpCalc(int expGiven)
        {
            if (winPokemon.expCount + enemyPokemon.expGiven >= winPokemon.expForLevelUp)
            {
                int extraExp = -1 * (winPokemon.expForLevelUp - (winPokemon.expCount + enemyPokemon.expGiven));
                NextLevelExp(level, expType);
                winPokemon.expCount = extraExp;
                if (winPokemon.expCount >= winPokemon.expForLevelUp)
                {
                    //TODO repeat top if statement again
                }
            }
            else
            {
                winPokemon.expCount += enemyPokemon.expGiven;
            }
         }

        //TODO When a pokemon is defeated, add its base stats as stat exp to the winning pokemon's corresponding stat
        //These values are used in the statcalc method but under the name EV

        public static int PokemonDefeatStatExp(int baseStat)  //not the actual pokemon's stat, but the base stat for its species
        {
            decimal statIncrease = Math.Floor(Math.Sqrt(baseStat) / 4);
            return statIncrease;
        }

        public static int StoreStatIncrease(int statIncrease)
        {
            WinPokemon.statExpBeforeLevelUp += statIncrease;
            return WinPokemon.statExpBeforeLevelUp;
        }

        //if leveled up, reset the ev counter for each stat
        public static void ResetStatExp()
        {
            WinPokemon.statExpBeforeLevelUp = 0;
        }

        public static int LevelUp(int level)
        {
            return pokemon.level++;
        }

        //SpriteSheet Front

        public static int GetFrontSpriteRow(int id)
        {
            return id / 16;
        }

        public static int GetFrontSpriteCol(int id)
        {
            return id % 16;
        }

    }
}
