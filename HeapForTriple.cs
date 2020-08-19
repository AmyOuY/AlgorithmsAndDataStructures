public class HeapForTriple
    {
        public List<Triple> _list;
        public int size;

        public HeapForTriple(List<Triple> list)
        {
            _list = list;
            size = list.Count;
        }

        public int MinChild(int i)
        {
            if (size < 2 * i + 1)
            {
                return 2 * i;
            }
            else
            {
                if (_list[2*i].GetLength() < _list[2 * i + 1].GetLength())
                {
                    return 2 * i;
                }
                else
                {
                    return 2 * i + 1;
                }
            }
        }


        public void BubbleUp(int i)
        {
            while (i > 1)
            {
                if (_list[i].GetLength() < _list[i / 2].GetLength())
                {
                    Triple temp = _list[i];
                    _list[i] = _list[i / 2];
                    _list[i / 2] = temp;
                }
                i /= 2;
            }
        }


        public void BubbleDown(int i)
        {
            while (2*i <= size)
            {
                int minChild = MinChild(i);
                if (_list[minChild].GetLength() < _list[i].GetLength())
                {
                    Triple temp = _list[minChild];
                    _list[minChild] = _list[i];
                    _list[i] = temp;
                }
                i = minChild;
            }
        }


        public void Insert(Triple t)
        {
            _list.Add(t);
            size++;
            BubbleUp(size);
        }


        public Triple DeleteMin()
        {
            Triple min = _list[1];
            _list[1] = _list[size];
            _list.RemoveAt(size);
            size--;
            BubbleDown(1);
            return min;
        }


        public void Delete(string n)
        {
            Triple targetNode = new Triple(null, null, 0);
            foreach (Triple t in _list)
            {
                if (t.SameNode(n))
                {
                    targetNode = t;
                    break;
                }
            }

            int idx = _list.IndexOf(targetNode);
            Triple lastNode = _list[size];
            _list[idx] = lastNode;
            _list.RemoveAt(size);
            size--;
            if (idx > 1)
            {
                if (lastNode.GetLength() < _list[idx / 2].GetLength())
                {
                    BubbleUp(idx);
                }
                else
                {
                    BubbleDown(idx);
                }
            }          

        }


        public bool ContainsNode(string n)
        {
            foreach (Triple t in _list)
            {
                if (t.SameNode(n))
                {
                    return true;
                }
            }
            return false;
        }


        public bool HeapIsEmpty()
        {
            return size == 0;
        }


        public int GetLength(string n) {
            Triple targetNode = new Triple(null, null, 0);
            foreach (Triple t in _list)
            {
                if (t.SameNode(n))
                {
                    return t.GetLength();
                }
            }
            return 0;
        }


        public List<Triple> Heapsify()
        {
            int i = size / 2;
            _list.Insert(0, new Triple(null, null, 0));
            while (i > 0)
            {
                BubbleDown(i);
                i--;
            }
            return _list;
        }


        public List<Triple> HeapSort()
        {
            List<Triple> sortedList = new List<Triple>();
            while (size > 0)
            {
                sortedList.Add(DeleteMin());
            }
            return sortedList;
        }


        static void Main(string[] args)
        {
            List<Triple> triple = new List<Triple>();

            triple.Add(new Triple("a", "i", 25));
            triple.Add(new Triple("b", "j", 29));
            triple.Add(new Triple("c", "k", 30));
            triple.Add(new Triple("d", "l", 14));
            triple.Add(new Triple("e", "m", 79));
            triple.Add(new Triple("f", "n", 2));
            triple.Add(new Triple("g", "o", 10));
            triple.Add(new Triple("h", "p", 12));

            HeapForTriple tripleList = new HeapForTriple(triple);

            Console.WriteLine("This is the heap: ");
            List<Triple> heap = tripleList.Heapsify();
            foreach (Triple t in heap)
            {
                t.PrintTriple();
            }


            Console.WriteLine(tripleList.HeapIsEmpty());

            Console.WriteLine(tripleList.GetLength("f"));

            Console.WriteLine("This is the sorted list: ");

            List<Triple> sortedList = tripleList.HeapSort();
            foreach (Triple t in sortedList)
            {
                t.PrintTriple();
            }

            Console.ReadLine();
        }
    }
