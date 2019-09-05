using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module01SyntaxReview
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Iteration Logic

            // for loop
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(string.Format("Iteration number {0}", i));
            }

            // foreach loop
            string[] names = { "Jamie", "Bryan", "David", "Mark", "Tessa", "Trisha" };
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }

            // while loop
            Console.WriteLine("Type 'enter' if you want to enter data to nowhere");
            string userResponse = Console.ReadLine();
            if (userResponse == "enter")
            {
                while(userResponse != "stop")
                {
                    Console.WriteLine("Enter Echo data now: (type 'stop' to end)");
                    userResponse = Console.ReadLine();
                    Console.WriteLine(string.Format("{0} echoed.",userResponse));

                }

                Console.WriteLine("While loop exited\n\n\n");
            }

            // Do loop - notice that userResponse will be 'stop' when loop starts but will still execute once.
            do
            {
                Console.WriteLine(string.Format("Beginning of Do Loop. User Response = {0}", userResponse));
                Console.WriteLine("Do Loop data to enter: ('stop' to stop the do)");
                userResponse = Console.ReadLine();
                Console.WriteLine(string.Format("UserResponse = {0} at end of the do loop",userResponse));
            } while (userResponse != "stop");

            #endregion

            #region Arrays

            int[] numberArray = new int[10];
            int[] initializedAndAssigned = { 1, 2, 3, 4, 5, 6 };

            int[,] twoDimensional = new int[10, 10];

            // Note the size of the arrays contained inside initial array do not need to be specified.
            int[][] jaggedArray = new int[10][];
            jaggedArray[0] = new int[5];
            jaggedArray[1] = new int[7];

            #endregion
        }
    }
}
