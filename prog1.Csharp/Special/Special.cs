// Special -- Parse tree node strategy for printing special forms

using System;

namespace Tree
{
    // There are several different approaches for how to implement the Special
    // hierarchy.  We'll discuss some of them in class.  The easiest solution
    // is to not add any fields and to use empty constructors.

    abstract public class Special
    {
        public abstract void print(Node t, int n, bool p);

        //Prints n spaces into the console
        public void printSpaces(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write(" ");
            }
        }
        public virtual bool formIsBeginLetCond()
        {
            return false;
        }
        public virtual bool formIsIfLambdaDefine()
        {
            return false;
        }
    }
    

   
}
