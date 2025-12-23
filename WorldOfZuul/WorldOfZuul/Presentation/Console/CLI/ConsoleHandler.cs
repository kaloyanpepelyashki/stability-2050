using WorldOfZuul.Presentation.Console.CLI;

namespace WorldOfZuul;

public class ConsoleHandler
{
    public Dictionary <string,MenuText> menus = new Dictionary <string,MenuText> ();

    public ConsoleHandler()
    {
        AddMenu("help[4] - Pro tip",
            "Pro tip:\nCorruption spreads fast. Honesty takes time.\nChoose actions that build long-term integrity, not quick wins.",
            "return to help menu", "ProTip");

        AddMenu("help[3] - Game Structure",
            "GAME STRUCTURE:\n- Each turn represents one year.\n- Each region’s CPI changes based on your decision.\n- The Global CPI is the average of all four regions.\n- A higher CPI means lower corruption.",
            "return to help menu", "GameStructure");
        
        AddMenu("Help[0] - CPI ",
            "CPI (Corruption Perception Index):\n- Represents how clean a region is.\n- Range: 0 = totally corrupt, 100 = fully transparent.",
            "return to the help menu", "CPI");
        
        AddMenu("help[1] - goal","GOAL:\n- Reach a global CPI of 80 before the year 2050.\n- If global CPI falls below 20, a corruption crisis begins.\n You’ll have 5 turns to recover","return to help menu", "Goal");

        AddMenu("help[2] - Basic Commands",
            "BASIC COMMANDS:\n- [number] -> choose your response to a dilemma.\n- 'north','west','east' or 'south' -> travel to another region.\n- help -> show this help menu.\n- quit -> exit the simulation. \n- leave -> shows possible exits out of a region",
            "return to help menu", "BasicCommands");
        
        AddMenu("HELP MENU", null,null, "help", ["CPI","Goal","BasicCommands","GameStructure","ProTip"],"Choose a number to read more or press ENTER to exit the help menu");
        
        AddMenu("STABILITY 2050",
            "Stabilty 2050 is a text based strategic game.\nYou are in a position of a diplomat,\nwho is trying to fight corruption.\nEvery action changes CPI - the measure of global trust.\nYour goal is to lead humanity to corruption-free world by 2050.\n",
            null, "welcome", ["help"], "Type '0' to learn how to play or press ENTER to continue.");
    }

    public void AddMenu(string header, string textBody, string enterPromptAction, string? menuName)
    {
        MenuText menu = new MenuText(header,textBody,enterPromptAction,menuName);
        
        menus.Add(menu.ToString(),menu);
    }
    
    public void AddMenu(string header, string? textBody, string? enterPromptAction, string? menuName, string[] submenus,string subMenuDescription)
    {
        MenuText[] trueMenus = new MenuText [submenus.Length];

        try
        {
            for (int i = 0; i < submenus.Length; i++)
            {
                trueMenus[i] = menus[submenus[i]];
            }
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine("Key doesnt exist in menu,\nFull error message: "+e.Message);
        }
        
        
        MenuText menu = new MenuText(header,textBody,enterPromptAction,menuName,trueMenus,subMenuDescription);
        menus.Add(menu.ToString(),menu);
    }
}