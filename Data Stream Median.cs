/** 
    two heaps, low and high heap, each represents half of the current list. 
	The length of low heap is kept to be n/2 at all time and the length 
	of high heap is either n/2 or n/2+1 depending on the length of list
	(1) length of (low, high) = (k, k)
	(2) length of (low, high) = (k, k + 1)
	After adding one number, they will become:
	(1) length of (low, high) = (k, k + 1)
	(2) length of (low, high) = (k + 1, k + 1) 
    */


public class MedianFinder {

    MaxHeap lowHeap;
    MinHeap highHeap;
    //length is even number
    bool even;
    
    /** initialize data structure */
    public MedianFinder() {
        lowHeap = new MaxHeap(new List<int>());
        lowHeap.BuildMaxHeap();
        highHeap = new MinHeap(new List<int>());
        highHeap.BuildMinHeap();
        even = true;
    }
    
    
    /** add one number to list */
    public void AddNum(int num) {
        if (even){
            //(k, k) to (k, k + 1)
            lowHeap.Insert(num);
            highHeap.Insert(lowHeap.DeleteMax());
        }else{
            //(k, k + 1) to (k + 1, k + 1)
            highHeap.Insert(num);
            lowHeap.Insert(highHeap.DeleteMin());
        }
        even = !even;
    }
    
    
    /** median is either mean of max of low heap and min of high heap(k, k)
    or min of high heap(k, k+1) */
    public double FindMedian() {
        if (even){
            return (lowHeap.GetMax() + highHeap.GetMin()) / 2.0;
        }else{
            return highHeap.GetMin();
        }
    }
}



/** heap class with min-value at the root */
public class MinHeap{
    IList<int> list;
    //size equals index of last element after adding 0 to begining of list
    int size;
    
    
    public MinHeap(IList<int> input){
        list = input;
        size = input.Count;
    }
    
    
    
    /** find index of min-child, parameter i is index */
    public int MinChild(int i){
        if (size < 2*i+1){ 
            //left-child only
            return 2*i;
        }else{ 
            //return index of min among 2 children
            if (list[2*i] <= list[2*i+1]){
                return 2*i;
            }else{
                return 2*i+1;
            }
        }
    }
    
    
    
    /** bubble up number at index i */
    public void BubbleUp(int i){
        while (i/2 > 0){
            // swap with parent(i/2) if number at i is smaller
            if (list[i] < list[i/2]){
                int temp = list[i/2];
                list[i/2] = list[i];
                list[i] = temp;
            }
            i /= 2;
        }
    }
    
    
    
    /** bubble down number at index i */
    public void BubbleDown(int i){
        while (2*i <= size){
            int minChild = MinChild(i);
            // swap with minChild if current number at i is larger
            if (list[i] > list[minChild]){
                int temp = list[minChild];
                list[minChild] = list[i];
                list[i] = temp;
            }
            i = minChild;
        }
    }
    
    
    
    /** insert number n into heap */
    public void Insert(int n){
        // insert n at the end of list then bubble up 
        list.Add(n);
        size++;
        BubbleUp(size);
    }
    
    
    
    /** get min-value of heap */
    public int GetMin(){
        // min-value is at the root
        return list[1];
    }
    
    
    /** delete min-value from heap */
    public int DeleteMin(){
        // swap root with last leaf and then bubble down from root
        int min = list[1];
        list[1] = list[size];
        list.RemoveAt(size);
        size--;
        BubbleDown(1);
        return min;
    }
    
    
    
    /** build heap from list */
    public IList<int> BuildMinHeap(){
        int i = size/2;
        list.Add(0);
        // start from last parent node, get the correct sub-branch, repeat for each parent node 
        while (i > 0){
            BubbleDown(i);
            i--;
        }
        return list;
    }
}




/** heap class with max-value at the root */
public class MaxHeap{
    IList<int> list;
    //size equals index of last element after adding 0 to begining of list
    int size;
    
    
    public MaxHeap(IList<int> input){
        list = input;
        size = input.Count;
    }
    
    
    
   /** find index of max-child, parameter i is index */
    public int MaxChild(int i){
        if (size < 2*i+1){
             //left-child only
            return 2*i;
        }else{
            //return index of max among 2 children
            if (list[2*i] > list[2*i+1]){
                return 2*i;
            }else{
                return 2*i+1;
            }
        }
    }
    
    
    
    /** bubble-up larger value */
    public void BubbleUp(int i){
        while (i/2 > 0){
            // swap with parent(i/2) if number at i is larger
            if (list[i] > list[i/2]){
                int temp = list[i];
                list[i] = list[i/2];
                list[i/2] = temp;
            }
            i /= 2;
        }
    }
    
    
    
    /** bubble-down smaller value */
    public void BubbleDown(int i){
        while (2*i <= size){
            // swap with maxChild if current number at i is smaller
            int maxChild = MaxChild(i);
            if (list[i] < list[maxChild]){
                int temp = list[i];
                list[i] = list[maxChild];
                list[maxChild] = temp;
            }
            i = maxChild;
        }
    }
    
    
    
     /** insert number n into heap */
    public void Insert(int n){
        // insert n at the end of list then bubble up 
        list.Add(n);
        size++;
        BubbleUp(size);
    }
    
    
    
    /** get max-value of heap */
    public int GetMax(){
        // max-value is at the root
        return list[1];
    }
    
    
    
     /** delete max-value from heap */
    public int DeleteMax(){
        // swap root with last leaf and then bubble down from root
        int max = list[1];
        list[1] = list[size];
        list.RemoveAt(size);
        size--;
        BubbleDown(1);
        return max;
    }
    
    
    
     /** build heap from list */
    public IList<int> BuildMaxHeap(){
        int i = size/2;
        list.Add(0);
        // start from last parent node, get the correct sub-branch, repeat for each parent node 
        while (i > 0){
            BubbleDown(i);
            i--;
        }
        return list;
    }
        
        
}
