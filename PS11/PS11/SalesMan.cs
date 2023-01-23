
using System;
using System.ComponentModel;

namespace ps11
{
	internal class SalesMan
	{
		public int N;
		int Best = int.MaxValue;
		int minWeight = int.MaxValue;
		int[,] graph;
		HashSet<int> visited;
		public SalesMan(int n)
		{
			N = n;
			graph = new int[N, N];
			visited = new HashSet<int>();

		}
		static void Main(string[] args)
		{
			//parse n
			int.TryParse(Console.ReadLine(), out int n);
			SalesMan salesman = new SalesMan(n);
			//parse edges and weights
			for (int i = 0; i < n; i++)
			{
				string[] tokens = Console.ReadLine().Split();
				for (int j = 0; j < tokens.Length; j++)
				{
					int.TryParse(tokens[j], out int edge);
					salesman.graph[j, i] = edge;
				}
			}
			salesman.travel(0, 0, 0, salesman);
			Console.WriteLine(salesman.Best);

		}
		public void travel(int currentVertex, int currentTotal, int currentLength, SalesMan salesman)
		{
			if (currentTotal + (salesman.N - currentLength) * salesman.minWeight >= salesman.Best)
			{
				return;
			}

			if (currentLength == salesman.N - 1)
			{
				salesman.Best = Math.Min(salesman.Best, currentTotal + salesman.graph[currentVertex, 0]);
			}

			else
			{
				for (int vertex = 0; vertex < salesman.N; vertex++)
				{
					if (!salesman.visited.Contains(vertex))
					{
						salesman.visited.Add(vertex);
						travel(vertex, currentTotal + salesman.graph[currentVertex, vertex], currentLength + 1, salesman);
						salesman.visited.Remove(vertex);
					}
				}
			}

		}
	}
	internal class Node
	{
		public int vertex1 { get; private set; }
		public int vertex2 { get; private set; }
		public int weight { get; private set; }
		public bool isVisited = false;
		public Node(int v1, int v2, int w)
		{
			vertex1 = v1;
			vertex2 = v2;
			weight = w;
		}
	}
}