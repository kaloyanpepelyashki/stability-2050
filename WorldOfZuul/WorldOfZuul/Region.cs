namespace WorldOfZuul
{
    public class RegionExits
    {
        
    }
    /// <summary>
    /// Represents a location (region) within the game.
    /// Each room has a short and long description, and can connect to other rooms through exits.
    /// </summary>
    public class Region
    {   
        /// <summary>
        /// The name (title) of the region
        /// </summary>
        public string RegionName { get; private set; }
        
        /// <summary>
        /// A brief description of the region, gives an overview of the region and the situation in the region
        /// </summary>
        public string RegionDescription { get; private set; }
        
        /// <summary>
        /// The state, of the region, where the player lands
        /// </summary>
        public State RegionState { get; private set; }
 
        public Dictionary<string, Region> Exits { get; private set; } = new();
        
        /// <summary>
        /// The index of the corruption preception in a given region
        /// </summary>
        public double RegionCpi { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class with the specified short and long descriptions.
        /// </summary>
        /// <param name="regionName">The name of the region.</param>
        /// <param name="regionDesc">The brief description of the region, gives an overview of the region and the situation in the region.</param>
        /// <param name="regionCpi"> The CPI of the region</param>
        /// <param name="stateProps"> A tuple holding the data needed for initialising the state in a specific region</param>
        public Region(string regionName, string regionDesc, double regionCpi, string stateName, string stateDescription)
        {
            RegionName = regionName;
            RegionDescription = regionDesc;
            RegionCpi = regionCpi;
            RegionState = new State(stateName, stateDescription);
        }
        
        /// <summary>
        /// Defines multiple exits from the room at once (north, east, south, and west).
        /// </summary>
        /// <param name="north">The region located to the north, or <c>null</c> if none.</param>
        /// <param name="east">The region located to the east, or <c>null</c> if none.</param>
        /// <param name="south">The region located to the south, or <c>null</c> if none.</param>
        /// <param name="west">The region located to the west, or <c>null</c> if none.</param>
        /// <remarks>
        /// This method is a convenience wrapper that calls <see cref="SetExit"/> for each direction.
        /// </remarks>
        public void SetExits(Region? north, Region? east, Region? south, Region? west)
        {
            //Checks for each exit, if a parameter is assigned, and if it is, sets an exit for it
            if (north != null) SetExit("north", north);
            if (east != null) SetExit("east", east);
            if(south != null) SetExit("south", south);
            if(west != null) SetExit("west", west);
        }
        
        /// <summary>
        /// Defines a single exit from this room to a neighboring room in the given direction.
        /// </summary>
        /// <param name="direction">The direction of the exit (e.g., "north", "east").</param>
        /// <param name="neighbor">The room in that direction, or <c>null</c> if no connection exists.</param>
        /// <remarks>
        /// If <paramref name="neighbor"/> is <c>null</c>, the exit is not created.
        /// </remarks>
        public void SetExit(string direction, Region? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }
    }
}
