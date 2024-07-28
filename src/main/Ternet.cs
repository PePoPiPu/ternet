using ternet.repositories;
using ternet.entities;
using ternet.console;
using System;

namespace ternet.main 
{
    public class Ternet 
    {
        public static void Main(string[] args)
        {
            ConsoleMenu menu = new ConsoleMenu();
            
            menu.DisplayMenu();
        }
    }
}