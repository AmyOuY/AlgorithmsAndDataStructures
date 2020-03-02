public class NumArray {
     
    // index n to 2n-1 stores nums 
    // index 1 to n-1 stores parent
    // parent equals sum of children
    int[] tree;
    int n;
    
    public NumArray(int[] nums) {
        if (nums.Length > 0){
            n = nums.Length;
            tree = new int[2*n];
            BuildTree(nums);
        }
    }
    
    
    public void BuildTree(int[] nums){
        for (int i = n, j = 0; i < 2*n; i++, j++){
            tree[i] = nums[j];
        }
        
        for (int i = n-1; i > 0; i--){
            tree[i] = tree[2*i] + tree[2*i+1];
        }
    }
    
    
    
    /** modify nums by updating the element at index i to val */
    public void Update(int i, int val) {
        // update val in correponding position of tree 
        i += n;
        tree[i] = val;
        
        while (i > 0){
            int left = i;
            int right = i;
           
            if (i%2 == 0){  // i is left child
                right = i+1;
            }else{ //i is right child
                left = i-1;
            }
            
            tree[i/2] = tree[left] + tree[right];
            // move up one level
            i /= 2;
        }
        
    }
    
    
    
    /** sum the elements between indices i and j (i ≤ j), inclusive */
    public int SumRange(int i, int j) {
        i += n;
        j += n;
        int sum = 0;
        while (i <= j){
            if (i%2 == 1){ // i  is right child of left branch
                sum += tree[i];
                i++; // move to right branch
            }
            
            if (j%2 == 0){ // j is left child of right branch
                sum += tree[j];
                j--; // move to left branch
            }
            
            i /= 2;
            j /= 2;
        }
        return sum;
    }
}
