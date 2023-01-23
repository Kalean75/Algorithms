using System;

//Devin White
//u0775759
//Ps3
namespace PS3
{
	class Ps3
	{
		static int N2;
		static int[,] rooms2;
		static int[,,] roomArray;

		//constructor
		public Ps3(int n, int[,] rooms)
		{
			rooms2 = rooms;
			N2 = n;
		}
		static void Main(string[] args)
		{
			int k;
			int N;
			string[] startTokens = Console.ReadLine().Split(' ');
			//get ids and put them in array
			int.TryParse(startTokens[0], out int roomNum);
			N = roomNum;
			int.TryParse(startTokens[1], out int closed);
			k = closed;
			//holds rooms
			int[,] rooms = new int[N, 2];
			//populate skills array
			for (int i = 0; i < N; i++)
			{
				String[] tokens = Console.ReadLine().Split(' ');
				//L ROOM
				if (int.TryParse(tokens[0], out int col0))
				{
					rooms[i, 0] = col0;
				}
				//R ROOM
				if (int.TryParse(tokens[1], out int col1))
				{
					rooms[i, 1] = col1;
				}
			}
			//roomArray = new int[N2, rooms2.Length, 3];
			//Are the 0,0 for anything?
			Console.ReadLine();
			Ps3 artist = new Ps3(N, rooms);
			//Console.WriteLine(NarrowGalleryRecursive(0, -1, k));
			Console.WriteLine(artist.NarrowGallery(N2, rooms2, k));

		}

		//recursive implementation of narrow Gallery
		static int NarrowGalleryRecursive(int r, int uncloseable, int k)
		{
			int num = 0;
			//base case
			if (k <= 0)
			{
				return 0;
			}
			if (r < 0)
			{
				return 0;
			}
			if (r > N2)
			{
				return 0;
			}

			//if k==n-r
			else if (k < N2 - r)
			{
				if (uncloseable == -1)
				{
					int maxoftwo = Math.Max(rooms2[r, 0] + NarrowGalleryRecursive(r + 1, 0, k - 1), rooms2[r, 1] + NarrowGalleryRecursive(r + 1, 1, k - 1));
					return Math.Max((rooms2[r, 0] + rooms2[r, 1] + NarrowGalleryRecursive(r + 1, -1, k)), maxoftwo);
				}
				if (uncloseable == 0)
				{
					return Math.Max(rooms2[r, 0] + NarrowGalleryRecursive(r + 1, 0, k - 1), rooms2[r, 0] + rooms2[r, 1] + NarrowGalleryRecursive(r + 1, -1, k));
				}
				if (uncloseable == 1)
				{
					return Math.Max(rooms2[r, 1] + NarrowGalleryRecursive(r + 1, 1, k - 1), rooms2[r, 0] + rooms2[r, 1] + NarrowGalleryRecursive(r + 1, -1, k));
				}
				return num;

			}
			else if (k == N2 - r)
			{
				if (uncloseable == -1)
				{
					return Math.Max(rooms2[r, 0] + NarrowGalleryRecursive(r + 1, 0, k - 1), rooms2[r, 1] + NarrowGalleryRecursive(r + 1, 1, k - 1));
				}
				if (uncloseable == 0)
				{
					return rooms2[r, 0] + NarrowGalleryRecursive(r + 1, 0, k - 1);
				}
				if (uncloseable == 1)
				{
					return rooms2[r, 1] + NarrowGalleryRecursive(r + 1, 1, k - 1);
				}
			}
			return num;
		}

		//Dynamic version of the narrow gallery
		public int NarrowGallery(int r, int[,] rooms, int k)
		{

			//r+1 to account for extra space. 3 to account for blocked L, blocked R and no block
			int[, , ] roomArray = new int[r, r + 1, 3];

			//initialize entire value table to negative infinity to account for 0 value rooms
			for (int i = 0; i < r; i++)
			{
				for (int j = 0; j <= k; j++)
				{
					for (int l = 0; l < 3; l++)
					{
						roomArray[i, j, l] = int.MinValue;
					}
				}

			}
			//fill bottom of gallery
			roomArray[0, 0, 2] = rooms[0, 1] + rooms[0, 0];
			//fill both possibilities of Row 1
			//row 1 right
			roomArray[0, 1, 1] = rooms[0, 1];
			//row 1 left
			roomArray[0, 1, 0] = rooms[0, 0];

			for (int i = 1; i < r; i++)
			{
				//can only continue if no more rooms to close..
				for (int j = 1; j <= i+1; j++)
				{
					if(j > k)
					{
						break;
					}
					else
					{
						//both open. Find the largest of previous options
						roomArray[i, j, 2] = rooms[i, 0] + rooms[i, 1] + Math.Max(roomArray[i - 1, j, 2], Math.Max(roomArray[i - 1, j, 1], roomArray[i - 1, j, 0]));
						//compare no closed with right closed previous options
						roomArray[i, j, 1] = Math.Max(roomArray[i - 1, j - 1, 2], roomArray[i - 1, j - 1, 1]) + rooms[i, 1];
						//compare no closed with left closed previous options
						roomArray[i, j, 0] = Math.Max(roomArray[i - 1, j - 1, 2], roomArray[i - 1, j - 1, 0]) + rooms[i, 0];
					}
				}
			}
			return Math.Max(roomArray[r - 1, k, 2], Math.Max(roomArray[r - 1, k, 1], roomArray[r - 1, k, 0]));
		}
	}
}
