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

    public static Menutext subMenuChooser(Menutext[] submenus, string description)
    {
        //parses input to return a submenu
        Boolean inputCorrect = false;
        int result = -1;

        while (!inputCorrect)
        {
            Console.WriteLine(description);
            for (int i = 0; i < submenus.Length; i++)
            {
                Console.WriteLine("["+i+"]"+submenus[i].ToString());
            }
            string input = Console.ReadLine();

            if (input == "")
            {
                Console.Clear();
                return null;
            }
            
            inputCorrect = int.TryParse(input, out result);
            
            
            if (!inputCorrect)
            {
                Console.WriteLine("Invalid input, input must be a number.");
            }
            
            if (result < 0)
            {
                inputCorrect = false;
                Console.WriteLine("Invalid input, number cant be negative.");
                
            }
            
            if (result > submenus.Length)
            {
                inputCorrect = false;
                Console.WriteLine("Invalid input, number doesnt correspond to any submenus");
                
            }
            
            Console.Clear();
        }

        if (result == -1)
        {
            throw new Exception("Something went wrong.");
        }

        return submenus[result];

    }
    
    public static void Header(string header)
    {
        Console.WriteLine("============================================");
        Console.WriteLine(header);
        Console.WriteLine("===========================================");
    }
    
    
}