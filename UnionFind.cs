public class UnionFind{
    public int count;
    public int[] parent;
    public int[] rank;
    public UnionFind(int n ){
        count = n;
        parent = new int[n];
        rank = new int[n];
        for (int i = 0; i < n; i++){
            parent[i] = i;
        }
    }
    
    //path compression
    public int Find(int p){
        while (p != parent[p]){
            parent[p] = parent[parent[p]];
            p = parent[p];
        }
        return p;
    }
    
    
    public void Union(int p, int q){
        int rootP = Find(p);
        int rootQ = Find(q);
        if (rootP == rootQ) return;
        if (rank[rootP] > rank[rootQ]){
            parent[rootQ] = rootP;
        }
        else if (rank[rootP] < rank[rootQ]){
            parent[rootP] = rootQ;
        }
        else{
            parent[rootQ] = rootP;
            rank[rootP]++;
        }
        count--;
    }
    
    
    public int Count(){
        return count;
    }
    
    
    public bool IsConnected(int p, int q){
        return Find(p) == Find(q);
    }
}


public class Solution {
    public int FindCircleNum(int[][] isConnected){
        int n = isConnected.Length;
        UnionFind uf = new UnionFind(n);
        for (int i = 0; i < n - 1; i++){
            for (int j = i + 1; j < n; j++){
                if (isConnected[i][j] == 1) uf.Union(i, j);
            }
        }
        return uf.count;
    }
