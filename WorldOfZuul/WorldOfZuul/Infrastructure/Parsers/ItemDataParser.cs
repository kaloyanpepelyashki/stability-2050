using System.Text.Json;
using WorldOfZuul.DTOs;
using WorldOfZuul.Exceptions;

namespace WorldOfZuul;

public class ItemDataParser
{
    
    private FileReader fileReader;
    private string _fileName;

    public ItemDataParser()
    {   
        _fileName = "var/item-data.json";
        fileReader = new FileReader();
        
    }
    
    public List<ItemDataDTO> DeserialiseItemData()
    {
        try
        {
            string rawJsonData = this.fileReader.ReadJsonFile(_fileName);
            List<ItemDataDTO> itemDataDtos = JsonSerializer.Deserialize<List<ItemDataDTO>>(rawJsonData);
            
            if(itemDataDtos == null)
            {
                throw new Exception("Error parsing data to DTO. Region data could not be parsed. Data equals to null");
            }

            return itemDataDtos;
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
        return new List<ItemDataDTO>(); 
    }
}