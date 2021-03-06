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
            Console.WriteLine("Let's print a receipt");

            //Checks that user puts correct number of articles within the allowed range, and in the correct format.
            //User is allowed to quit the program as well.
            bool Continue = TryReadNrArticles($"\nHow many articles do you want? Between 1-10", out nrArticles, _minNrArticles, _maxNrArticles);

            EnterArticles(); //Method that let's the user enter the articles in correct format.
            PrintReceipt(); //Method that prints out a reciept.
        }
        private static void EnterArticles()
        {

            string[] articleData;
            int counter = 0;
            Article article = new Article();
            while (counter < nrArticles)
            {
                try
                {
                    Console.WriteLine($"\nPlease enter name and price for article #{counter + 1} separeted by ; (example Apple; 2,50):");
                    articleData = Console.ReadLine().Split(";"); // splits the inputs with ";".

                    if (articleData.Length != 2)
                    {
                        Console.WriteLine("Wrong format, try again!");
                        continue;
                    }
                    
                    if (!string.IsNullOrEmpty(articleData[0]) && !string.IsNullOrWhiteSpace(articleData[0])) 
                        //checks if the string is entered correct
                    {
                        article.Name = articleData[0];
                    }
                    else
                    {
                        Console.WriteLine("Name error!");
                        continue;
                    }

                    bool checkValue = decimal.TryParse(articleData[1], out decimal price);
                    if(checkValue)
                    {
                        article.Price = price;
                    }
                       else
                    {
                        Console.WriteLine("Price error!"); //checks if price is entered in correct format
                        continue;
                      
                    }

                    articles[counter] = article;
                }
                catch
                {
                    
                    Console.WriteLine($"Wrong input, try again!");
                    continue;
                }
                counter++;             
                continue;
            }

        }
        private static void PrintReceipt()
        {
            // Printout our articles
            Console.WriteLine("\nReceipt Purchased Article");
            Console.WriteLine("Purchase date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Console.WriteLine($"\nNumber of items purchased: {nrArticles}");
            Console.WriteLine($"\n{"#",-7} {"Name",-30} {"Price",-200}");

            decimal totalPrice = 0;

            for (int i = 0; i < nrArticles; i++)

            {
                totalPrice = totalPrice + articles[i].Price;
                Console.WriteLine($"{i+1,-7} {articles[i].Name,-30} {articles[i].Price,-200:C2}");
               

            }
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Total price: {totalPrice,35:C2}");
            decimal totalVat = totalPrice * _vat;
            Console.WriteLine($"Total VAT: {totalVat,37:C2}");

        }
        #region Userinput
        private static bool TryReadNrArticles(string question, out int number, int minInt, int maxInt)
        {
           
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
        #endregion

    }




}