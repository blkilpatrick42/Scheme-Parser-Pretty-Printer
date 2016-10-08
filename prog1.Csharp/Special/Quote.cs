// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
    public class Quote : Special
    {
	public Quote() { }

        public override void print(Node t, int n, bool p)
        {
            if (t.getCdr().isNull())
            {
                t.getCar().print(n, false);
                t.getCdr().print(n, p);
            }
            else
            {
                t.getCar().print(n, false);
                Console.Write(" ");
                t.getCdr().print(n, p);
            }
        }
    }
}

