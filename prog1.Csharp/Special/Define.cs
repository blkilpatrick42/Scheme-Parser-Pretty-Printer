// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
	public Define() {}

        public override void print(Node t, int n, bool p)
        {
            if (t.getCdr().getCar().isPair())
            {
                if (t.getCdr().isNull())
                {
                    t.getCar().print(n, p);
                    t.getCdr().print(0, true);
                }
                else
                {
                    t.getCar().print(n, p);
                    Console.Write(" ");
                    n += 4;
                    t.getCdr().print(n, true);


                }
            }
            else 
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
        public override bool formIsBeginLetCond()
        {
            return true;
        }
        public override bool formIsIfLambdaDefine()
        {
            return true;
        }
    }
}


