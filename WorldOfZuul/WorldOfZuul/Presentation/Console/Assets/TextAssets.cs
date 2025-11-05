
using WorldOfZuul.Presentation.Console.CLI;
namespace WorldOfZuul.Presentation.Console.Assets;

public class TextAssets
{
    public static string? EnterPrompt(string action)
    {
        System.Console.WriteLine("--------------------------------------------");
        System.Console.WriteLine("Press ENTER to " + action+" ");
        System.Console.Write("> ");
        
        string? input = System.Console.ReadLine();
        System.Console.Clear();
        return input;
    }

    public static MenuText? SubMenuChooser(MenuText[] submenus, string description)
    {
        //parses input to return a submenu
        Boolean inputCorrect = false;
        int result = -1;

        while (!inputCorrect)
        {
            for (int i = 0; i < submenus.Length; i++)
            {
                System.Console.WriteLine("["+i+"]"+submenus[i]);
            }
            System.Console.WriteLine("--------------------------------------------");
            System.Console.WriteLine(description);
            System.Console.Write("> ");
            string? input = System.Console.ReadLine();

            if (input == "")
            {
                System.Console.Clear();
                return null;
            }
            
            inputCorrect = int.TryParse(input, out result);
            
            
            if (!inputCorrect)
            {
                System.Console.WriteLine("Invalid input, input must be a number.");
            }
            
            if (result < 0)
            {
                inputCorrect = false;
                System.Console.WriteLine("Invalid input, number cant be negative.");
                
            }
            
            if (result >= submenus.Length)
            {
                inputCorrect = false;
                System.Console.WriteLine("Invalid input, number doesnt correspond to any submenus");
            }

            System.Console.Clear();
        }

        if (result == -1)
        {
            throw new Exception("Something went wrong.");
        }

        return submenus[result];

    }
    
    public static void Header(string header)
    {
        System.Console.WriteLine("============================================");
        System.Console.WriteLine(header);
        System.Console.WriteLine("===========================================");
    }
    
    
}