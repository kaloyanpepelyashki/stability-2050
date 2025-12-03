﻿using WorldOfZuul.Presentation.Console.Assets;

namespace WorldOfZuul;

/// <summary>
/// the MenuText class functions as a template that can be instantiated to make different menus by providing a header text, text body
/// and the prompt displayed after 'press ENTER to'
/// </summary>

public class MenuText
{
    private readonly string? enterPromptAction;
    private readonly string header;
    private readonly string? textBody;
    private readonly string? menuName;
    private readonly MenuText[] submenus = Array.Empty<MenuText>();
    private readonly bool hasSubmenus;
    private readonly string subMenuDesc = string.Empty;

    public override string ToString()
    {
        return menuName ?? "this menu has no name";
    }

    public MenuText(string header, string textBody, string? enterPromptAction, string? menuName)
    {
        this.menuName = menuName;
        this.header = header;
        this.textBody = textBody;
        this.enterPromptAction = enterPromptAction;
        this.hasSubmenus = false;
    }
    
    public MenuText(string header, string? textBody, string? enterPromptAction, string? menuName, MenuText[] submenus, string subMenuDescription)
    {
        ArgumentNullException.ThrowIfNull(submenus);
        ArgumentNullException.ThrowIfNull(subMenuDescription);

        this.subMenuDesc = subMenuDescription;
        this.menuName = menuName;
        this.header = header;
        this.textBody = textBody;
        this.enterPromptAction = enterPromptAction;
        this.submenus = submenus;
        this.hasSubmenus = true;
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
                MenuText? menu = TextAssets.SubMenuChooser(submenus, subMenuDesc);
                if (menu == null)
                {
                    break;
                }
                menu.Display();
            }
            
        }
        
    }
}