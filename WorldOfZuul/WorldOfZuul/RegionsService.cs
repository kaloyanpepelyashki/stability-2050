using WorldOfZuul.DTOs;

namespace WorldOfZuul;

/// <summary>
/// In charge of handling all tasks related to regions, their initialisation, mutation etc.
/// </summary>
public class RegionsService
{   
    //TODO - has to be changed, it must be passed as a DI at runtime through the constructor
    private RegionDataParser regionDataParser = new RegionDataParser();
    
    public RegionsService()
    {
        
    }
    
    /// <summary>
    /// In charge of initialsing the regions, their states and their exits
    /// </summary>
    /// <returns>Returns a list of regions</returns>
    public List<Region> InitialiseRegions()
    {
        try
        {
            List<RegionDTO> regionDTOs = regionDataParser.DeserializeRegionData();
            
            List<Region> regions = regionDTOs.Select(dto => 
                new Region(
                    dto.RegionName,
                    dto.RegionDescription,
                    dto.RegionCPI,
                    dto.State.StateName,
                    dto.State.StateDescription
                )
            ).ToList();

            Dictionary<string, Region> regionsDict = ConvertListToDictionary(regions);
            
            // Iterates over the regions and together with that iterates over the regionDTOs to assign the exits (present in the regionDTOs) to the regions
            for (int i = 0; i < regions.Count; i++)
            {
                string currentRegionTitle = regions[i].RegionName;
                
                for (int j = 0; j < regionDTOs.Count; j++)
                {
                    if (regionDTOs[j].RegionName ==  currentRegionTitle)
                    { 
                        RegionDTO currentRegionDto =  regionDTOs[j];
                        //Before assigning each of the exits from the DTO, it's checked if the exit is not an empty string, to avoid breaking it and throwing an exception that his key doesn't exist in the dictionary
                        Region? northExit = (!string.IsNullOrEmpty(currentRegionDto.Exits.North)) ? regionsDict[currentRegionDto.Exits.North] : null;
                        Region? eastExit = (!string.IsNullOrEmpty(currentRegionDto.Exits.East)) ? regionsDict[currentRegionDto.Exits.East] : null;
                        Region? southExit = (!string.IsNullOrEmpty(currentRegionDto.Exits.South)) ? regionsDict[currentRegionDto.Exits.South] : null;
                        Region? westExit = (!string.IsNullOrEmpty(currentRegionDto.Exits.West)) ? regionsDict[currentRegionDto.Exits.West] : null;
                        
                        //Assigns the (sets) the exits to the region, currently itereated over.
                        regions[i].SetExits(northExit, eastExit, southExit, westExit); 
                    }
                } 
            }
            
            
            // TODO REMOVE THE VOLLOWING CODE BLOCK, IT'S JUST FOR TESTING PURPOSES
            Console.WriteLine("====== Regions and the exits to them: ");

            for (var i = 0; i < regions.Count; i++)
            {   
                Console.WriteLine($"Exits for {regions[i].RegionName} :");
                foreach (KeyValuePair<string, Region> exit in regions[i].Exits)
                {
                    Console.WriteLine($"   -{exit.Key} exit {exit.Value.RegionName}");
                }
            }    
            
            // CODE BLOCK ENDS HERE
            
            return regions;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
        }

        return new List<Region>();
    }

    private Dictionary<string, Region> ConvertListToDictionary(List<Region> regions)
    {
        try
        {
            Dictionary<string, Region> dict = regions.ToDictionary(reg => reg.RegionName);

            return dict;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error converting list of regions to a dictionary");
            throw e; 
        }
    }
    
}