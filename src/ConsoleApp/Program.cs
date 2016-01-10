using Pokemanz.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemanz.ConsoleApp
{
    //QUESTIONS
    //Where to put "this.thing"
    //How to choose a random class so i can print it
    //How do i return a pokemon based on user choice and print it?
    class Program
    {
        static Pokemon ChoosePokemon()
        {
            IPokemonRepository repository = PokemonExcelRepository.Create();
            while (true)
            { 
                Console.WriteLine("Choose a Pokemon\n\n");
                Console.WriteLine("Type 'b' for Bulbasuar\n");
                Console.WriteLine("Type 'c' for Charmander\n");
                Console.WriteLine("Type 's' for Squirtle\n");
                string name = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine();


                switch (name)
                {
                    case "b":
                        name = "Bulbasuar";
                        break;
                    case "c":
                        name = "Charmander";
                        break;
                    case "s":
                        name = "squirtle";
                        break;
                    default:
                        Console.WriteLine("\nInvaild input, please try again.\n");
                        continue;
                }

                Pokemon starter = repository.GetPokemonByName(name);

                Program.PrintStats(starter);

                Console.WriteLine("\nDo you want to choose " + starter.Name + "?\n'y' for yes, 'n' for no.");
                string pokechoice = Console.ReadLine();
                switch (pokechoice)
                {
                    case "y":
                        return starter;
                    default:
                        continue;
                }
            }
        }


        static void Main(string[] args)
        {
            Pokemon starter = ChoosePokemon();
            Console.WriteLine("Starter Pokemon: " + starter.Name);
            Console.ReadLine();

        }

        public static void PrintStats(Pokemon pokemon)
        {
            Console.WriteLine("Name: " + pokemon.Name);
            Console.WriteLine("Type 1: " + pokemon.Type1);
            Console.WriteLine("Level: " + pokemon.Level + "\n");

            Console.WriteLine("Hp: " + pokemon.Hp);
            Console.WriteLine("Attack: " + pokemon.Attack);
            Console.WriteLine("Defense: " + pokemon.Defense);
            Console.WriteLine("Special Attack: " + pokemon.SpAttack);
            Console.WriteLine("Special Defense: " + pokemon.SpDefense);
            Console.WriteLine("Speed: " + pokemon.Speed);
        }
    }
}






/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class PlayerTurn // Template
    {
        public int symbol;
        public int row;
        public int col;
    }
    public class Program
    {
        public const int playerX = 1;
        public const int playerO = 4;
        public static int[][] ticTacBoard = new int[3][] // Array of an Array of integers
        {
            new int[]{0, 0, 0} ,
            new int[]{0, 0, 0} ,
            new int[]{0, 0, 0}
        };

        // METHOD: Main
        static void Main(string[] args)
        {
            StartGame();
        }



        //METHOD: PLayer inputs row and column
        static PlayerTurn GetPlayerTurn(int player) // take template playerTurn = GetPlayerTurn(currentPlayer) and pass int 4 or int 1 to int player
        {
            Console.Write("\n\nPlayer ");
            switch (player)
            {
                case playerX:
                    Console.WriteLine("X turn:");
                    break;
                case playerO:
                    Console.WriteLine("O: turn:");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(player));
            }
            Console.WriteLine("\nPlease type a row number.");
            int rowValue = ReadIntValue() - 1; // rowValue = intVal returned from ReadIntValue, -1 to match user input with actual column
            Console.WriteLine("\nPlease type a column number.");
            int colValue = ReadIntValue() - 1; // colValue = intVal returned from ReadIntValue -1 to match user input with actual column

            if (rowValue >= 0 && rowValue <= ticTacBoard.Length) // check for valid row number
            {
                if (colValue >= 0 && colValue <= ticTacBoard[rowValue].Length) // check for valid column number
                {

                    if (ticTacBoard[rowValue][colValue] != playerX && ticTacBoard[rowValue][colValue] != playerO) //tile does not have an X or O on it
                    {
                        return new PlayerTurn
                        {
                            row = rowValue,
                            col = colValue,
                            symbol = player
                        };
                    }

                    else //tile already has an X or O on it
                    {
                        Console.WriteLine("\n\n------------------ERROR------------------");
                        Console.WriteLine("This tile has already been taken.");
                        Console.WriteLine("-----------------------------------------");
                        return GetPlayerTurn(player);
                    }
                }
            }

            Console.WriteLine("\n\n------------------ERROR------------------");
            Console.WriteLine("The tile you entered doesn't exist");
            Console.WriteLine("-----------------------------------------");
            return GetPlayerTurn(player);
        }




        // METHOD: Check to make sure user types an integer
        private static int ReadIntValue()
        {
            bool isRow = true;
            string intStr = Console.ReadLine();
            int intVal;
            if (!int.TryParse(intStr, out intVal)) // Take the input from ReadLine (inStr) and if it's an integer, put it in intVal
            {
                Console.WriteLine("\n\n------------------ERROR------------------");
                Console.WriteLine("Please type in an integer.");
                Console.WriteLine("-----------------------------------------");
                return ReadIntValue(); // recursive, restarts itself until the input is a valid integer
            }
            else
            {
                return intVal;
            }
        }


        // METHOD: Apply turn
        public static void ApplyTurn(PlayerTurn playerTurn) // Applying turn takes values (row, col, symbol) from return new PlayerTurn from GetPlayerTurn(currentPlayer)
        {
            ticTacBoard[playerTurn.row][playerTurn.col] = playerTurn.symbol; // update array
        }


        // METHOD: Output Board
        private static void OutputNewBoard()
        {
            string boardTile;
            Console.WriteLine("");
            for (int i = 0; i < ticTacBoard.Length; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < ticTacBoard[i].Length; j++)
                {
                    switch (ticTacBoard[i][j])
                    {
                        case 1:
                            boardTile = "X";
                            break;
                        case 4:
                            boardTile = "O";
                            break;
                        default:
                            boardTile = "-";
                            break;
                    }
                    //ticTacBoard[i][j] = int.Parse(boardTile);
                    Console.Write(boardTile + "  ");
                }
                Console.WriteLine("\n");
            }
        }

        // METHOD: Reset Board
        private static void ResetBoard()
        {
            for (int i = 0; i < ticTacBoard.Length; i++)
            {
                for (int j = 0; j < ticTacBoard[i].Length; j++)
                {
                    ticTacBoard[i][j] = 0;
                }
            }
        }

        //METHOD NEW PLAYER TURN
        static int SwitchPlayer(int _currentPlayer)
        {
            switch (_currentPlayer)
            {
                case playerX:
                    _currentPlayer = playerO;
                    break;
                case playerO:
                    _currentPlayer = playerX;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_currentPlayer));
            }
            return _currentPlayer;
        }



        //METHOD: See if player has won after they take their turn
        static bool WinCheck(PlayerTurn playerTurn)
        {
            //ROWS
            bool rowMatch = DoesRowMatch(playerTurn);

            //COL
            bool colMatch = DoesColMatch(playerTurn);

            //NORTH WEST
            bool northWestMatch = DoesNorthWestMatch(playerTurn);

            //SOUTH EAST
            bool southEastMatch = DoesSouthEastMatch(playerTurn);

            //NORTH EAST
            bool northEastMatch = DoesNorthEastMatch(playerTurn);

            //SOUTH WEST
            bool southWestMatch = DoesSouthWestMatch(playerTurn);


            if (rowMatch || colMatch || northWestMatch || southEastMatch || northEastMatch || southWestMatch == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //ROW MATCH
        private static bool DoesRowMatch(PlayerTurn playerTurn)
        {
            for (int j = 0; j < ticTacBoard[playerTurn.row].Length; j++) //check row of rowValue for win
            {
                if (ticTacBoard[playerTurn.row][j] != playerTurn.symbol) //first instance of no match, exit and check rest of possible wins
                {
                    return false;
                }
            }
            return true;
        }

        //COL MATCH
        private static bool DoesColMatch(PlayerTurn playerTurn)
        {
            for (int i = 0; i < ticTacBoard.Length; i++)
            {
                for (int j = 0; j < ticTacBoard[playerTurn.row].Length; j++)
                {
                    if (ticTacBoard[i][playerTurn.col] != playerTurn.symbol) //first instance of no match, exit and check rest of possible wins
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //DIAGONAL: North West
        private static bool DoesNorthWestMatch(PlayerTurn playerTurn)
        {
            for (int i = ticTacBoard.Length - 1; i >= 0; i--)
            {
                for (int j = ticTacBoard[playerTurn.row].Length - 1; j >= 0; j--)
                {
                    if (ticTacBoard[i][j] != playerTurn.symbol) //first instance of no match, exit and check rest of possible wins
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        //DIAGONAL: South East
        public static bool DoesSouthEastMatch(PlayerTurn playerTurn)
        {
            for (int i = 0; i < ticTacBoard.Length; i++)
            {
                if (ticTacBoard[i][i] != playerTurn.symbol) //first instance of no match, exit and check rest of possible wins
                {
                    return false;
                }
            }
            return true;
        }


        //DIAGONAL: North East
        private static bool DoesNorthEastMatch(PlayerTurn playerTurn)
        {
            for (int i = ticTacBoard.Length - 1; i >= 0; i--)
            {
                for (int j = ticTacBoard[playerTurn.row].Length - 1; j >= 0; j--)
                {
                    if (ticTacBoard[i][j] != playerTurn.symbol) //first instance of no match, exit and check rest of possible wins
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        //DIAGONAL: South West
        private static bool DoesSouthWestMatch(PlayerTurn playerTurn)
        {
            for (int i = 0; i < ticTacBoard.Length; i++)
            {
                if (ticTacBoard[i][2 - i] != playerTurn.symbol) //first instance of no match, exit and check rest of possible wins
                {
                    return false;
                }
            }
            return true;
        }

        static void ApplyWinCheck(bool _gameOver, PlayerTurn playerTurn)
        {
            string playerLetter;

            switch (playerTurn.symbol)
            {
                case 1:
                    playerLetter = "X";
                    break;
                case 4:
                    playerLetter = "O";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerLetter));
            }

            if (_gameOver == true)
            {
                Console.WriteLine("Player " + playerLetter + " wins!\n");
                NewGameCheck();
            }
        }

        static void NewGameCheck()
        {
            Console.WriteLine("Press 'N' for New Game or press 'X' to close.");
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "n":
                    ResetBoard();
                    StartGame();
                    break;
                case "x":
                    //Environment.Exit(0);
                    Console.Write("Pressed X");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("\n\n------------------ERROR------------------");
                    Console.WriteLine("Invalid Input.");
                    Console.WriteLine("-----------------------------------------");
                    NewGameCheck();
                    break;
            }
        }
        //METHOD: Start Game
        static void StartGame()
        {
            bool gameOver = false;
            int currentPlayer = playerX;

            Console.WriteLine("\nTic Tac Toe\n");

            OutputNewBoard();

            while (!gameOver)
            {

                PlayerTurn playerTurn = GetPlayerTurn(currentPlayer); // new template of PlayerTurn = playerTurn. currentPlayer = int 4 or int 1 passed to GetPlayerTurn function.

                ApplyTurn(playerTurn); // Applying turn takes values (row, col, symbol) from return new PlayerTurn from GetPlayerTurn(currentPlayer)

                OutputNewBoard();

                gameOver = WinCheck(playerTurn);

                ApplyWinCheck(gameOver, playerTurn);

                currentPlayer = SwitchPlayer(currentPlayer);
            }
        }
    }
}*/





