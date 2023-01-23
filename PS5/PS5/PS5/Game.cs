using System;
using System.Collections.Generic;
//Devin White
//U0775759
//PS5
// Gold Game/WFS
namespace PS5
{
	/// <summary>
	/// Solves the Labyrinth Gold game using an implementation of whatever first Search
	/// </summary>
	class Game
	{
		int H;
		int W;
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="height">Width of labyrinth</param>
		/// <param name="width">Height of labyrinth</param>
		public Game(int height, int width)
		{
			H = height;
			W = width;
		}
		/// <summary>
		/// Main Method
		/// </summary>
		/// <param name="args">Command Line arguments</param>
		static void Main(string[] args)
		{
			//width, height, startingPosX, starting	PosY
			int W, H, Px = 0, Py = 0;
			string[] startTokens = Console.ReadLine().Split(' ');
			//get Width
			int.TryParse(startTokens[0], out int width);
			W = width;
			//get Height
			int.TryParse(startTokens[1], out int height);
			H = height;
			//Construct labyrinth
			Node[,] Labyrinth = new Node[H, W];
			//parse lines
			for (int i = 0; i < H; i++)
			{
				string tokens = Console.ReadLine();
				for (int j = 0; j < W; j++)
				{
					//assign Node values for graph
					Labyrinth[i, j] = new Node(j, i, tokens[j]);
					//player starting location
					if (tokens[j] == 'P')
					{
						Px = j;
						Py = i;
					}
				}
			}
			Game game = new Game(H, W);
			Console.WriteLine(game.WhateverFirstSearch(Labyrinth, Px, Py));
		}

		/// <summary>
		/// Performs A graph traversal to find the maximum amount of gold without stepping into traps
		/// </summary>
		/// <param name="labyrinth">The Labyrinth,made up of nodes with position and tile values(# are walls, . open spaces, P starting position, T traps, and G Gold)</param>
		/// <param name="px">Starting position X</param>
		/// <param name="py">Starting position Y</param>
		/// <returns>The maximum amount of gold possible without falling into a trap</returns>
		private int WhateverFirstSearch(Node[,] labyrinth, int px, int py)
		{
			/*WhateverFirstSearch(s):
			put s into the bag
			while the bag is not empty
			take v from the bag
			if v is unmarked
			mark v
			for each edge vw
			put w into the bag*/
			//amount of gold
			int gold = 0;
			Queue<Node> graph = new Queue<Node>();
			bool[,] visitedTile = new bool[labyrinth.GetLength(0), labyrinth.GetLength(1)];
			visitedTile[py, px] = true;
			//put s into the bag
			//add starting point
			graph.Enqueue(labyrinth[py, px]);
			//while the bag is not empty
			while (graph.Count > 0)
			{
				//get current position
				//take v from the bag
				Node currentNode = graph.Dequeue();
				int PosX = currentNode.x;
				int PosY = currentNode.y;
				//if current position is gold increment count
				if (currentNode.tile == 'G')
				{
					gold++;
				}
				//check for traps in Y
				if (labyrinth[currentNode.y + 1, currentNode.x].tile == 'T' || labyrinth[currentNode.y - 1, currentNode.x].tile == 'T')
				{

					continue;
				}
				//check for traps in X
				if (labyrinth[currentNode.y, currentNode.x + 1].tile == 'T' || labyrinth[currentNode.y, currentNode.x - 1].tile == 'T')
				{
					continue;
				}
				//if v is unmarked
				if (!visitedTile[PosY, PosX])
				{
					//mark v
					visitedTile[PosY, PosX] = true;
				}
				//for each edge vw
				//x values
				for (int i = -1; i <= 1; i += 2)
				{
					int newPos = PosX + i;
					//make sure in the bounds of the map
					if (PosX + i >= 0 && PosX + i < W)
					{
						//y x+1
						//mark edges
						//check if not visited and not a wall
						if (!visitedTile[PosY, newPos] && labyrinth[PosY, newPos].tile != '#')
						{
							visitedTile[PosY, newPos] = true;
							//add to queue
							//put w into the bag 
							graph.Enqueue(labyrinth[PosY, newPos]);
						}
					}
				}
				//for each edge vw
				//y values
				for (int i = -1; i <= 1; i += 2)
				{
					int newPos = PosY + i;
					//make sure in the bounds of the map
					if (PosY + i >= 0 && PosY + i < H)
					{
						//check if not visited and not a wall
						if (!visitedTile[newPos, PosX] && labyrinth[newPos, PosX].tile != '#')
						{
							//y+1 x
							//mark edges
							visitedTile[newPos, PosX] = true;
							//add to queue
							//put w into the bag
							graph.Enqueue(labyrinth[newPos, PosX]);
						}
					}
				}
			}
			return gold;
		}
	}

	/// <summary>
	/// a node/vertex for the graph to allow traversal
	/// </summary>
	class Node
	{
		//Xpos
		public int x;
		//Ypos
		public int y;
		//value within tile(#,G,T,.)
		public char tile { get; private set; }
		/// <summary>
		/// Constructs a graph node
		/// </summary>
		/// <param name="x">X position</param>
		/// <param name="y">Y position</param>
		/// <param name="tile">The value on the tile((#,G,T,.)</param>
		public Node(int x, int y, char tile)
		{
			//x position
			this.x = x;
			//y position
			this.y = y;
			//character in tile
			this.tile = tile;
		}
	}
}
