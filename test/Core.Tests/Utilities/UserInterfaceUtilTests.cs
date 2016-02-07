using Pokemanz.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests.Utilities
{
    public class UserInterfaceUtilTests
    {
		[Theory]
		[InlineData("This is some dialogue. Really Really long dialogue.", 4)]
		[InlineData("This is some dialogue. Really Really long dialogue. Ethan is dumb.", 5)]
		[InlineData("Ethan dumb.", 1)]
		[InlineData("                                   Ethan           d               dumb                                             s          .", 3)]
		[InlineData("                                            ", 0)]
		[InlineData("supercalifragilisticexpialidosious", 2)]
		public void Test(string fullDialogue, int expectedLineCount)
		{
			IEnumerable<string> enumerableDialogue = UserInterfaceUtil.DivideDialogPerScreen(fullDialogue);
			int lineCount = enumerableDialogue.Count();
			Assert.Equal(expectedLineCount, lineCount);
		}
    }
}
