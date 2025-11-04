namespace WorldOfZuul;

/// <summary>
/// the MenuText class functions as a template that can be instantiated to make different menus by providing a header text, text body
/// and the prompt displayed after 'press ENTER to'
/// </summary>

public class MenuText
{
    private string? enterPromptAction;
    private string header;
    private string textBody;
    private string? menuName;
    private MenuText?[] submenus;
    private bool hasSubmenus = false;
    private string subMenuDesc;

    public override string ToString()
    {
        if (menuName != null)
        {
            return menuName;
        }
        return "this menu has no name";
    }

    public MenuText(string header, string textBody, string? enterPromptAction, string? menuName)
    {
        this.menuName = menuName;
        this.header = header;
        this.textBody = textBody;
        this.enterPromptAction = enterPromptAction;
    }
    
    public MenuText(string header, string? textBody, string? enterPromptAction, string? menuName, MenuText[] submenus,string subMenuDescription)
    {
        this.subMenuDesc = subMenuDescription;
        this.menuName = menuName;
        this.header = header;
        this.textBody = textBody;
        this.enterPromptAction = enterPromptAction;
        this.submenus = submenus;
        hasSubmenus = true;
    }

    public void Display()
    {
        
        if (!hasSubmenus)
        {
            TextAssets.Header(header);
            Console.WriteLine(textBody);
            if (enterPromptAction != null)
            {
                TextAssets.EnterPrompt(enterPromptAction);
            }
            
        }
        else
        {
            while (true)
            {
                TextAssets.Header(header);
                if (textBody != null)
                {
                    Console.WriteLine(textBody);
                }
                MenuText menu = TextAssets.subMenuChooser(submenus,subMenuDesc);
                if (menu == null)
                {
                    break;
                }
                menu.Display();
            }
            
        }
        
    }
}