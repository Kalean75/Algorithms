using System.Diagnostics;
//Devin White
//u0775759
//PS9
namespace ps9
{
	/// <summary>
	/// Finds the new road which makes the smallest path
	/// </summary>
	class Commute
	{
		int N;
		Node[] coordinates;
		double[,] distances;
		double[,] graph;
		double count = 0;
		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="n">Total number of intersections(vertices)</param>
		/// <param name="coords">coordinates of intersections</param>
		public Commute(int n, Node[] coords)
		{
			N = n;
			coordinates = coords;
			distances = new double[n, n];
			graph = new double[n, n];
		}
		/// <summary>
		/// Main method
		/// </summary>
		public static void Main()
		{
			//num intersections
			int.TryParse(Console.ReadLine(), out int intersections);
			int n = intersections;
			//double count = 0;
			Node[] coords = new Node[n];
			//parse coordinates
			for (int i = 0; i < n; i++)
			{
				string[] tokens = Console.ReadLine().Split();
				int.TryParse(tokens[0], out int x);
				int.TryParse(tokens[1], out int y);
				coords[i] = new Node(x, y);
			}

			Commute trans = new Commute(n, coords);
			//distances of all possible roads
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					trans.distances[i, j] = trans.calculateDistance(coords[j].x, coords[i].x, coords[j].y, coords[i].y);
					trans.distances[j, i] = trans.distances[i, j];
				}
			}
			//initialize to infinity
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					trans.graph[i, j] = double.MaxValue;
				}
			}
			//nodes are can reach themselves in no time.
			for (int i = 0; i < n; i++)
			{
				trans.graph[i, i] = 0;
			}
			//Sqrt((x2 - x)^2+ (y2-y1)^2)
			int.TryParse(Console.ReadLine(), out int roads);
			int m = roads;
			for (int j = 0; j < m; j++)
			{
				string[] tokens = Console.ReadLine().Split();
				int.TryParse(tokens[0], out int road1);
				int.TryParse(tokens[1], out int road2);
				trans.graph[road1,road2] = trans.distances[road1,road2];
				trans.graph[road2, road1] = trans.distances[road1, road2];
			}
			Console.WriteLine(trans.BuildRoad(trans.graph));
		}

		/// <summary>
		/// Calculates the distance
		/// </summary>
		/// <param name="x1"></param>
		/// <param name="x2"></param>
		/// <param name="y1"></param>
		/// <param name="y2"></param>
		/// <returns></returns>
		public double calculateDistance(int x1,int x2, int y1, int y2)
		{
			return Math.Sqrt(Math.Pow((x1-x2),2) + Math.Pow((y1-y2),2));
		}
		public double calcualateNewDistance(double[,] data, int n1, int n2) 
		{
			double[,] newDistances = new double[N, N];
			Array.Copy(data, newDistances, data.Length);
			double sum = 0;
			newDistances[n1, n2] = distances[n1, n2];
			newDistances[n2, n1] = distances[n2, n1];
			// Floyd Warshall
			/*for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
				{
					for (int k = 0; k < N; k++)
					{
						v[j, k] = Math.Min(v[j, k], v[j, i] + v[i, k]);
						v[k, j] = v[j, k];
					}
				}
			}*/
			//modified floyd warshall to avoid repeated calculations
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
				{
					newDistances[i,j] = Math.Min(newDistances[i,j], newDistances[i,n1] + newDistances[n1,n2] + newDistances[n2,j]);
					newDistances[i,j] = Math.Min(newDistances[i,j], newDistances[i,n2] + newDistances[n2,n1] + newDistances[n1,j]);
				}
			}
			//total weight with the new possible road
			for (int i = 0; i < N; i++)
			{
				for (int j = i; j < N; j++)
				{
					sum += newDistances[i, j];
				}
			}


			return sum;
		}
		/// <summary>
		/// calculates the shortest distanace possible by adding a new road(edge) to the graph
		/// </summary>
		/// <param name="distance"></param>
		/// <param name="graph">The graph consisting of current roads(edges) and current intersections(vertices)</param>
		/// <returns></returns>
		public double BuildRoad(double[,] graph)
		{
			//floyd Warshall
			for(int i = 0; i < N; i++)
			{
				for(int j = 0; j < N; j++)
				{
					for(int k = 0; k < N; k++)
					{
						graph[j, k] = Math.Min(graph[j,k],graph[j, i] + graph[i, k]);
						graph[k, j] = graph[j, k];
					}
				}
			}
			//total weight without new road(for debugging)
			for (int i = 0; i < N; i++)
			{
				for(int j = i; j  < N; j++)
				{
					count += graph[i, j];
				}
			}
			//double[,]  = newDistances new double[N, N];
			double newsum = count;
			//finds the best new road out of possible new roads
			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N; j++)
				{
					double tempSum = calcualateNewDistance(graph, i, j);
					if (tempSum < newsum)
					{
						newsum = tempSum;
					}
				}
			}
				return newsum;
		}
	}
	/// <summary>
	/// The node which holds intersection coordinates
	/// </summary>
	public class Node
	{
		//X coordinate
		public int x { get; private set; }
		//Y coordinate
		public int y { get; private set; }

		//public int time { get; private set; }
		/// <summary>
		/// The intersections 
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		public Node(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

}