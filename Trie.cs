public class Trie {
    
    public class TrieNode{
        public TrieNode[] children;
        public bool isWord;
        
        public TrieNode(){
            children = new TrieNode[26];
            isWord = false;
        }
    }
    
    
    private TrieNode root;

    /** Initialize data structure. */
    public Trie() {
        root = new TrieNode();
    }
    
    
    /** Inserts a word into the trie. */
    public void Insert(string word) {
        TrieNode p = root;
        for (int i = 0; i < word.Length; i++){
            int idx = word[i] - 'a';
            if (p.children[idx] == null){
                p.children[idx] = new TrieNode();
            }
            p = p.children[idx];
        }
        p.isWord = true;
    }
    
    
    
    /** Returns if the word is in the trie. */
    public bool Search(string word) {
        TrieNode node = Find(word);
        return node != null && node.isWord;
    }
    
    
    /** Returns if there is any word in the trie that starts with the given prefix. */
    public bool StartsWith(string prefix) {
        TrieNode node = Find(prefix);
        return node != null;
    }
    
    
    /** Search for word from root. */
    public TrieNode Find(string word){
        TrieNode p = root;
        for (int i = 0; i < word.Length; i++){
            int idx = word[i] - 'a';
            if (p.children[idx] == null) return null;
            p = p.children[idx];
        }
        return p;
    }
}
