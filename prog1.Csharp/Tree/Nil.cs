// Nil -- Parse tree node class for representing the empty list

using System;

namespace Tree
{
    public class Nil : Node
    {
        public Nil() { }
  
        public override void print(int n)
        {
            print(n, false);
        }

        public override void print(int n, bool p)
        {
            if (p)
            {
                Console.Write("\n");
                n -= 4;
                for (int i = 0; i < n; i++)
                    Console.Write(" ");
                Console.Write(")");
                if (n == 0)
                {
                    Console.Write("\n");
                }
            }
            else { 
                Console.Write(")");
            }
        }

        public override bool isNull() { return true; } 
    }
}
