public class DijkstraByHeap
    {
        public string ShortestPath(Dictionary<string, Dictionary<string, int>> graph, string source, string destination)
        {
            // get all nodes of the graph
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
            distances.Add(source, 0);
            Dictionary<string, string> path = new Dictionary<string, string>();

            List<Triple> tripleList = new List<Triple>();

            // build heap with nodes that connected to source 
            // key is the min distance of the source to the node(headNode of the triple) 
            foreach (string u in graph[source].Keys)
            {
                int len = graph[source][u];
                tripleList.Add(new Triple(u, source, len));
            }

            HeapForTriple heap = new HeapForTriple(tripleList);
            heap.Heapsify();

            // X stores nodes that being checked
            HashSet<string> X = new HashSet<string>();
            X.Add(source);

            // loop until checking all nodes
            while (!X.Equals(allNodes)){
                if (heap.HeapIsEmpty()) break;
                               
                // extract node with min distance from heap
                Triple minTriple = heap.DeleteMin();
                string minNode = minTriple.GetHeadNode();
                X.Add(minNode);
                distances.Add(minNode, minTriple.GetLength());
                path.Add(minNode, minTriple.GetTailNode());
            
                // update key value for nodes(headNodes) that form edge with minNode(tailNode) 
                if (graph.ContainsKey(minNode))
                {
                    foreach (string v in graph[minNode].Keys)
                    {
                        int newLen = distances[minNode] + graph[minNode][v];
                        if (!heap.ContainsNode(v))
                        { 
                            heap.Insert(new Triple(v, minNode, newLen));
                        }
                        else
                        {
                            int oldLen = heap.GetLength(v);
                            if (newLen < oldLen)
                            {
                                heap.Delete(v);
                                heap.Insert(new Triple(v, minNode, newLen));
                            }
                        }
                    }
                }
                
            }

            if (!path.ContainsKey(destination)) return "Path not exist!";
            StringBuilder sb = new StringBuilder();
            string n = destination;
            sb.Append(n);
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
            Dictionary<string, Dictionary<string, int>> directedMap = new Dictionary<string, Dictionary<string, int>>();
            directedMap.Add("A", addEdge(addEdge(addEdge(new Dictionary<string, int>(), "B", 2), "C", 5), "D", 1));
            directedMap.Add("B", addEdge(new Dictionary<string, int>(), "C", 1));
            directedMap.Add("C", addEdge(new Dictionary<string, int>(), "F", 5));
            directedMap.Add("D", addEdge(addEdge(addEdge(new Dictionary<string, int>(), "B", 2), "C", 3), "E", 1));
            directedMap.Add("E", addEdge(addEdge(new Dictionary<string, int>(), "C", 1), "F", 2));

            Console.WriteLine("Compute Dijkstra's scores using Heap");
            DijkstraByHeap dijkstra = new DijkstraByHeap();
            
            Console.WriteLine(dijkstra.ShortestPath(directedMap, "A", "C"));
           
            Console.ReadLine();
        }
    }
