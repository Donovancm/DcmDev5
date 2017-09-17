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
            ViewData();
        }
        static void InsertData()
        {
            using (var db = new MovieContext())
            {
                Movie m = new Movie
                {
                    Title = "No country for old men",
                    Actors = new System.Collections.Generic.List<Actor> {
                        new Actor{Name = "Tommy Lee"},
                        new Actor{Name = "Xavier Berdem"}
                    }
                };
                db.Movies.Add(m);
                db.SaveChanges();
            }
        }
        //Step 2
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
    }
}
