using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Efi;
using makesquare;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] M = new double [3,3];
            double[,] K = new double[3, 3];
            Random aleatorio = new Random();

            for (int i = 0; i < 4; i++)
            {
                for (int J = 0; J < 4; J++)
                {
                    M[i, J] = aleatorio.Next(0, 100);
                    K[i, J] = aleatorio.Next(0, 100);
                }
            }

            makesquare.Class1 class1 = new Class1();
            //class1.makesquare();

            Console.Read();

        }
    }
}
