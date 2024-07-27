using Spectre.Console;
using ternet.entities;
using ternet.repositories;

namespace ternet.console
{
    public class ConsoleMenu
    {
        UserRepository user = new UserRepository();
        User loggedUser = new User();
        bool isLoggedIn = false;
        bool userIsAdmin = false;


        public bool DisplayMenu()
        {

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
            PrintLogo();
            PrintCenteredTitle($"[bold green]What would you like to do, {loggedUser.user_name}?[/]");
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] { "Sudo Menu", "Community Posts", "Inbox"}));

                switch (choice)
                {
                    case "Sudo Menu":
                        DisplaySudoMenu();
                        break;
                    case "Community Posts":
                        DisplayCommunityPostsMenu();
                        break;
                    case "Inbox":
                        DisplayInboxMenu();
                        break;
                    case "Exit":
                        AnsiConsole.MarkupLine("Goodbye!");
                        return;
                }
            }
        }

        public void DisplayUserMenu()
        {

        }

        public void DisplaySudoMenu()
        {
            PrintCenteredTitle($"[bold green]You're in the general SUDO menu, what would you like to do?[/]");
            PrintCenteredTitle($"[bold yellow]SUDO ACTIONS REQUIRE NO CONFIRMATION. PLEASE PROCEED WITH CAUTION[/]");
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] { "Users Control Panel", "Messages Control Panel", "Posts Control Panel", "Return"}));

                switch (choice)
                {
                    case "Users Control Panel":
                        DisplayUserControlPanel();
                        break;
                    case "Messages Control Panel":
                        DisplayMessagesControlPanel();
                        break;
                    case "Posts Control Panel":
                        DisplayPostsControlPanel();
                        break;
                    case "Return":
                        return;
                }
            }
        }

        public void DisplayCommunityPostsMenu()
        {

        }

        public void DisplayInboxMenu()
        {

        }

        // Sudo Menu Methods

        public void DisplayUserControlPanel()
        {
            PrintCenteredTitle($"[bold green]User Control Panel: Please choose an action.[/]");
            PrintCenteredTitle($"[bold yellow]SUDO ACTIONS REQUIRE NO CONFIRMATION. PLEASE PROCEED WITH CAUTION[/]");

            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] { "Update User Credentials", "Delete User", "Return"}));
                switch(choice)
                {
                    case "Update User Credentials":
                        List<User> users = new List<User>();
                        User choiceUser = new User();
                        List<string> usernames = new List<string>();
                        var table = new Table();

                        users = user.GetAllUsers();

                        foreach (User user in users)
                        {
                            #pragma warning disable CS8604 // Possible null reference argument.
                            usernames.Add(user.user_name);
                        }

                        var selectedUser = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Please select a [green]user.[/]?")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more users)[/]")
                                .AddChoices(usernames));
                        
                        choiceUser = user.GetUserInfoByName(selectedUser);
                        
                        // Show user info
                        table.AddColumn(new TableColumn("Username").Centered());
                        table.AddColumn(new TableColumn("Admin Status").Centered());

                        // Shows yes or no in admin status column
                        if(choiceUser.isAdmin)
                        {
                            table.AddRow(new Markup(selectedUser), new Markup("YES"));
                        }
                        else
                        {
                            table.AddRow(new Markup(selectedUser), new Markup("NO"));
                        }
                        
                        if(choiceUser.isAdmin)
                        {
                            if (!AnsiConsole.Confirm("Change status to [blue]normal user[/]?"))
                            {
                                AnsiConsole.MarkupLine("Going back...");
                                return;
                            }
                            user.UpdateUser(choiceUser.user_id, choiceUser.user_name, choiceUser.user_pass, false);
                        }
                        else 
                        {
                            if (!AnsiConsole.Confirm("Change status to [blue]Admin[/]?"))
                            {
                                AnsiConsole.MarkupLine("Going back...");
                                return;
                            }
                            user.UpdateUser(choiceUser.user_id, choiceUser.user_name, choiceUser.user_pass, true);
                        }
                        
                        break;
                }
            }
        }

        public void DisplayMessagesControlPanel()
        {

        }

        public void DisplayPostsControlPanel()
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

                    loggedUser = user.GetUserInfoByName(username);

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

