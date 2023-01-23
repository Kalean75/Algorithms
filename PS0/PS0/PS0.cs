using System;
using System.Collections;
using System.Collections.Generic;

namespace PS0
{
	/*Input
The first line contains integers 𝑛 and 𝑘 separated by a space, where 1≤𝑛≤10000 and 1≤𝑘≤1000. 
	The 𝑛 words, one to a line, follow. Each word contains exactly 𝑘 lower case letters. (The words are not necessarily in any dictionary you’ve ever seen.)

Output
Produce a single line of output that contains the number of words on the list that are not anagrams of any other words on the list. This number, of course, 
	should be between 0 and 𝑛 inclusive. Your algorithm should finish in less than 4 seconds.
	 */
	class PS0
	{
		static void Main(string[] args)
		{
			string[] tokens = Console.ReadLine().Split(' ');
			int.TryParse(tokens[0], out int n);
			int.TryParse(tokens[1], out int k);
			string[] wordarray = new string[n];
			int count = 0;
			Dictionary<string, List<string>> table = new Dictionary<string, List<string>>();
			for (int i = 0; i < n; i++)
			{
				string word = Console.ReadLine();
				string sortedWord = sortString(word);
				if (!table.ContainsKey(sortedWord))
				{
					table.Add(sortedWord, new List<string>());
				}
				table[sortedWord].Add(word);
			}
			foreach(KeyValuePair<string,List<string>> entries in table)
			{
				if(entries.Value.Count <= 1)
				{
					count++;
				}
			}
			Console.WriteLine(count);

		}
		private static string sortString(string word)
		{
			char[] stringCharacters = word.ToCharArray();
			Array.Sort(stringCharacters);
			return new string(stringCharacters);
		}
	}
}
