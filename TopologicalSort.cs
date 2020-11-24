public class TopologicalSort {

     // BSF using Queue
     public int[] FindOrder(int numCourses, int[][] prerequisites) {
         int[] ans = new int[numCourses];
         Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
         int[] inDegree = new int[numCourses];
         foreach (int[] p in prerequisites){
             inDegree[p[0]]++;
             if (!dict.ContainsKey(p[1])) dict[p[1]] = new List<int>();
             dict[p[1]].Add(p[0]);
         }
        
         Queue<int> queue = new Queue<int>();
         for (int i = 0; i < numCourses; i++){
             if (inDegree[i] == 0) queue.Enqueue(i);
         }
        
        
         int index = 0;
         while (queue.Count > 0){
             int n = queue.Dequeue();
             ans[index++] = n;
             if (dict.ContainsKey(n)){
                 foreach (int i in dict[n]){
                     if (--inDegree[i] == 0) queue.Enqueue(i);
                 }
             }
         }
         return index == numCourses? ans : new int[0];
     }
    
    
    
    // DFS 
    public int[] FindOrder(int numCourses, int[][] prerequisites){
        Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
        foreach (int[] p in prerequisites){
            if (!dict.ContainsKey(p[1])) dict[p[1]] = new List<int>();
            dict[p[1]].Add(p[0]);
        }
        
        List<int> order = new List<int>();
        HashSet<int> explored = new HashSet<int>();
        for (int s = 0; s < numCourses; s++){
            if (!explored.Contains(s) && !DFS(dict, explored, order, s)){
                return new int[0];
            }
        }
        
        return order.ToArray();
    }
    
    
    private bool DFS(Dictionary<int, List<int>> dict, HashSet<int> explored, List<int> order, int s){
        if (order.Contains(s)) return true;
        if (explored.Contains(s)) return false;
        explored.Add(s);
        if (dict.ContainsKey(s)){
            foreach (int i in dict[s]){
                if (!DFS(dict, explored, order, i)) return false;
                              
            }
        }
        
        // Assign largest ordering to the sink vertex, then peel off the sink vertex and its incoming arcs recursively 
        order.Insert(0, s);
        return true;
    }
}
