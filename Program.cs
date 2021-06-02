using System;

namespace loops_array
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i=0; i<10; i++)
            {
                if (i==5)
                {
                    continue;
                }

                Console.WriteLine(i);
            }

            string[] cars = { "BMW", "Ford", "Toyota", "Honda", "Ferrari" };

            Console.WriteLine("\nForeach Loop");

            foreach (string i in cars)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("\nFor Loop");

            for (int j=0; j<cars.Length; j++)
            {
                Console.WriteLine(cars[j]);
            }

            Console.WriteLine("\nArray Slicing");

            Console.WriteLine(cars[0]);
            Console.WriteLine(cars[1]);

            Console.WriteLine("---------------------");

            Console.WriteLine(cars[0][0]);
            Console.WriteLine(cars[0][1]);
            Console.WriteLine(cars[1][0]);


            Console.WriteLine("\nDo While Loop");

            int k = 1;
            do
            {
                Console.WriteLine(k++);         // first display then increment (for next)
            }
            while (k < 10);



            Console.WriteLine("\nWhile Loop");

           
            while(k < 20)
            {
                Console.WriteLine(++k);     // first increment then display
            }
        }
    }
}
