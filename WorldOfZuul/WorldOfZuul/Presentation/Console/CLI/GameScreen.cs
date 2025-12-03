using WorldOfZuul.Presentation.Console.CLI;

namespace WorldOfZuul;

public class GameScreen
{
    private CpiTracker cpiTracker;

    private World _world = null;
    
    private TurnCounter currentTurn;
    
    private int barPercent = 5; //how many percent one bar represents
    
    private MenuText movement;

    private MenuText main;

    private MenuText region;
    
    Region? currentRegion;
    
    Region? lastRegion;
    public bool hasMoved;
    public bool left;

    public GameScreen(TurnCounter turnCounter, CpiTracker? cpiTracker, Region? currentRegion, Region? lastRegion, World? world)
    {
        try
        {
            if (world == null | cpiTracker == null)
            {
                throw new Exception("World or cpiTracker not set. Equal to zero");
            }

            this.cpiTracker = cpiTracker;
            this._world = world;
            this.currentTurn = turnCounter;

            this.currentRegion = currentRegion;
            this.lastRegion = lastRegion;
            double regionalCpi;

            string currentRegionName;
            if (currentRegion == null)
            {
                currentRegionName = "region not found";
                regionalCpi = -1;
            }
            else
            {
                currentRegionName = currentRegion.RegionName;
                regionalCpi = currentRegion.RegionCpi;
            }



            movement = new MenuText("year: " + (_world.Year) + "   |   " + "turn: " +
                                    turnCounter.currentTurn + "/25\n" +
                                    "Global cpi:\n" + PercentBar(cpiTracker.GlobalCpi) + "\n \n" +
                                    "Region: " + currentRegionName + "\n" + "Regional cpi: \n" +
                                    PercentBar(regionalCpi), Exits(), null, "gameScreen");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error instantiating GameScreen: {e.Message}");
        }

        region = new MenuText(StandardHeader(currentTurn,currentRegion.RegionName,currentRegion.RegionCpi),RegionInfo(currentRegion),null,"region");
        
        main = new MenuText(StandardHeader(currentTurn,currentRegion.RegionName,currentRegion.RegionCpi),
            "You are entering "+currentRegion.RegionName +".\nWould you like to leave or stay?\n- to leave, type 'leave', or type 'stay' to stay in "+currentRegion.RegionName,".",
            "gameScreen");
        
        movement = new MenuText(StandardHeader(currentTurn,currentRegion.RegionName,currentRegion.RegionCpi), Exits(), null,"gameScreen");
    }

    private string StandardHeader(TurnCounter turnCounter,string currentRegionName,double regionalCpi)
    {
        return "Year: " + (2025 + turnCounter.currentTurn - 1) + "   |   " + "Turn: " + turnCounter.currentTurn +
               "/25\n" +
               "Global cpi:\n" + PercentBar(cpiTracker.GlobalCpi) + "\n \n" +

               "Region: " + currentRegionName + "\n" + "Regional cpi: \n" +
               PercentBar(regionalCpi);

    }

    private string RegionInfo(Region region)
    {
        
        string txt = "Region description: "+region.RegionDescription;

        txt += "\nto access the regions quiz type 'quiz' ";

        return txt;

    }

    public void Display()
    {
        if (hasMoved)
        {
            Console.Clear();

            if (left)
            {
                movement.Display();
                return;
            }
            
            main.Display();
            bool validInput = false;
            while (!validInput) 
            {
                string input = Console.ReadLine();
                switch (input) 
                {
                    case "stay": 
                        Console.Clear(); 
                        region.Display();
                        validInput = true;
                        hasMoved = false;
                        break;
            
                    case "leave": 
                        Console.Clear();
                        movement.Display();
                        left = true;
                        validInput = true;
                        break;
                    default:
                        Console.Write("please choose whether to stay or leave\n");
                        break;
                } 
            }
            
        }
        else
        {
            left = false;
            Console.Clear();
            region.Display();
        }

    }

    public void Update(Region currentRegion, Region lastRegion)
    {
        //updates all the menuScreens
        
        if (lastRegion == this.currentRegion||this.lastRegion == currentRegion)
        {
            //if you have moved to a new region hasMoved is set to true to trigger the screen that prompt you to either stay or leave,
            //as well as setting left to false to stop the movement menu to appear
            hasMoved = true;
            left = false;
        }
        
        this.currentRegion = currentRegion;
        this.lastRegion = lastRegion;

        var currentRegionName = currentRegion.RegionName;
        var regionalCpi = currentRegion.RegionCpi;

        region = new MenuText(StandardHeader(currentTurn,currentRegionName,regionalCpi),RegionInfo(currentRegion),null,"region");
        
        movement = new MenuText(StandardHeader(currentTurn,currentRegionName,regionalCpi), Exits(), null,"gameScreen");
        
        main = new MenuText(StandardHeader(currentTurn,currentRegionName,regionalCpi),"You are entering "+currentRegion.RegionName +".\nWould you like to leave or stay?\n- to leave, type 'leave', or type 'stay' to stay in "+currentRegion.RegionName,".",
        "gameScreen");
    }

    private string Exits()
    {
        if (currentRegion == null)
        {
            return "an error has occured";
        }
        
        string exits = "neighboring regions: \n";

        string[] directions = { "north", "south", "east", "west" };

        //goes through each direction and checks if the key exists in which case there exists an exit in that direction, and so the template "type {key} to go to {regionname}\n" will be added to the string exits
        foreach (string key in directions)
        {
            if (currentRegion.Exits.ContainsKey(key))
            {
                string dir = currentRegion.Exits[key].RegionName;
                exits += "type " + key + " to go to " + dir + "\n";
            }
        }
        
        //fills out the template "or you can also go back to 'lastRegion.regionName' by typing back" only if the lastRegion field is not null
        if (lastRegion != null)
        {
            exits += "\nor you can also go back to "+lastRegion.RegionName+" by typing back";
        }
        
        return exits;

    }

    private string PercentBar(double cpi)
    {
        //errror handling
        if (cpi < 0)
        {
            return "An error has occured";
        }
        
        //find out how many '/' should be added
        int bars = (int)Math.Round(cpi / barPercent);

        string percentBar ="";
        
        //adds the appropriate amount of '/'
        for (int i = 0; i < bars; i++)
        {
            percentBar += "/";
        }
        
        // fills the rest of percentbar with '.'
        for(int i = bars*barPercent;i<100;i+=barPercent)
        {
            percentBar += ".";
        }
        
        //append the cpi percent at the end of the percentbar
        percentBar += "  " + cpi +"%";
        
        return percentBar;
    }
}