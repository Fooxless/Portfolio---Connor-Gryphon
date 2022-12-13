namespace MovieManage
{

    class MainInterface
    {
        Menu welcomemenu;
        private MemberCollection registered_members;
        private MovieCollection movies = new MovieCollection();
        private List<MovieGenre> moiveGenreList = new List<MovieGenre>();
        private List<MovieClassification> movieclasslist = new List<MovieClassification>();
        private Member member = null;


        private void init()
        {
            moiveGenreList.Add(MovieGenre.Action);
            moiveGenreList.Add(MovieGenre.Comedy);
            moiveGenreList.Add(MovieGenre.Western);
            moiveGenreList.Add(MovieGenre.Drama);
            moiveGenreList.Add(MovieGenre.History);

            movieclasslist.Add(MovieClassification.G);
            movieclasslist.Add(MovieClassification.PG);
            movieclasslist.Add(MovieClassification.M);
            movieclasslist.Add(MovieClassification.M15Plus);

            registered_members = new MemberCollection(10);
        }

        static void Main(string[] args)
        {

            MainInterface welcomemenu = new MainInterface();

            welcomemenu.init();

            welcomemenu.startup();

        }
        public MainInterface()
        {
            welcomemenu = new Menu();

        }

        bool finishedMain = true;
        bool finishedSecondary;

        public void startup()
        {
            string intro = "==============================================================\n" +
              "Welcome to Community Library Movie DVD Mangement System\n" + "==============================================================\n";
            Console.WriteLine(intro);
            welcomemenu.Add("Staff Login", staffUser);
            welcomemenu.Add("Member Login", loginUser);
            welcomemenu.Add("Empirical Analysis", empiricalAnalysis);
            welcomemenu.Add("Add Test Data", test);
            welcomemenu.Add("Close", close);
            while (finishedMain)
                welcomemenu.Display("========================Main Menu========================\n");
        }

        public void close()
        {
            finishedMain = false;
        }


        public void staffUser()
        {
            string userName = UserInterface.GetInput("\nEnter Username");
            string password = UserInterface.GetPassword("Enter Password");
            if (userName == "staff" && password == "today123")
            {
                finishedSecondary = false;
                while (!finishedSecondary)
                {
                    Menu staff_user_menu = new Menu();
                    staff_user_menu.Add("Add new DVDs of a new movie to the system", addDVD);
                    staff_user_menu.Add("Remove DVDs of a movie from the system", removeDVD);
                    staff_user_menu.Add("Register a new member with the system", registerMember);
                    staff_user_menu.Add("Remove a registered member from the system", removeMember);
                    staff_user_menu.Add("Display a member's contact phone number, given the member's name", contactNumberMember);
                    staff_user_menu.Add("Display all members who are currently renting a particular movie", membersRentingMovie);
                    staff_user_menu.Add("Return to the main menu", returnMain);
                    staff_user_menu.Display("========================Staff Menu========================\n");
                }
            }
            else
            {
                UserInterface.Error("\nIncorrect Username or Password");
            }
        }

        public void loginUser()
        {
            string firstName = UserInterface.GetInput("\nEnter First Name");
            string lastName = UserInterface.GetInput("Enter Last Name");
            string pin = UserInterface.GetPassword("Enter Password");

            if (registered_members.Number == 0)
            {
                UserInterface.Error("\nNo Members Exist");
            }
            else
            {
            bool Auth = false;
                for (int i = 0; i < registered_members.Members.Length; i++)
                {
                    if (registered_members.Members[i] != null)
                    {
                        Member memb = registered_members.Members[i];
                        if (firstName == memb.FirstName && lastName == memb.LastName && pin == memb.Pin)
                        {
                            Auth = true;
                            member = memb;
                        }
                    }
                }

                if (Auth == true)
                {
                    finishedSecondary = false;
                    while (!finishedSecondary)
                    {
                        Menu login_user_menu = new Menu();
                        login_user_menu.Add("Browse all the movies", browseMovies);
                        login_user_menu.Add("Display all the information about a movie, given the title of the movie", movieInfo);
                        login_user_menu.Add("Borrow a movie DVD", borrowMovie);
                        login_user_menu.Add("Return a movie DVD", returnMovie);
                        login_user_menu.Add("List current borrowing movies", currentBorrowing);
                        login_user_menu.Add("Display the top 3 movies rented by the members", topThree);
                        login_user_menu.Add("Return to the main menu", returnMain);
                        login_user_menu.Display("========================Member Menu========================\n");
                    }
                }
                else
                {
                    UserInterface.Error("\nIncorrect Username or Password");
                }
            }
        }


        // Staff Menu Stuff
        public void addDVD()
        {
            string title = UserInterface.GetInput("Please enter movie title");

            IMovie found = movies.Search(title);

            if (found == null)
            {
                // obtain values from staff user
                UserInterface.DisplayList("\nChoose from these genres", moiveGenreList);
                int genre = Int32.Parse(UserInterface.GetInput("Please enter genre (1-5)"));
                UserInterface.DisplayList("\nChoose from these classifications", movieclasslist);
                int classification = Int32.Parse(UserInterface.GetInput("Please enter classification (1-4)"));
                int duration = Int32.Parse(UserInterface.GetInput("\nPlease enter duration (in minutes)"));
                int availCopies = Int32.Parse(UserInterface.GetInput("\nPlease enter amount of copies"));

                // cast to enum 
                MovieGenre genre1 = (MovieGenre)genre;
                MovieClassification class1 = (MovieClassification)classification;

                Movie aMovie = new Movie(title, genre1, class1, duration, availCopies);
                if (movies.Insert(aMovie) != true)
                {
                    UserInterface.Error("Movie was not added");
                }
                else
                {
                    UserInterface.Message("Movie was added to the collection");
                }
            }
            else
            {
                int availCopies = Int32.Parse(UserInterface.GetInput("Please update amount of available copies"));
                found.AvailableCopies += availCopies;
                UserInterface.Message("Copies were added to the movie");
            }
        }

        public void removeDVD()
        {
            string title = UserInterface.GetInput("Please enter movie title");
            int toRemove = Int32.Parse(UserInterface.GetInput("Please enter the amount of DVD's to remove"));

            IMovie movie = movies.Search(title);
            if (movie == null)
            {
                UserInterface.Error("Movie does not exist");
            }
            else
            {
                if (movie.AvailableCopies - toRemove <= 0)
                {
                    movies.Delete(movie);
                    UserInterface.Message("Movie was deleted");
                }
                else
                {
                    movie.AvailableCopies -= toRemove;
                    UserInterface.Message(movie.Title + " has a total of " + movie.AvailableCopies + " available copies");
                    UserInterface.Message("Copies were removed");
                }
            }
        }

        public void registerMember()
        {
            string firstname = UserInterface.GetInput("Please enter members first name");
            string lastname = UserInterface.GetInput("Please enter members last name");
            string number = UserInterface.GetInput("Please enter members phone number");
            string pin = UserInterface.GetInput("Please enter members pin");

            if (IMember.IsValidPin(pin) != true || IMember.IsValidContactNumber(number) != true)
            {
                UserInterface.Error("Pin or Phone number is incorrectly entered");
            }
            else
            {
                Member newmem = new Member(firstname, lastname, number, pin);
                registered_members.Add(newmem);
                UserInterface.Message("Member was registered");
            }
        }

        public void removeMember()
        {

            string firstname = UserInterface.GetInput("Please enter members first name");
            string lastname = UserInterface.GetInput("Please enter members last name");
       
            Member deletemem = new Member(firstname, lastname);
            IMovie[] array = movies.ToArray();
            bool found = registered_members.Search(deletemem);
            bool broke = true;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Borrowers.Search(deletemem) == true)
                {
                    broke = false;
                    break;
                }
            }

            if (found != true)
            {
                UserInterface.Error("Member does not exist");
            }
            else if (broke == false)
            {
                UserInterface.Error("Member is currently borrowing a movie and cannot be deleted");
            }
            else
            {
                registered_members.Delete(deletemem);
                UserInterface.Message("Member deleted from collection");
            }
        }

        public void contactNumberMember()
        {
            string firstname = UserInterface.GetInput("Please enter members first name");
            string lastname = UserInterface.GetInput("Please enter members last name");

            Member phonemem = new Member(firstname, lastname);
            IMember member = registered_members.SearchMem(phonemem);
            if (member == null)
            {
                UserInterface.Error("Member does not exist");
            }
            else
            {
                string number = member.ContactNumber;
                UserInterface.Message("Members phone number is " + number);
            }
        }

        public void membersRentingMovie()
        {
            string title = UserInterface.GetInput("Please enter the movie title");
            IMovie rentmovie = movies.Search(title);
            if (rentmovie == null)
            {
                UserInterface.Error("Movie title doesnt exist");
            }
            else
            {
                UserInterface.Message("Members currently borrowing movie " + title + '\n' + rentmovie.Borrowers.ToString());
            }
        }

        public void returnMain()
        {
            member = null;
            finishedSecondary = true;
        }



        // Staff Menu Stuff

        // Browse and display all movie details
        public void browseMovies()
        {
            // display movie heading
            UserInterface.Message("\n========================== Movies =========================");

            // grab movie array
            IMovie[] movie_array = movies.ToArray();

            if (movie_array.Length != 0)
            {
                foreach (Movie movie in movie_array)
                {
                    // display
                    UserInterface.Message(movie.ToString());
                }

                // get number of movies and display
                UserInterface.Message("Number of Movies: " + movie_array.Length.ToString());
            }
            else
            {
                UserInterface.Error("No movies in library");
            }
        }

        // Get a specific movie and display its information
        public void movieInfo()
        {
            // display movie heading
            UserInterface.Message("\n======================= Movie Info ========================");

            // get movie array, prepare name list
            IMovie[] movie_array = movies.ToArray();
            List<string> movie_names = new List<string>();

            if (movie_array.Length != 0)
            {
                // store movie names in list
                foreach (Movie movie_in_array in movie_array)
                {
                    movie_names.Add(movie_in_array.Title);
                }

                // display name list
                UserInterface.DisplayList("\nMovies in Library", movie_names);

                // get movie name from input
                string movie_name = UserInterface.GetInput("Please enter a Movie Name");
                // find movie
                IMovie movie = movies.Search(movie_name);

                // display if not null, else movie not found
                if (movie != null)
                {
                    UserInterface.Message(movie.ToString());
                }
                else
                {
                    UserInterface.Message("\n");
                    UserInterface.Error("Movie not found");
                }
            } 
            else
            {
                UserInterface.Error("No movies in library");
            }
            
        }

        // Borrow the movie
        public void borrowMovie()
        {
            // display movie heading
            UserInterface.Message("\n===================== Borrow a Movie ======================");

            // get movie array, prepare name list
            IMovie[] movie_array = movies.ToArray();
            List<string> movie_names = new List<string>();

            if (movie_array.Length != 0)
            {
                // store movie names in list
                foreach (Movie movie_in_array in movie_array)
                {
                    movie_names.Add(movie_in_array.Title);
                }

                // display name list
                UserInterface.DisplayList("\nMovies in Library", movie_names);


                UserInterface.Message("\n");
                // get movie name from input
                string movie_name = UserInterface.GetInput("Please enter a Movie Name");
                IMovie movie = movies.Search(movie_name);

                // try to add borrower, if already in list, fail
                if (movie != null)
                {
                    if (movie.AddBorrower(member))
                    {
                        // add movie to borrowed by member
                        member.Collection.Insert(movie);
                        UserInterface.Message("Movie successfully borrowed!");
                    }
                    else
                    {
                        UserInterface.Error("Movie was unsuccessfully borrowed");
                    }
                }
                else
                {
                    UserInterface.Error("Movie was not found");
                }
            }
            else
            {
                UserInterface.Error("No movies in library");
            }
        }

        // Return a movie
        public void returnMovie()
        {
            // display movie heading
            UserInterface.Message("\n===================== Return a Movie ======================");

            // get movie array, prepare name list
            IMovie[] movie_array = movies.ToArray();

            if (movie_array.Length != 0)
            {
                // display movies currently borrowed by member
                currentBorrowing();

                // get movie name from input
                string movie_name = UserInterface.GetInput("Please enter a Movie Name");
                IMovie movie = movies.Search(movie_name);

                // try to remove borrower
                if (movie != null)
                {
                    if (movie.RemoveBorrower(member))
                    {
                        // remove movie from borrowed by member
                        member.Collection.Delete(movie);
                        UserInterface.Message("Movie successfully returned!");
                    }
                    else
                    {
                        UserInterface.Error("Movie was unsuccessfully returned");
                    }
                }
                else
                {
                    UserInterface.Error("Movie was not found");
                }
            }
            else
            {
                UserInterface.Error("No movies in library");
            }
        }

        // Display list of movies currently borrowed by current member
        public void currentBorrowing()
        {
            // display movie heading
            UserInterface.Message("\n================ Current Borrowed Movies ==================");


            // prepare name list
            List<string> movie_names = new List<string>();

            IMovie[] movie_array = member.Collection.ToArray();

            if (movie_array.Length != 0)
            {
                // insert names
                foreach (Movie movie_in_array in movie_array)
                {
                    movie_names.Add(movie_in_array.Title);
                }

                // display names
                UserInterface.DisplayList("\nYour Borrowed Movies", movie_names);
            } else
            {
                UserInterface.Error("Not movies currently borrowed");
            }            
        }

        public void topThree()
        {
            /*
            Display the top three (3) most frequently borrowed movies by the members in the descending order of their frequency.
            The display should include the title of the movie and frequency(the number of times that the movie has
            been borrowed by registered members by now)
            */

            UserInterface.Message("\n========================== Top Three Movies =========================");
            int count = 1;
            foreach (IMovie movie in topThreeAlgorithm(movies.ToArray()))
            {
                if (movie != null)
                {
                    UserInterface.Message("Position: " + count + "\nMovie: " + movie.Title + " \nFrequency of Borrows: " + movie.NoBorrowings);
                    count++;
                }
                
            }
        }

        // Empirical Analysis
        private int BasicOpCount = 0;

        // Given an array where the elements are randomly stored, find the three largest elements in the array.
        // Pre-condition: nil
        // Post-condition: top three (3) most frequently borrowed movies by the members in the descending order of their frequency
        public IMovie[] topThreeAlgorithm(IMovie[] movies)
        {
            BasicOpCount = 0;

            IMovie[] TopThree = new IMovie[3];

            foreach (IMovie movie in movies)
            {
                if (TopThree[0] == null)
                {
                    TopThree[0] = movie;
                }
                else
                {
                    if (movie.NoBorrowings > TopThree[0].NoBorrowings)
                    {
                        BasicOpCount++;
                        if (TopThree[1] == null)
                        {
                            TopThree[1] = TopThree[0];
                        }
                        else
                        {
                            TopThree[2] = TopThree[1];
                            TopThree[1] = TopThree[0];

                        }
                        TopThree[0] = movie;
                    }
                    else
                    {
                        BasicOpCount++;
                        if (TopThree[1] == null)
                        {
                            TopThree[1] = movie;
                        }
                        else
                        {
                            if (movie.NoBorrowings > TopThree[1].NoBorrowings)
                            {
                                BasicOpCount++;
                                TopThree[2] = TopThree[1];
                                TopThree[1] = movie;
                            }
                            else
                            {
                                BasicOpCount++;
                                if (TopThree[2] == null)
                                {
                                    TopThree[2] = movie;
                                }
                                else
                                {
                                    if (movie.NoBorrowings > TopThree[2].NoBorrowings)
                                    {
                                        BasicOpCount++;
                                        TopThree[2] = movie;
                                    }
                                    BasicOpCount++;
                                }
                            }

                        }
                    }

                }
            }
            return TopThree;
        }



        public void empiricalAnalysis()
        {
            UserInterface.Message("\n========================== Analysis =========================");
            int[] sampleSizes = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

            foreach (int sample in sampleSizes)
            {
                MovieCollection analysismovies = new MovieCollection();

                Random rnd = new Random();

                for (int i = 0; i < sample; i++)
                {
                    string movieName = "Movie " + (i + 1);
                    Movie movie = new Movie(movieName, MovieGenre.History, MovieClassification.M15Plus, i, i);

                    movie.NoBorrowings = rnd.Next(1, 1000);

                    analysismovies.Insert(movie);

                } 
                    topThreeAlgorithm(analysismovies.ToArray());
                    UserInterface.Message("Sample Size: " + sample + "\nBasic Operation Count: " + BasicOpCount);
               
            }
   
        }


        public void test()
        {
            UserInterface.Message("\n========================== Testing Data =========================");

            Movie Movie1 = new Movie("Star Wars", MovieGenre.Action, MovieClassification.M, 120, 50);
            Movie Movie2 = new Movie("Cowboys 1", MovieGenre.History, MovieClassification.M15Plus, 150, 0);
            Movie Movie3 = new Movie("Cowboys 2", MovieGenre.History, MovieClassification.M15Plus, 170, 30);
            Movie Movie4 = new Movie("Cowboys 2", MovieGenre.History, MovieClassification.M15Plus, 170, 30);
            Movie Movie5 = new Movie("zbba", MovieGenre.History, MovieClassification.M15Plus, 170, 30);
            Movie Movie6 = new Movie("Toy Story", MovieGenre.Comedy, MovieClassification.G, 90, 100);
            Movie Movie7 = new Movie("Cowboys 3", MovieGenre.History, MovieClassification.M15Plus, 170, 30);
            Movie Movie8 = new Movie("Iron Man", MovieGenre.History, MovieClassification.M15Plus, 170, 30);
            Movie Movie9 = new Movie("Stranger Things", MovieGenre.Comedy, MovieClassification.G, 90, 100);


            Movie1.NoBorrowings = 10;
            Movie6.NoBorrowings = 42;
            Movie5.NoBorrowings = 40;
            Movie3.NoBorrowings = 16;
            Movie2.NoBorrowings = 20;
            Movie7.NoBorrowings = 50;
            Movie8.NoBorrowings = 61;
            Movie9.NoBorrowings = 32;



            movies.Insert(Movie1);
            movies.Insert(Movie2);
            movies.Insert(Movie3);
            movies.Insert(Movie4);
            movies.Insert(Movie5);
            movies.Insert(Movie6);
            movies.Insert(Movie7);
            movies.Insert(Movie8);
            movies.Insert(Movie9);

            Member Member1 = new Member("Elon", "Musk", "0123456789", "1234");
            Member Member2 = new Member("Mark", "Zuckerberg", "0123456789", "1234");
            Member Member3 = new Member("Jeffrey", "Bezos", "0123456789", "1234");
            Member Member4 = new Member("Apple", "Musk", "0123456789", "1234");

            registered_members.Add(Member1);
            registered_members.Add(Member2);
            registered_members.Add(Member3);
            registered_members.Add(Member4);

            Movie1.Borrowers.Add(Member1);
            Member1.Collection.Insert(Movie1);


            Movie2.Borrowers.Add(Member1);
            Member1.Collection.Insert(Movie2);

            Movie1.Borrowers.Add(Member3);
            Member3.Collection.Insert(Movie1);

            Movie1.Borrowers.Add(Member4);
            Member4.Collection.Insert(Movie1);

            UserInterface.Message("\nMembers");
            UserInterface.Message("\n" + registered_members.ToString());


            UserInterface.Message("\nMovies");
            IMovie[] movie_arr = movies.ToArray();
            foreach (Movie movie in movie_arr) {
                UserInterface.Message(movie.ToString());
            }
           


        }
    }
}
