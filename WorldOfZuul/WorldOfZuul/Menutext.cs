namespace WorldOfZuul;

/// <summary>
/// the MenuText class functions as a template that can be instantiated to make different menus by providing a header text, text body
/// and the prompt displayed after 'press ENTER to'
/// </summary>

public class Menutext
{
    private string enterPromptAction;
    private string header;
    private string textBody;
    private string? menuName;
    private Menutext?[] submenus;
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

    public Menutext(string header, string textBody, string enterPromptAction, string? menuName)
    {
        this.menuName = menuName;
        this.header = header;
        this.textBody = textBody;
        this.enterPromptAction = enterPromptAction;
    }
    
    public Menutext(string header, string textBody, string enterPromptAction, string? menuName, Menutext[] submenus,string subMenuDescription)
    {
        this.subMenuDesc = subMenuDescription;
        this.menuName = menuName;
        this.header = header;
        this.textBody = textBody;
        this.enterPromptAction = enterPromptAction;
        this.submenus = submenus;
        hasSubmenus = true;
    }

    public void display()
    {
        textAssets.Header(header);
        Console.WriteLine(textBody);
        if (!hasSubmenus)
        {
            textAssets.EnterPrompt(enterPromptAction);
        }
        else
        {
            Menutext menu = textAssets.subMenuChooser(submenus,subMenuDesc);
            menu.display();
        }
        
    }
}