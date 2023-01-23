namespace PS8
{
    //Devin White
    //u0775759
    internal class Shrink
    {
        Dictionary<int, List<Node>> nodeDict;
        double[] factorization;
        int N;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="n">number of corridors</param>
        public Shrink(int n)
        {
            nodeDict = new Dictionary<int, List<Node>>(n);
            factorization = new double[n];
            N = n - 1;
        }
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">Command line argument </param>
        static void Main(string[] args)
        {
            //intersections and corridors
            int n, m;
            string[] Tokens = Console.ReadLine().Split(' ');
            //get N
            int.TryParse(Tokens[0], out int intersections);
            n = intersections;
            //get corridors(m)
            int.TryParse(Tokens[1], out int corridors);
            m = corridors;

            Shrink shrink = new Shrink(n);
            for (int i = 0; i < n; i++)
            {
                shrink.nodeDict.Add(i, new List<Node>());
                //assume distance of infinity for every path initially
                shrink.factorization[i] = -int.MaxValue;
            }
            //add paths/corridors into dictionary
            for (int i = 0; i < m; i++)
            {
                string[] LineTokens = Console.ReadLine().Split(' ');
                int.TryParse(LineTokens[0], out int key);
                int.TryParse(LineTokens[1], out int val);
                double.TryParse(LineTokens[2], out double weight);
                shrink.nodeDict[key].Add(new Node(val, weight));
                shrink.nodeDict[val].Add(new Node(key, weight));
            }
            //start has starting weight
            shrink.factorization[0] = 1.0;

            Console.WriteLine(shrink.Dijikstra(shrink.nodeDict, shrink.factorization, shrink.N));
        }


        /* Dijikstra pseudoCode:
         * D(s):
         *Initialize SSSP(s)
         *Insert(s, 0)
         *while the priority queue is not empty
         *   u = extract Min
         *   for all edges u->v
         *       if u->v is tense
         *           Relax(u->v)
         *           if v is in the priority queue
         *               DecreaseKey(v, dist(v))
         *           else
         *               Insert(v, dist(v))
         */
        /// <summary>
        /// A variant of Dijikstra's algorithm which finds the maximum weight path by inserting the inverse wight
        /// into a priority queue(such that the highest weight paths have the highest priority.
        /// </summary>
        /// <param name="routes">Dictionary of routes and size if taken</param>
        /// <param name="factorization">the weight so far</param>
        /// <param name="n">The total number of corridors</param>
        /// <returns>The maximum weight path</returns>
        public double Dijikstra(Dictionary<int, List<Node>> routes, double[] factorization, int n)
        {
            PriorityQueue<int, double> queue = new PriorityQueue<int, double>();
            queue.Enqueue(0, 1.0);
            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                foreach (Node node in routes[u])
                {
                    double factor = node.factorWeight * factorization[u];
                    if (factorization[node.corridorNum] < factor)
                    {
                        factorization[node.corridorNum] = factor;
                        queue.Enqueue(node.corridorNum, -factor);
                    }
                }
            }
            return factorization[n];
        }
    }
    /// <summary>
    /// Node which contains the weight(factorization) and corridor
    /// </summary>
    public class Node
    {
        //the distance corridor distance. e.g 0 to 1 = 1
        public int corridorNum { get; private set; }
        //the factorization size after taking route
        public double factorWeight { get; private set; }
        /// <summary>
        /// Node with factorization and numbers between corridor
        /// </summary>
        /// <param name="n"> corridor distance</param>
        /// <param name="w">factorization after taking route</param>
        public Node(int n, double w)
        {
            factorWeight = w;
            corridorNum = n;
        }
    }

}