﻿using WorldOfZuul.Presentation.Console.Assets;

 namespace WorldOfZuul.Presentation.Console.CLI
{
    public class MenuText
    {
        private string? enterPromptAction;
        private string header;
        private string? textBody;
        private string? menuName;
        private MenuText?[]? submenus;
        private bool hasSubmenus = false;
        private string? subMenuDesc;

        public override string ToString()
        {
            return menuName ?? "This menu has no name";
        }

        public MenuText(string header, string? textBody, string? enterPromptAction, string? menuName)
        {
            this.menuName = menuName;
            this.header = header;
            this.textBody = textBody;
            this.enterPromptAction = enterPromptAction ?? null;
        }

        public MenuText(string header, string? textBody, string? enterPromptAction, string? menuName, MenuText?[] submenus, string subMenuDesc)
        {
            this.subMenuDesc = subMenuDesc;
            this.menuName = menuName;
            this.header = header;
            this.textBody = textBody;
            this.enterPromptAction = enterPromptAction;
            this.submenus = submenus;
            hasSubmenus = true;
        }

        public string Display()
        {
            System.Console.Clear();
            if (!hasSubmenus)
            {
                TextAssets.Header(header);
                if (textBody != null) System.Console.WriteLine(textBody);
                if (enterPromptAction != null)
                {
                    return TextAssets.EnterPrompt(enterPromptAction);
                }
            }
            else
            {
                while (true)
                {
                    TextAssets.Header(header);
                    if (textBody != null) System.Console.WriteLine(textBody);

                    MenuText? menu = TextAssets.SubMenuChooser(submenus, subMenuDesc);
                    if (menu == null) break;
                    menu.Display();
                }
            }
            return "";
        }
        
    }
}