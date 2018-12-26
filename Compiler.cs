using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DevPython
{
    class Tokenizer
    {
        #region PRIVATE_VARIABLES
        String _buf;
        public int buf;
        public int cur;
        public int inp;
        public int start;
        public int end;
        public int line_start;
        public int done;
        public int indent;
        public int[] indstack;
        public bool atbol;
        public int pendin;
        public int lineno;
        public int level;
        public int cont_line;
        #endregion

        public Tokenizer(String input)
        {
            _buf = input;
            // buf = cur = end = inp = 0;
            done = E_OK;
            indstack = new int[64]; // indent stack size
            atbol = true;
        }

        public bool ok()
        {
            return done == E_OK;
        }

        public int get(out String p)
        {
            nextline:
            p = "";
            int c;
            bool blankline = false;//, nonascii = false;
            start = 0;

            /* Get indentation level */
            if (atbol)
            {
                int col = 0;
                atbol = false;
                for (;;)
                {
                    c = nextc();
                    if (c == ' ')
                        col++;
                    else if (c == '\t')
                        col += 4;
                    else break;
                }
                backup(c);
                if (c == '#' || c == '\n')
                {
                    /* Lines with only whitespace and/or comments
                       shouldn't affect the indentation and are
                       not passed to the parser as NEWLINE tokens,
                       except *totally* empty lines in interactive
                       mode, which signal the end of a command group. */
                    if (col == 0 && c == '\n')
                    {
                        blankline = false; /* Let it through */
                    }
                    else
                    {
                        blankline = true; /* Ignore completely */
                    }
                    /* We can't jump back right here since we still
                       may need to skip to the end of a comment */
                }
                if (!blankline && level == 0)
                {
                    if (col == indstack[indent])
                    {
                        /* No change */
                    }
                    else if (col > indstack[indent])
                    {
                        /* Indent -- always one */
                        // if (indent + 1 >= MAXINDENT) stack will be boomed, but we don't care.
                        pendin++;
                        indstack[++indent] = col;
                    }
                    else /* col < tok->indstack[tok->indent] */
                    {
                        /* Dedent -- any number, must be consistent */
                        while (indent > 0 && col < indstack[indent])
                        {
                            pendin--;
                            indent--;
                        }
                        if (col != indstack[indent])
                        {
                            done = E_DEDENT;
                            cur = inp;
                            return ERRORTOKEN;
                        }
                    }
                }
            }

            start = cur;

            /* Return pending indents/dedents */
            if (pendin != 0)
            {
                if (pendin < 0)
                {
                    pendin++;
                    return DEDENT;
                }
                else
                {
                    pendin--;
                    return INDENT;
                }
            }

            again:
            start = 0;

            /* Skip spaces */
            do
            {
                c = nextc();
            } while (c == ' ' || c == '\t');

            /* Set start of current token */
            start = cur - 1;

            /* Skip comment */
            if (c == '#')
            {
                while (c != EOF && c != '\n')
                {
                    c = nextc();
                }
            }

            /* Check for EOF and errors now */
            if (c == EOF)
            {
                return done == E_EOF ? ENDMARKER : ERRORTOKEN;
            }

            /* Identifier (most frequent token!) */
            if (is_potential_identifier_start(c))
            {
                /* Process the various legal combinations of b"", r"", u"", and f"". */
                bool saw_b = false, saw_r = false, saw_u = false, saw_f = false;
                for (;;)
                {
                    if (!(saw_b || saw_u || saw_f) && (c == 'b' || c == 'B'))
                        saw_b = true;
                    /* Since this is a backwards compatibility support literal we don't
                       want to support it in arbitrary order like byte literals. */
                    else if (!(saw_b || saw_u || saw_r || saw_f)
                             && (c == 'u' || c == 'U'))
                    {
                        saw_u = true;
                    }
                    /* ur"" and ru"" are not supported */
                    else if (!(saw_r || saw_u) && (c == 'r' || c == 'R'))
                    {
                        saw_r = true;
                    }
                    else if (!(saw_f || saw_b || saw_u) && (c == 'f' || c == 'F'))
                    {
                        saw_f = true;
                    }
                    else
                    {
                        break;
                    }
                    c = nextc();
                    if (c == '"' || c == '\'')
                    {
                        goto letter_quote;
                    }
                }
                while (is_potential_identifier_char(c))
                {
                    /*if (c >= 128)
                    {
                        nonascii = true;
                    }*/
                    c = nextc();
                }
                backup(c);

                p = _buf.Substring(start, cur - start);

                return NAME;
            }

            /* Newline */
            if (c == '\n')
            {
                atbol = true;
                if (blankline || level > 0)
                {
                    goto nextline;
                }

                p = _buf.Substring(start, cur - start - 1); /* Leave '\n' out of the string */
                cont_line = 0;
                return NEWLINE;
            }

            /* Newline about \r */
            if (c == '\r')
            {
                c = nextc();
                if(c == '\n') {
                    atbol = true;
                    if (blankline || level > 0)
                    {
                        goto nextline;
                    }

                    p = _buf.Substring(start, cur - start - 1); /* Leave '\n' out of the string */
                    cont_line = 0;
                    return NEWLINE;
                }
                backup(c);
            }

            /* Period or number starting with period? */
            if (c == '.')
            {
                c = nextc();
                if (is_digit(c))
                {
                    if (is_digit(c))
                    {
                        c = decimal_tail();
                        if (c == 0)
                        {
                            return ERRORTOKEN;
                        }
                    }
                    if (c == 'e' || c == 'E')
                    {
                        int e;
                        // exponent:
                        e = c;
                        /* Exponent part */
                        c = nextc();
                        if (c == '+' || c == '-')
                        {
                            c = nextc();
                            if (!is_digit(c))
                            {
                                done = E_TOKEN;
                                backup(c);
                                return ERRORTOKEN;
                            }
                        }
                        else if (!is_digit(c))
                        {
                            backup(c);
                            backup(e);
                            p = _buf.Substring(start, cur - start);
                            return NUMBER;
                        }
                        c = decimal_tail();
                        if (c == 0)
                        {
                            return ERRORTOKEN;
                        }
                    }
                    if (c == 'j' || c == 'J')
                    {
                        /* Imaginary part */
                        // imaginary:
                        c = nextc();
                    }
                }
                else if (c == '.')
                {
                    c = nextc();
                    if (c == '.')   // ...
                    {
                        p = _buf.Substring(start, cur - start);
                        return ELLIPSIS;
                    }
                    else
                    {
                        backup(c);
                    }
                    backup('.');
                }
                else
                {
                    backup(c);
                }
                p = _buf.Substring(start, cur - start);
                return DOT;
            }

            /* Number */
            if (is_digit(c))
            {
                if (c == '0')
                {
                    /* Hex, octal or binary -- maybe. */
                    c = nextc();
                    if (c == 'x' || c == 'X')
                    {
                        /* Hex */
                        c = nextc();
                        do
                        {
                            if (c == '_')
                            {
                                c = nextc();
                            }
                            if (!isxdigit(c))
                            {
                                done = E_TOKEN;
                                backup(c);
                                return ERRORTOKEN;
                            }
                            do
                            {
                                c = nextc();
                            } while (isxdigit(c));
                        } while (c == '_');
                    }
                    else if (c == 'o' || c == 'O')
                    {
                        /* Octal */
                        c = nextc();
                        do
                        {
                            if (c == '_')
                            {
                                c = nextc();
                            }
                            if (c < '0' || c >= '8')
                            {
                                done = E_TOKEN;
                                backup(c);
                                return ERRORTOKEN;
                            }
                            do
                            {
                                c = nextc();
                            } while ('0' <= c && c < '8');
                        } while (c == '_');
                    }
                    else if (c == 'b' || c == 'B')
                    {
                        /* Binary */
                        c = nextc();
                        do
                        {
                            if (c == '_')
                            {
                                c = nextc();
                            }
                            if (c != '0' && c != '1')
                            {
                                done = E_TOKEN;
                                backup(c);
                                return ERRORTOKEN;
                            }
                            do
                            {
                                c = nextc();
                            } while (c == '0' || c == '1');
                        } while (c == '_');
                    }
                    else
                    {
                        int nonzero = 0;
                        /* maybe old-style octal; c is first char of it */
                        /* in any case, allow '0' as a literal */
                        for (;;)
                        {
                            if (c == '_')
                            {
                                c = nextc();
                                if (!is_digit(c))
                                {
                                    done = E_TOKEN;
                                    backup(c);
                                    return ERRORTOKEN;
                                }
                            }
                            if (c != '0')
                            {
                                break;
                            }
                            c = nextc();
                        }
                        if (is_digit(c))
                        {
                            nonzero = 1;
                            c = decimal_tail();
                            if (c == 0)
                            {
                                return ERRORTOKEN;
                            }
                        }
                        if (c == '.')
                        {
                            c = nextc();
                            //goto fraction;
                            if (is_digit(c))
                            {
                                c = decimal_tail();
                                if (c == 0)
                                {
                                    return ERRORTOKEN;
                                }
                            }
                            if (c == 'e' || c == 'E')
                            {
                                int e;
                                // exponent:
                                e = c;
                                /* Exponent part */
                                c = nextc();
                                if (c == '+' || c == '-')
                                {
                                    c = nextc();
                                    if (!is_digit(c))
                                    {
                                        done = E_TOKEN;
                                        backup(c);
                                        return ERRORTOKEN;
                                    }
                                }
                                else if (!is_digit(c))
                                {
                                    backup(c);
                                    backup(e);
                                    p = _buf.Substring(start, cur - start);
                                    return NUMBER;
                                }
                                c = decimal_tail();
                                if (c == 0)
                                {
                                    return ERRORTOKEN;
                                }
                            }
                            if (c == 'j' || c == 'J')
                            {
                                /* Imaginary part */
                                // imaginary:
                                c = nextc();
                            }
                        }
                        else if (c == 'e' || c == 'E')
                        {
                            int e = c;
                            /* Exponent part */
                            c = nextc();
                            if (c == '+' || c == '-')
                            {
                                c = nextc();
                                if (!is_digit(c))
                                {
                                    done = E_TOKEN;
                                    backup(c);
                                    return ERRORTOKEN;
                                }
                            }
                            else if (!is_digit(c))
                            {
                                backup(c);
                                backup(e);
                                p = _buf.Substring(start, cur - start);
                                return NUMBER;
                            }
                            c = decimal_tail();
                            if (c == 0)
                            {
                                return ERRORTOKEN;
                            }
                            if (c == 'j' || c == 'J')
                            {
                                /* Imaginary part */
                                // imaginary:
                                c = nextc();
                            }
                        }
                        else if (c == 'j' || c == 'J')
                        {
                            //goto imaginary;
                            c = nextc();
                        }
                        else if (nonzero != 0)
                        {
                            /* Old-style octal: now disallowed. */
                            done = E_TOKEN;
                            backup(c);
                            return ERRORTOKEN;
                        }
                    }
                }
                else
                {
                    /* Decimal */
                    c = decimal_tail();
                    if (c == 0)
                    {
                        return ERRORTOKEN;
                    }
                    {
                        /* Accept floating point numbers. */
                        if (c == '.')
                        {
                            c = nextc();
                            // fraction:
                            /* Fraction */
                            if (is_digit(c))
                            {
                                c = decimal_tail();
                                if (c == 0)
                                {
                                    return ERRORTOKEN;
                                }
                            }
                        }
                        if (c == 'e' || c == 'E')
                        {
                            int e;
                            // exponent:
                            e = c;
                            /* Exponent part */
                            c = nextc();
                            if (c == '+' || c == '-')
                            {
                                c = nextc();
                                if (!is_digit(c))
                                {
                                    done = E_TOKEN;
                                    backup(c);
                                    return ERRORTOKEN;
                                }
                            }
                            else if (!is_digit(c))
                            {
                                backup(c);
                                backup(e);
                                p = _buf.Substring(start, cur - start);
                                return NUMBER;
                            }
                            c = decimal_tail();
                            if (c == 0)
                            {
                                return ERRORTOKEN;
                            }
                        }
                        if (c == 'j' || c == 'J')
                        {
                            /* Imaginary part */
                            // imaginary:
                            c = nextc();
                        }
                    }
                }
                backup(c);
                p = _buf.Substring(start, cur - start);
                return NUMBER;
            }

            letter_quote:
            /* String */
            if (c == '\'' || c == '"')
            {
                int quote = c;
                int quote_size = 1;             /* 1 or 3 */
                int end_quote_size = 0;

                /* Find the quote size and start of string */
                c = nextc();
                if (c == quote)
                {
                    c = nextc();
                    if (c == quote)
                    {
                        quote_size = 3;
                    }
                    else
                    {
                        end_quote_size = 1;     /* empty string found */
                    }
                }
                if (c != quote)
                {
                    backup(c);
                }

                /* Get rest of string */
                while (end_quote_size != quote_size)
                {
                    c = nextc();
                    if (c == EOF)
                    {
                        if (quote_size == 3)
                        {
                            done = E_EOFS;
                        }
                        else
                        {
                            done = E_EOLS;
                        }
                        cur = inp;
                        return ERRORTOKEN;
                    }
                    if (quote_size == 1 && c == '\n')
                    {
                        done = E_EOLS;
                        cur = inp;
                        return ERRORTOKEN;
                    }
                    if (c == quote)
                    {
                        end_quote_size += 1;
                    }
                    else
                    {
                        end_quote_size = 0;
                        if (c == '\\')
                        {
                            nextc();  /* skip escaped char */
                        }
                    }
                }
                p = _buf.Substring(start, cur - start);
                return STRING;
            }

            /* Line continuation */
            if (c == '\\')
            {
                c = nextc();
                if (c != '\n')
                {
                    done = E_LINECONT;
                    cur = inp;
                    return ERRORTOKEN;
                }
                cont_line = 1;
                goto again; /* Read next line */
            }

            /* Check for two-character token */
            {
                int c2 = nextc();
                int token = _TwoChars(c, c2);
                if (token != OP)
                {
                    int c3 = nextc();
                    int token3 = _ThreeChars(c, c2, c3);
                    if (token3 != OP)
                    {
                        token = token3;
                    }
                    else
                    {
                        backup(c3);
                    }
                    p = _buf.Substring(start, cur - start);
                    return token;
                }
                backup(c2);
            }

            /* Keep track of parentheses nesting level */
            switch (c)
            {
                case '(':
                case '[':
                case '{':
                    level++;
                    break;
                case ')':
                case ']':
                case '}':
                    level--;
                    break;
            }

            /* Punctuation character */
            p = _buf.Substring(start, cur - start);
            return _OneChar(c);
        }

        #region PRIVATE_FUNCTIONS
        bool is_potential_identifier_start(int c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_' || (c >= 128);
        }

        bool is_potential_identifier_char(int c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '_' || (c >= 128);
        }

        bool is_digit(int c)
        {
            return (c >= '0' && c <= '9');
        }

        bool isxdigit(int c)
        {
            return is_digit(c) || ("ABCDEFabcdef".IndexOf((char)c) != -1);
        }

        int nextc()
        {
            if (cur != inp)
                return _buf[cur++]; /* Fast path */
            if (done != E_OK)
                return EOF;
            while (++end < _buf.Length)
                if (_buf[end] == '\n')
                {
                    if (start == 0) buf = cur;
                    line_start = cur;
                    inp = end;
                    lineno++;
                    return _buf[cur++];
                }
            done = E_EOF;
            return EOF;
        }

        void backup(int c)
        {
            cur--;
        }

        int decimal_tail()
        {
            int c;
            for (;;)
            {
                do
                {
                    c = nextc();
                } while (is_digit(c));
                if (c != '_')
                {
                    break;
                }
                c = nextc();
                if (!is_digit(c))
                {
                    done = E_TOKEN;
                    backup(c);
                    return 0;
                }
            }
            return c;
        }

        int _OneChar(int c)
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

        int _TwoChars(int c1, int c2)
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

        int _ThreeChars(int c1, int c2, int c3)
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
        static public readonly string[] _TokenNames = {
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
    /* This table must match the const ints in token.h! */
    "OP",
    "<ERRORTOKEN>",
    "COMMENT",
    "NL",
    "ENCODING",
    "<N_TOKENS>"
};
        const int EOF = -1;
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
        const int E_OK = 10;      /* No error */
        const int E_EOF = 11;      /* End Of File */
        const int E_INTR = 12;      /* Interrupted */
        const int E_TOKEN = 13;      /* Bad token */
        const int E_SYNTAX = 14;      /* Syntax error */
        const int E_NOMEM = 15;      /* Ran out of memory */
        const int E_DONE = 16;      /* Parsing complete */
        const int E_ERROR = 17;      /* Execution error */
        const int E_TABSPACE = 18;      /* Inconsistent mixing of tabs and spaces */
        const int E_OVERFLOW = 19;      /* Node had too many children */
        const int E_TOODEEP = 20;      /* Too many indentation levels */
        const int E_DEDENT = 21;      /* No matching outer block for dedent */
        const int E_DECODE = 22;      /* Error in decoding into Unicode */
        const int E_EOFS = 23;      /* EOF in triple-quoted string */
        const int E_EOLS = 24;      /* EOL in single-quoted string */
        const int E_LINECONT = 25;      /* Unexpected characters after a line continuation */
        const int E_IDENTIFIER = 26;     /* Invalid characters in identifier */
        const int E_BADSINGLE = 27;      /* Ill-formed single statement input */
        #endregion
    }

    class Parser
    {
        public stack p_stack;       /* Stack of parser states */
        public grammar p_grammar;   /* Grammar to use */
        public _node p_tree;        /* Top of parse tree */

        public Parser(grammar _grammar, short start)
        {
            p_grammar = _grammar;
            if (p_grammar.g_accel == 0)
                p_grammar.addAccelerators();
            p_tree = new _node(start);
            p_stack = new stack();
            p_stack.reset();
            p_stack.push(p_grammar.findDFA(start), p_tree);
        }

        int classify(int type, String str)
        {
            grammar g = p_grammar;
            int n = g.g_ll.ll_nlabels;
            if(type == NAME)
            for (int i = 0; i < n; i++)
            {
                label l = g.g_ll.ll_label[i];
                if (l.lb_type == NAME && l.lb_str.Equals(str))
                    return i;
            }
            for (int i = 0; i < n; i++)
            {
                label l = g.g_ll.ll_label[i];
                if (l.lb_type == type && l.lb_str.Equals(""))
                    return i;
            }
            return -1; // Illegal token
        }

        public int addToken(int type, String str, int lineno, int col_offset) // expected_ret
        {
            int ilabel = classify(type, str) , err;
            if (ilabel < 0)
                return E_SYNTAX;
            /* Loop until the token is shifted or an error occurred */
            for (;;)
            {
                /* Fetch the current dfa and state */
                dfa d = p_stack.s_top.s_dfa;
                state s = d.d_state[p_stack.s_top.s_state];

                /* Check accelerator */
                if (s.s_lower <= ilabel && ilabel < s.s_upper)
                {
                    int x = s.s_accel[ilabel - s.s_lower];
                    if (x != -1)
                    {
                        if ((x & (1 << 7))!=0)
                        {
                            /* Push non-terminal */
                            int nt = (x >> 8) + 256;
                            int arrow = x & ((1 << 7) - 1);
                            dfa d1 = p_grammar.findDFA(nt);
                            if ((err = p_stack.doPush(nt, d1, arrow, lineno, col_offset)) > 0)
                                return err;
                            continue;
                        }

                        /* Shift the token */
                        if ((err = p_stack.doShift(type, str,
                                        x, lineno, col_offset)) > 0)
                            return err;

                        /* Pop while we are in an accept-only state */
                        while (true) {
                            s = d.d_state[p_stack.s_top.s_state];
                            if (s.s_accept == 0 || s.s_narcs != 1) break;
                            p_stack.pop();
                            if (p_stack.empty())
                            {
                                return E_DONE;
                            }
                            d = p_stack.s_top.s_dfa;
                        }
                        return E_OK;
                    }
                }

                if (s.s_accept != 0) {
                    /* Pop this dfa and try again */
                    p_stack.pop();
                    if (p_stack.empty()) {
                        return E_SYNTAX;
                    }
                    continue;
                }

                /* Stuck, report syntax error */
                /*
                 * if (expected_ret) {
                    if (s->s_lower == s->s_upper - 1) {
                        // Only one possible expected token 
                        * expected_ret = ps->p_grammar->
                            g_ll.ll_label[s->s_lower].lb_type;
                    }
                    else
                        * expected_ret = -1;
                }*/
                return E_SYNTAX;
            }
        }

        #region SOME_CONSTANTS
        const int EOF = -1;
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
        const int E_OK = 10;      /* No error */
        const int E_EOF = 11;      /* End Of File */
        const int E_INTR = 12;      /* Interrupted */
        const int E_TOKEN = 13;      /* Bad token */
        const int E_SYNTAX = 14;      /* Syntax error */
        const int E_NOMEM = 15;      /* Ran out of memory */
        const int E_DONE = 16;      /* Parsing complete */
        const int E_ERROR = 17;      /* Execution error */
        const int E_TABSPACE = 18;      /* Inconsistent mixing of tabs and spaces */
        const int E_OVERFLOW = 19;      /* Node had too many children */
        const int E_TOODEEP = 20;      /* Too many indentation levels */
        const int E_DEDENT = 21;      /* No matching outer block for dedent */
        const int E_DECODE = 22;      /* Error in decoding into Unicode */
        const int E_EOFS = 23;      /* EOF in triple-quoted string */
        const int E_EOLS = 24;      /* EOL in single-quoted string */
        const int E_LINECONT = 25;      /* Unexpected characters after a line continuation */
        const int E_IDENTIFIER = 26;     /* Invalid characters in identifier */
        const int E_BADSINGLE = 27;      /* Ill-formed single statement input */
        #endregion
    }

    class Compiler
    {
        public static void run(String S, Main M)
        {
            // 编译
            try {
                M.clearLog();
                M.printLog("开始编译...\n");

                S += "\n\n"; // newline hack
                Tokenizer t = new Tokenizer(S);
                grammar g = Grammar._Grammar;
                while (true) {
                    _node n = parsetok(t, g, 256);
                    if (n != null)
                    {
                        M.printLog("语法树信息：");
                        M.printLog(show_node(n));
                    }
                    else
                    {
                        M.printLog("语法错误。");
                        break;
                    }
                    if (t.cur >= S.Length - 3)
                    {
                        M.printLog("语法分析结束。");
                        break;
                    }
                }

            }
            catch(Exception e)
            {
                M.printOutput("编译器内部错误：\n"+e.ToString());
                return;
            }

            // 运行
            M.printOutput("开始运行"+M.Filename+"...\n");
            M.openProcess();
        }

        public static void debug(String S, Main M)
        {
            // 编译
            try
            {
                M.clearLog();
                M.printLog("开始编译...\n");

                S += "\n\n"; // newline hack
                Tokenizer t = new Tokenizer(S);
                grammar g = Grammar._Grammar;
                _node n = parsetok(t, g, 256);
                if (n != null)
                {
                    M.printLog("语法树信息：\n");
                    M.printLog(show_node(n));
                }
                else
                {
                    M.printLog("语法错误。");
                }

            }
            catch (Exception e)
            {
                M.printOutput("编译器内部错误：\n" + e.ToString());
                return;
            }

            // 运行并调试
            Process p;
            M.printOutput("开始调试" + M.Filename + "...\n");
            M.openDebugger(out p);
            M.debugging = true;
            int lineno = 0;
            bool sendN = false, sendS = false, sendC = true;
            bool running = false, hasRead = false;

            // 设置断点
            foreach(var i in M.breakpoint) {
                if(i.Value)
                {
                    p.StandardInput.WriteLine("b " + i.Key.ToString());
                    p.StandardInput.Flush();
                }
            }

            // bool waitForRead = true, hadRead = false, stepOn = false;
            new Thread(()=> {
                for (;;)
                {
                    // waitForRead 锁会引起读写阻塞，难以处理多行输入，故注释之
                    //if (waitForRead)
                    {
                        //waitForRead = false;

                        string line = p.StandardOutput.ReadLine();
                        M.printLog("found: " + line);

                        // 删除(Pdb)，可能在任意位置出现，比较bob
                        if (line.StartsWith("(Pdb) ")) line = line.Substring(6);

                        // 判断起始字符
                        if (line.StartsWith("> "))
                        { // 当前行号和历史栈信息
                            // 获取行号
                            int numStart = line.IndexOf('(')+1;
                            lineno = 0;
                            for(;line[numStart]>='0' && line[numStart] <= '9'; numStart++)
                            {
                                lineno = lineno * 10 + line[numStart] - '0';
                            }
                            M.printLog("当前运行第" + lineno.ToString() + "行。\n");

                        }
                        else if(line.StartsWith("-> "))
                        { // 当前运行光标位置。
                            if (running)
                            {
                                running = false;
                            // M.printOutput("第"+lineno.ToString()+"行遇到断点停留。");
                            }
                            // 刷新断点监视
                            List<String> ls, lv;
                            ls = M.getWatch();
                            lv = new List<String>();
                            for(int i=0; i<ls.Count; i++)
                            {
                                p.StandardInput.WriteLine("p " + ls[i]);
                                p.StandardInput.Flush();
                                Thread.Sleep(7);
                                string li = p.StandardOutput.ReadLine();
                                if (li.StartsWith("(Pdb) ")) li = li.Substring(6);
                                lv.Add(li);
                            }
                            M.refreshWatch(ls, lv);

                            // 刷新当前行号
                            M.setLine(lineno);

                            hasRead = true;
                        }
                        else if (line.StartsWith("Breakpoint") || line.StartsWith("End of file"))
                        { // 断点相关
                        }
                        else if (line.StartsWith("--Return--") || line.IndexOf("finish")!=-1)
                        { // 程序结束
                            M.printOutput("\n程序已返回。\n");
                            M.clearLine();
                            p.Close();
                            p = null;
                            M.debugging = false;
                            Thread.CurrentThread.Abort();
                        }
                        else
                        { // 当前输出。打到屏幕上
                            M.printOutput(line);
                        }
                    }
                    Thread.Sleep(37);
                }
            }).Start(); // and it will never stop.

            new Thread(()=>
            {
                while (!hasRead)
                {
                    Thread.Sleep(17);
                }
                for (;;)
                {
                    if (p == null)
                    {
                        Thread.CurrentThread.Abort();
                        return;
                    }
                    if (sendN)
                    {   // 下一步
                        p.StandardInput.WriteLine('n');
                        p.StandardInput.Flush();
                        sendN = false;
                        //waitForRead = true;
                        continue;
                    }
                    if (sendS)
                    {   // 单步进入
                        p.StandardInput.WriteLine('s');
                        p.StandardInput.Flush();
                        sendS = false;
                        //waitForRead = true;
                        continue;
                    }
                    if (sendC)
                    {   // 继续运行
                        p.StandardInput.WriteLine('c');
                        p.StandardInput.Flush();
                        sendC = false;
                        running = true;
                        //waitForRead = true;
                        continue;
                    }
                    Thread.Sleep(17);
                }
            }).Start();

            new Thread(() =>
            {
                Thread.Sleep(377);

                for (;;)
                {
                    if (p == null)
                    {
                        Thread.CurrentThread.Abort();
                        return;
                    }
                   /* if (hadRead)
                    {
                        hadRead = false;

                        // Bob with lineno
                        M.printOutput("行号 " + lineno.ToString());

                        // TODO: 判断是否遇到断点
                        bool reachBreak = false;
                        
                        // 如果在继续运行模式，且未遇到断点，就自动下一步
                        if (!reachBreak && stepOn)
                        {
                            sendN = true;
                        }
                        continue;
                    }*/
                    if (M.btnNext)
                    {
                        // 断点状态下按了下一步按钮
                        M.btnNext = false;

                        // 为防止冲突，先等待sendN释放
                        while (sendN)
                        {
                            Thread.Sleep(7);
                        }
                        sendN = true;
                    }
                    if (M.btnStep)
                    {
                        // 断点状态下按了下一步按钮
                        M.btnStep = false;

                        // 为防止冲突，先等待sendS释放
                        while (sendS)
                        {
                            Thread.Sleep(7);
                        }
                        sendS = true;
                    }
                    if (M.btnContinue)
                    {
                        // 断点状态下按了下一步按钮
                        M.btnContinue = false;

                        // 为防止冲突，先等待sendC释放
                        while (sendC)
                        {
                            Thread.Sleep(7);
                        }
                        sendC = true;
                    }
                    Thread.Sleep(7);
                }
            }).Start();
            
        }

        public static int getname(String S, int pos, out String name, out List<int> namelist)
        {
            Tokenizer t = new Tokenizer(S);
            int type = 0;
            name = "";
            namelist = null;
            while (t.cur < pos)
            {
                type = t.get(out name);
            }
            if (type != NAME && t.cur == pos)
            {
                type = t.get(out name);
            }
            if (type != NAME)
            {
                name = "";
                return 0;
            }
            int possa = t.cur - name.Length;
            namelist = new List<int>();
            String oldname;
            t = new Tokenizer(S);
            while (true)
            {
                type = t.get(out oldname);
                if (type == NAME && name == oldname && t.cur-name.Length != possa)
                {
                    namelist.Add(t.cur - name.Length);
                }
                else if (type == EOF || type == OP)
                {
                    break;
                }
            }
            return possa;
        }

        public static void rename(ref String S, int pos, String newname)
        {
            Tokenizer t = new Tokenizer(S);
            String name = "", oldname;
            int type = 0;
            while (t.cur<pos)
            {
                type = t.get(out name);
            }
            if (type != NAME || name.Length <= 0)
            {
                return;
            }
            String mid = "";
            while (mid.Length < name.Length) mid += "@";
            oldname = name;
            t = new Tokenizer(S);
            while (true)
            {
                type = t.get(out name);
                if(type == NAME && name == oldname)
                {
                    S = S.Remove(t.cur - name.Length, name.Length).Insert(t.cur - name.Length, mid);
                }
                else if(type == EOF || type == OP)
                {
                    break;
                }
            }
            S = S.Replace(mid, newname);
        }

        #region PRIVATE_FUNCTIONS

        static _node parsetok(Tokenizer tok, grammar g, short start)
        //  int flags, perrdetail *err_ret
        {
            Parser ps = new Parser(g, start);
            _node n = null;
            int started = 0;
            int error = 0;
            for (;;)
            {
                int type;
                String str;
                int col_offset = -1;

                type = tok.get(out str);
                if (type == ERRORTOKEN)
                {
                    //err_ret->error = tok->done;
                    error = tok.done;
                    break;
                }
                if (type == ENDMARKER && started != 0)
                {
                    type = NEWLINE; /* Add an extra newline */
                    started = 0;
                    if (tok.indent != 0)
                    {
                        tok.pendin = -tok.indent;
                        tok.indent = 0;
                    }
                }
                else started = 1;
                /*
                if (a != NULL && a >= tok->line_start)
                {
                    col_offset = Py_SAFE_DOWNCAST(a - tok->line_start, intptr_t, int);
                }
                else
                {
                    col_offset = -1;
                }
                */
                // err_ret->error = ...
                error = ps.addToken((int)type, str,
                                       tok.lineno, col_offset);
                if (error != E_OK)
                {
                    if (error != E_DONE)
                    {
                        // err_ret->token = type;
                    }
                    break;
                }
            }

            if (error == E_DONE)
            {
                n = ps.p_tree;
                ps.p_tree = null;
            }
            else
                n = null;

            if (n == null)
            {
                if (tok.done == E_EOF)
                    //err_ret->error = E_EOF;
                    error = E_EOF;
                //err_ret->lineno = tok->lineno;
                /*if (tok->buf != NULL)
                {
                    size_t len;
                    assert(tok->cur - tok->buf < INT_MAX);
                    err_ret->offset = (int)(tok->cur - tok->buf);
                    len = tok->inp - tok->buf;
                    err_ret->text = (char*)PyObject_MALLOC(len + 1);
                    if (err_ret->text != NULL)
                    {
                        if (len > 0)
                            strncpy(err_ret->text, tok->buf, len);
                        err_ret->text[len] = '\0';
                    }
                }*/
            }
            /*else if (tok->encoding != NULL)
            {
                node* r = PyNode_New(encoding_decl);
                if (r)
                    r->n_str = PyObject_MALLOC(strlen(tok->encoding) + 1);
                if (!r || !r->n_str)
                {
                    err_ret->error = E_NOMEM;
                    if (r)
                        PyObject_FREE(r);
                    n = NULL;
                    goto done;
                }
                strcpy(r->n_str, tok->encoding);
                tok->encoding = NULL;
                r->n_nchildren = 1;
                r->n_child = n;
                n = r;
            }*/

            done:
            return n;
        }

        static String show_node(_node n)
        {
            String output = "";
            dfs_node_1(n, 0, ref output);
            //dfs_node_2(n);

            return output;
        }

        static void dfs_node_1(_node n, int depth, ref String s)
        {
            if (n.n_nchildren == 1) dfs_node_1(n.n_child[0], depth, ref s);
                // ignore limb nodes with only one child.
            else if (n.n_nchildren == 0)
            {
                for (int i = 0; i < depth; i++) s += "\t";
                s += "Leaf: " + Tokenizer._TokenNames[n.n_type] + " " + n.n_str + "\n";
            }
            else
            {
                for (int i = 0; i < depth; i++) s += "\t";
                s += "Node " + n.n_type + " with " + n.n_nchildren + " children:\n";
                foreach(_node _n in n.n_child){
                    dfs_node_1(_n, depth + 1, ref s);
                }
            }
        }

        #endregion

        #region SOME_CONSTANTS
        const int EOF = -1;
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
        const int E_OK = 10;      /* No error */
        const int E_EOF = 11;      /* End Of File */
        const int E_INTR = 12;      /* Interrupted */
        const int E_TOKEN = 13;      /* Bad token */
        const int E_SYNTAX = 14;      /* Syntax error */
        const int E_NOMEM = 15;      /* Ran out of memory */
        const int E_DONE = 16;      /* Parsing complete */
        const int E_ERROR = 17;      /* Execution error */
        const int E_TABSPACE = 18;      /* Inconsistent mixing of tabs and spaces */
        const int E_OVERFLOW = 19;      /* Node had too many children */
        const int E_TOODEEP = 20;      /* Too many indentation levels */
        const int E_DEDENT = 21;      /* No matching outer block for dedent */
        const int E_DECODE = 22;      /* Error in decoding into Unicode */
        const int E_EOFS = 23;      /* EOF in triple-quoted string */
        const int E_EOLS = 24;      /* EOL in single-quoted string */
        const int E_LINECONT = 25;      /* Unexpected characters after a line continuation */
        const int E_IDENTIFIER = 26;     /* Invalid characters in identifier */
        const int E_BADSINGLE = 27;      /* Ill-formed single statement input */
        #endregion
    }

}