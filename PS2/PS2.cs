using System;
using System.Collections.Generic;

namespace PS2
{
	//Devin White
	//U0775759
	class PS2
	{

		int[,] HeightTable;
		int[] Heights;
		int maxHeight;

		//constructor
		public PS2(int[] climbdistances, int maxDistance)
		{
			Heights = climbdistances;
			HeightTable = new int[Heights.Length + 1, 2000];
			this.maxHeight = maxDistance;

		}

		//My old recursive minWall. It isn't very fast or smart but I love him
		/* Takes a starting height and an index
		 * returns the minimum distance to complete, or 100000 if impossible
		 */
		public int MinWallRecursive(int index, int startingHeight)
		{
			//base case
			if (index > climbingDistances.Length - 1 && startingHeight <= 0)
			{
				return 0;
			}
			else if (index > climbingDistances.Length - 1 && startingHeight > 0)
			{
				return 100000;
			}
			else
			{
				int newStartHeight = climbingDistances[index];
				//no choice but to go up.
				if (startingHeight < newStartHeight)
				{
					return MinWallRecursive(index + 1, startingHeight + newStartHeight);
				}
				else
				{
					//Take maximum of each route
					int up = Math.Max(MinWallRecursive(index + 1, startingHeight + newStartHeight), startingHeight);
					int down = Math.Max(MinWallRecursive(index + 1, startingHeight - newStartHeight), startingHeight);
					return Math.Min(up, down);
				}
			}

		}
		//Minwall with DP. That sounds dirty..
		/* DP version of minWall. Maps values to a 2D array and calculates the minimum distance to complete the exercises
		 */
		public int minWall(int[] climbingdist, int maxH)
		{
			Heights = climbingdist;

			//instantiate base cases
			for (int i = 0; i < HeightTable.GetLength(0); i++)
			{
				for (int j = 0; j < HeightTable.GetLength(1); j++)
				{
					HeightTable[i, j] = 100000;
				}
			}
			HeightTable[0, 0] = 0;


			for (int i = 0; i < Heights.Length; i++)
			{
				for (int j = 0; j <= maxHeight; j++)
				{
					int heightUp = Heights[i] + j;
					int heightDown = j - Heights[i];
					int currentHeight = HeightTable[i, j];
					int peakHeight = Math.Max(heightUp, currentHeight);
					int nextIndex = i + 1;

					if (heightDown > -1)
					{
						if (currentHeight < HeightTable[nextIndex, heightDown])
						{
							HeightTable[nextIndex, heightDown] = currentHeight;
						}
						if (peakHeight < HeightTable[nextIndex, heightUp])
						{
							HeightTable[nextIndex, heightUp] = peakHeight;
						}
					}
					else
					{

						if (peakHeight < HeightTable[nextIndex, heightUp])
						{
							HeightTable[nextIndex, heightUp] = peakHeight;
						}

					}


				}
			}
			return HeightTable[Heights.Length, 0];

		}
		public static int[] climbingDistances;
		static void Main(string[] args)
		{
			int.TryParse(Console.ReadLine(), out int numDistances);
			//get ids and put them in array
			climbingDistances = new int[numDistances];
			int[] climbingDistances2 = new int[numDistances];
			int H = 0;
			string[] distanceTokens = Console.ReadLine().Split(' ');
			//populate skills array
			for (int i = 0; i < distanceTokens.Length; i++)
			{
				if (int.TryParse(distanceTokens[i], out int distance))
				{
					climbingDistances[i] = distance;
					climbingDistances2[i] = distance;
				}
			}
			foreach (int distance in climbingDistances)
			{
				H += distance;
			}
			PS2 climber = new PS2(climbingDistances2, H);
			//Console.WriteLine(MinWallRecursive(0, 0));
			Console.WriteLine(climber.minWall(climbingDistances2, H));
		}
	}
}

