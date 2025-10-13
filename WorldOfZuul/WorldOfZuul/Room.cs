namespace WorldOfZuul
{
    /// <summary>
    /// Represents a location (room) within the game.
    /// Each room has a short and long description, and can connect to other rooms through exits.
    /// </summary>
    public class Room
    {   
        /// <summary>
        /// Gets a brief, one-line description of the room.
        /// </summary>
        public string ShortDescription { get; private set; }
        /// <summary>
        /// Gets a detailed description of the room, shown when the player uses the "look" command.
        /// </summary>
        public string LongDescription { get; private set;}
        public Dictionary<string, Room> Exits { get; private set; } = new();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class with the specified short and long descriptions.
        /// </summary>
        /// <param name="shortDesc">The brief description of the room.</param>
        /// <param name="longDesc">The detailed description of the room.</param>
        public Room(string shortDesc, string longDesc)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
        }
        
        /// <summary>
        /// Defines multiple exits from the room at once (north, east, south, and west).
        /// </summary>
        /// <param name="north">The room located to the north, or <c>null</c> if none.</param>
        /// <param name="east">The room located to the east, or <c>null</c> if none.</param>
        /// <param name="south">The room located to the south, or <c>null</c> if none.</param>
        /// <param name="west">The room located to the west, or <c>null</c> if none.</param>
        /// <remarks>
        /// This method is a convenience wrapper that calls <see cref="SetExit"/> for each direction.
        /// </remarks>
        public void SetExits(Room? north, Room? east, Room? south, Room? west)
        {
            SetExit("north", north);
            SetExit("east", east);
            SetExit("south", south);
            SetExit("west", west);
        }
        
        /// <summary>
        /// Defines a single exit from this room to a neighboring room in the given direction.
        /// </summary>
        /// <param name="direction">The direction of the exit (e.g., "north", "east").</param>
        /// <param name="neighbor">The room in that direction, or <c>null</c> if no connection exists.</param>
        /// <remarks>
        /// If <paramref name="neighbor"/> is <c>null</c>, the exit is not created.
        /// </remarks>
        public void SetExit(string direction, Room? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }
    }
}
