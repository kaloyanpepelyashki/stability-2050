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

    public Menutext(string header, string textBody, string enterPromptAction)
    {
        this.header = header;
        this.textBody = textBody;
        this.enterPromptAction = enterPromptAction;
    }

    public void display()
    {
        textAssets.Header(header);
        Console.WriteLine(textBody);
        textAssets.EnterPrompt(enterPromptAction);
    }
}