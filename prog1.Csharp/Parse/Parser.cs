// Parser -- the parser for the Scheme printer and interpreter
//
// Defines
//
//   class Parser;
//
// Parses the language
//
//   exp  ->  ( rest
//         |  #f
//         |  #t
//         |  ' exp
//         |  integer_constant
//         |  string_constant
//         |  identifier
//    rest -> )
//         |  exp+ [. exp] )
//
// and builds a parse tree.  Lists of the form (rest) are further
// `parsed' into regular lists and special forms in the constructor
// for the parse tree node class Cons.  See Cons.parseList() for
// more information.
//
// The parser is implemented as an LL(0) recursive descent parser.
// I.e., parseExp() expects that the first token of an exp has not
// been read yet.  If parseRest() reads the first token of an exp
// before calling parseExp(), that token must be put back so that
// it can be reread by parseExp() or an alternative version of
// parseExp() must be called.
//
// If EOF is reached (i.e., if the scanner returns a NULL) token,
// the parser returns a NULL tree.  In case of a parse error, the
// parser discards the offending token (which probably was a DOT
// or an RPAREN) and attempts to continue parsing with the next token.

using System;
using Tokens;
using Tree;

namespace Parse
{
    public class Parser {
        //readonly pointers to true, false, and nil constants
        public readonly BoolLit tBool = new BoolLit(true);
        public readonly BoolLit fBool = new BoolLit(false);
        public readonly Nil nilPoint = new Nil();
	
        private Scanner scanner;

        public Parser(Scanner s) { scanner = s; }
        
        //parses grammar for exp ->
        public Node parseExp()
        {
            Token tok;
            tok = scanner.getNextToken();
            //checks for end of file
            if(tok == null)
            {
                Nil retNil = nilPoint;
                return retNil;
            }
            //checks for case of left parenthesis
            if(tok.getType() == TokenType.LPAREN)
            {
                return parseRest(true);
            }
            //checks for case of false constant
            else if (tok.getType() == TokenType.FALSE)
            {
                BoolLit ret = fBool;
                return ret;
            }
            //checks for case of true constant
            else if (tok.getType() == TokenType.TRUE)
            {
                BoolLit ret = tBool;
                return ret;
            }
            //checks for case of QUOTE symbol
            else if (tok.getType() == TokenType.QUOTE)
            {
                return new Cons(new Ident("quote"), new Cons(parseExp(), new Nil()), true);
            }
            //checks for case int constant
            else if (tok.getType() == TokenType.INT)
            {
                return new IntLit(tok.getIntVal());
            }
            //checks for case of string constant
            else if (tok.getType() == TokenType.STRING)
            {
                return new StringLit(tok.getStringVal());
            }
            //checks for case of identifier
            else if (tok.getType() == TokenType.IDENT)
            {
                return new Ident(tok.getName());
            }

            return null;
        }

        //second parseExp for parsing exp-> grammer,
        //modified to be passed a token as an argument
        //instead of reading the token during the method
        //call.
        public Node parseExp(Token tok)
        {
            //checks for end of file
            if (tok == null)
            {
                Nil retNil = nilPoint;
                return retNil;
            }
            //checks for case of left parenthesis
            if (tok.getType() == TokenType.LPAREN)
            {
                return parseRest(true);
            }
            //checks for case of false constant
            else if (tok.getType() == TokenType.FALSE)
            {
                BoolLit ret = fBool;
                return ret;
            }
            //checks for case of true constant
            else if (tok.getType() == TokenType.TRUE)
            {
                BoolLit ret = tBool;
                return ret;
            }
            //checks for case of QUOTE symbol
            else if (tok.getType() == TokenType.QUOTE)
            {
                return new Cons(new Ident("quote"), new Cons(parseExp(), new Nil()), true);
            }
            //checks for case int constant
            else if (tok.getType() == TokenType.INT)
            {
                return new IntLit(tok.getIntVal());
            }
            //checks for case of string constant
            else if (tok.getType() == TokenType.STRING)
            {
                return new StringLit(tok.getStringVal());
            }
            //checks for case of identifier
            else if (tok.getType() == TokenType.IDENT)
            {
                return new Ident(tok.getName());
            }

            return null;
        }

        //method for parsing rest-> grammar
        protected Node parseRest(bool startsList)
        {
            Token tok;
            tok = scanner.getNextToken();
            if(tok.getType() == TokenType.RPAREN)
            {
                Nil retNil = nilPoint;
                return retNil;
            }
            else
                return new Cons(parseExp(tok), parseRest(false),startsList);
            return null;
        }
    }
}

