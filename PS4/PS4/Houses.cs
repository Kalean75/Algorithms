

using System;
using System.Collections.Generic;
/*Devin White
 * u0775759
 * PS4
 */
namespace PS4
{
    class Houses
    {
        public double[] plots { get; private set; }
        public double[] houses { get; private set; }
        //constructor
        public Houses(double[] house, double[] plot)
        {
            plots = plot;
            houses = house;
        }
        static void Main(string[] args)
        {
            int N, M, K;
            string[] startTokens = Console.ReadLine().Split(' ');
            //get ids and put them in array
            int.TryParse(startTokens[0], out int NSize);
            N = NSize;
            int.TryParse(startTokens[1], out int MSize);
            M = MSize;
            int.TryParse(startTokens[2], out int KSize);
            K = KSize;
            string[] plotTokens = Console.ReadLine().Split(' ');
            //holds plots
            double[] plots = new double[N];
            for (int i = 0; i < plotTokens.Length; i++)
            {
                int.TryParse(plotTokens[i], out int plotSize);
                plots[i] = plotSize;
            }
            //Round houses
            string[] roundHouseTokens = Console.ReadLine().Split(' ');
            double[] Houses = new double[M + K];
            for (int i = 0; i < roundHouseTokens.Length; i++)
            {
                int.TryParse(roundHouseTokens[i], out int roundSize);
                Houses[i] = roundSize;
            }
            //Square houses
            //Round houses
            string[] squareHouseTokens = Console.ReadLine().Split(' ');
            for (int i = 0; i < K; i++)
            {
                double.TryParse(squareHouseTokens[i], out double squareSize);
                //side length = sqrt(2) * radius
                // radius = side length/sqrt(2)
                // a square fits into a circle with the equation (s*sqrt(2))/2
                //M+i because continuing from the end of round houses
                Houses[M + i] = (squareSize * Math.Sqrt(2)) / 2;
            }
            Array.Sort(plots);
            Array.Sort(Houses);
            Houses builder = new Houses(Houses, plots);
            Console.WriteLine(builder.Build(builder.plots, builder.houses));

        }

        //Greedy method to solve problem
        public int Build(double[] plot, double[] house)
        {
            int totalHouses = 0;
            int N = plot.Length - 1;
            int MK = house.Length - 1;
            for (int i = MK; i >= 0; i--)
            {
                if (house[i] < plots[N])
                {
                    totalHouses++;
                    if (N - 1 >= 0)
                    {
                        N--;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return totalHouses;
        }
    }
}
