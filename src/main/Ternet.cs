using ternet.repositories;
using ternet.entities;
using System;

namespace ternet.main 
{
    public class Ternet 
    {
        public static void Main(string[] args)
        {
            CommentPostRepository commentRepo = new CommentPostRepository();

            int option;

            Console.WriteLine(" -- Welcome to TerNet! Your terminal social network. -- ");
            Console.WriteLine(" -- What would you like to do? --");
            Console.WriteLine("1. See CommentsPosts table");

            // Type casting. ReadLine() only reads strings
            option = Convert.ToInt32(Console.ReadLine());

            if (option == 1)
            {
                foreach (CommentPost comment in commentRepo.GetCommentPost())
                {
                    Console.WriteLine(comment);
                }
            }
        }
    }
}