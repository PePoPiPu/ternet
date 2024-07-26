using Spectre.Console;
using ternet.entities;
using ternet.repositories;

namespace ternet.console
{
    public class ConsoleMenu
    {
        UserRepository user = new UserRepository();
        bool isLoggedIn = false;
        
        public bool DisplayMenu()
        {
            bool userIsAdmin = false;

            PrintLogo();
            PrintCenteredTitle("[bold green]Welcome to Ternet[/]");

            while (!isLoggedIn)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] { "Login", "Exit" }));

                switch (choice)
                {
                    case "Login":
                        Login();
                        break;
                    case "Exit":
                        AnsiConsole.MarkupLine("Goodbye!");
                        return false;
                }
            }

            return userIsAdmin;
        }

        public void DisplayAdminMenu()
        {

        }

        public void DisplayUserMenu()
        {
            
        }

        private void PrintCenteredTitle(string title)
        {
            string centeredTitle = CenterText(title);
            AnsiConsole.MarkupLine(centeredTitle);
            AnsiConsole.MarkupLine(""); // Blank line for spacing
        }

        private void PrintLogo()
        {
            var logoLines = new[]
            {
                @"░▒▓████████▓▒░▒▓████████▓▒░▒▓███████▓▒░░▒▓███████▓▒░░▒▓████████▓▒░▒▓████████▓▒░", 
                @"   ░▒▓█▓▒░   ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░         ░▒▓█▓▒░    ",
                @"   ░▒▓█▓▒░   ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░         ░▒▓█▓▒░    ",     
                @"   ░▒▓█▓▒░   ░▒▓██████▓▒░ ░▒▓███████▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓██████▓▒░    ░▒▓█▓▒░    ",     
                @"   ░▒▓█▓▒░   ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░         ░▒▓█▓▒░    ",     
                @"   ░▒▓█▓▒░   ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░         ░▒▓█▓▒░    ", 
                @"   ░▒▓█▓▒░   ░▒▓████████▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓████████▓▒░  ░▒▓█▓▒░    "                             
            };

            foreach (var line in logoLines)
            {
                AnsiConsole.MarkupLine(CenterText($"[bold green]{line}[/]"));
            }
            AnsiConsole.MarkupLine("\n\n");
        }

        private string CenterText(string text)
        {
            int screenWidth = Console.WindowWidth;
            int textWidth = text.Length;
            int spaces = (screenWidth - textWidth) / 2;

            // Avoid negative spacing
            if (spaces < 0) spaces = 0;

            return new string(' ', spaces) + text;
        }

        private void Login()
        {
            bool userIsAdmin;
            string username = "";
            string password = "";
            while (!isLoggedIn)
            {
                AnsiConsole.MarkupLine("[green]Login selected: Please use your credentials[/]");
                username = AnsiConsole.Ask<string>("Enter username:");
                password = AnsiConsole.Prompt(new TextPrompt<string>("Enter password:").Secret());

                if (user.LogInUser(username, password))
                {
                    AnsiConsole.MarkupLine($"[green]Welcome back, {username}![/]");
                    isLoggedIn = true;
                    if (CheckAdminPrivileges(username)) 
                    {
                        userIsAdmin = true;
                    } 
                } 
                else
                {
                    AnsiConsole.MarkupLine($"[red]Wrong username or password, please try again: [/]");
                }            
            }
        }

        private bool CheckAdminPrivileges(string userName)
        {
            bool isAdmin = false;

            if (user.CheckAdminPrivileges(userName) == 1){
                isAdmin = true;
            }

            return isAdmin;
        }
    }

}

