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
            //ModifyData();
            DataInsertion();
        }

        static void DataInsertion()
        {
            using ( var db = new MovieContext())
            {
                Movie m = new Movie 
                {
                    Title = "Divorce Italian Style",
                    Actors = new System.Collections.Generic.List<Actor>
                    {
                        new Actor { Name = "Marcello Mastroianni", Birth = new DateTime(1988, 8, 29), Gender = "Male,"},
                        new Actor { Name = "Daniela Rocca", Birth = new DateTime(1986, 5, 1), Gender = "Female,"}                  
                    }
                };
                Movie m2 = new Movie
                {
                    Title = "Freedom for All",
                    Actors = new System.Collections.Generic.List<Actor>
                    {
                        new Actor { Name = "John Adams", Birth = new DateTime(1982, 3, 15), Gender = "Male,"},
                        new Actor { Name = "Eva Moonstar", Birth = new DateTime(1985, 2, 12), Gender = "Female,"},
                        new Actor { Name = "Adamska Ghost", Birth = new DateTime(1995, 5, 13), Gender = "Male,"}
                    }
                };
                db.Add(m);
                db.Add(m2);
                db.SaveChanges();
                Console.WriteLine("m" + "m2" + "has been added to the database");
            }
        }
        static void Projection()
        {
            using ( var db = new MovieContext())
            {
                
            }
        }
        static void Join()
        {
            using ( var db = new MovieContext())
            {
                
            }
        }




//      Week 1 methods below
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
