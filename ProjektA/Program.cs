using System;

namespace ProjektA
{
    class Program
    {
        struct Article
        {
            public string Name;
            public decimal Price;
        }

        const int _minNrArticles = 1;
        const int _maxNrArticles = 10;
        const int _maxArticleNameLength = 20;
        const decimal _vat = 0.25M;

        static Article[] articles = new Article[_maxNrArticles];
        static int nrArticles;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Project Part A");
            Console.WriteLine("Let's print a recepie");

            //Checks that user puts correct number of articles within the allowed range, and in the correct format.
            //User is allowed to quit the program as well.
            bool Continue = TryReadNrArticles($"\nHow many articles do you want? Between 1-10", out nrArticles, _minNrArticles, _maxNrArticles);

            EnterArticles(); //Method that lets the user enters the articles in correct format.
            PrintReciept(); //Method that prints out a reciept.
        }
        private static void EnterArticles()
        {
            string[] input;
            Article article = new Article();

            for (int i = 0; i < nrArticles; i++)
            {
                Console.WriteLine($"\nPlease enter name and price for article #{i + 1} separeted by ; (example Apple; 2,50):");
                input = Console.ReadLine().Split(";"); // splits the inputs with ";".

                article.Name = input[0];
                article.Price = Convert.ToDecimal(input[1]);
                if (string.IsNullOrEmpty(input[0]) || string.IsNullOrWhiteSpace(input[0]))
                {
                    Console.WriteLine("Wrong name, try again");
                }
                
                articles[i] = article;

            }
               

        }
        private static void PrintReciept()
        {
            // Printout our articles
            Console.WriteLine("\nReciept Purchased Article");
            Console.WriteLine("Purchase date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Console.WriteLine($"\nNumber of items purchased: {nrArticles}");
            Console.WriteLine($"\n{"#",-7} {"Name",-30} {"Price",-200}");

            decimal totalPrice = 0;

            for (int i = 1; i <= nrArticles; i++) 

            {
                totalPrice = totalPrice + articles[i-1].Price;
                Console.WriteLine($"{i, -7} {articles[i-1].Name,-30} {articles[i-1].Price, -200:C2}");
                //varför måste man ha -1?

            }
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Total price: {totalPrice,35:C2}");
            decimal totalVat = totalPrice * _vat;
            Console.WriteLine($"Total VAT: {totalVat,37:C2}");
            Console.WriteLine();



        }
        private static bool TryReadNrArticles(string question, out int number, int minInt, int maxInt)
        {
            number = 0;
            minInt = 1;
            maxInt = 10;
            string sInput;
            do
            {
                Console.WriteLine($"{question} ({minInt}-{maxInt} or Q to quit)?");
                sInput = Console.ReadLine();
                if (int.TryParse(sInput, out number) && number >= minInt && number <= maxInt)
                {
                    return true;
                }
                else if (sInput != "Q" && sInput != "q")
                {
                    Console.WriteLine("Wrong input, please try again.");
                }
            }
            while ((sInput != "Q" && sInput != "q"));
            return false;
        }

    }
    

}
