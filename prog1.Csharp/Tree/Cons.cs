// Cons -- Parse tree node class for representing a Cons node

using System;

namespace Tree
{
    public class Cons : Node
    {
        private Node car;
        private Node cdr;
        private Special form;
        private bool startsList = false; //flag for printing the open parenthesis at list start
        private bool inList = false; //flag for skipping indentations on interior lists

        //accessor for startsListS
        public override bool getStartsList()
        {
            return startsList;
        }

        //recursive method which sets the inList flag to true for all children
        //of THIS, including THIS if init is true
        public override void flipInList(bool init)
        {
            if (!init)
            {
                inList = true;
            }
            this.cdr.flipInList(false);
            
        }

        //recursively traverses the list and
        //sets initial flags for not indenting interior lists
        public void flagListA()
        {
            if (car.getStartsList())
            {
                if (this.inList)
                {
                    car.flipInList(false);
                }
                else
                {
                    car.flipInList(true);
                }
            }
        }

        //second recursive function which traverses the list
        //once more, and unflags nodes which are whithin interior
        //lists but also part of special cases such as Begin, Let, Cond,
        //Define, if, Lambda
        public void flagListB()
        {
            if (this.form.formIsBeginLetCond())
            {
                flipFlag();
            }
            if (this.form.formIsIfLambdaDefine())
            {
                cdr.getCar().setInList(true);
            }
        }

        //recursively flips off the inList flags down the cdr
        //side of THIS node's subtree
        public override void flipFlag()
        {
            setInList(false);
            cdr.flipFlag();
        }

        //mutator for inList
        public override void setInList(bool b)
        {
            inList = b;
        }
    
        public Cons(Node a, Node d)
        {
            car = a;
            cdr = d;
            parseList();
        }

        public Cons(Node a, Node d, bool c)
        {
            car = a;
            cdr = d;
            startsList = c;
            parseList();
        }

        // parseList() `parses' special forms, constructs an appropriate
        // object of a subclass of Special, and stores a pointer to that
        // object in variable form.  It would be possible to fully parse
        // special forms at this point.  Since this causes complications
        // when using (incorrect) programs as data, it is easiest to let
        // parseList only look at the car for selecting the appropriate
        // object from the Special hierarchy and to leave the rest of
        // parsing up to the interpreter.
        void parseList()
        {
            //constructs form by token in Car
            if (car.getName() == "quote")
            {
                form = new Quote();
            }
            else if (car.getName() == "begin")
            {
                form = new Begin();
                
            }
            else if(car.getName() == "cond")
            {
                form = new Cond();
            }
            else if(car.getName() == "define")
            {
                form = new Define();
            }
            else if(car.getName() == "if")
            {
                form = new If();
            }
            else if(car.getName() == "lambda")
            {
                form = new Lambda();
            }
            else if(car.getName() == "let")
            {
                form = new Let();
            }
            else if(car.getName() == "set!")
            {
                form = new Set();
            }
            else
            {
                form = new Regular();
            }

        }
        
        //intitial print statement with no boolean,
        //in normal cases, this is only called once
        //at the start of the printing
        public override void print(int n)
        {
            flagListA(); //flips inList for interior lists
            flagListB(); //undoes inList flips for special cases
            if (form != null)
            {
                if (startsList) //if the node starts a list
                {
                    printSpaces(n); //indent
                    Console.Write("("); //open paren
                }
                form.print(this, n, false); //call to form print
            }
        }

        public override void print(int n, bool p)
        {
            flagListA(); //flips inList for interior lists
            flagListB(); //undoes inList flips for special cases
            if (form != null)
            {
                if (startsList || (p && !(this.getCar().isPair()))) //if starts list or is a lone constant
                {
                    if (n>0 && !inList) //if indention spaces greater than 0 and node is not already in a list
                    {
                        Console.Write("\n");        //write newline
                        printSpaces(n);       //indent
                    }
                    
                    if (startsList)
                    {
                        Console.Write("("); //print open paren for beginning list
                    }
                }
                form.print(this, n, p); //call further form.print
            }
        }

        public override bool isPair() { return true; }

        public override Node getCar() {
            if (car != null)
            {
                return car;
            }
            return null;
        }
        public override Node getCdr() {
            if (cdr != null)
            {
                return cdr;
            }
            return null;
        }

        public override void setCar(Node a) {
            car = a;
            parseList();
        }
        public override void setCdr(Node d) {
            cdr = d;
        }
    }
}

