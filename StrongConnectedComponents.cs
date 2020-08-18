// Kosaraju's Two-Pass Algorithm for computing Strongly Connected Components in Directed Graph
    class StrongConnectedComponents
    {
        // Direct graph with nodes from 1, 2,....to n
        private readonly Dictionary<int, HashSet<int>> _graph;
        private readonly HashSet<int> allNodes;
        int n;
        int finishTime;


        public StrongConnectedComponents(Dictionary<int, HashSet<int>> graph)
        {
            _graph = graph;
            allNodes = new HashSet<int>();           
        }


        public Dictionary<int, string> ComputeSCC()
        {
            // compute largest node number n
            GetAllNodes();

            // reverse the graph
            Dictionary<int, HashSet<int>> reverseGraph = ReverseGraph(_graph);

            // first DFS: compute finish-time for the reversed graph
            Dictionary<int, int> allFinishTime = GetFinishTime(reverseGraph);

            // replace each node with its finish-time           
            Dictionary<int, HashSet<int>> graphByTime = GraphByFinishTime(allFinishTime);

            // map each finish-time to node for converting the node to original number in second DFS
            Dictionary<int, int> finishTimeToNodeMap = new Dictionary<int, int>();
            foreach (int u in allFinishTime.Keys)
            {
                finishTimeToNodeMap.Add(allFinishTime[u], u);
            }

            // second DFS: compute SCC based on finish-time graph, reverse the graph and convert the finish-time back to original node number 
            return SecondDFS(finishTimeToNodeMap, graphByTime);
        }


        // Compute the largest node number
        private void GetAllNodes()
        {
            foreach (int u in _graph.Keys)
            {
                allNodes.Add(u);
                foreach (int v in _graph[u])
                {
                    allNodes.Add(v);
                }
            }
            n = allNodes.Count;
        }


        // Reverse the edge directions of original graph
        private Dictionary<int, HashSet<int>> ReverseGraph(Dictionary<int, HashSet<int>> graph)
        {
            Dictionary<int, HashSet<int>> reverse = new Dictionary<int, HashSet<int>>();
            foreach (int u in graph.Keys)
            {
                foreach (int v in graph[u])
                {
                    if (!reverse.ContainsKey(v))
                    {
                        reverse.Add(v, new HashSet<int>());
                    }
                    reverse[v].Add(u);
                }               
            }
            return reverse;
        }


        // Run first DFS on reversed-edge graph to compute finish-time for each node
        private Dictionary<int, int> GetFinishTime(Dictionary<int, HashSet<int>> graph)
        {
            Dictionary<int, int> allFinishTime = new Dictionary<int, int>();
            HashSet<int> explored = new HashSet<int>();
            Stack<int> stack = new Stack<int>();
            
            for (int i = n; i > 0; i--)
            {
                if (!explored.Contains(i))
                {
                    DFS1(graph, i, explored, stack, allFinishTime);
                }
            }
            return allFinishTime;
        }


        // First DFS helper for finish-time
        private void DFS1(Dictionary<int, HashSet<int>> graph, int i, HashSet<int> explored, Stack<int> stack, Dictionary<int, int> allFinishTime)
        {
            explored.Add(i);
            stack.Push(i);

            while (stack.Count > 0)
            {
                int u = stack.Pop();
                if (graph.ContainsKey(u))
                {
                    foreach (int v in graph[u])
                    {
                        if (!explored.Contains(v))
                        {
                            DFS1(graph, v, explored, stack, allFinishTime);
                        }
                    }
                }
            }

            finishTime++;
            allFinishTime.Add(i, finishTime);           
        }


        // Replace each node number with its finish-time
        private Dictionary<int, HashSet<int>> GraphByFinishTime(Dictionary<int, int> allFinishTime)
        {
            Dictionary<int, HashSet<int>> newGraph = new Dictionary<int, HashSet<int>>();
            foreach (int k in _graph.Keys)
            {
                int u = allFinishTime[k];
                HashSet<int> values = new HashSet<int>();
                foreach (int v in _graph[k])
                {
                    values.Add(allFinishTime[v]);
                }
                newGraph.Add(u, values);
            }
            return newGraph;
        }


        // Run second DFS on the finish-time graph to compute SCC and reverse the edge direction for the output 
        private Dictionary<int, string> SecondDFS(Dictionary<int, int> finishTimeToNodeMap, Dictionary<int, HashSet<int>> graphByTime)
        {
            Dictionary<int, string> scc = new Dictionary<int, string>();
            HashSet<int> explored = new HashSet<int>();
            Stack<int> stack = new Stack<int>();
            Dictionary<int, int> path = new Dictionary<int, int>();

            for (int i = n; i > 0; i--)
            {
                if (!explored.Contains(i))
                {
                    DFS2(graphByTime, i, explored, stack, path);

                    StringBuilder sb = new StringBuilder();
                    sb.Append(finishTimeToNodeMap[i]);
                    int u = i;
                    while (path.ContainsKey(u))
                    {
                        sb.Append("->" + finishTimeToNodeMap[path[u]]);
                        u = path[u];
                    }
                    scc.Add(finishTimeToNodeMap[i], sb.ToString());
                }
                path = new Dictionary<int, int>();
            }
            return scc;
        }


        // Second DFS for computing SCC and store each component in path dictionary with leader node as head of path 
        private void DFS2(Dictionary<int, HashSet<int>> graph, int i, HashSet<int> explored, Stack<int> stack, Dictionary<int, int> path)
        {
            explored.Add(i);
            stack.Push(i);

            while (stack.Count > 0)
            {
                int u = stack.Pop();
                if (graph.ContainsKey(u))
                {
                    foreach (int v in graph[u])
                    {
                        if (!explored.Contains(v))
                        {
                            path.Add(u, v);
                            DFS2(graph, v, explored, stack, path);
                        }                      
                    }
                }
            }
        }
        
        
        
        static void Main(string[] args)
        {
            Dictionary<int, HashSet<int>> graph = new Dictionary<int, HashSet<int>>();

            graph.Add(1, new HashSet<int>(new[] { 4 }));
            graph.Add(2, new HashSet<int>(new[] { 8 }));
            graph.Add(3, new HashSet<int>(new[] { 6 }));
            graph.Add(4, new HashSet<int>(new[] { 7 }));
            graph.Add(5, new HashSet<int> (new[] { 2 }));
            graph.Add(6, new HashSet<int>(new[] { 9 }));
            graph.Add(7, new HashSet<int>(new[] { 1 }));
            graph.Add(8, new HashSet<int>(new[] { 5, 6 }));
            graph.Add(9, new HashSet<int>(new[] { 3, 7 }));


            StrongConnectedComponents scc = new StrongConnectedComponents(graph);
            
            Dictionary<int, string> ans = scc.ComputeSCC();
            foreach (KeyValuePair<int, string> p in ans)
            {
                Console.Write("leader node of the component is " + p.Key + ": " + p.Value + ",  ");
            }
            Console.ReadLine();            
        }
    }
