using System.Text.Json;
using WorldOfZuul.DTOs;
using WorldOfZuul.Exceptions;

namespace WorldOfZuul;

public class RegionDataParser
{
    private FileReader fileReader;
    private string fileName;

    public RegionDataParser()
    {   
        this.fileName = "region-data.json";
        this.fileReader = new FileReader();
    }

    public List<Region> DeserializeRegionData()
    {
        try
        {
            string rawJsonData = this.fileReader.ReadJsonFile(fileName);
            List<RegionDTO> regionDtos = JsonSerializer.Deserialize<List<RegionDTO>>(rawJsonData);
            
            if(regionDtos == null)
            {
                throw new Exception("Error parsing data to DTO. Region data could not be parsed. Data equals to null");
            }

            List<Region> regions = regionDtos.Select(dto =>
                new Region(
                    dto.RegionName,
                    dto.RegionDescription,
                    dto.RegionCPI,
                    dto.State.StateName,
                    dto.State.StateDescription
                )
            ).ToList();

            if (regions == null)
            {
                throw new Exception("Error parsing DTO to class.");
            }
            
            return regions;
        }
        catch (FileReadException e)
        {
            Console.WriteLine("Error in DeserializeRegionData method. Error reading file.");
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error in DeserializeRegionData method. Error deserializing file.");
            Console.WriteLine(e.Message);
        }
        
        //Default Fallback 
        return new List<Region>();
    }
}