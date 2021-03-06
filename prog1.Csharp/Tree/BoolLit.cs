// BoolLit -- Parse tree node class for representing boolean literals

using System;

namespace Tree
{
    public class BoolLit : Node
    {
        private bool boolVal;
  
        public BoolLit(bool b)
        {
            boolVal = b;
        }

        public override void print(int n, bool p)
        {
            // There got to be a more efficient way to print n spaces.
            //for (int i = 0; i < n; i++)
            //        Console.Write(" ");
            if (p)
            {
                Console.Write("\n");
                for (int i = 0; i < n; i++)
                    Console.Write(" ");
                if (boolVal)
                    Console.Write("#t");
                else
                    Console.Write("#f");
            }
            else
            {
                if (boolVal)
                    Console.Write("#t");
                else
                    Console.Write("#f");
            }

            
        }

        public override bool isBool() { return true; }  
    }
}
