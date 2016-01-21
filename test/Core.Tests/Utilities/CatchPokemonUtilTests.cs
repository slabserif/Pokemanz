using Pokemanz.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Utilities
{
	public class CatchPokemonUtilTests
	{
		[Theory]
		[InlineData(1, 1, 1, 0, 1)]
		[InlineData(200, 100, 2, 10, 11)]
		[InlineData(200, 100, 2, 255, 255)]
		public void Test1(int hpMax, int hpCurrent, int ballCatchRateModifier, int statusConditionModifier, int expectedCatchRate)
		{
			double catchRate = CatchPokemonUtil.GetCatchRate(hpMax, hpCurrent, ballCatchRateModifier, statusConditionModifier);
			Assert.Equal(expectedCatchRate, catchRate);
		}
	}
}
