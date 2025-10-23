using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{   
    /// <summary>
    /// Represents a command entered by the player.
    /// A command typically consists of a primary command word (e.g., "look", "north")
    /// and optionally a second word (e.g., "take apple") for future expansion.
    /// </summary>
    public class Command
    {   
        /// <summary>
        /// Gets the main command word entered by the player.
        /// Examples include "look", "north", "help", or "quit".
        /// </summary>
        public string Name { get; }
        public string? SecondWord { get; } // this might be used for future expansions, such as "take apple".
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="name">The primary command word entered by the player.</param>
        /// <param name="secondWord">
        /// An optional second word providing additional context for the command.
        /// Defaults to <c>null</c> if not provided.
        /// </param>
        public Command(string name, string? secondWord = null)
        {
            Name = name;
            SecondWord = secondWord;
        }
    }
}
