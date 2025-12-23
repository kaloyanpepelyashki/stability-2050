using System.Text.Json;
using WorldOfZuul.DTOs;
using WorldOfZuul.Exceptions;

namespace WorldOfZuul;

public class RegionDataParser : IRegionDataParser
{
    private FileReader fileReader;
    private string fileName;

    public RegionDataParser()
    {   
        this.fileName = "var/region-data.json";
        this.fileReader = new FileReader();
    }
    
    /// <summary>
    /// This method is in charge of reading the data from the JSON file storing the Regions information and deserialising the JSON string
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public List<RegionDTO> DeserializeRegionData()
    {
        try
        {
            string rawJsonData = this.fileReader.ReadJsonFile(fileName);
            List<RegionDTO> regionDtos = JsonSerializer.Deserialize<List<RegionDTO>>(rawJsonData);
            
            if(regionDtos == null)
            {
                throw new Exception("Error parsing data to DTO. Region data could not be parsed. Data equals to null");
            }

            return regionDtos;
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
        return new List<RegionDTO>();
    }
}