namespace WorldOfZuul;

public class GameScreen
{
    CpiTracker cpiTracker;

    private World _world = null;
    
    TurnCounter currentTurn;
    
    private int barPercent = 5; //how many percent one bar represents
    
    private Menutext movement;
    
    Region? currentRegion;
    
    Region? lastRegion;

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



            movement = new Menutext("year: " + (_world.Year) + "   |   " + "turn: " +
                                    turnCounter.currentTurn + "/25\n" +
                                    "Global cpi:\n" + percentBar(cpiTracker.GlobalCpi) + "\n \n" +
                                    "Region: " + currentRegionName + "\n" + "Regional cpi: \n" +
                                    percentBar(regionalCpi), exits(), null, "gameScreen");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error instantiating GameScreen: {e.Message}");
        }
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
        
        movement = new Menutext("year: " + (_world.Year)+"   |   "+"turn: "+currentTurn.currentTurn+"/25\n"+
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