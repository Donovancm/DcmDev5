using System;
using Model;
using System.Linq;

namespace Week1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //InsertData();
            //ViewData();
            ModifyData();
        }
        static void InsertData()
        {
            using (var db = new MovieContext())
            {

                Movie foundMovie = db.Movies.Find(0);
                Console.WriteLine();
                foundMovie.Title = "No country for old men";
                int count = db.SaveChanges();
                Console.WriteLine("{0} records has been changed", count);

                foreach (var Movie in db.Movies)
                {
                    Console.WriteLine(" - {0} ", Movie.Title);
                }
            }
        }
        //Step 3
        static void ViewData()
        {
            using (var db = new MovieContext())
            {
                foreach (var movie in db.Movies)
                {
                    Console.WriteLine("Found movie with title" + movie.Title);
                    foreach (var actor in db.Actors.Where(a => movie.Id == a.MovieId))
                    {
                        Console.WriteLine("Found actor with name" + actor.Name);
                    }
                }
            }
        }
        static void ModifyData()
        {
            using (var db = new MovieContext())
            {
                Movie foundMovie = db.Movies.Find(1);
                Console.WriteLine("Found movie with title" + foundMovie.Title);
                foundMovie.Title = "White cats, Black cats...";
                db.SaveChanges();
                Console.WriteLine("Title changed");

                foreach(var Movie in db.Movies)
                {
                    Console.WriteLine(" - {0}", Movie.Title);
                }
            }
        }
    }
}
