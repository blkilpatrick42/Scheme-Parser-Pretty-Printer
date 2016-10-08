// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        public Regular(){ }

        public override void print(Node t, int n, bool p)
        {
            if (t.getCdr().isNull())
            {
                t.getCar().print(n,false);
                t.getCdr().print(n,p);
                
            }
            else
            {
                t.getCar().print(n,false);
                Console.Write(" ");
                t.getCdr().print(n,p);
            }
        }
    }
}


