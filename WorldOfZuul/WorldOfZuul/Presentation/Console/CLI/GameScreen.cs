namespace WorldOfZuul;

public class GameScreen
{
    CpiTracker cpiTracker;
    
    TurnCounter currentTurn;
    
    private int barPercent = 5; //how many percent one bar represents
    
    private Menutext movement;
    
    Region? currentRegion;
    
    Region? lastRegion;

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
        
        
        
        movement = new Menutext("year: " + (2025 + turnCounter.currentTurn-1)+"   |   "+"turn: "+turnCounter.currentTurn+"/25\n"+
        "Global cpi:\n"+percentBar(cpiTracker.GlobalCpi)+"\n \n"+
            "Region: "+currentRegionName+"\n"+"Regional cpi: \n"+
            percentBar(regionalCpi), exits(), null,"gameScreen");
    }

    public void display()
    {
        Console.Clear();
        movement.display();
    }

    public void update(Region currentRegion, Region lastRegion)
    {
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
        
        movement = new Menutext("year: " + (2025 + currentTurn.currentTurn)+"   |   "+"turn: "+currentTurn.currentTurn+"/25\n"+
                                "Global cpi:\n"+percentBar(cpiTracker.GlobalCpi)+"\n \n"+
                                "Region: "+currentRegionName+"\n"+"Regional cpi: \n"+
                                percentBar(regionalCpi), exits(), null,"gameScreen");
        
    }

    public string exits()
    {
        if (currentRegion == null)
        {
            return "an error has occured";
        }
        
        string exits = "neighboring regions: \n";


        try
        {
            string north = currentRegion.Exits["north"].RegionName;
            exits += "[north] : " + north+"\n";
        }
        catch (KeyNotFoundException)
        {
            
        }

        try
        {
            string south = currentRegion.Exits["south"].RegionName;
            exits += "[south] : "+ south+"\n";
        }
        catch (KeyNotFoundException)
        {
            
        }

        try
        {
            string east = currentRegion.Exits["east"].RegionName;
            exits += "[east] : "+ east+"\n";
        }
        catch (KeyNotFoundException)
        {
            
        }

        try
        {
            string west = currentRegion.Exits["west"].RegionName;
            exits += "[west] : "+ west+"\n";
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

    public string percentBar(double cpi)
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