using System;
using System.Collections.Generic;
//Devin White
//U0775759
//PS7
namespace PS7
{
	/// <summary>
	/// Finds the minimum length of cable needed to connect all treehouses(minimum spanning tree)
	/// </summary>
	class TreeHouses
	{
		//PQ to get our smallest edges
		PriorityQueue<Edge, double> pathQueue = new PriorityQueue<Edge, double>();
		public TreeHouse[] houses;
		public int[] Path;
		/// <summary>
		/// Constructor. Initializes arrays to length n. X and y to nxn to account for coordinate sizes
		/// </summary>
		/// <param name="n"></param>
		public TreeHouses(int n)
		{
			//the parent id/path containing vertices. All Treehouses/vertices in a path share the same path Id
			this.Path = new int[n];
			//array to keep track of treehouses(vertices)
			houses = new TreeHouse[n];
		}
		static void Main(string[] args)
		{
			//number of houses, houses on ground, pre-existing cables
			int n, e, p;
			string[] Tokens = Console.ReadLine().Split(' ');
			//get N
			int.TryParse(Tokens[0], out int N);
			n = N;
			//get E(number of houses on ground)
			int.TryParse(Tokens[1], out int E);
			e = E;
			//get p(number of houses with cables already. E.G forced edges to be ignored in final solution)
			int.TryParse(Tokens[2], out int P);
			p = P;

			TreeHouses houses = new TreeHouses(n);
			//connect the ground floor treehouses to each other
			for (int i = 0; i < e; i++)
			{
				string[] groundTokens = Console.ReadLine().Split(' ');
				double.TryParse(groundTokens[0], out double xPos);
				double.TryParse(groundTokens[1], out double yPos);
				houses.houses[i] = new TreeHouse(xPos,yPos);
				//id is index
				houses.Path[i] = 0;
			}
			//parse n lines getting all vertex coordinates
			for (int i = e; i < n; i++)
			{
				string[] treeHouseTokens = Console.ReadLine().Split(' ');
				double.TryParse(treeHouseTokens[0], out double xPos);
				double.TryParse(treeHouseTokens[1], out double yPos);
				houses.houses[i] = new TreeHouse(xPos, yPos);
				//id is index
				houses.Path[i] = i;
			}
			//existing connections between treehouses
			for (int j = 0; j < p; j++)
			{
				string[] existingCableTokens = Console.ReadLine().Split(' ');
				int.TryParse(existingCableTokens[0], out int xPos);
				int.TryParse(existingCableTokens[1], out int yPos);
				//subtract 1 to get actual 0 based index
				houses.join(xPos - 1, yPos - 1);
			}
			//print solution
			Console.WriteLine(houses.BuildCables(n, houses));
		}

		/// <summary>
		/// Finds the minimum cables needed to connect all treehouses(finds the minimum spanning tree)
		/// </summary>
		/// <param name="n">the number of treehouses</param>
		/// <param name="houses">the current Houses object</param>
		/// <returns></returns>
		private double BuildCables(int n, TreeHouses houses)
		{
			//add edges for all vertices not provided in P(not already connected)
			for (int i = 0; i < n; i++)
			{
				for (int j = i + 1; j < n; j++)
				{
					double k = houses.findCableDist(houses.houses[i].x, houses.houses[j].x, houses.houses[i].y, houses.houses[j].y);
					houses.pathQueue.Enqueue(new Edge(i, j, k), k);
				}
			}
			double cables = 0;
			for (int i = 0; i < houses.pathQueue.Count; i++)
			{
				Edge no = houses.pathQueue.Dequeue();
				int u = no.u;
				int v = no.v;
				if (houses.find(u) != houses.find(v))
				{
					houses.join(u, v);
					cables += no.weight;
				}
			}
			return cables;
		}

		/// <summary>
		/// finds the distance(weight) between treehouses
		/// </summary>
		/// <param name="x1">X coordinate 1</param>
		/// <param name="x2">x coordinate 2</param>
		/// <param name="y1">y coordinate 1</param>
		/// <param name="y2">y cordinate 2</param>
		/// <returns></returns>
		double findCableDist(double x1, double x2, double y1, double y2)
		{
			double distance = Math.Sqrt(Math.Pow((x2 - x1),2) + Math.Pow((y2 - y1),2));
			return distance;
		}
		/// <summary>
		/// connects two sets of vertices to each other
		/// </summary>
		/// <param name="u"></param>
		/// <param name="v"></param>
		void join(int u, int v)
		{
			Path[find(u)] = find(v);
		}
		/// <summary>
		/// finds the set containing the item
		/// </summary>
		/// <param name="u"></param>
		/// <returns>the set containing the item</returns>
		int find(int u)
		{
			if(Path[u]==u)
			{
				return u;
			}
			else
			{
				return find(Path[u]);
			}
		}
	}
	/// <summary>
	/// The edges
	/// </summary>
	public class Edge
	{
		public int u { get; private set; }
		public int v { get; private set; }
	public double weight { get; private set; }
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="U"></param>
		/// <param name="V"></param>
		/// <param name="w">weight</param>
		public Edge(int U, int V, double w)
		{
			u = U;
			v= V;
			weight = w;
			
		}

	}

	/// <summary>
	/// the houses positions represented by XY coordinates
	/// </summary>
	public class TreeHouse
	{
		//xposition
		public double x { get; private set; }
		//yposition
		public double y { get; private set; }
		/// <summary>
		/// >
		/// </summary>
		/// <param name="U"></param>
		/// <param name="V"></param>
		/// <param name="w"></param>
		public TreeHouse(double X, double Y)
		{
			x = X;
			y = Y;

		}

	}

}
