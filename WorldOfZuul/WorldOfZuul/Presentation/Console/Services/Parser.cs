using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    /// <summary>
    /// The Parser class is responsible for interpreting player input.
    /// It breaks the player's text command into words and creates a Command object if valid.
    /// </summary>
    public class Parser
    {
        private readonly CommandWords commandWords = new();
        /// <summary>
        ///Parses the player's input and converts it into a Command object.
        /// Returns null if the input is invalid or unrecognized.
        /// </summary>
        public Command? GetCommand(string inputLine)
        {
            string[] words = inputLine.Split();

            if (words.Length == 0 || !commandWords.IsValidCommand(words[0]))
            {
                return null;
            }

            if (words.Length > 1)
            {
                return new Command(words[0], words[1]);
            }

            return new Command(words[0]);
        }
    }

}
