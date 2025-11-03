namespace WorldOfZuul;

public class GameScreen
{
    CpiTracker cpiTracker;
    
    TurnCounter currentTurn;
    
    private int barPercent = 5; //how many percent one bar represents
    
    private Menutext movement;

    private Menutext main;

    private Menutext region;
    
    Region? currentRegion;
    
    Region? lastRegion;

    public bool hasMoved = true;

    public bool left;

    public GameScreen(TurnCounter turnCounter,CpiTracker cpiTracker,Region? currentRegion,Region? lastRegion)
    {
        this.cpiTracker = cpiTracker;
        this.currentTurn = turnCounter;
        
        this.currentRegion =  currentRegion;
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

        region = new Menutext(standardHeader(currentTurn,currentRegionName,regionalCpi),regionInfo(currentRegion),null,"region");
        
        main = new Menutext(standardHeader(currentTurn,currentRegionName,regionalCpi),
            "you are entering "+currentRegionName +" would you like to leave or stay, to leave type 'leave' or type 'stay' to stay in the region",
            null,
            "gameScreen");
        
        movement = new Menutext(standardHeader(currentTurn,currentRegionName,regionalCpi), exits(), null,"gameScreen");
    }

    private string standardHeader(TurnCounter turnCounter,string currentRegionName,double regionalCpi)
    {
        return "year: " + (2025 + turnCounter.currentTurn - 1) + "   |   " + "turn: " + turnCounter.currentTurn +
               "/25\n" +
               "Global cpi:\n" + percentBar(cpiTracker.GlobalCpi) + "\n \n" +

               "Region: " + currentRegionName + "\n" + "Regional cpi: \n" +
               percentBar(regionalCpi);

    }

    private string regionInfo(Region region)
    {
        
        string txt = "region description: "+region.RegionDescription;

        txt += "\nto acces the regions quiz type 'placeholder' ";

        return txt;

    }

    public void display()
    {
        
        if (hasMoved)
        {
            Console.Clear();

            if (left)
            {
                movement.display();
                return;
            }
            
            main.display();
            bool validInput = false;
            while (!validInput) 
            {
                string input = Console.ReadLine();
                switch (input) 
                {
                    case "stay": 
                        Console.Clear(); 
                        region.display();
                        validInput = true;
                        hasMoved = false;
                        break;
            
                    case "leave": 
                        Console.Clear();
                        movement.display();
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
            region.display();
        }

    }

    public void update(Region currentRegion, Region lastRegion)
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
        
        region = new Menutext(standardHeader(currentTurn,currentRegionName,regionalCpi),regionInfo(currentRegion),null,"region");
        
        movement = new Menutext(standardHeader(currentTurn,currentRegionName,regionalCpi), exits(), null,"gameScreen");
        
        main = new Menutext(standardHeader(currentTurn,currentRegionName,regionalCpi),"you are entering "+currentRegionName +" would you like to leave or stay, to leave type 'leave' or type 'stay' to stay in the region.", null,"gameScreen");
        
    }

    private string exits()
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

    private string percentBar(double cpi)
    {
        //errror handling
        if (cpi < 0)
        {
            return "an error has occured";
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