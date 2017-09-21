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
            // Week 1
            //InsertData();
            //ViewData();
            //ModifyData();
            //Week 2
            //DataInsertion();
            //Projection();
            //GroupingAggregation();
            //Join();
            SubQueryAggration();
        }

        static void DataInsertion()
        {
            using ( var db = new MovieContext())
            {
                /* Movie m = new Movie 
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
                }; */
                Movie m3 = new Movie
                {
                    Title = "Solid Snake ",
                    Release = new DateTime(2005, 2, 1),
                    Actors = new System.Collections.Generic.List<Actor>
                    {
                        new Actor { Name = "Jack Snake", Birth = new DateTime(1963, 8, 5), Gender = "Male,"},
                        new Actor { Name = "Luna Snake", Birth = new DateTime(1990, 9, 19), Gender = "Female,"},
                        new Actor { Name = "David Snake", Birth = new DateTime(1993, 5, 27), Gender = "Male,"}
                    }
                };
                //db.Add(m);
                //db.Add(m2);
                db.Add(m3);
                db.SaveChanges();
                Console.WriteLine("m" + "m2" + "m3" + "has been added to the database");
            }
        }
        static void Projection()
        {
            using ( var db = new MovieContext())
            {
                //Projection
                //

                //var projected_movies = from m in db.Movies select new { m.Title, m.Release};
                var projected_movies = from m in db.Movies select m;
                Console.WriteLine("Movie title | Release");
                foreach (var movie in projected_movies)
                {
                    Console.WriteLine("- {0} | {1} ", movie.Title, movie.Release);
                } 

                var projected_movies2 = from m in db.Movies where m.Release > new DateTime(2000, 1, 1) select m;
                Console.WriteLine("Movie title | Release from later then 2000");
                foreach (var movie in projected_movies2)
                {
                    Console.WriteLine("- {0} | {1} ", movie.Title, movie.Release);
                }

                var projected_movies3 = from m in db.Movies where m.Release > new DateTime(2000, 1, 1) orderby m.Release descending select m;
                Console.WriteLine("Movie title | Release sort by descending");
                foreach (var movie in projected_movies3)
                {
                    Console.WriteLine("- {0} | {1} ", movie.Title, movie.Release);
                }

            }
        }
        static void GroupingAggregation()
        {
            using (var db = new MovieContext())
            {
                /*var projected_movies4 = from a in db.Actors group a by a.Gender into genderGroup select genderGroup;
                foreach (var movie in projected_movies4)
                {
                    Console.WriteLine("+ {0} ", movie.Key);
                    foreach (var actor in movie)
                    {
                        Console.WriteLine("-- {0} ", actor.Name);
                    }
                }*/
                var result = 
                from m in db.Movies 
                from actor in db.Actors where actor.MovieId == m.Id 
                group actor by actor.Gender into GenderGroup 
                select Tuple.Create(
                    GenderGroup.Key, GenderGroup.Count()
                );
                Console.WriteLine("Gender | Number of actors");
                foreach (var item in result)
                {
                    Console.WriteLine("{0} | {1} ", item.Item1, item.Item2);
                }
            }
        }
        static void Join()
        {
            using ( var db = new MovieContext())
            {
                var projected_movies5 = 
                from movie in db.Movies 
                from actor in db.Actors where movie.Id == actor.Id select new {
                    Title = movie.Title, ActorName = actor.Name
                };
                Console.WriteLine("Movie title and Actor name");
                foreach (var movie in projected_movies5)
                {
                    Console.WriteLine("- {0} | {1} ", movie.Title, movie.ActorName);
                }
            }
        }
        static void SubQueryAggration()
        {
            using ( var db = new MovieContext())
            { 
                var projected_movies6 =
                from movie in db.Movies let actors_of_movie =
                (
                    from actor in db.Actors where actor.MovieId == movie.Id select actor
                )
                where actors_of_movie.Count() < 3 
                select new 
                {
                    Title = movie.Title,
                    ActorsCount = actors_of_movie.Count()
                };
                Console.WriteLine(" Movies and actors");
                foreach (var movie in projected_movies6)
                {
                    Console.WriteLine("{0} | {2} ", movie.Title, movie.ActorsCount );
                }
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
