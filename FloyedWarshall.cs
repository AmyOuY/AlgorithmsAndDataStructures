public Dictionary<string, Dictionary<string, int>> AllPairsShortestPath(Dictionary<string, Dictionary<string, int>> graph)
        {
            Dictionary<int, Dictionary<string, Dictionary<string, int>>> dp = new Dictionary<int, Dictionary<string, Dictionary<string, int>>>();
            HashSet<string> allNodes = new HashSet<string>();
            foreach (string u in graph.Keys)
            {
                allNodes.Add(u);
                foreach (string v in graph[u].Keys)
                {
                    allNodes.Add(v);
                }
            }

            for (int i = 0; i <= allNodes.Count; i++)
            {
                dp[i] = new Dictionary<string, Dictionary<string, int>>();
                foreach (string u in allNodes)
                {
                    dp[i][u] = new Dictionary<string, int>();
                    foreach (string v in allNodes)
                    {
                        if (i == 0)
                        {
                            if (u == v)
                            {
                                dp[i][u][v] = 0;
                            }
                            else if (graph.ContainsKey(u) && graph[u].ContainsKey(v))
                            {
                                dp[i][u][v] = graph[u][v];
                            }
                            else
                            {
                                dp[i][u][v] = int.MaxValue;
                            }
                        }
                        else
                        {
                            dp[i][u][v] = int.MaxValue;
                        }
                    }
                }
            }

            List<string> nodes = new List<string>(allNodes);           

            for (int k = 1; k <= nodes.Count; k++)
            {
                List<string> internalNodes = nodes.GetRange(0, k);
                for (int i = 0; i < nodes.Count; i++)
                {
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        foreach (string t in internalNodes)
                        {
                            if (dp[k - 1][nodes[i]][t] != int.MaxValue && dp[k - 1][t][nodes[j]] != int.MaxValue)
                            {
                                dp[k][nodes[i]][nodes[j]] = Math.Min(dp[k - 1][nodes[i]][nodes[j]], Math.Min(dp[k][nodes[i]][nodes[j]], dp[k - 1][nodes[i]][t] + dp[k - 1][t][nodes[j]]));
                            }
                            else
                            {
                                dp[k][nodes[i]][nodes[j]] = Math.Min(dp[k][nodes[i]][nodes[j]], dp[k - 1][nodes[i]][nodes[j]]);
                            }
                        }
                    }
                }
            }
            return dp[nodes.Count];
        }


        static Dictionary<string, int> addEdge(Dictionary<string, int> map, string s, int i)
        {
            map.Add(s, i);
            return map;
        }


        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, int>> map = new Dictionary<string, Dictionary<string, int>>();
            map.Add("s", addEdge(addEdge(new Dictionary<string, int>(), "v", 2), "x", 4));
            map.Add("v", addEdge(addEdge(new Dictionary<string, int>(), "w", -2), "x", 1));
            map.Add("x", addEdge(new Dictionary<string, int>(), "t", 4));
            map.Add("w", addEdge(new Dictionary<string, int>(), "t", 2));


            FloyedWarshall fw = new FloyedWarshall();
            Dictionary<string, Dictionary<string, int>> path = fw.AllPairsShortestPath(map);

            
            foreach (string i in path.Keys)
            {
                Console.Write(i + ": ");
                foreach (KeyValuePair<string, int> p in path[i])
                {
                    Console.Write(p.Key + ":" + p.Value + "; ");
                }
                Console.WriteLine();
            }            
            Console.ReadLine();
        }
    }
