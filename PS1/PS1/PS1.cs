using System;
using System.Collections.Generic;

namespace PS1
{
	public class PS1
	{
		//The first line of input contains one integer, 𝑁 (1≤𝑁≤1,000,000), the total number of participants.
		//The second line contains exactly N distinct IDs(1≤ID≤1,000,000), not necessarily in order, one for each of the N participants.
		//The third line contains exactly N skills(1≤skill≤1,000,000), not necessarily distinct, one for each of the N participants.
		//static int teamTargetSkill { get; set; } = 0;
		//static int totalSkill = 0;
		static int recursionlevel = 0;
		static void Main(string[] args)
		{
			int.TryParse(Console.ReadLine(), out int participants);
			//get ids and put them in array
			string[] idTokens = Console.ReadLine().Split(' ');
			//get skills and put them in array
			string[] skillTokens = Console.ReadLine().Split(' ');
			int[] skills = new int[participants];
			int totalSkills = 0;
			Dictionary<int, int> weightMapping = new Dictionary<int, int>();
			//populate skills array
			for (int i = 0; i < skillTokens.Length; i++)
			{
				if (int.TryParse(skillTokens[i], out int skill))
				{
					skills[i] = skill;
					totalSkills += skill;
				}
			}
			int[] ids = new int[participants];
			//populate ids array
			for (int i = 0; i < idTokens.Length; i++)
			{
				if (int.TryParse(idTokens[i], out int id))
				{
					ids[i] = id;
					weightMapping.Add(ids[i], skills[i]);
				}
			}
			Console.WriteLine(Momselect(ids, skills, (ids.Length - 1) / 2));
		}


		public static int Momselect(int[] ids, int[] skills, int k)
		{
			int n = ids.Length - 1;
			//Base case
			if(n  <= 24)
			{
				Array.Sort(ids, skills);
				return quickSelect(ids, skills, k);
			}
			/*
			if(n <= 5)
			{
				Array.Sort(ids, skills);
				return ids[(ids.Length-1)/2];
			}
			*/
			else
			{			
				//find median of five
				int medianOfFive;
				int[] MedianOfFiveId, MedianOfFiveSkill;
				FindMedianofFive(ids, skills, out medianOfFive, out MedianOfFiveId, out MedianOfFiveSkill);
				int mom = Momselect(MedianOfFiveId, MedianOfFiveSkill, medianOfFive/2);
				int new_pivot = Array.IndexOf(ids, mom);
				//int new_pivot = ids.Length/2;
				int r = Partition(ids, skills, new_pivot);

				List<int> lowerId = new List<int>();
				List<int> lowerSkills = new List<int>();
				List<int> HigherId = new List<int>();
				List<int> HigherSkills = new List<int>();
				int higherWeights = 0;
				int lowerWeights = 0;
				int pivotWeight = skills[k];
				int totalWeights = 0;
				int j = 0;

				for (int i = 0; i < r ;i++)
				{
					lowerId.Add(ids[i]);
					lowerSkills.Add(skills[i]);
					lowerWeights+=skills[i];
				}
				for(int i = r + 1; i <= n; i++)
				{
					HigherId.Add(ids[i]);
					HigherSkills.Add(skills[i]);
					higherWeights+=skills[i];
					j++;
				}
				totalWeights = lowerWeights+pivotWeight+higherWeights;
				//PrintDebugInfo(ids, k,mom, r, lowerWeights, higherWeights, totalWeights);
				if (k==r)
				{
					return mom;
				}
				else if (k < r)
				{
					//Console.WriteLine(k);
					return Momselect(lowerId.ToArray(), lowerSkills.ToArray(), k);
				}
				else
				{
					//Console.WriteLine(k-r);'
					return Momselect(HigherId.ToArray(), HigherSkills.ToArray(), k - r- 1);
				}
				/*if (totalWeights/2 >= lowerWeights && totalWeights/2 >= higherWeights)
				{
					return mom;
				}
				else if (totalWeights/2 < lowerWeights)
				{
					//Console.WriteLine(k);
					return Momselect(lowerId.ToArray(), lowerSkills.ToArray(), k);
				}
				else
				{
					//Console.WriteLine(k-r);'
					return Momselect(HigherId.ToArray(), HigherSkills.ToArray(), k - r - 1);
				}*/
			}
		}

		public static int quickSelect(int[] ids, int[] skills, int k)
		{
			int n = ids.Length - 1;
			if(n == 0)
			{
				return ids[0];
			}
			else
			{
				int pivot = k/2;
				int r = Partition(ids, skills, pivot);

				List<int> lowerId = new List<int>();
				List<int> lowerSkills = new List<int>();
				List<int> HigherId = new List<int>();
				List<int> HigherSkills = new List<int>();
				int higherWeights = 0;
				int lowerWeights = 0;
				int pivotWeight = skills[k];
				int totalWeights = 0; int j = 0;
				for (int i = 0; i < r; i++)
				{
					lowerId.Add(ids[i]);
					lowerSkills.Add(skills[i]);
					lowerWeights+=skills[i];
				}
				for (int i = r + 1; i <= n; i++)
				{
					HigherId.Add(ids[i]);
					HigherSkills.Add(skills[i]);
					higherWeights+=skills[i];
					j++;
				}
				/*if (totalWeights/2 >= lowerWeights && totalWeights/2 >= higherWeights)
				{
					return ids[k];
				}
				else if (totalWeights/2 < lowerWeights)
				{
					//Console.WriteLine(k);
					return Momselect(lowerId.ToArray(), lowerSkills.ToArray(), k);
				}
				else
				{
					//Console.WriteLine(k-r);'
					return Momselect(HigherId.ToArray(), HigherSkills.ToArray(), k - r - 1);
				}*/
				if (k==r)
				{
					return ids[k];
				}
				if (k < r)
				{
					ids = lowerId.ToArray();
					skills = lowerSkills.ToArray();
					//Console.WriteLine(k);
					return quickSelect(ids, skills, k);
				}
				else
				{
					ids = HigherId.ToArray();
					skills = HigherSkills.ToArray();
					//Console.WriteLine(k-r);'
					return quickSelect(ids, skills, k - r - 1);
				}
			}
		}
		private static void PrintDebugInfo(int[] ids, int k, int r, int mom, int skillsLower, int skillsHigher, int totalskills)
		{
			Console.WriteLine("Recursion Level " + recursionlevel);
			recursionlevel++;
			//Console.WriteLine("Total skill level " + totalskills);
			//Console.WriteLine("team skill level " + totalskills/2);
			//Console.WriteLine("Higher array skill " + skillsHigher);
			//Console.WriteLine("Lower array Skils " + skillsLower);
			Console.WriteLine("pivot(k) " + k);
			Console.WriteLine("newpivotindex(r) " + r);
			//Console.WriteLine("newpivotidid[r] " + ids[r] + "\n");
			Console.WriteLine("newpivotindex(mom) " + mom + "\n");
		}

		private static void FindMedianofFive(int[] ids, int[] skills, out int medianOfFive, out int[] MedianOfFiveId, out int[] MedianOfFiveSkill)
		{
			medianOfFive = ids.Length/5;
			MedianOfFiveId =new int[medianOfFive];
			MedianOfFiveSkill = new int[medianOfFive];
			for (int i = 1; i <= medianOfFive; i++)
			{
				if (5*i - 1 < ids.Length)
				{
					MedianOfFiveId[i-1] = MedianOfFive(ids[5 * i - 5], ids[5 * i - 4], ids[5 * i - 3], ids[5 * i - 2], ids[5 * i - 1]);
					MedianOfFiveSkill[i-1] = MedianOfFive(skills[5 * i - 5], skills[5 * i - 4], skills[5 * i - 3], skills[5 * i - 2], skills[5 * i - 1]);
				}
				else if (5*i - 2 < ids.Length)
				{
					MedianOfFiveId[i-1] = MedianOfFive(ids[5 * i - 5], ids[5 * i - 4], ids[5 * i - 3], ids[ 5 * i - 2], int.MaxValue);
					MedianOfFiveSkill[i-1] = MedianOfFive(skills[5 * i - 5], skills[5 * i - 4], skills[5 * i - 3], skills[5 * i - 2], int.MaxValue);
				}
				else if (5*i - 3 < ids.Length)
				{
					MedianOfFiveId[i-1] = MedianOfFive(ids[5 * i - 5], ids[5 * i - 4], ids[5 * i - 3], int.MaxValue, int.MaxValue);
					MedianOfFiveSkill[i-1] = MedianOfFive(skills[5 * i - 5], skills[5 * i - 4], skills[5 * i - 3], int.MaxValue, int.MaxValue);
				}
				else if (5*i - 4 < ids.Length)
				{
					MedianOfFiveId[i-1] = MedianOfFive(ids[5 * i - 5], ids[5 * i - 4], int.MaxValue, int.MaxValue, int.MaxValue);
					MedianOfFiveSkill[i-1] = MedianOfFive(skills[5 * i - 5], skills[5 * i - 4], int.MaxValue, int.MaxValue, int.MaxValue);
				}
				else
				{
					MedianOfFiveId[i-1] = MedianOfFive(ids[5 * i - 5], int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);
					MedianOfFiveSkill[i-1] = MedianOfFive(skills[5 * i - 5], int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);
				}
			}
		}
		private static int MedianOfFive(int a, int b, int c, int d, int e)
		{
			int[] array = { a, b, c, d, e };
			Array.Sort(array);
			return array[(array.Length)/2];
		}

		//remove last 2 int if needed
		static int Partition(int[] ids, int[] skills, int pivot)
		{

			int n = ids.Length - 1;
			if (ids[n] != ids[pivot])
			{
				SwapArrayElements(ids, pivot, n);
				SwapArrayElements(skills, pivot, n);
			}
			int lessthan = 0;
			for(int i = 0;  i < n; i++)
			{
				if(ids[i] < ids[n])
				{
					SwapArrayElements(ids, lessthan, i);
					SwapArrayElements(skills, lessthan, i);
					//Console.WriteLine("less than " + lessthan);
					lessthan++;
				}
			}
			SwapArrayElements(ids, n, lessthan);
			SwapArrayElements(skills, n, lessthan);
			//Console.WriteLine("less than " + (lessthan) + " pivot " + pivot);
			return lessthan;
		}

		static void SwapArrayElements(int[] array, int value1, int value2)
		{
			int temp = array[value1];
			array[value1] = array[value2];
			array[value2] = temp;
		}
	}
}
