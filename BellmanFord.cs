class BellmanFord
    {

        private Dictionary<string, Dictionary<string, int>> ReverseGraph(Dictionary<string, Dictionary<string, int>> graph)
        {
            Dictionary<string, Dictionary<string, int>> reverse = new Dictionary<string, Dictionary<string, int>>();
            foreach (string u in graph.Keys)
            {                
                foreach (string v in graph[u].Keys)
                {
                    if (!reverse.ContainsKey(v))
                    {
                        reverse.Add(v, new Dictionary<string, int>());
                    }
                    reverse[v][u] = graph[u][v];
                }
            }
            return reverse;
        }


      
        public string ShortestPath(Dictionary<string, Dictionary<string, int>> graph, string source, string destination)
        {
            Dictionary<string, string> path = new Dictionary<string, string>();
            Dictionary<int, Dictionary<string, int>> dp = new Dictionary<int, Dictionary<string, int>>();
            
            HashSet<string> allNodes = new HashSet<string>();

            foreach (string u in graph.Keys)
            {
                allNodes.Add(u);
                foreach (string v in graph[u].Keys)
                {
                    allNodes.Add(v);
                }
            }
           
            for (int i = 0; i < allNodes.Count; i++)
            {
                dp[i] = new Dictionary<string, int>();

                foreach (string u in allNodes)
                {
                    if (u == source)
                    {
                        dp[i][u] = 0;
                    }
                    else
                    {
                        dp[i][u] = int.MaxValue;
                    }
                    
                }
            }

            Dictionary<string, Dictionary<string, int>> reversedGraph = ReverseGraph(graph);
 
            for (int i = 1; i < allNodes.Count; i++)
            {
                foreach (string u in allNodes)
                {
                    if (reversedGraph.ContainsKey(u))
                    {
                        // check all in-degree of u for min distance
                        foreach (string v in reversedGraph[u].Keys)
                        {
                            if (dp[i - 1][u] != int.MaxValue && dp[i - 1][v] != int.MaxValue)
                            {
                                dp[i][u] = Math.Min(dp[i-1][u], Math.Min(dp[i][u], dp[i - 1][v] + reversedGraph[u][v]));                              
 
                                if (dp[i][u] == dp[i - 1][v] + reversedGraph[u][v])
                                {
                                    path[u] = v;                                   
                                }
                            }
                            else if (dp[i - 1][u] != int.MaxValue)
                            {
                                dp[i][u] = Math.Min(dp[i][u], dp[i - 1][u]);
                            }
                            else if (dp[i - 1][v] != int.MaxValue)
                            {
                                dp[i][u] = Math.Min(dp[i][u], dp[i - 1][v] + reversedGraph[u][v]);
                                if (dp[i][u] == dp[i - 1][v] + reversedGraph[u][v])
                                {
                                    path[u] = v;
                                }
                            }                                            
                        }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(destination);
            string n = destination;
            while (path.ContainsKey(n))
            {
                string w = path[n];
                sb.Insert(0, "->(" + graph[w][n] + ")");
                sb.Insert(0, w);
                n = w;
            }
            return sb.ToString();
        }
        
        
        static Dictionary<string, int> addEdge(Dictionary<string, int> map, string s, int i)
        {
            map.Add(s, i);
            return map;
        }


        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, int>> map = new Dictionary<string, Dictionary<string, int>>();
            map.Add("A", addEdge(addEdge(addEdge(new Dictionary<string, int>(), "B", 2), "C", 5), "D", 1));
            map.Add("B", addEdge(new Dictionary<string, int>(), "C", 1));
            map.Add("C", addEdge(new Dictionary<string, int>(), "F", 5));
            map.Add("D", addEdge(addEdge(addEdge(new Dictionary<string, int>(), "B", 2), "C", 3), "E", 1));
            map.Add("E", addEdge(addEdge(new Dictionary<string, int>(), "C", 1), "F", 2));


            BellmanFord bellmanFord = new BellmanFord();
            Console.WriteLine(bellmanFord.ShortestPath(map, "A", "A"));
            Console.ReadLine();
        }
    }
