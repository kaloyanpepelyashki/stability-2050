namespace WorldOfZuul;

public class FileReader
{

    public FileReader(){}

    public string ReadJsonFile(string filename)
    {
        try
        {
           
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            string jsonString = File.ReadAllText(filePath);

            if (jsonString == null)
            {
                throw new FileNotFoundException("No contents found. File contents were empty or failed to be read");
            }
            return jsonString;
        }
        catch (Exception e)
        {
            throw new FileNotFoundException(e.Message);
        }
    }
}