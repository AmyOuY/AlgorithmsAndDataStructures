class Dijkstra
    {

        public string ShortestPath(Dictionary<string, Dictionary<string, int>> graph, string source, string destination)
        {
            HashSet<string> allNodes = new HashSet<string>();
            foreach (string u in graph.Keys)
            {
                allNodes.Add(u);
                foreach (string v in graph[u].Keys)
                {
                    allNodes.Add(v);
                }
            }

            
            Dictionary<string, int> distances = new Dictionary<string, int>();
            foreach (string u in distances.Keys)
            {
                distances.Add(u, int.MaxValue);
            }
            distances[source] = 0;

            Dictionary<string, string> path = new Dictionary<string, string>();
            HashSet<string> X = new HashSet<string>();
            X.Add(source);

            while (!X.Equals(allNodes))
            {
                string minHead = null;
                string minTail = null;
                int minDist = int.MaxValue;
                int nodeCount = 0;

                foreach (string u in X)
                {
                    if (graph.ContainsKey(u))
                    {
                        foreach (string v in graph[u].Keys)
                        {
                            if (!X.Contains(v))
                            {
                                nodeCount++;
                                int dist = distances[u] + graph[u][v];
                                if (dist < minDist)
                                {
                                    minTail = u;
                                    minHead = v;
                                    minDist = dist;
                                }
                            }                           
                        }
                    }
                }

                if (nodeCount == 0) break;

                X.Add(minHead);
                distances.Add(minHead, minDist);
                path.Add(minHead, minTail);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(destination);
            string n = destination;
            while (path.ContainsKey(n))
            {
                string v = path[n];
                int len = graph[v][n];
                sb.Insert(0, " ->(" + len + ") ");
                sb.Insert(0, v);
                n = v;
            }

            return sb.ToString();
        }
        
        
        public static Dictionary<string, int> addEdge(Dictionary<string, int> graph, string s, int i)
        {
            graph.Add(s, i);
            return graph;
        }


        static void Main(string[] args)
        {
            //Dictionary<string, Dictionary<string, int>> undirectedMap = new Dictionary<string, Dictionary<string, int>>();

            Dictionary<string, Dictionary<string, int>> directedMap = new Dictionary<string, Dictionary<string, int>>();
            directedMap.Add("A", addEdge(addEdge(addEdge(new Dictionary<string, int>(), "B", 2), "C", 5), "D", 1));
            directedMap.Add("B", addEdge(new Dictionary<string, int>(), "C", 1));
            directedMap.Add("C", addEdge(new Dictionary<string, int>(), "F", 5));
            directedMap.Add("D", addEdge(addEdge(addEdge(new Dictionary<string, int>(), "B", 2), "C", 3), "E", 1));
            directedMap.Add("E", addEdge(addEdge(new Dictionary<string, int>(), "C", 1), "F", 2));

            directedMap.Add("G", addEdge(new Dictionary<string, int>(), "G", 1));

            Dijkstra dijkstra = new Dijkstra();
            
            Console.WriteLine(dijkstra.ShortestPath(directedMap, "A", "F"));
            
            Console.ReadLine();
        }
    }
