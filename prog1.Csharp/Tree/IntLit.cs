// IntLit -- Parse tree node class for representing integer literals

using System;

namespace Tree
{
    public class IntLit : Node
    {
        private int intVal;

        public IntLit(int i)
        {
            intVal = i;
        }

        public override void print(int n, bool p)
        {
            // There got to be a more efficient way to print n spaces.
            //for (int i = 0; i < n; i++)
            //       Console.Write(" ");

            if (p)
            {
                Console.WriteLine("");
                for (int i = 0; i < n; i++)
                    Console.Write(" ");
                Console.Write(intVal);
            }
            else
                Console.Write(intVal);
        }

        public override bool isNumber() { return true; }  
    }
}
