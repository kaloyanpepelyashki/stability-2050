namespace WorldOfZuul;

public class textAssets
{
    public static void EnterPrompt(string action)
    {
        Console.WriteLine("--------------------------------------------");
        Console.Write("Press ENTER to " + action+" ");
        
        Console.ReadLine();
        Console.Clear();

    }
    
    public static void Header(string header)
    {
        Console.WriteLine("============================================");
        Console.WriteLine(header);
        Console.WriteLine("===========================================");
    }
    
    
}