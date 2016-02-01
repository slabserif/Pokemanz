using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core
{
	public interface IMoveRepository
	{
		Move Get(int id);
		Move GetRandom();
	}
}
