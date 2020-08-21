class SequenceAlignment
    {
        string[] X;
        string[] Y;
        Dictionary<string, Dictionary<string, int>> scores;
        int[][] dp;


        public SequenceAlignment(string[] x, string[] y)
        {
            X = x;
            Y = y;
            scores = new Dictionary<string, Dictionary<string, int>>();
            dp = new int[x.Length][];
            for (int i = 0; i < x.Length; i++)
            {
                dp[i] = new int[y.Length];
            }
        }


        private void ScoreMatrix(List<string> alphabet, int diagScore, int offDiagScore, int dashScore)
        {
            foreach (string k in alphabet)
            {
                Dictionary<string, int> innerMap = new Dictionary<string, int>();
                if (k.Equals("-"))
                {
                    foreach (string s in alphabet)
                    {
                        innerMap[s] = dashScore;
                        innerMap[k] = 0;
                    }
                }
                else
                {
                    foreach (string s in alphabet)
                    {
                        if (s.Equals(k))
                        {
                            innerMap.Add(s, diagScore);
                        }
                        else if (s.Equals("-"))
                        {
                            innerMap.Add(s, dashScore);
                        }
                        else
                        {
                            innerMap.Add(s, offDiagScore);
                        }
                    }
                }
                scores.Add(k, innerMap);
            }
        }


        public int[][] AlignScore()
        {
            int lx = X.Length;
            int ly = Y.Length;

            for (int i = 0; i < lx; i++) dp[i][0] = i * scores["A"]["-"];
            for (int i = 0; i < ly; i++) dp[0][i] = i * scores["-"]["A"];

            for (int i = 1; i < lx; i++)
            {
                for (int j = 1; j < ly; j++)
                {
                    dp[i][j] = Math.Max(dp[i - 1][j - 1] + scores[X[i]][Y[j]],
                                        Math.Max(dp[i - 1][j] + scores[X[i]]["-"],
                                        dp[i][j - 1] + scores["-"][Y[j]]));
                }
            }

            return dp;
        }


        public Dictionary<string, string> AlignSequence()
        {
            StringBuilder alignX = new StringBuilder();
            StringBuilder alignY = new StringBuilder();
            Dictionary<string, string> ans = new Dictionary<string, string>();

            int i = X.Length - 1, j = Y.Length - 1;
            while (i > 0 && j > 0)
            {
                if (dp[i][j] == dp[i-1][j-1] + scores[X[i]][Y[j]])
                {
                    alignX.Insert(0, X[i]);
                    alignY.Insert(0, Y[j]);
                    i--;
                    j--;
                }
                else if (dp[i][j] == dp[i-1][j] + scores[X[i]]["-"])
                {
                    alignX.Insert(0, X[i]);
                    alignY.Insert(0, "-");
                    i--;
                }
                else
                {
                    alignX.Insert(0, "-");
                    alignY.Insert(0, Y[j]);
                    j--;
                }
            }

            while (i > 0)
            {
                alignX.Insert(0, X[i]);
                alignY.Insert(0, "-");
                i--;
            }

            while (j > 0)
            {
                alignX.Insert(0, "-");
                alignY.Insert(0, Y[j]);
                j--;
            }

            ans.Add("X", alignX.ToString());
            ans.Add("Y", alignY.ToString());
            return ans;
        } 

        static void Main(string[] args)
        {
            List<string> alphabet = new List<string>(new[] { "H", "A", "P", "L", "E", "-" });
            string[] X = { "", "A", "P", "P", "L", "E" };
            string[] Y = { "", "H", "A", "P", "E" };

            SequenceAlignment seqAlign = new SequenceAlignment(X, Y);

            seqAlign.ScoreMatrix(alphabet, 1, -1, -1);

            Dictionary<string, Dictionary<string, int>> scores = seqAlign.scores;
            foreach (string k in alphabet)
            {
                Console.Write(k + " ");
                foreach (KeyValuePair<string, int> p in scores[k])
                {
                    Console.Write(p.Key + " " + p.Value + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            seqAlign.AlignScore();

            Dictionary<string, string> ans = seqAlign.AlignSequence();
            Console.WriteLine(ans["X"]);
            Console.WriteLine(ans["Y"]);
            Console.ReadLine();
        }
    }
