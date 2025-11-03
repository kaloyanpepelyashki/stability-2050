namespace WorldOfZuul;

public class GameScreen
{
    CpiTracker cpiTracker;
    
    TurnCounter currentTurn;
    
    private int barPercent = 5; //how many percent one bar represents
    
    private MenuText movement;

    private MenuText main;

    private MenuText region;
    
    Region? currentRegion;
    
    Region? lastRegion;

    private bool hasMoved = true;

    private bool left;

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

        region = new MenuText("year: " + (2025 + turnCounter.currentTurn - 1) + "   |   " + "turn: " +
                              turnCounter.currentTurn + "/25\n" +
                              "Global cpi:\n" + PercentBar(cpiTracker.GlobalCpi) + "\n \n" +
                              "Region: " + currentRegionName + "\n" + "Regional cpi: \n" +
                              PercentBar(regionalCpi),RegionInfo(currentRegion),null,"region");
        
        main = new MenuText("year: " + (2025 + turnCounter.currentTurn-1)+"   |   "+"turn: "+turnCounter.currentTurn+"/25\n"+
                            "Global cpi:\n"+PercentBar(cpiTracker.GlobalCpi)+"\n \n"+
     
                            "Region: "+currentRegionName+"\n"+"Regional cpi: \n"+
                            PercentBar(regionalCpi),"you are entering "+currentRegionName +" would you like to leave or stay, to leave type 'leave' or type 'stay' to stay in the region", null,"gameScreen");
        
        movement = new MenuText("year: " + (2025 + turnCounter.currentTurn-1)+"   |   "+"turn: "+turnCounter.currentTurn+"/25\n"+
        "Global cpi:\n"+PercentBar(cpiTracker.GlobalCpi)+"\n \n"+
            "Region: "+currentRegionName+"\n"+"Regional cpi: \n"+
            PercentBar(regionalCpi), Exits(), null,"gameScreen");
    }

    public string RegionInfo(Region region)
    {
        
        string txt = "region description: "+region.RegionDescription;

        txt += "\nto acces the regions quiz type 'placeholder' ";

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
        if (lastRegion == this.currentRegion||this.lastRegion == currentRegion)
        {
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
        
        region = new MenuText("year: " + (2025 + currentTurn.currentTurn - 1) + "   |   " + "turn: " +
                              currentTurn.currentTurn + "/25\n" +
                              "Global cpi:\n" + PercentBar(cpiTracker.GlobalCpi) + "\n \n" +
                              "Region: " + currentRegionName + "\n" + "Regional cpi: \n" +
                              PercentBar(regionalCpi),RegionInfo(currentRegion),null,"region");
        
        movement = new MenuText("year: " + (2025 + currentTurn.currentTurn)+"   |   "+"turn: "+currentTurn.currentTurn+"/25\n"+
                                "Global cpi:\n"+PercentBar(cpiTracker.GlobalCpi)+"\n \n"+
                                "Region: "+currentRegionName+"\n"+"Regional cpi: \n"+
                                PercentBar(regionalCpi), Exits(), null,"gameScreen");
        
        main = new MenuText("year: " + (2025 + currentTurn.currentTurn-1)+"   |   "+"turn: "+currentTurn.currentTurn+"/25\n"+
                            "Global cpi:\n"+PercentBar(cpiTracker.GlobalCpi)+"\n \n"+
     
                            "Region: "+currentRegionName+"\n"+"Regional cpi: \n"+
                            PercentBar(regionalCpi),"you are entering "+currentRegionName +" would you like to leave or stay, to leave type 'leave' or type 'stay' to stay in the region.", null,"gameScreen");
        
    }

    public string Exits()
    {
        if (currentRegion == null)
        {
            return "an error has occured";
        }
        
        string exits = "neighboring regions: \n";


        try
        {
            string north = currentRegion.Exits["north"].RegionName;
            exits += "type 'north' to go to " + north+"\n";
        }
        catch (KeyNotFoundException)
        {
            
        }

        try
        {
            string south = currentRegion.Exits["south"].RegionName;
            exits += "type 'south' to go to "+ south+"\n";
        }
        catch (KeyNotFoundException)
        {
            
        }

        try
        {
            string east = currentRegion.Exits["east"].RegionName;
            exits += "type 'east' to go to "+ east+"\n";
        }
        catch (KeyNotFoundException)
        {
            
        }

        try
        {
            string west = currentRegion.Exits["west"].RegionName;
            exits += "type 'west' to go to "+ west+"\n";
        }
        catch (KeyNotFoundException)
        {
            
        }

        if (lastRegion != null)
        {
            exits += "\nor you can also go back to "+lastRegion.RegionName+" by typing back";
        }
        
        return exits;

    }

    public string PercentBar(double cpi)
    {
        if (cpi < 0)
        {
            return "an error has occured";
        }
        
        int bars = (int)Math.Round(cpi / barPercent);

        string percentBar ="";
        
        for (int i = 0; i < bars; i++)
        {
            percentBar += "/";
        }
        
        for(int i = bars*barPercent;i<100;i+=barPercent)
        {
            percentBar += ".";
        }
        
        percentBar += "  " + cpi +"%";
        
        return percentBar;

    }
    
}