using System;
using System.Collections.Generic;
//PS6
//Devin White
//U0775759
namespace PS6
{
	//input is a DAG; multi-sources, find toposort; propagate using topological order
	class ingredients
	{
		//number of dishes
		long N;
		//number of dependencies
		long M;
		//dictionary that holds edges along with the num required to make dish
        Dictionary<long, Dictionary<long, long>> recipeDict = new Dictionary<long, Dictionary<long, long>>();
		//count of ingredients
		long[] ingcount;
		//total of each "food"
		long[] food;
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="n">Number of dishes</param>
		/// <param name="m">number of dependencies</param>
		/// <param name="recipeDict">Dictionary that hold edges along with number required for dish</param>
		/// <param name="food">the amounts of food desired</param>
        public ingredients(long n, long m, Dictionary<long, Dictionary<long, long>> recipeDict, long[] food)
		{
			N = n;
			M = m;
			this.recipeDict = recipeDict;
            ingcount = new long[N];
			this.food = food;
        }
		/// <summary>
		/// Main method
		/// </summary>
		/// <param name="args">Command line arguments</param>
		static void Main(string[] args)
		{
			//width, height, startingPosX, starting	PosY
			long N, M;
			string[] Tokens = Console.ReadLine().Split(' ');
			//get N
			long.TryParse(Tokens[0], out long food);
			N = food;
			//get M
			long.TryParse(Tokens[1], out long dependency);
			M = dependency;
			//Construct Dictionary storing dependencies
			Dictionary<long, Dictionary<long,long>> Dict = new Dictionary<long, Dictionary<long, long>>();
			long[] foods = new long[N];
			string[] Tokens2 = Console.ReadLine().Split(' ');
			for(int i = 0; i < N; i++)
			{
				long.TryParse(Tokens2[i], out long fd);
				foods[i] = fd;
				if (!Dict.ContainsKey(i))
				{
					Dict.Add(i, new Dictionary<long, long>());
				}
			}
			//parse lines
			for(int i=0; i< M; i++)
			{
				string[] dependencies = Console.ReadLine().Split(' ');
				long.TryParse(dependencies[0], out long dep1);
				long.TryParse(dependencies[1], out long dep2);
				long.TryParse(dependencies[2], out long dep3);
				if (!Dict.ContainsKey(dep2))
				{

					Dict.Add(dep2, new Dictionary<long, long>());
					Dict[dep1].Add(dep2, dep3);
				}
				else
				{
					Dict[dep1].Add(dep2, dep3);
				}
			}
            ingredients ingredients = new ingredients(N, M, Dict, foods);
			ingredients.Cook();
		}
		/// <summary>
		/// performs a deptfirstSearch/topological sort and calculated ingredients
		/// </summary>
		/// <param name="vertexNumber"> The current vertex</param>
		/// <param name="visited">the list of visited vertices</param>
        void depthFirstSearch(long vertexNumber, bool[] visited)
        {

            // Mark the node
            visited[vertexNumber] = true;
			foreach (KeyValuePair<long, long> vertex in recipeDict[vertexNumber])
			{
				//if not visited, visit attached vertices
				if (!visited[vertex.Key])
				{
					depthFirstSearch(vertex.Key, visited);
				}
			}
			//if has no dependencies, ingredients are the initial amount
			if (recipeDict[vertexNumber].Count == 0)
			{
                ingcount[vertexNumber] = food[vertexNumber];
            }
			//else multiply dependencies
			else
			{
				if(recipeDict.TryGetValue(vertexNumber, out var f))
				{
					foreach (long key in f.Keys)
					{
						ingcount[vertexNumber] += (ingcount[key] * f[key]);
					}
					//add amount of ingredient wanted to final amount
					ingcount[vertexNumber] += food[vertexNumber];
				}
                
			}
        }
		/// <summary>
		/// Initialization for depth first search/topological sort
		/// and prints the ingredients
		/// </summary>
        void Cook()
        {
			//initialize visited array
			bool[] visited = new bool[N];
			//depth first search on all the not visited vertices
            for (long i = 0; i< N; i++)
            {
                if (visited[i] == false)
				{
					depthFirstSearch(i, visited);
				}
            }
			// print the ingredients
			for (int j = 0; j < ingcount.Length; j++)
            {
                Console.Write(ingcount[j] + " ");
            }
        }
	}
}
