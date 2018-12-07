using System;
using System.Text;
using System.Windows.Forms;

namespace DevPython
{
    class Tokenizer
    {
        String buf;
        int cur;
        int start;
        int done;
        int indent;
        int[] indstack;
        int atbol;
        int pendin;
        int lineno;
        int level;
        Tokenizer(String _buf)
        {
            buf = _buf;
            cur = 0;
            start = 0;
            // ....
        }

        public int get(out String p)
        {
            p = "";
            return 0;
        }

        #region PRIVATE_FUNCTIONS
        int OneChar(int c)
        {
            switch (c)
            {
                case '(': return LPAR;
                case ')': return RPAR;
                case '[': return LSQB;
                case ']': return RSQB;
                case ':': return COLON;
                case ',': return COMMA;
                case ';': return SEMI;
                case '+': return PLUS;
                case '-': return MINUS;
                case '*': return STAR;
                case '/': return SLASH;
                case '|': return VBAR;
                case '&': return AMPER;
                case '<': return LESS;
                case '>': return GREATER;
                case '=': return EQUAL;
                case '.': return DOT;
                case '%': return PERCENT;
                case '{': return LBRACE;
                case '}': return RBRACE;
                case '^': return CIRCUMFLEX;
                case '~': return TILDE;
                case '@': return AT;
                default: return OP;
            }
        }

        int TwoChars(int c1, int c2)
        {
            switch (c1)
            {
                case '=':
                    switch (c2)
                    {
                        case '=': return EQEQUAL;
                    }
                    break;
                case '!':
                    switch (c2)
                    {
                        case '=': return NOTEQUAL;
                    }
                    break;
                case '<':
                    switch (c2)
                    {
                        case '>': return NOTEQUAL;
                        case '=': return LESSEQUAL;
                        case '<': return LEFTSHIFT;
                    }
                    break;
                case '>':
                    switch (c2)
                    {
                        case '=': return GREATEREQUAL;
                        case '>': return RIGHTSHIFT;
                    }
                    break;
                case '+':
                    switch (c2)
                    {
                        case '=': return PLUSEQUAL;
                    }
                    break;
                case '-':
                    switch (c2)
                    {
                        case '=': return MINEQUAL;
                        case '>': return RARROW;
                    }
                    break;
                case '*':
                    switch (c2)
                    {
                        case '*': return DOUBLESTAR;
                        case '=': return STAREQUAL;
                    }
                    break;
                case '/':
                    switch (c2)
                    {
                        case '/': return DOUBLESLASH;
                        case '=': return SLASHEQUAL;
                    }
                    break;
                case '|':
                    switch (c2)
                    {
                        case '=': return VBAREQUAL;
                    }
                    break;
                case '%':
                    switch (c2)
                    {
                        case '=': return PERCENTEQUAL;
                    }
                    break;
                case '&':
                    switch (c2)
                    {
                        case '=': return AMPEREQUAL;
                    }
                    break;
                case '^':
                    switch (c2)
                    {
                        case '=': return CIRCUMFLEXEQUAL;
                    }
                    break;
                case '@':
                    switch (c2)
                    {
                        case '=': return ATEQUAL;
                    }
                    break;
            }
            return OP;
        }

        int ThreeChars(int c1, int c2, int c3)
        {
            switch (c1)
            {
                case '<':
                    switch (c2)
                    {
                        case '<':
                            switch (c3)
                            {
                                case '=':
                                    return LEFTSHIFTEQUAL;
                            }
                            break;
                    }
                    break;
                case '>':
                    switch (c2)
                    {
                        case '>':
                            switch (c3)
                            {
                                case '=':
                                    return RIGHTSHIFTEQUAL;
                            }
                            break;
                    }
                    break;
                case '*':
                    switch (c2)
                    {
                        case '*':
                            switch (c3)
                            {
                                case '=':
                                    return DOUBLESTAREQUAL;
                            }
                            break;
                    }
                    break;
                case '/':
                    switch (c2)
                    {
                        case '/':
                            switch (c3)
                            {
                                case '=':
                                    return DOUBLESLASHEQUAL;
                            }
                            break;
                    }
                    break;
                case '.':
                    switch (c2)
                    {
                        case '.':
                            switch (c3)
                            {
                                case '.':
                                    return ELLIPSIS;
                            }
                            break;
                    }
                    break;
            }
            return OP;
        }
        #endregion

        #region SOME_CONSTANTS
        static readonly string[] _PyParser_TokenNames = {
    "ENDMARKER",
    "NAME",
    "NUMBER",
    "STRING",
    "NEWLINE",
    "INDENT",
    "DEDENT",
    "LPAR",
    "RPAR",
    "LSQB",
    "RSQB",
    "COLON",
    "COMMA",
    "SEMI",
    "PLUS",
    "MINUS",
    "STAR",
    "SLASH",
    "VBAR",
    "AMPER",
    "LESS",
    "GREATER",
    "EQUAL",
    "DOT",
    "PERCENT",
    "LBRACE",
    "RBRACE",
    "EQEQUAL",
    "NOTEQUAL",
    "LESSEQUAL",
    "GREATEREQUAL",
    "TILDE",
    "CIRCUMFLEX",
    "LEFTSHIFT",
    "RIGHTSHIFT",
    "DOUBLESTAR",
    "PLUSEQUAL",
    "MINEQUAL",
    "STAREQUAL",
    "SLASHEQUAL",
    "PERCENTEQUAL",
    "AMPEREQUAL",
    "VBAREQUAL",
    "CIRCUMFLEXEQUAL",
    "LEFTSHIFTEQUAL",
    "RIGHTSHIFTEQUAL",
    "DOUBLESTAREQUAL",
    "DOUBLESLASH",
    "DOUBLESLASHEQUAL",
    "AT",
    "ATEQUAL",
    "RARROW",
    "ELLIPSIS",
    /* This table must match the #defines in token.h! */
    "OP",
    "<ERRORTOKEN>",
    "COMMENT",
    "NL",
    "ENCODING",
    "<N_TOKENS>"
};
        const int ENDMARKER = 0;
        const int NAME = 1;
        const int NUMBER = 2;
        const int STRING = 3;
        const int NEWLINE = 4;
        const int INDENT = 5;
        const int DEDENT = 6;
        const int LPAR = 7;
        const int RPAR = 8;
        const int LSQB = 9;
        const int RSQB = 10;
        const int COLON = 11;
        const int COMMA = 12;
        const int SEMI = 13;
        const int PLUS = 14;
        const int MINUS = 15;
        const int STAR = 16;
        const int SLASH = 17;
        const int VBAR = 18;
        const int AMPER = 19;
        const int LESS = 20;
        const int GREATER = 21;
        const int EQUAL = 22;
        const int DOT = 23;
        const int PERCENT = 24;
        const int LBRACE = 25;
        const int RBRACE = 26;
        const int EQEQUAL = 27;
        const int NOTEQUAL = 28;
        const int LESSEQUAL = 29;
        const int GREATEREQUAL = 30;
        const int TILDE = 31;
        const int CIRCUMFLEX = 32;
        const int LEFTSHIFT = 33;
        const int RIGHTSHIFT = 34;
        const int DOUBLESTAR = 35;
        const int PLUSEQUAL = 36;
        const int MINEQUAL = 37;
        const int STAREQUAL = 38;
        const int SLASHEQUAL = 39;
        const int PERCENTEQUAL = 40;
        const int AMPEREQUAL = 41;
        const int VBAREQUAL = 42;
        const int CIRCUMFLEXEQUAL = 43;
        const int LEFTSHIFTEQUAL = 44;
        const int RIGHTSHIFTEQUAL = 45;
        const int DOUBLESTAREQUAL = 46;
        const int DOUBLESLASH = 47;
        const int DOUBLESLASHEQUAL = 48;
        const int AT = 49;
        const int ATEQUAL = 50;
        const int RARROW = 51;
        const int ELLIPSIS = 52;
        const int OP = 53;
        const int ERRORTOKEN = 54;
        const int COMMENT = 55;
        const int NL = 56;
        const int ENCODING = 57;
        const int N_TOKENS = 58;
#endregion
    }

    class Parser
    {
        stack p_stack;       /* Stack of parser states */
        grammar p_grammar;   /* Grammar to use */
        _node p_tree;        /* Top of parse tree */

        #region SOME_DEFINITIONS
        struct label {
            int lb_type;
            String lb_str;
        };
        const int EMPTY = 0; 
        /* A list of labels */
        class labellist {
            int ll_nlabels;
            label[] ll_label;
        };
        /* An arc from one state to another */
        struct arc {
            short a_lbl;          /* Label of this arc */
            short a_arrow;        /* State where this arc goes to */
        };
        /* A state in a DFA */
        class state {
            int s_narcs;
            arc[] s_arc;         /* Array of arcs */
        };
        /* A DFA */
        class dfa {
            int d_type;        /* Non-terminal this represents */
            String d_name;     /* For printing */
            int d_initial;     /* Initial state */
            int d_nstates;
            state[] d_state;   /* Array of states */
            // bitset d_first;
        };
        /* A grammar */
        class grammar {
            int g_ndfas;
            dfa[] g_dfa;       /* Array of DFAs */
            labellist g_ll;
            int g_start;       /* Start symbol of the grammar */
            int g_accel;       /* Set if accelerators present */
        };
        class _node {
            short n_type;
            String n_str;
            int n_lineno;
            int n_col_offset;
            int n_nchildren;
            _node[] n_child;
        };
        class stackentry {
            int s_state;       /* State in current DFA */
            dfa s_dfa;         /* Current DFA */
            _node s_parent;    /* Where to add next node */
        }
        class stack {
            stackentry s_top;       /* Top entry */
            stackentry[] s_base;    /* Array of stack entries */
        }
        #endregion
    }

    class Compiler
    {
        public static void run(String S)
        {

        }
    }
}
