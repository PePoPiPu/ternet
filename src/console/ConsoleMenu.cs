using Spectre.Console;
using ternet.entities;
using ternet.repositories;

namespace ternet.console
{
    public class ConsoleMenu
    {
        UserRepository user = new UserRepository();
        PostRepository posts = new PostRepository();
        PostCommentRepository comment = new PostCommentRepository();
        MessageRepository messageRepo = new MessageRepository();
        User loggedUser = new User();
        bool isLoggedIn = false;
        bool userIsAdmin = false;


        public void DisplayMenu()
        {

            PrintLogo();
            AnsiConsole.MarkupLine("[bold green]Welcome to Ternet[/]");

            while (!isLoggedIn)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] {"Sign Up", "Login", "Exit" }));

                switch (choice)
                {
                    case "Sign Up":
                        SignUp();
                        break;
                    case "Login":
                        Login();
                        break;
                    case "Exit":
                        AnsiConsole.MarkupLine("Goodbye!");
                        return;
                }
            }
            DisplayAdminMenu();
        }

        public void DisplayAdminMenu()
        {
            AnsiConsole.MarkupLine($"[bold green]What would you like to do, {loggedUser.user_name}?[/]");
            while (true)
            {   
                if (userIsAdmin)
                {
                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .AddChoices(new[] { "Sudo Menu", "Community Posts", "Messages", "Exit"}));

                    switch (choice)
                    {
                        case "Sudo Menu":
                            DisplaySudoMenu();
                            break;
                        case "Community Posts":
                            DisplayCommunityPostsMenu();
                            break;
                        case "Messages":
                            DisplayMessagesMenu();
                            break;
                        case "Exit":
                            AnsiConsole.MarkupLine("Goodbye!");
                            return;
                    }
                }
                else
                {
                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .AddChoices(new[] {"Community Posts", "Messages", "Exit"}));

                    switch (choice)
                    {
                        case "Community Posts":
                            DisplayCommunityPostsMenu();
                            break;
                        case "Messages":
                            DisplayMessagesMenu();
                            break;
                        case "Exit":
                            AnsiConsole.MarkupLine("Goodbye!");
                            return;
                    }
                }
            }
        }


        public void DisplaySudoMenu()
        {
            AnsiConsole.MarkupLine($"[bold green]You're in the general SUDO menu, what would you like to do?[/]");
            AnsiConsole.MarkupLine($"[bold yellow]SUDO ACTIONS REQUIRE NO CONFIRMATION. PLEASE PROCEED WITH CAUTION[/]");
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] { "Users Control Panel", "Posts Control Panel", "Return"}));

                switch (choice)
                {
                    case "Users Control Panel":
                        DisplayUserControlPanel();
                        break;
                    case "Return":
                        return;
                }
            }
        }

        public void DisplayCommunityPostsMenu()
        {
            AnsiConsole.MarkupLine($"[bold green]COMMUNITY POSTS: What would you like to do, {loggedUser.user_name}?[/]");
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] { "See Posts","Post Something", "Return"}));

                switch (choice)
                {
                    case "See Posts":
                        SeePosts();
                        break;
                    case "Post Something":
                        CreatePost();
                        break;
                    case "Return":
                        return;
                }
            }
        }


        public void SeePosts()
        {
            List<Post> postList = new List<Post>();
            List<string> postsTitles = new List<string>();
            Post currentPost = new Post();

            postList = posts.GetAllPosts();

            foreach (Post post in postList)
            {
                postsTitles.Add(post.post_title);
            }

            // Add a return option at the end of the list
            postsTitles.Add("Return");

            var selectedPost = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Please select a [green]Post.[/]")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more users)[/]")
                                .AddChoices(postsTitles));

            if (selectedPost == "Return")
            {
                return;
            }
            else
            {
                // Display post title 
                AnsiConsole.MarkupLine($"[bold royalblue1]Title:[/] [bold blue]{selectedPost}[/]");

                // Display post body
                // Retrieve the post that contains the post title from the posts list using LINQ
                // LINQ allows to do a query inside a list.
                currentPost = postList.FirstOrDefault(post => post.post_title == selectedPost);

                AnsiConsole.MarkupLine($"[bold royalblue1]Body[/]: [bold blue]{currentPost.post_body}[/]");

                // Display options for the post 
                AnsiConsole.MarkupLine($"[bold green]What would you like to do, {loggedUser.user_name}?[/]");
                while (true)
                {
                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .AddChoices(new[] { "Delete Post", "Add a comment","See Comments", "Return"}));

                    switch (choice)
                    {
                        case "Delete Post":
                            if (!AnsiConsole.Confirm("[red bold] You're about to delete this posts and its comments, are you sure?[/]"))
                            {
                                AnsiConsole.MarkupLine("Going back...");
                                return;
                            }
                            else 
                            {
                                AnsiConsole.MarkupLine("Post deleted! Going back...");
                                posts.DeletePost(currentPost.post_id);
                                return;
                            }
                        case "Add a comment":
                            var newComment = AnsiConsole.Ask<string>("Write your comment: ");
                            comment.InsertComment(newComment, loggedUser.user_id, currentPost.post_id);
                            AnsiConsole.MarkupLine("Comment posted! Going back...");
                            break;
                        case "See Comments":
                            var table = new Table();
                            List<PostComment> comments = new List<PostComment>();
                            comments = comment.GetCommentsByPost(currentPost.post_id);

                            AnsiConsole.MarkupLine($"[bold green]Seeing comments for:[/] [bold blue]{currentPost.post_title}[/]");

                            // Create a table
                            table.AddColumn(new TableColumn("User")).Centered();
                            table.AddColumn(new TableColumn("Comment")).Centered();
                            table.AddColumn(new TableColumn("Likes")).Centered();
                            foreach (PostComment comment in comments)
                            {
                                table.AddRow(new Markup($"{comment.posterUserName}"), new Markup($"{comment.pc_text}"), new Markup($"{comment.pc_likes}"));
                            }
                            
                            // Display comments
                            AnsiConsole.Write(table);

                            AnsiConsole.MarkupLine($"[bold green]What would you like to do, {loggedUser.user_name}?[/]");
                            // Give option to like comment
                            choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .AddChoices(new[] { "Like Comment", "Return"}));
                            switch(choice)
                            {
                                case "Like Comment":
                                    List<string> commentList = new List<string>();
                                    PostComment currentComment = new PostComment();
                                    // Print comment list to select from
                                    foreach (PostComment comment in comments)
                                    {
                                        commentList.Add(comment.pc_text);
                                    }

                                    // Add a return option
                                    commentList.Add("Return");

                                    var selectedComment = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                        .Title("Please select a [green]user.[/]")
                                        .PageSize(10)
                                        .MoreChoicesText("[grey](Move up and down to reveal more users)[/]")
                                        .AddChoices(commentList));

                                    if (!AnsiConsole.Confirm("Like comment?"))
                                    {
                                        AnsiConsole.WriteLine("Going back...");
                                        return;
                                    }
                                    else
                                    {
                                        // Get selected comment likes and add 1
                                        currentComment = comments.FirstOrDefault(comment => comment.pc_text == selectedComment);
                                        currentComment.pc_likes += 1;

                                        // Update database with new value
                                        comment.UpdateCommentLikes(currentComment.pc_id, currentComment.pc_likes);

                                        AnsiConsole.WriteLine("You liked that comment! Going back...");
                                        return;
                                    }
                                case "Return":
                                    AnsiConsole.WriteLine("Going back...");
                                    return;
                            }
                            break;
                        case "Return":
                            return;
                    }
                }
                
            }

        }

        public void CreatePost()
        {
            Post newPost = new Post();

            // Ask user for post title
            var newPostTitle = AnsiConsole.Ask<string>("What would you like to name your post? ");
            var newPostBody = AnsiConsole.Ask<string>("Type away your post content! ");

            posts.InsertPost(newPostTitle, newPostBody, loggedUser.user_id);

            AnsiConsole.WriteLine($"Posted {newPostTitle} succesfully!");

        }
        public void DisplayMessagesMenu()
        {
            AnsiConsole.MarkupLine($"[bold green] Messages: What would you like to do {loggedUser.user_name}?[/]");
            
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] { "Inbox", "Sent messages", "Contacts", "Send message", "Return"}));

                switch (choice)
                {
                    case "Inbox":
                        DisplayInboxMenu();
                        break;
                    case "Sent messages":
                        DisplaySentMessagesMenu();
                        break;
                    case "Contacts":
                         DisplayContactsMenu();
                        break;
                    case "Send message":
                        DisplaySendMessageMenu();
                        break;
                    case "Return":
                        return;
                }
            }
        }

        public void DisplayContactsMenu() 
        {
            List<User> contacts = new List<User>();
            List<Message> sentMessages = new List<Message>();
            User currentSelectedUser = new User();
            List<string> contactsUsernames = new List<string>();

            // Retrieve list of all messages sent by logged in user
            sentMessages = messageRepo.GetMessagesBySenderId(loggedUser.user_id);

            // Retrieve each user info with the id
            foreach(Message message in sentMessages)
            {
                contacts.Add(user.GetUserInfoById(message.message_receiver));
            }

            // Add usernames to list
            foreach (User user in contacts)
            {
                contactsUsernames.Add(user.user_name);
            }

            // Add a return option
            contactsUsernames.Add("Return");

            AnsiConsole.MarkupLine($"[bold green] Contacts: What would you like to do {loggedUser.user_name}?[/]");

            // Show options to send message to contact
            var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] { "Send a message", "Return"}));
            switch (choice)
            {
                case "Open message":
                    var selectedUser = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Please select a [green]message.[/]")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more messages)[/]")
                            .AddChoices(contactsUsernames));

                    if (selectedUser == "Return")
                    {
                        return;
                    }
                    else
                    {
                        // Retrieve selected user info 
                        currentSelectedUser = contacts.FirstOrDefault(user => user.user_name == selectedUser);

                        int selectedUserId = currentSelectedUser.user_id; 
                        string messageTitle = AnsiConsole.Ask<string>("Introduce message subject: ");
                        string messageBody = AnsiConsole.Ask<string>("Introduce message body: ");

                        messageRepo.InsertMessage(messageTitle, messageBody, selectedUserId, loggedUser.user_id);

                        AnsiConsole.MarkupLine($"[bold green] Message sent successfully to {currentSelectedUser.user_name}![/]");
                    }
                    break;
                case "Return":
                    return;
            }
        }

        public void DisplaySendMessageMenu()
        {
            // Get list of all registered users
            List<User> users = user.GetAllUsers();
            List<string> usernames = new List<string>();
            User currentSelectedUser = new User();
            AnsiConsole.MarkupLine($"[bold green] Send a message: Who do you want to write to {loggedUser.user_name}?[/]");
            
            foreach (User user in users)
            {
                usernames.Add(user.user_name);
            }

            // Add a return option
            usernames.Add("Return");

            // Print usernames for choice
            var selectedUser = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Please select a [green]message.[/]")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more messages)[/]")
                            .AddChoices(usernames));

            if (selectedUser == "Return")
            {
                return;
            } 
            else
            {
                // Retrieve selected user info 
                currentSelectedUser = users.FirstOrDefault(user => user.user_name == selectedUser);

                int selectedUserId = currentSelectedUser.user_id; 
                string messageTitle = AnsiConsole.Ask<string>("Introduce message subject: ");
                string messageBody = AnsiConsole.Ask<string>("Introduce message body: ");

                messageRepo.InsertMessage(messageTitle, messageBody, selectedUserId, loggedUser.user_id);

                AnsiConsole.MarkupLine($"[bold green] Message sent successfully to {currentSelectedUser.user_name}![/]");
            }
        }
        public void DisplaySentMessagesMenu()
        {
            List<Message> messages = new List<Message>();
            List<string> messagesTitles = new List<string>();
            Message currentMessage = new Message();
            User receiver = new User();
            var table = new Table();

            AnsiConsole.MarkupLine($"[bold green] Inbox: Displaying sent messages[/]");

            messages = messageRepo.GetMessagesBySenderId(loggedUser.user_id);

            // Display messages in table form
            table.AddColumn("Sent to");
            table.AddColumn("Subject");

            // Get receiver User
            receiver = user.GetUserInfoById(currentMessage.message_receiver);

            foreach (Message message in messages)
            {
                messagesTitles.Add(message.message_title);
                table.AddRow(new Markup($"{receiver.user_name}"), new Markup($"{message.message_title}"));
            }
            
            // Add a return option when selecting a message
            messagesTitles.Add("Return");

            AnsiConsole.MarkupLine($"[bold green] Messages: What would you like to do {loggedUser.user_name}?[/]");

            // Add options to edit or delete a message

            var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] { "Open message", "Return"}));

            switch (choice)
            {
                case "Open message":
                    var selectedMessage = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Please select a [green]message.[/]")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more messages)[/]")
                            .AddChoices(messagesTitles));

                    if (selectedMessage == "Return")
                    {
                        return;
                    }
                    else
                    {
                        // Retrieve selected message object from the list with LINQ
                        currentMessage = messages.FirstOrDefault(message => message.message_title == selectedMessage);

                        // Display title and body
                        AnsiConsole.MarkupLine($"[bold royalblue1]Sent to:[/] [bold blue]{receiver.user_name}[/]");
                        AnsiConsole.MarkupLine($"[bold royalblue1]Subject:[/] [bold blue]{currentMessage.message_title}[/]");
                        AnsiConsole.MarkupLine($"[bold royalblue1]Body:[/] [bold blue]{currentMessage.message_body}[/]");

                        // Show options to reply or delete
                        choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .AddChoices(new[] { "Edit message title", "Delete message", "Return"}));

                        switch (choice)
                        {
                            case "Edit message title":
                                string newTitle = AnsiConsole.Ask<string>("Type in title: ");
                                string newBody = AnsiConsole.Ask<string>("Type in body: ");

                                messageRepo.UpdateMessage(currentMessage.message_id, newTitle, newBody, loggedUser.user_id, receiver.user_id);

                                break;
                            case "Delete message":
                                if (!AnsiConsole.Confirm("Are you sure you want to delete this message? This action is undoable!"))
                                {
                                    AnsiConsole.MarkupLine("[bold green] Going back...[/]");
                                    return;
                                }
                                else
                                {
                                    messageRepo.DeleteMessage(currentMessage.message_id);
                                    AnsiConsole.MarkupLine("[bold yellow] Message deleted. Going back...[/]");
                                    return;
                                }
                            case "Return":
                                return;
                        }
                    }
                    break;
                case "Return":
                    return;
            }
        }

        public void DisplayInboxMenu()
        {
            List<Message> messages = new List<Message>();
            List<string> messagesTitles = new List<string>();
            User sender = new User();
            Message currentMessage = new Message();
            var table = new Table();

            AnsiConsole.MarkupLine($"[bold green] Inbox: Displaying received messages[/]");

            messages = messageRepo.GetAllMessages(loggedUser.user_id);

            // Display messages in table form
            table.AddColumn("Sender");
            table.AddColumn("Subject");

            foreach (Message message in messages)
            {
                // Retrieve sender username
                sender = user.GetUserInfoById(message.message_sender);
                messagesTitles.Add(message.message_title);
                table.AddRow(new Markup($"{sender.user_name}"), new Markup($"{message.message_title}"));
            }

            AnsiConsole.Write(table);
            // Add a return option when selecting a message
            messagesTitles.Add("Return");

            AnsiConsole.MarkupLine($"[bold green] Messages: What would you like to do {loggedUser.user_name}?[/]");

            // Get sender User
            sender = user.GetUserInfoById(currentMessage.message_receiver);

            var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] { "Open message", "Return"}));

            switch (choice)
            {
                case "Open message":
                    var selectedMessage = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Please select a [green]message.[/]")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more messages)[/]")
                            .AddChoices(messagesTitles));

                    if (selectedMessage == "Return")
                    {
                        return;
                    }
                    else
                    {
                        // Retrieve selected message object from the list with LINQ
                        currentMessage = messages.FirstOrDefault(message => message.message_title == selectedMessage);

                        // Display title and body
                        AnsiConsole.MarkupLine($"[bold royalblue1]Sent By:[/] [bold blue]{sender.user_name}[/]");
                        AnsiConsole.MarkupLine($"[bold royalblue1]Subject:[/] [bold blue]{currentMessage.message_title}[/]");
                        AnsiConsole.MarkupLine($"[bold royalblue1]Body:[/] [bold blue]{currentMessage.message_body}[/]");

                        // Show options to reply or delete
                        choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .AddChoices(new[] { "Reply", "Delete message", "Return"}));

                        switch (choice)
                        {
                            case "Reply":
                                int receiverId = currentMessage.message_sender; 
                                string messageReplyTitle = AnsiConsole.Ask<string>("Introduce message title: ");
                                string messageReplyBody = AnsiConsole.Ask<string>("Introduce message body: ");

                                messageRepo.InsertMessage(messageReplyTitle, messageReplyBody, receiverId, loggedUser.user_id);

                                AnsiConsole.MarkupLine($"[bold green] Reply sent successfully to {sender.user_name}![/]");

                                break;
                            case "Delete message":
                                if (!AnsiConsole.Confirm("Are you sure you want to delete this message? This action is undoable!"))
                                {
                                    AnsiConsole.MarkupLine("[bold green] Going back...[/]");
                                    return;
                                }
                                else
                                {
                                    messageRepo.DeleteMessage(currentMessage.message_id);
                                    AnsiConsole.MarkupLine("[bold yellow] Message deleted. Going back...[/]");
                                    return;
                                }
                            case "Return":
                                return;
                        }
                    }
                    break;
                case "Return":
                    return;
            }
        }

        // Sudo Menu Methods

        public void DisplayUserControlPanel()
        {
            AnsiConsole.MarkupLine($"[bold green]User Control Panel: Please choose an action.[/]");
            AnsiConsole.MarkupLine($"[bold yellow]SUDO ACTIONS REQUIRE NO CONFIRMATION. PLEASE PROCEED WITH CAUTION[/]");

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

                        // Add a return option
                        usernames.Add("Return");

                        var selectedUser = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Please select a [green]user.[/]")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more users)[/]")
                                .AddChoices(usernames));
                        
                        
                        if(selectedUser == "Return")
                        {
                            return;
                        }
                        else
                        {
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

        private void SignUp()
        {
            string username = "";
            string password = "";
            string usernameCheck = "";
            string passCheck = "";
            bool matchingCredentials = false;

            AnsiConsole.MarkupLine("[green]Sign up selected: Please enter desired credentials[/]");
            username = AnsiConsole.Ask<string>("Enter username:");
            password = AnsiConsole.Prompt(new TextPrompt<string>("Enter password:").Secret());

            while (!matchingCredentials)
            {
                AnsiConsole.MarkupLine("[green]Please confirm your credentials.[/]");
                usernameCheck = AnsiConsole.Ask<string>("Enter username:");
                passCheck = AnsiConsole.Prompt(new TextPrompt<string>("Enter password:").Secret());

                if (username == usernameCheck && password == passCheck)
                {
                    AnsiConsole.MarkupLine("[bold green]Signing up![/]");
                    matchingCredentials = true;
                    AnsiConsole.MarkupLine("[bold green]Successfully signed up![/]");
                    user.InsertUser(username, password, false);
                    DisplayMenu();
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