using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core.Utilities
{
	public class UserInterfaceUtil
	{


		public static IEnumerable<string> DivideDialogPerScreen(string fullDialogue)
		{
			while (!String.IsNullOrWhiteSpace(fullDialogue))
			{
				int fullLength = fullDialogue.Length;
				int maxLength = 20; //Size of dialogue window
				if (fullLength >= maxLength) 
				{
					string truncatedDialogue = fullDialogue.Substring(0, maxLength); //Truncate to the maxLength

					int lastSpaceIndex = truncatedDialogue.LastIndexOf(" ");
					if (lastSpaceIndex >= 0)
					{
						truncatedDialogue = truncatedDialogue.Substring(0, lastSpaceIndex); //Tuncate at last full word
					}

					if (!String.IsNullOrWhiteSpace(truncatedDialogue)) //if string only has spaces after being truncated, only triggered when number of letters in word is greater than max length
					{
						yield return truncatedDialogue;
					}

					string remainingText = fullDialogue.Substring(truncatedDialogue.Length, fullLength - truncatedDialogue.Length); //get rest of text that follows what was printed on this screen
					fullDialogue = remainingText; //start the while loop over with only the remaining text

				}
				else
				{
					yield return fullDialogue;
					break;
				}

			}
		}

	}
}
