// Lambda -- Parse tree node strategy for printing the special form lambda

using System;

namespace Tree
{
    public class Lambda : Special
    {
	public Lambda() { }

        public override void print(Node t, int n, bool p)
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

