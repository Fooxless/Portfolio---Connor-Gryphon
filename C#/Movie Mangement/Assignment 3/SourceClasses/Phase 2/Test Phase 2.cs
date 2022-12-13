//namespace CAB301Assignment3
//{

//    using System;
//    class Test2
//    {
//        static void Main(string[] args)
//        {
//            // Testing Area Phase 2
//            Test2 test = new Test2();

//            // Movies
//            Movie Movie1 = new Movie("Star Wars", MovieGenre.Action, MovieClassification.M, 120, 50);
//            Movie Movie2 = new Movie("Cowboys 1", MovieGenre.History, MovieClassification.M15Plus, 150, 0);
//            Movie Movie3 = new Movie("Cowboys 2", MovieGenre.History, MovieClassification.M15Plus, 170, 30);
//            Movie Movie4 = new Movie("Cowboys 2", MovieGenre.History, MovieClassification.M15Plus, 170, 30);
//            Movie Movie5 = new Movie("zbba", MovieGenre.History, MovieClassification.M15Plus, 170, 30);
//            Movie Movie6 = new Movie("Toy Story", MovieGenre.Comedy, MovieClassification.G, 90, 100);
//            // Memvers 
//            Member Member1 = new Member("Elon", "Musk");
//            Member Member2 = new Member("Mark", "Zuckerberg");
//            Member Member3 = new Member("Jeffrey", "Bezos");
//            Member Member4 = new Member("Apple", "Musk");

//            Console.ForegroundColor = ConsoleColor.Magenta;
//            Console.WriteLine("-----------------------------------------------------");
//            Console.WriteLine("Movie Testing");
//            Console.WriteLine("-----------------------------------------------------\n");
//            test.MethodPrint("AddBorrwer\n");

//            test.TitlePrint("No Borrwers");
//            test.MovieInfoPrint(Movie1);


//            test.TitlePrint("1 Borrwer");
//            Movie1.AddBorrower(Member1);  //code
//            test.ActionPrint("AddBorrower");
//            test.MovieInfoPrint(Movie1);

//            test.TitlePrint("1 Borrwer - same person");
//            Movie1.AddBorrower(Member1); //code
//            test.ActionPrint("AddBorrower");
//            test.MovieInfoPrint(Movie1);

//            test.TitlePrint("0 Available Copies");
//            test.MovieInfoPrint(Movie2);
//            Movie2.AddBorrower(Member2); //code
//            Movie1.AddBorrower(Member1); //code
//            test.ActionPrint("AddBorrower");
//            test.MovieInfoPrint(Movie2);


//            test.MethodPrint("Remove Borrower\n");

//            test.TitlePrint("Remove Musk from Star Wars");
//            test.MovieInfoPrint(Movie1);
//            test.ActionPrint("Remove Musk");
//            Movie1.RemoveBorrower(Member1); //code
//            test.MovieInfoPrint(Movie1);

//            test.MethodPrint("CompareTo - Dictionary Order\n");

//            test.TitlePrint("Compare - before");
//            Console.WriteLine(Movie3.Title + " compare to " + Movie1.Title);
//            test.ActionPrint("Compare");
//            Console.WriteLine(Movie3.CompareTo(Movie1)); //code

//            test.TitlePrint("Compare - same");
//            Console.WriteLine(Movie3.Title + " compare to " + Movie4.Title);
//            test.ActionPrint("Compare");
//            Console.WriteLine(Movie3.CompareTo(Movie4)); //code

//            test.TitlePrint("Compare - after");
//            Console.WriteLine(Movie3.Title + " compare to " + Movie2.Title);
//            test.ActionPrint("Compare");
//            Console.WriteLine(Movie3.CompareTo(Movie2)); //code

//            test.TitlePrint("me test");
//            Console.WriteLine(Movie2.Title + " compare to " + Movie3.Title);
//            test.ActionPrint("Compare");
//            Console.WriteLine(Movie2.CompareTo(Movie3)); //code

//            test.MethodPrint("ToString\n");

//            test.TitlePrint("To String method");
//            Console.WriteLine("Title, genre, classification, duration, and the number of available copies of this movie has been returned \n" + Movie1.ToString());

//            Console.ForegroundColor = ConsoleColor.Blue;
//            Console.WriteLine("-----------------------------------------------------");
//            Console.ForegroundColor = ConsoleColor.Magenta;
//            Console.WriteLine("\n-----------------------------------------------------");
//            Console.WriteLine("Movie Collection Testing");
//            Console.WriteLine("-----------------------------------------------------");

//            test.MethodPrint("IsEmpty\n");

//            test.TitlePrint("Empty Collection");
//            // Empty Movie Collection
//            MovieCollection MovCollection = new MovieCollection();
//            test.ActionPrint("Create Movie Collection");
//            Console.WriteLine(MovCollection.IsEmpty());

//            test.MethodPrint("Insert\n");

//            test.TitlePrint("Create Movie Collection");
//            Console.WriteLine(MovCollection.Number);
//            test.ActionPrint("add movie 1");
//            Console.WriteLine(MovCollection.Insert(Movie1));
//            Console.WriteLine(MovCollection.Number);
//            test.ActionPrint("add movie 2");
//            Console.WriteLine(MovCollection.Insert(Movie2));
//            Console.WriteLine(MovCollection.Number);
//            test.ActionPrint("add movie 5");
//            Console.WriteLine(MovCollection.Insert(Movie5));
//            Console.WriteLine(MovCollection.Number);

//            test.TitlePrint("Create Node of Nodes");
//            test.ActionPrint("add movie 3");
//            Console.WriteLine(MovCollection.Insert(Movie3));
//            Console.WriteLine(MovCollection.Number);

//            test.TitlePrint("Movies with same name");
//            test.ActionPrint("add movie 4");
//            Console.WriteLine(MovCollection.Insert(Movie4));
//            Console.WriteLine(MovCollection.Number);

//            test.MethodPrint("Search - object\n");

//            test.TitlePrint("Search for Cowboys 2");
//            test.ActionPrint("search");
//            Console.WriteLine(MovCollection.Search(Movie3));

//            test.TitlePrint("Search for Star Wars");
//            test.ActionPrint("search");
//            Console.WriteLine(MovCollection.Search(Movie1));

//            test.TitlePrint("Search for Toy Story");
//            test.ActionPrint("search");
//            Console.WriteLine(MovCollection.Search(Movie6));

//            test.MethodPrint("Search - movie title\n");

//            test.TitlePrint("Search for Cowboys 2");
//            test.ActionPrint("search");
//            Console.WriteLine(MovCollection.Search("Cowboys 2").ToString());

//            test.TitlePrint("Search for Star Wars");
//            test.ActionPrint("search");
//            Console.WriteLine(MovCollection.Search("Star Wars").ToString());

//            test.TitlePrint("Search for Toy Story");
//            test.ActionPrint("search");
//            Console.WriteLine(MovCollection.Search("Toy Story"));


//            test.MethodPrint("To Array\n");

//            test.TitlePrint("MovCollection Array");
//            test.ActionPrint("toarray");
//            for (int i = 0; i < MovCollection.ToArray().Length; i++)
//            {
//                Console.WriteLine(MovCollection.ToArray()[i].ToString());
//            }
//            Console.WriteLine(MovCollection.ToArray());

//            test.MethodPrint("Clear\n");

//            test.TitlePrint("Clear MovCollection");
//            test.ActionPrint("clear");
//            //MovCollection.Clear();
//            Console.WriteLine(MovCollection.ToArray()[0]);

//            test.MethodPrint("Delete\n");

//            test.TitlePrint("Delete a Star Wars");
//            test.ActionPrint("delete");
//            MovCollection.Delete(Movie1);
//            test.TitlePrint("MovCollection Array");
//            test.ActionPrint("toarray");
//            Console.WriteLine(MovCollection.ToArray().Length);
//            for (int i = 0; i < MovCollection.ToArray().Length; i++)
//            {
//                Console.WriteLine(MovCollection.ToArray()[i].ToString());
//            }


//            test.MethodPrint("End\n");

//        }


//        private void ActionPrint(string doing)
//        {
//            Console.ForegroundColor = ConsoleColor.Yellow;
//            Console.WriteLine(doing);
//            Console.ForegroundColor = ConsoleColor.White;
//        }

//        private void TitlePrint(string doing)
//        {
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.WriteLine(doing);
//            Console.ForegroundColor = ConsoleColor.White;
//        }

//        private void MethodPrint(string doing)
//        {
//            Console.ForegroundColor = ConsoleColor.Blue;
//            Console.WriteLine("-----------------------------------------------------");
//            Console.WriteLine(doing);
//            Console.ForegroundColor = ConsoleColor.White;
//        }

//        private void MovieInfoPrint(Movie print)
//        {
//            Console.WriteLine("Movie Info \n" + print.ToString());
//            Console.WriteLine("Borrowers:  " + print.Borrowers.ToString());
//        }
//    }
//}