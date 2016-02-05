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
				int maxLength = 20;
				if (fullLength >= maxLength)
				{
					string truncatedDialogue = fullDialogue.Substring(0, maxLength);
					if (truncatedDialogue.Contains(" "))
					{
						truncatedDialogue = truncatedDialogue.Substring(0, truncatedDialogue.LastIndexOf(" "));

						if (String.IsNullOrWhiteSpace(truncatedDialogue))
						{
							Console.WriteLine("Empty string");
							truncatedDialogue = fullDialogue.Substring(0, maxLength);
							Console.WriteLine("Empty String Truncated Dialogue: " + truncatedDialogue + "\n");
						}
						Console.WriteLine("Truncated Dialogue: " + truncatedDialogue);

						string nextScreenText = fullDialogue.Substring(truncatedDialogue.Length, fullLength - truncatedDialogue.Length);
						Console.WriteLine("Next Screen Text: " + nextScreenText + "\n");
						fullDialogue = nextScreenText;
					}
					else
					{
						Console.WriteLine("No Full words.");
						string nextScreenText = fullDialogue.Substring(0, fullLength - maxLength);
						Console.WriteLine("Next Screen Text: " + nextScreenText + "\n");
						fullDialogue = nextScreenText;
					}

				}
				else
				{
					Console.WriteLine("Not Truncated: " + fullDialogue + "\n");
					fullDialogue = " ";
				}

			}
			Console.WriteLine("No more text left.");
		}

	}
}
