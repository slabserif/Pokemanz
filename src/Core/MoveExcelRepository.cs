using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public class MoveExcelRepository : ExcelRepository<Move>, IMoveRepository
	{
		private MoveExcelRepository(List<Move> moveList) : base(moveList)
		{

		}

		public Move Get(int id)
		{
			Move move = this.itemList.FirstOrDefault(m => m.Id == id);
			if(move == null)
			{
				throw new Exception($"There is no move with the id '{id}'");
			}
			return move;
		}

		public static MoveExcelRepository Create()
		{
			List<Move> moveList = MoveExcelRepository.ParseFile().ToList();
			return new MoveExcelRepository(moveList);
		}

		public static IEnumerable<Move> ParseFile()
		{

			Func<string, PropertyInfo, object> parseCell = (cell, propertyInfo) =>
			{
				if (propertyInfo.PropertyType == typeof(int))
				{
					return int.Parse(cell);
				}
				else if (propertyInfo.PropertyType == typeof(MoveCategory))
				{
					return Enum.Parse(typeof(MoveCategory), cell);
				}
				else if(propertyInfo.PropertyType == typeof(PokemonType))
				{
					return Enum.Parse(typeof(PokemonType), cell);
				}
				else
				{
					return cell;
				}
			};

			Func<Move, Move> endParse = (move) =>
			{
				return move;
			};

			return ExcelRepository<Move>.ParseFile(parseCell, endParse, "Core.compiler.resources.moves.txt");
		}

	}
}
