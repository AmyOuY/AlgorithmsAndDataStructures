public class Triple
    {
        public string _headNode;
        public string _tailNode;
        public int _length;

        public Triple(string headNode, string tailNode, int length)
        {
            _headNode = headNode;
            _tailNode = tailNode;
            _length = length;
        }

        public void SetHeadNode(string h)
        {
            _headNode = h;
        }

        public void SetTailNode(string t)
        {
            _tailNode = t;
        }

        public void SetLength(int l)
        {
            _length = l;
        }

        public string GetHeadNode()
        {
            return _headNode;
        }

        public string GetTailNode()
        {
            return _tailNode;
        }

        public int GetLength()
        {
            return _length;
        }

        public bool SameNode(string n)
        {
            return _headNode == n;
        }

        public bool SameEdge(Triple t)
        {
            return _headNode == t.GetHeadNode() && _tailNode == t.GetTailNode() && _length == t.GetLength();
        }

        public bool Contains(string s)
        {
            return _headNode == s;
        }
        
        public void PrintTriple()
        {
            Console.WriteLine(_tailNode + " ->" + _headNode + " : " + _length);
        }
    }
