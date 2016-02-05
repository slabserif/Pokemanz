using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemanz.Core.Utilities
{
	public class UserInterfaceUtil
	{

		public static void DivideDialogPerScreen(string fullDialogue)
		{
			fullDialogue = "This is some dialogue. Really Really long dialogue.";

			while (!String.IsNullOrWhiteSpace(fullDialogue))
			{
				int fullLength = fullDialogue.Length;
				int maxLength = 20; //Size of dialogue window
				if (fullLength >= maxLength) 
				{
					string truncatedDialogue = fullDialogue.Substring(0, maxLength); //Truncate to the maxLength
					if (truncatedDialogue.Contains(" ")) // if more than one word, (extra check for empty string below)
					{
						truncatedDialogue = truncatedDialogue.Substring(0, truncatedDialogue.LastIndexOf(" ")); //Tuncate at last full word

						if (String.IsNullOrWhiteSpace(truncatedDialogue)) //if string only has spaces after being truncated, only triggered when number of letters in word is greater than max length
						{
							Console.WriteLine("Empty string error");
							truncatedDialogue = fullDialogue.Substring(0, maxLength);
							Console.WriteLine("Empty String Truncated Dialogue: " + truncatedDialogue + "\n");
						}
						Console.WriteLine("Truncated Dialogue: " + truncatedDialogue);

						string remainingText = fullDialogue.Substring(truncatedDialogue.Length, fullLength - truncatedDialogue.Length); //get rest of text that follows what was printed on this screen
						Console.WriteLine("Remaining Text: " + remainingText + "\n");
						fullDialogue = remainingText; //start the while loop over with only the remaining text
					}
					else
					{
						Console.WriteLine("No Full words."); // could cause infinite loop
						string remainingText = fullDialogue.Substring(0, fullLength - maxLength);
						Console.WriteLine("Remaining Text: " + remainingText + "\n");
						fullDialogue = remainingText;
					}

				}
				else
				{
					Console.WriteLine("Not Truncated: " + fullDialogue + "\n"); //if dialogue length does not cut any words (also run when the last set of words can be used) 
					fullDialogue = " "; //clear the dialogue so the while loop closes
				}

			}
			Console.WriteLine("No more text left."); //If all dialogue has been printed
		}

	}
}
