using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DevPython
{
    public struct label
    {
        public label(int a, String b) { lb_type = a; lb_str = b; }
        public int lb_type;
        public String lb_str;
    };

    /* A list of labels */
    public struct labellist
    {
        public int ll_nlabels;
        public label[] ll_label;
    };

    /* An arc from one state to another */
    public struct arc
    {
        public arc(short a, short b) { a_lbl = a; a_arrow = b; }
        public short a_lbl;          /* Label of this arc */
        public short a_arrow;        /* State where this arc goes to */
    };

    /* A state in a DFA */
    public class state
    {
        public state(int a, arc[] b) { s_narcs = a; s_arc = b; }
        public int s_narcs;
        public arc[] s_arc;         /* Array of arcs */
                                    /* Optional accelerators */
        public int s_lower;       /* Lowest label index */
        public int s_upper;       /* Highest label index */
        public int[] s_accel;       /* Accelerator */
        public int s_accept;      /* Nonzero for accepting state */
    };

    /* A DFA */
    public class dfa
    {
        public int d_type;        /* Non-terminal this represents */
        public String d_name;     /* For printing */
        public int d_initial;     /* Initial state */
        public int d_nstates;
        public state[] d_state;   /* Array of states */
        public byte[] d_first;

        public dfa(int a, String b, int c, int d, state[] e, String f)
        {
            d_type = a;
            d_name = b;
            d_initial = c;
            d_nstates = d;
            d_state = e;
            d_first = new byte[23];
            for (int i = 0; i < 23; i++)
                d_first[i] = (byte)((f[i * 3] - '0') * 64 + (f[i * 3 + 1] - '0') * 8 + (f[i * 3 + 2] - '0'));
        }
    };

    /* A grammar */
    public class grammar
    {
        public int g_ndfas;
        public dfa[] g_dfa;       /* Array of DFAs */
        public labellist g_ll;
        public int g_start;       /* Start symbol of the grammar */
        public int g_accel;       /* Set if accelerators present */

        public void addAccelerators()
        {
            g_accel = 1;
            for (int i = 0; i < g_ndfas; i++)
            {
                _fixdfa(g_dfa[i]);
            }
        }
        void _fixdfa(dfa d)
        {
            for (int i = 0; i < d.d_nstates; i++)
            {
                _fixstate(d.d_state[i]);
            }
        }
        void _fixstate(state s)
        {
            int k;
            int nl = g_ll.ll_nlabels;
            s.s_accept = 0;
            int[] accel = new int[nl];
            for (k = 0; k < nl; k++)
                accel[k] = -1;
            for (k = s.s_narcs; --k >= 0;)
            {
                arc a = s.s_arc[s.s_narcs - k - 1];
                int lbl = a.a_lbl;
                label l = g_ll.ll_label[lbl];
                int type = l.lb_type;
                /*if (a.a_arrow >= (1 << 7))
                {
                    printf("XXX too many states!\n");
                    continue;
                }*/
                if (type >= 256)
                {
                    dfa d1 = g_dfa[type - 256];
                    /*if (type - NT_OFFSET >= (1 << 7))
                    {
                        printf("XXX too high nonterminal number!\n");
                        continue;
                    }*/
                    for (int ibit = 0; ibit < g_ll.ll_nlabels; ibit++)
                    {
                        if ((d1.d_first[ibit / 8] & (1 << (ibit % 8))) > 0)
                        {
                            /*if (accel[ibit] != -1)
                                printf("XXX ambiguity!\n");*/
                            accel[ibit] = a.a_arrow | (1 << 7) |
                                ((type - 256) << 8);
                        }
                    }
                }
                else if (lbl == 0)
                    s.s_accept = 1;
                else if (lbl >= 0 && lbl < nl)
                    accel[lbl] = a.a_arrow;
            }
            while (nl > 0 && accel[nl - 1] == -1)
                nl--;
            for (k = 0; k < nl && accel[k] == -1;)
                k++;
            if (k < nl)
            {
                s.s_accel = new int[nl - k];
                s.s_lower = k;
                s.s_upper = nl;
                for (int i = 0; k < nl; i++, k++)
                    s.s_accel[i] = accel[k];
            }
        }
        public dfa findDFA(int type)
        {
            dfa d = g_dfa[type - 256];
            Debug.Assert(d.d_type == type);
            return d;
        }
    };

    public class _node
    {
        public short n_type;
        public String n_str;
        public int n_lineno;
        public int n_col_offset;
        public int n_nchildren;
        public _node[] n_child;
        
        public _node(short t=0)
        {
            n_type = t;
        }
        public int addChild(int type, String str, int lineno, int col_offset)
        {
            return 0;
        }
    };

    public class stackentry
    {
        public int s_state;       /* State in current DFA */
        public dfa s_dfa;         /* Current DFA */
        public _node s_parent;    /* Where to add next node */
    }

    public class stack
    {
        public int __s_top;       /* Top entry */
        public stackentry s_top
        {
            get { return s_base[__s_top]; }
            set { s_base[__s_top] = value; }
        }
        public stackentry[] s_base;    /* Array of stack entries */
        public stack()
        {
            s_base = new stackentry[MAXSTACK];
        }
        public void reset()
        {
            __s_top = MAXSTACK;
        }
        public int push(dfa d, _node parent)
        {
            if (__s_top == 0)
                return -1;// Stack overflow!!
            __s_top--;
            s_top = new stackentry();
            s_top.s_dfa = d;
            s_top.s_parent = parent;
            s_top.s_state = 0;
            return 0;
        }
        public bool empty()
        {
            return __s_top == MAXSTACK;
        }
        public void pop()
        {
            __s_top++;
        }
        public int doPush(int type, dfa d, int newstate, int lineno, int col_offset)
        {
            _node n = s_top.s_parent;
            Debug.Assert(!empty());
            int err = n.addChild(type, "", lineno, col_offset);
            if (err != 0) return err;
            s_top.s_state = newstate;
            return push(d, n.n_child[n.n_nchildren - 1]);
        }
        public int doShift(int type, String str, int newstate, int lineno, int col_offset)
        {
            Debug.Assert(!empty());
            int err = s_top.s_parent.addChild(type, str, lineno, col_offset);
            if (err != 0)
                return err;
            s_top.s_state = newstate;
            return 0;
        }
        static int MAXSTACK = 2048;
    }

    class Grammar
    {
        #region DFA_CONSTANTS

        static Grammar() {
            _Grammar = new grammar();
            _Grammar.g_ndfas = 87;
            _Grammar.g_dfa = dfas;
            _Grammar.g_ll = new labellist();
            _Grammar.g_ll.ll_nlabels = 177;
            _Grammar.g_ll.ll_label = labels;
            _Grammar.g_start = 256;
        }
        static arc[] arcs_0_0 = {
    new arc(2,1),
    new arc(3,1),
    new arc(4,2),
};
        static arc[] arcs_0_1 = {
    new arc(0,1),
};
        static arc[] arcs_0_2 = {
    new arc(2,1),
};
        static state[] states_0 = {
    new state(3,arcs_0_0),
    new state(1,arcs_0_1),
    new state(1,arcs_0_2),
};
        static arc[] arcs_1_0 = {
    new arc(2,0),
    new arc(6,0),
    new arc(7,1),
};
        static arc[] arcs_1_1 = {
    new arc(0,1),
};
        static state[] states_1 = {
    new state(3,arcs_1_0),
    new state(1,arcs_1_1),
};
        static arc[] arcs_2_0 = {
    new arc(9,1),
};
        static arc[] arcs_2_1 = {
    new arc(2,1),
    new arc(7,2),
};
        static arc[] arcs_2_2 = {
    new arc(0,2),
};
        static state[] states_2 = {
    new state(1,arcs_2_0),
    new state(2,arcs_2_1),
    new state(1,arcs_2_2),
};
        static arc[] arcs_3_0 = {
    new arc(11,1),
};
        static arc[] arcs_3_1 = {
    new arc(12,2),
};
        static arc[] arcs_3_2 = {
    new arc(13,3),
    new arc(2,4),
};
        static arc[] arcs_3_3 = {
    new arc(14,5),
    new arc(15,6),
};
        static arc[] arcs_3_4 = {
    new arc(0,4),
};
        static arc[] arcs_3_5 = {
    new arc(15,6),
};
        static arc[] arcs_3_6 = {
    new arc(2,4),
};
        static state[] states_3 = {
    new state(1,arcs_3_0),
    new state(1,arcs_3_1),
    new state(2,arcs_3_2),
    new state(2,arcs_3_3),
    new state(1,arcs_3_4),
    new state(1,arcs_3_5),
    new state(1,arcs_3_6),
};
        static arc[] arcs_4_0 = {
    new arc(10,1),
};
        static arc[] arcs_4_1 = {
    new arc(10,1),
    new arc(0,1),
};
        static state[] states_4 = {
    new state(1,arcs_4_0),
    new state(2,arcs_4_1),
};
        static arc[] arcs_5_0 = {
    new arc(16,1),
};
        static arc[] arcs_5_1 = {
    new arc(18,2),
    new arc(19,2),
    new arc(20,2),
};
        static arc[] arcs_5_2 = {
    new arc(0,2),
};
        static state[] states_5 = {
    new state(1,arcs_5_0),
    new state(3,arcs_5_1),
    new state(1,arcs_5_2),
};
        static arc[] arcs_6_0 = {
    new arc(21,1),
};
        static arc[] arcs_6_1 = {
    new arc(19,2),
};
        static arc[] arcs_6_2 = {
    new arc(0,2),
};
        static state[] states_6 = {
    new state(1,arcs_6_0),
    new state(1,arcs_6_1),
    new state(1,arcs_6_2),
};
        static arc[] arcs_7_0 = {
    new arc(22,1),
};
        static arc[] arcs_7_1 = {
    new arc(23,2),
};
        static arc[] arcs_7_2 = {
    new arc(24,3),
};
        static arc[] arcs_7_3 = {
    new arc(25,4),
    new arc(27,5),
};
        static arc[] arcs_7_4 = {
    new arc(26,6),
};
        static arc[] arcs_7_5 = {
    new arc(28,7),
};
        static arc[] arcs_7_6 = {
    new arc(27,5),
};
        static arc[] arcs_7_7 = {
    new arc(0,7),
};
        static state[] states_7 = {
    new state(1,arcs_7_0),
    new state(1,arcs_7_1),
    new state(1,arcs_7_2),
    new state(2,arcs_7_3),
    new state(1,arcs_7_4),
    new state(1,arcs_7_5),
    new state(1,arcs_7_6),
    new state(1,arcs_7_7),
};
        static arc[] arcs_8_0 = {
    new arc(13,1),
};
        static arc[] arcs_8_1 = {
    new arc(29,2),
    new arc(15,3),
};
        static arc[] arcs_8_2 = {
    new arc(15,3),
};
        static arc[] arcs_8_3 = {
    new arc(0,3),
};
        static state[] states_8 = {
    new state(1,arcs_8_0),
    new state(2,arcs_8_1),
    new state(1,arcs_8_2),
    new state(1,arcs_8_3),
};
        static arc[] arcs_9_0 = {
    new arc(30,1),
    new arc(33,2),
    new arc(34,3),
};
        static arc[] arcs_9_1 = {
    new arc(31,4),
    new arc(32,5),
    new arc(0,1),
};
        static arc[] arcs_9_2 = {
    new arc(30,6),
    new arc(32,7),
    new arc(0,2),
};
        static arc[] arcs_9_3 = {
    new arc(30,8),
};
        static arc[] arcs_9_4 = {
    new arc(26,9),
};
        static arc[] arcs_9_5 = {
    new arc(30,10),
    new arc(33,11),
    new arc(34,3),
    new arc(0,5),
};
        static arc[] arcs_9_6 = {
    new arc(32,7),
    new arc(0,6),
};
        static arc[] arcs_9_7 = {
    new arc(30,12),
    new arc(34,3),
    new arc(0,7),
};
        static arc[] arcs_9_8 = {
    new arc(32,13),
    new arc(0,8),
};
        static arc[] arcs_9_9 = {
    new arc(32,5),
    new arc(0,9),
};
        static arc[] arcs_9_10 = {
    new arc(32,5),
    new arc(31,4),
    new arc(0,10),
};
        static arc[] arcs_9_11 = {
    new arc(30,14),
    new arc(32,15),
    new arc(0,11),
};
        static arc[] arcs_9_12 = {
    new arc(32,7),
    new arc(31,16),
    new arc(0,12),
};
        static arc[] arcs_9_13 = {
    new arc(0,13),
};
        static arc[] arcs_9_14 = {
    new arc(32,15),
    new arc(0,14),
};
        static arc[] arcs_9_15 = {
    new arc(30,17),
    new arc(34,3),
    new arc(0,15),
};
        static arc[] arcs_9_16 = {
    new arc(26,6),
};
        static arc[] arcs_9_17 = {
    new arc(32,15),
    new arc(31,18),
    new arc(0,17),
};
        static arc[] arcs_9_18 = {
    new arc(26,14),
};
        static state[] states_9 = {
    new state(3,arcs_9_0),
    new state(3,arcs_9_1),
    new state(3,arcs_9_2),
    new state(1,arcs_9_3),
    new state(1,arcs_9_4),
    new state(4,arcs_9_5),
    new state(2,arcs_9_6),
    new state(3,arcs_9_7),
    new state(2,arcs_9_8),
    new state(2,arcs_9_9),
    new state(3,arcs_9_10),
    new state(3,arcs_9_11),
    new state(3,arcs_9_12),
    new state(1,arcs_9_13),
    new state(2,arcs_9_14),
    new state(3,arcs_9_15),
    new state(1,arcs_9_16),
    new state(3,arcs_9_17),
    new state(1,arcs_9_18),
};
        static arc[] arcs_10_0 = {
    new arc(23,1),
};
        static arc[] arcs_10_1 = {
    new arc(27,2),
    new arc(0,1),
};
        static arc[] arcs_10_2 = {
    new arc(26,3),
};
        static arc[] arcs_10_3 = {
    new arc(0,3),
};
        static state[] states_10 = {
    new state(1,arcs_10_0),
    new state(2,arcs_10_1),
    new state(1,arcs_10_2),
    new state(1,arcs_10_3),
};
        static arc[] arcs_11_0 = {
    new arc(36,1),
    new arc(33,2),
    new arc(34,3),
};
        static arc[] arcs_11_1 = {
    new arc(31,4),
    new arc(32,5),
    new arc(0,1),
};
        static arc[] arcs_11_2 = {
    new arc(36,6),
    new arc(32,7),
    new arc(0,2),
};
        static arc[] arcs_11_3 = {
    new arc(36,8),
};
        static arc[] arcs_11_4 = {
    new arc(26,9),
};
        static arc[] arcs_11_5 = {
    new arc(36,10),
    new arc(33,11),
    new arc(34,3),
    new arc(0,5),
};
        static arc[] arcs_11_6 = {
    new arc(32,7),
    new arc(0,6),
};
        static arc[] arcs_11_7 = {
    new arc(36,12),
    new arc(34,3),
    new arc(0,7),
};
        static arc[] arcs_11_8 = {
    new arc(32,13),
    new arc(0,8),
};
        static arc[] arcs_11_9 = {
    new arc(32,5),
    new arc(0,9),
};
        static arc[] arcs_11_10 = {
    new arc(32,5),
    new arc(31,4),
    new arc(0,10),
};
        static arc[] arcs_11_11 = {
    new arc(36,14),
    new arc(32,15),
    new arc(0,11),
};
        static arc[] arcs_11_12 = {
    new arc(32,7),
    new arc(31,16),
    new arc(0,12),
};
        static arc[] arcs_11_13 = {
    new arc(0,13),
};
        static arc[] arcs_11_14 = {
    new arc(32,15),
    new arc(0,14),
};
        static arc[] arcs_11_15 = {
    new arc(36,17),
    new arc(34,3),
    new arc(0,15),
};
        static arc[] arcs_11_16 = {
    new arc(26,6),
};
        static arc[] arcs_11_17 = {
    new arc(32,15),
    new arc(31,18),
    new arc(0,17),
};
        static arc[] arcs_11_18 = {
    new arc(26,14),
};
        static state[] states_11 = {
    new state(3,arcs_11_0),
    new state(3,arcs_11_1),
    new state(3,arcs_11_2),
    new state(1,arcs_11_3),
    new state(1,arcs_11_4),
    new state(4,arcs_11_5),
    new state(2,arcs_11_6),
    new state(3,arcs_11_7),
    new state(2,arcs_11_8),
    new state(2,arcs_11_9),
    new state(3,arcs_11_10),
    new state(3,arcs_11_11),
    new state(3,arcs_11_12),
    new state(1,arcs_11_13),
    new state(2,arcs_11_14),
    new state(3,arcs_11_15),
    new state(1,arcs_11_16),
    new state(3,arcs_11_17),
    new state(1,arcs_11_18),
};
        static arc[] arcs_12_0 = {
    new arc(23,1),
};
        static arc[] arcs_12_1 = {
    new arc(0,1),
};
        static state[] states_12 = {
    new state(1,arcs_12_0),
    new state(1,arcs_12_1),
};
        static arc[] arcs_13_0 = {
    new arc(3,1),
    new arc(4,1),
};
        static arc[] arcs_13_1 = {
    new arc(0,1),
};
        static state[] states_13 = {
    new state(2,arcs_13_0),
    new state(1,arcs_13_1),
};
        static arc[] arcs_14_0 = {
    new arc(37,1),
};
        static arc[] arcs_14_1 = {
    new arc(38,2),
    new arc(2,3),
};
        static arc[] arcs_14_2 = {
    new arc(37,1),
    new arc(2,3),
};
        static arc[] arcs_14_3 = {
    new arc(0,3),
};
        static state[] states_14 = {
    new state(1,arcs_14_0),
    new state(2,arcs_14_1),
    new state(2,arcs_14_2),
    new state(1,arcs_14_3),
};
        static arc[] arcs_15_0 = {
    new arc(39,1),
    new arc(40,1),
    new arc(41,1),
    new arc(42,1),
    new arc(43,1),
    new arc(44,1),
    new arc(45,1),
    new arc(46,1),
};
        static arc[] arcs_15_1 = {
    new arc(0,1),
};
        static state[] states_15 = {
    new state(8,arcs_15_0),
    new state(1,arcs_15_1),
};
        static arc[] arcs_16_0 = {
    new arc(47,1),
};
        static arc[] arcs_16_1 = {
    new arc(48,2),
    new arc(49,3),
    new arc(31,4),
    new arc(0,1),
};
        static arc[] arcs_16_2 = {
    new arc(0,2),
};
        static arc[] arcs_16_3 = {
    new arc(50,2),
    new arc(9,2),
};
        static arc[] arcs_16_4 = {
    new arc(50,5),
    new arc(47,5),
};
        static arc[] arcs_16_5 = {
    new arc(31,4),
    new arc(0,5),
};
        static state[] states_16 = {
    new state(1,arcs_16_0),
    new state(4,arcs_16_1),
    new state(1,arcs_16_2),
    new state(2,arcs_16_3),
    new state(2,arcs_16_4),
    new state(2,arcs_16_5),
};
        static arc[] arcs_17_0 = {
    new arc(27,1),
};
        static arc[] arcs_17_1 = {
    new arc(26,2),
};
        static arc[] arcs_17_2 = {
    new arc(31,3),
    new arc(0,2),
};
        static arc[] arcs_17_3 = {
    new arc(26,4),
};
        static arc[] arcs_17_4 = {
    new arc(0,4),
};
        static state[] states_17 = {
    new state(1,arcs_17_0),
    new state(1,arcs_17_1),
    new state(2,arcs_17_2),
    new state(1,arcs_17_3),
    new state(1,arcs_17_4),
};
        static arc[] arcs_18_0 = {
    new arc(26,1),
    new arc(51,1),
};
        static arc[] arcs_18_1 = {
    new arc(32,2),
    new arc(0,1),
};
        static arc[] arcs_18_2 = {
    new arc(26,1),
    new arc(51,1),
    new arc(0,2),
};
        static state[] states_18 = {
    new state(2,arcs_18_0),
    new state(2,arcs_18_1),
    new state(3,arcs_18_2),
};
        static arc[] arcs_19_0 = {
    new arc(52,1),
    new arc(53,1),
    new arc(54,1),
    new arc(55,1),
    new arc(56,1),
    new arc(57,1),
    new arc(58,1),
    new arc(59,1),
    new arc(60,1),
    new arc(61,1),
    new arc(62,1),
    new arc(63,1),
    new arc(64,1),
};
        static arc[] arcs_19_1 = {
    new arc(0,1),
};
        static state[] states_19 = {
    new state(13,arcs_19_0),
    new state(1,arcs_19_1),
};
        static arc[] arcs_20_0 = {
    new arc(65,1),
};
        static arc[] arcs_20_1 = {
    new arc(66,2),
};
        static arc[] arcs_20_2 = {
    new arc(0,2),
};
        static state[] states_20 = {
    new state(1,arcs_20_0),
    new state(1,arcs_20_1),
    new state(1,arcs_20_2),
};
        static arc[] arcs_21_0 = {
    new arc(67,1),
};
        static arc[] arcs_21_1 = {
    new arc(0,1),
};
        static state[] states_21 = {
    new state(1,arcs_21_0),
    new state(1,arcs_21_1),
};
        static arc[] arcs_22_0 = {
    new arc(68,1),
    new arc(69,1),
    new arc(70,1),
    new arc(71,1),
    new arc(72,1),
};
        static arc[] arcs_22_1 = {
    new arc(0,1),
};
        static state[] states_22 = {
    new state(5,arcs_22_0),
    new state(1,arcs_22_1),
};
        static arc[] arcs_23_0 = {
    new arc(73,1),
};
        static arc[] arcs_23_1 = {
    new arc(0,1),
};
        static state[] states_23 = {
    new state(1,arcs_23_0),
    new state(1,arcs_23_1),
};
        static arc[] arcs_24_0 = {
    new arc(74,1),
};
        static arc[] arcs_24_1 = {
    new arc(0,1),
};
        static state[] states_24 = {
    new state(1,arcs_24_0),
    new state(1,arcs_24_1),
};
        static arc[] arcs_25_0 = {
    new arc(75,1),
};
        static arc[] arcs_25_1 = {
    new arc(9,2),
    new arc(0,1),
};
        static arc[] arcs_25_2 = {
    new arc(0,2),
};
        static state[] states_25 = {
    new state(1,arcs_25_0),
    new state(2,arcs_25_1),
    new state(1,arcs_25_2),
};
        static arc[] arcs_26_0 = {
    new arc(50,1),
};
        static arc[] arcs_26_1 = {
    new arc(0,1),
};
        static state[] states_26 = {
    new state(1,arcs_26_0),
    new state(1,arcs_26_1),
};
        static arc[] arcs_27_0 = {
    new arc(76,1),
};
        static arc[] arcs_27_1 = {
    new arc(26,2),
    new arc(0,1),
};
        static arc[] arcs_27_2 = {
    new arc(77,3),
    new arc(0,2),
};
        static arc[] arcs_27_3 = {
    new arc(26,4),
};
        static arc[] arcs_27_4 = {
    new arc(0,4),
};
        static state[] states_27 = {
    new state(1,arcs_27_0),
    new state(2,arcs_27_1),
    new state(2,arcs_27_2),
    new state(1,arcs_27_3),
    new state(1,arcs_27_4),
};
        static arc[] arcs_28_0 = {
    new arc(78,1),
    new arc(79,1),
};
        static arc[] arcs_28_1 = {
    new arc(0,1),
};
        static state[] states_28 = {
    new state(2,arcs_28_0),
    new state(1,arcs_28_1),
};
        static arc[] arcs_29_0 = {
    new arc(80,1),
};
        static arc[] arcs_29_1 = {
    new arc(81,2),
};
        static arc[] arcs_29_2 = {
    new arc(0,2),
};
        static state[] states_29 = {
    new state(1,arcs_29_0),
    new state(1,arcs_29_1),
    new state(1,arcs_29_2),
};
        static arc[] arcs_30_0 = {
    new arc(77,1),
};
        static arc[] arcs_30_1 = {
    new arc(82,2),
    new arc(83,2),
    new arc(12,3),
};
        static arc[] arcs_30_2 = {
    new arc(82,2),
    new arc(83,2),
    new arc(12,3),
    new arc(80,4),
};
        static arc[] arcs_30_3 = {
    new arc(80,4),
};
        static arc[] arcs_30_4 = {
    new arc(33,5),
    new arc(13,6),
    new arc(84,5),
};
        static arc[] arcs_30_5 = {
    new arc(0,5),
};
        static arc[] arcs_30_6 = {
    new arc(84,7),
};
        static arc[] arcs_30_7 = {
    new arc(15,5),
};
        static state[] states_30 = {
    new state(1,arcs_30_0),
    new state(3,arcs_30_1),
    new state(4,arcs_30_2),
    new state(1,arcs_30_3),
    new state(3,arcs_30_4),
    new state(1,arcs_30_5),
    new state(1,arcs_30_6),
    new state(1,arcs_30_7),
};
        static arc[] arcs_31_0 = {
    new arc(23,1),
};
        static arc[] arcs_31_1 = {
    new arc(86,2),
    new arc(0,1),
};
        static arc[] arcs_31_2 = {
    new arc(23,3),
};
        static arc[] arcs_31_3 = {
    new arc(0,3),
};
        static state[] states_31 = {
    new state(1,arcs_31_0),
    new state(2,arcs_31_1),
    new state(1,arcs_31_2),
    new state(1,arcs_31_3),
};
        static arc[] arcs_32_0 = {
    new arc(12,1),
};
        static arc[] arcs_32_1 = {
    new arc(86,2),
    new arc(0,1),
};
        static arc[] arcs_32_2 = {
    new arc(23,3),
};
        static arc[] arcs_32_3 = {
    new arc(0,3),
};
        static state[] states_32 = {
    new state(1,arcs_32_0),
    new state(2,arcs_32_1),
    new state(1,arcs_32_2),
    new state(1,arcs_32_3),
};
        static arc[] arcs_33_0 = {
    new arc(85,1),
};
        static arc[] arcs_33_1 = {
    new arc(32,2),
    new arc(0,1),
};
        static arc[] arcs_33_2 = {
    new arc(85,1),
    new arc(0,2),
};
        static state[] states_33 = {
    new state(1,arcs_33_0),
    new state(2,arcs_33_1),
    new state(2,arcs_33_2),
};
        static arc[] arcs_34_0 = {
    new arc(87,1),
};
        static arc[] arcs_34_1 = {
    new arc(32,0),
    new arc(0,1),
};
        static state[] states_34 = {
    new state(1,arcs_34_0),
    new state(2,arcs_34_1),
};
        static arc[] arcs_35_0 = {
    new arc(23,1),
};
        static arc[] arcs_35_1 = {
    new arc(82,0),
    new arc(0,1),
};
        static state[] states_35 = {
    new state(1,arcs_35_0),
    new state(2,arcs_35_1),
};
        static arc[] arcs_36_0 = {
    new arc(88,1),
};
        static arc[] arcs_36_1 = {
    new arc(23,2),
};
        static arc[] arcs_36_2 = {
    new arc(32,1),
    new arc(0,2),
};
        static state[] states_36 = {
    new state(1,arcs_36_0),
    new state(1,arcs_36_1),
    new state(2,arcs_36_2),
};
        static arc[] arcs_37_0 = {
    new arc(89,1),
};
        static arc[] arcs_37_1 = {
    new arc(23,2),
};
        static arc[] arcs_37_2 = {
    new arc(32,1),
    new arc(0,2),
};
        static state[] states_37 = {
    new state(1,arcs_37_0),
    new state(1,arcs_37_1),
    new state(2,arcs_37_2),
};
        static arc[] arcs_38_0 = {
    new arc(90,1),
};
        static arc[] arcs_38_1 = {
    new arc(26,2),
};
        static arc[] arcs_38_2 = {
    new arc(32,3),
    new arc(0,2),
};
        static arc[] arcs_38_3 = {
    new arc(26,4),
};
        static arc[] arcs_38_4 = {
    new arc(0,4),
};
        static state[] states_38 = {
    new state(1,arcs_38_0),
    new state(1,arcs_38_1),
    new state(2,arcs_38_2),
    new state(1,arcs_38_3),
    new state(1,arcs_38_4),
};
        static arc[] arcs_39_0 = {
    new arc(91,1),
    new arc(92,1),
    new arc(93,1),
    new arc(94,1),
    new arc(95,1),
    new arc(19,1),
    new arc(18,1),
    new arc(17,1),
    new arc(96,1),
};
        static arc[] arcs_39_1 = {
    new arc(0,1),
};
        static state[] states_39 = {
    new state(9,arcs_39_0),
    new state(1,arcs_39_1),
};
        static arc[] arcs_40_0 = {
    new arc(21,1),
};
        static arc[] arcs_40_1 = {
    new arc(19,2),
    new arc(95,2),
    new arc(93,2),
};
        static arc[] arcs_40_2 = {
    new arc(0,2),
};
        static state[] states_40 = {
    new state(1,arcs_40_0),
    new state(3,arcs_40_1),
    new state(1,arcs_40_2),
};
        static arc[] arcs_41_0 = {
    new arc(97,1),
};
        static arc[] arcs_41_1 = {
    new arc(26,2),
};
        static arc[] arcs_41_2 = {
    new arc(27,3),
};
        static arc[] arcs_41_3 = {
    new arc(28,4),
};
        static arc[] arcs_41_4 = {
    new arc(98,1),
    new arc(99,5),
    new arc(0,4),
};
        static arc[] arcs_41_5 = {
    new arc(27,6),
};
        static arc[] arcs_41_6 = {
    new arc(28,7),
};
        static arc[] arcs_41_7 = {
    new arc(0,7),
};
        static state[] states_41 = {
    new state(1,arcs_41_0),
    new state(1,arcs_41_1),
    new state(1,arcs_41_2),
    new state(1,arcs_41_3),
    new state(3,arcs_41_4),
    new state(1,arcs_41_5),
    new state(1,arcs_41_6),
    new state(1,arcs_41_7),
};
        static arc[] arcs_42_0 = {
    new arc(100,1),
};
        static arc[] arcs_42_1 = {
    new arc(26,2),
};
        static arc[] arcs_42_2 = {
    new arc(27,3),
};
        static arc[] arcs_42_3 = {
    new arc(28,4),
};
        static arc[] arcs_42_4 = {
    new arc(99,5),
    new arc(0,4),
};
        static arc[] arcs_42_5 = {
    new arc(27,6),
};
        static arc[] arcs_42_6 = {
    new arc(28,7),
};
        static arc[] arcs_42_7 = {
    new arc(0,7),
};
        static state[] states_42 = {
    new state(1,arcs_42_0),
    new state(1,arcs_42_1),
    new state(1,arcs_42_2),
    new state(1,arcs_42_3),
    new state(2,arcs_42_4),
    new state(1,arcs_42_5),
    new state(1,arcs_42_6),
    new state(1,arcs_42_7),
};
        static arc[] arcs_43_0 = {
    new arc(101,1),
};
        static arc[] arcs_43_1 = {
    new arc(66,2),
};
        static arc[] arcs_43_2 = {
    new arc(102,3),
};
        static arc[] arcs_43_3 = {
    new arc(9,4),
};
        static arc[] arcs_43_4 = {
    new arc(27,5),
};
        static arc[] arcs_43_5 = {
    new arc(28,6),
};
        static arc[] arcs_43_6 = {
    new arc(99,7),
    new arc(0,6),
};
        static arc[] arcs_43_7 = {
    new arc(27,8),
};
        static arc[] arcs_43_8 = {
    new arc(28,9),
};
        static arc[] arcs_43_9 = {
    new arc(0,9),
};
        static state[] states_43 = {
    new state(1,arcs_43_0),
    new state(1,arcs_43_1),
    new state(1,arcs_43_2),
    new state(1,arcs_43_3),
    new state(1,arcs_43_4),
    new state(1,arcs_43_5),
    new state(2,arcs_43_6),
    new state(1,arcs_43_7),
    new state(1,arcs_43_8),
    new state(1,arcs_43_9),
};
        static arc[] arcs_44_0 = {
    new arc(103,1),
};
        static arc[] arcs_44_1 = {
    new arc(27,2),
};
        static arc[] arcs_44_2 = {
    new arc(28,3),
};
        static arc[] arcs_44_3 = {
    new arc(104,4),
    new arc(105,5),
};
        static arc[] arcs_44_4 = {
    new arc(27,6),
};
        static arc[] arcs_44_5 = {
    new arc(27,7),
};
        static arc[] arcs_44_6 = {
    new arc(28,8),
};
        static arc[] arcs_44_7 = {
    new arc(28,9),
};
        static arc[] arcs_44_8 = {
    new arc(104,4),
    new arc(99,10),
    new arc(105,5),
    new arc(0,8),
};
        static arc[] arcs_44_9 = {
    new arc(0,9),
};
        static arc[] arcs_44_10 = {
    new arc(27,11),
};
        static arc[] arcs_44_11 = {
    new arc(28,12),
};
        static arc[] arcs_44_12 = {
    new arc(105,5),
    new arc(0,12),
};
        static state[] states_44 = {
    new state(1,arcs_44_0),
    new state(1,arcs_44_1),
    new state(1,arcs_44_2),
    new state(2,arcs_44_3),
    new state(1,arcs_44_4),
    new state(1,arcs_44_5),
    new state(1,arcs_44_6),
    new state(1,arcs_44_7),
    new state(4,arcs_44_8),
    new state(1,arcs_44_9),
    new state(1,arcs_44_10),
    new state(1,arcs_44_11),
    new state(2,arcs_44_12),
};
        static arc[] arcs_45_0 = {
    new arc(106,1),
};
        static arc[] arcs_45_1 = {
    new arc(107,2),
};
        static arc[] arcs_45_2 = {
    new arc(32,1),
    new arc(27,3),
};
        static arc[] arcs_45_3 = {
    new arc(28,4),
};
        static arc[] arcs_45_4 = {
    new arc(0,4),
};
        static state[] states_45 = {
    new state(1,arcs_45_0),
    new state(1,arcs_45_1),
    new state(2,arcs_45_2),
    new state(1,arcs_45_3),
    new state(1,arcs_45_4),
};
        static arc[] arcs_46_0 = {
    new arc(26,1),
};
        static arc[] arcs_46_1 = {
    new arc(86,2),
    new arc(0,1),
};
        static arc[] arcs_46_2 = {
    new arc(108,3),
};
        static arc[] arcs_46_3 = {
    new arc(0,3),
};
        static state[] states_46 = {
    new state(1,arcs_46_0),
    new state(2,arcs_46_1),
    new state(1,arcs_46_2),
    new state(1,arcs_46_3),
};
        static arc[] arcs_47_0 = {
    new arc(109,1),
};
        static arc[] arcs_47_1 = {
    new arc(26,2),
    new arc(0,1),
};
        static arc[] arcs_47_2 = {
    new arc(86,3),
    new arc(0,2),
};
        static arc[] arcs_47_3 = {
    new arc(23,4),
};
        static arc[] arcs_47_4 = {
    new arc(0,4),
};
        static state[] states_47 = {
    new state(1,arcs_47_0),
    new state(2,arcs_47_1),
    new state(2,arcs_47_2),
    new state(1,arcs_47_3),
    new state(1,arcs_47_4),
};
        static arc[] arcs_48_0 = {
    new arc(3,1),
    new arc(2,2),
};
        static arc[] arcs_48_1 = {
    new arc(0,1),
};
        static arc[] arcs_48_2 = {
    new arc(110,3),
};
        static arc[] arcs_48_3 = {
    new arc(6,4),
};
        static arc[] arcs_48_4 = {
    new arc(6,4),
    new arc(111,1),
};
        static state[] states_48 = {
    new state(2,arcs_48_0),
    new state(1,arcs_48_1),
    new state(1,arcs_48_2),
    new state(1,arcs_48_3),
    new state(2,arcs_48_4),
};
        static arc[] arcs_49_0 = {
    new arc(112,1),
    new arc(113,2),
};
        static arc[] arcs_49_1 = {
    new arc(97,3),
    new arc(0,1),
};
        static arc[] arcs_49_2 = {
    new arc(0,2),
};
        static arc[] arcs_49_3 = {
    new arc(112,4),
};
        static arc[] arcs_49_4 = {
    new arc(99,5),
};
        static arc[] arcs_49_5 = {
    new arc(26,2),
};
        static state[] states_49 = {
    new state(2,arcs_49_0),
    new state(2,arcs_49_1),
    new state(1,arcs_49_2),
    new state(1,arcs_49_3),
    new state(1,arcs_49_4),
    new state(1,arcs_49_5),
};
        static arc[] arcs_50_0 = {
    new arc(112,1),
    new arc(115,1),
};
        static arc[] arcs_50_1 = {
    new arc(0,1),
};
        static state[] states_50 = {
    new state(2,arcs_50_0),
    new state(1,arcs_50_1),
};
        static arc[] arcs_51_0 = {
    new arc(116,1),
};
        static arc[] arcs_51_1 = {
    new arc(35,2),
    new arc(27,3),
};
        static arc[] arcs_51_2 = {
    new arc(27,3),
};
        static arc[] arcs_51_3 = {
    new arc(26,4),
};
        static arc[] arcs_51_4 = {
    new arc(0,4),
};
        static state[] states_51 = {
    new state(1,arcs_51_0),
    new state(2,arcs_51_1),
    new state(1,arcs_51_2),
    new state(1,arcs_51_3),
    new state(1,arcs_51_4),
};
        static arc[] arcs_52_0 = {
    new arc(116,1),
};
        static arc[] arcs_52_1 = {
    new arc(35,2),
    new arc(27,3),
};
        static arc[] arcs_52_2 = {
    new arc(27,3),
};
        static arc[] arcs_52_3 = {
    new arc(114,4),
};
        static arc[] arcs_52_4 = {
    new arc(0,4),
};
        static state[] states_52 = {
    new state(1,arcs_52_0),
    new state(2,arcs_52_1),
    new state(1,arcs_52_2),
    new state(1,arcs_52_3),
    new state(1,arcs_52_4),
};
        static arc[] arcs_53_0 = {
    new arc(117,1),
};
        static arc[] arcs_53_1 = {
    new arc(118,0),
    new arc(0,1),
};
        static state[] states_53 = {
    new state(1,arcs_53_0),
    new state(2,arcs_53_1),
};
        static arc[] arcs_54_0 = {
    new arc(119,1),
};
        static arc[] arcs_54_1 = {
    new arc(120,0),
    new arc(0,1),
};
        static state[] states_54 = {
    new state(1,arcs_54_0),
    new state(2,arcs_54_1),
};
        static arc[] arcs_55_0 = {
    new arc(121,1),
    new arc(122,2),
};
        static arc[] arcs_55_1 = {
    new arc(119,2),
};
        static arc[] arcs_55_2 = {
    new arc(0,2),
};
        static state[] states_55 = {
    new state(2,arcs_55_0),
    new state(1,arcs_55_1),
    new state(1,arcs_55_2),
};
        static arc[] arcs_56_0 = {
    new arc(108,1),
};
        static arc[] arcs_56_1 = {
    new arc(123,0),
    new arc(0,1),
};
        static state[] states_56 = {
    new state(1,arcs_56_0),
    new state(2,arcs_56_1),
};
        static arc[] arcs_57_0 = {
    new arc(124,1),
    new arc(125,1),
    new arc(126,1),
    new arc(127,1),
    new arc(128,1),
    new arc(129,1),
    new arc(130,1),
    new arc(102,1),
    new arc(121,2),
    new arc(131,3),
};
        static arc[] arcs_57_1 = {
    new arc(0,1),
};
        static arc[] arcs_57_2 = {
    new arc(102,1),
};
        static arc[] arcs_57_3 = {
    new arc(121,1),
    new arc(0,3),
};
        static state[] states_57 = {
    new state(10,arcs_57_0),
    new state(1,arcs_57_1),
    new state(1,arcs_57_2),
    new state(2,arcs_57_3),
};
        static arc[] arcs_58_0 = {
    new arc(33,1),
};
        static arc[] arcs_58_1 = {
    new arc(108,2),
};
        static arc[] arcs_58_2 = {
    new arc(0,2),
};
        static state[] states_58 = {
    new state(1,arcs_58_0),
    new state(1,arcs_58_1),
    new state(1,arcs_58_2),
};
        static arc[] arcs_59_0 = {
    new arc(132,1),
};
        static arc[] arcs_59_1 = {
    new arc(133,0),
    new arc(0,1),
};
        static state[] states_59 = {
    new state(1,arcs_59_0),
    new state(2,arcs_59_1),
};
        static arc[] arcs_60_0 = {
    new arc(134,1),
};
        static arc[] arcs_60_1 = {
    new arc(135,0),
    new arc(0,1),
};
        static state[] states_60 = {
    new state(1,arcs_60_0),
    new state(2,arcs_60_1),
};
        static arc[] arcs_61_0 = {
    new arc(136,1),
};
        static arc[] arcs_61_1 = {
    new arc(137,0),
    new arc(0,1),
};
        static state[] states_61 = {
    new state(1,arcs_61_0),
    new state(2,arcs_61_1),
};
        static arc[] arcs_62_0 = {
    new arc(138,1),
};
        static arc[] arcs_62_1 = {
    new arc(139,0),
    new arc(140,0),
    new arc(0,1),
};
        static state[] states_62 = {
    new state(1,arcs_62_0),
    new state(3,arcs_62_1),
};
        static arc[] arcs_63_0 = {
    new arc(141,1),
};
        static arc[] arcs_63_1 = {
    new arc(142,0),
    new arc(143,0),
    new arc(0,1),
};
        static state[] states_63 = {
    new state(1,arcs_63_0),
    new state(3,arcs_63_1),
};
        static arc[] arcs_64_0 = {
    new arc(144,1),
};
        static arc[] arcs_64_1 = {
    new arc(33,0),
    new arc(11,0),
    new arc(145,0),
    new arc(146,0),
    new arc(147,0),
    new arc(0,1),
};
        static state[] states_64 = {
    new state(1,arcs_64_0),
    new state(6,arcs_64_1),
};
        static arc[] arcs_65_0 = {
    new arc(142,1),
    new arc(143,1),
    new arc(148,1),
    new arc(149,2),
};
        static arc[] arcs_65_1 = {
    new arc(144,2),
};
        static arc[] arcs_65_2 = {
    new arc(0,2),
};
        static state[] states_65 = {
    new state(4,arcs_65_0),
    new state(1,arcs_65_1),
    new state(1,arcs_65_2),
};
        static arc[] arcs_66_0 = {
    new arc(150,1),
};
        static arc[] arcs_66_1 = {
    new arc(34,2),
    new arc(0,1),
};
        static arc[] arcs_66_2 = {
    new arc(144,3),
};
        static arc[] arcs_66_3 = {
    new arc(0,3),
};
        static state[] states_66 = {
    new state(1,arcs_66_0),
    new state(2,arcs_66_1),
    new state(1,arcs_66_2),
    new state(1,arcs_66_3),
};
        static arc[] arcs_67_0 = {
    new arc(151,1),
    new arc(152,2),
};
        static arc[] arcs_67_1 = {
    new arc(152,2),
};
        static arc[] arcs_67_2 = {
    new arc(153,2),
    new arc(0,2),
};
        static state[] states_67 = {
    new state(2,arcs_67_0),
    new state(1,arcs_67_1),
    new state(2,arcs_67_2),
};
        static arc[] arcs_68_0 = {
    new arc(13,1),
    new arc(155,2),
    new arc(157,3),
    new arc(23,4),
    new arc(160,4),
    new arc(161,5),
    new arc(83,4),
    new arc(162,4),
    new arc(163,4),
    new arc(164,4),
};
        static arc[] arcs_68_1 = {
    new arc(50,6),
    new arc(154,6),
    new arc(15,4),
};
        static arc[] arcs_68_2 = {
    new arc(154,7),
    new arc(156,4),
};
        static arc[] arcs_68_3 = {
    new arc(158,8),
    new arc(159,4),
};
        static arc[] arcs_68_4 = {
    new arc(0,4),
};
        static arc[] arcs_68_5 = {
    new arc(161,5),
    new arc(0,5),
};
        static arc[] arcs_68_6 = {
    new arc(15,4),
};
        static arc[] arcs_68_7 = {
    new arc(156,4),
};
        static arc[] arcs_68_8 = {
    new arc(159,4),
};
        static state[] states_68 = {
    new state(10,arcs_68_0),
    new state(3,arcs_68_1),
    new state(2,arcs_68_2),
    new state(2,arcs_68_3),
    new state(1,arcs_68_4),
    new state(2,arcs_68_5),
    new state(1,arcs_68_6),
    new state(1,arcs_68_7),
    new state(1,arcs_68_8),
};
        static arc[] arcs_69_0 = {
    new arc(26,1),
    new arc(51,1),
};
        static arc[] arcs_69_1 = {
    new arc(165,2),
    new arc(32,3),
    new arc(0,1),
};
        static arc[] arcs_69_2 = {
    new arc(0,2),
};
        static arc[] arcs_69_3 = {
    new arc(26,4),
    new arc(51,4),
    new arc(0,3),
};
        static arc[] arcs_69_4 = {
    new arc(32,3),
    new arc(0,4),
};
        static state[] states_69 = {
    new state(2,arcs_69_0),
    new state(3,arcs_69_1),
    new state(1,arcs_69_2),
    new state(3,arcs_69_3),
    new state(2,arcs_69_4),
};
        static arc[] arcs_70_0 = {
    new arc(13,1),
    new arc(155,2),
    new arc(82,3),
};
        static arc[] arcs_70_1 = {
    new arc(14,4),
    new arc(15,5),
};
        static arc[] arcs_70_2 = {
    new arc(166,6),
};
        static arc[] arcs_70_3 = {
    new arc(23,5),
};
        static arc[] arcs_70_4 = {
    new arc(15,5),
};
        static arc[] arcs_70_5 = {
    new arc(0,5),
};
        static arc[] arcs_70_6 = {
    new arc(156,5),
};
        static state[] states_70 = {
    new state(3,arcs_70_0),
    new state(2,arcs_70_1),
    new state(1,arcs_70_2),
    new state(1,arcs_70_3),
    new state(1,arcs_70_4),
    new state(1,arcs_70_5),
    new state(1,arcs_70_6),
};
        static arc[] arcs_71_0 = {
    new arc(167,1),
};
        static arc[] arcs_71_1 = {
    new arc(32,2),
    new arc(0,1),
};
        static arc[] arcs_71_2 = {
    new arc(167,1),
    new arc(0,2),
};
        static state[] states_71 = {
    new state(1,arcs_71_0),
    new state(2,arcs_71_1),
    new state(2,arcs_71_2),
};
        static arc[] arcs_72_0 = {
    new arc(26,1),
    new arc(27,2),
};
        static arc[] arcs_72_1 = {
    new arc(27,2),
    new arc(0,1),
};
        static arc[] arcs_72_2 = {
    new arc(26,3),
    new arc(168,4),
    new arc(0,2),
};
        static arc[] arcs_72_3 = {
    new arc(168,4),
    new arc(0,3),
};
        static arc[] arcs_72_4 = {
    new arc(0,4),
};
        static state[] states_72 = {
    new state(2,arcs_72_0),
    new state(2,arcs_72_1),
    new state(3,arcs_72_2),
    new state(2,arcs_72_3),
    new state(1,arcs_72_4),
};
        static arc[] arcs_73_0 = {
    new arc(27,1),
};
        static arc[] arcs_73_1 = {
    new arc(26,2),
    new arc(0,1),
};
        static arc[] arcs_73_2 = {
    new arc(0,2),
};
        static state[] states_73 = {
    new state(1,arcs_73_0),
    new state(2,arcs_73_1),
    new state(1,arcs_73_2),
};
        static arc[] arcs_74_0 = {
    new arc(108,1),
    new arc(51,1),
};
        static arc[] arcs_74_1 = {
    new arc(32,2),
    new arc(0,1),
};
        static arc[] arcs_74_2 = {
    new arc(108,1),
    new arc(51,1),
    new arc(0,2),
};
        static state[] states_74 = {
    new state(2,arcs_74_0),
    new state(2,arcs_74_1),
    new state(3,arcs_74_2),
};
        static arc[] arcs_75_0 = {
    new arc(26,1),
};
        static arc[] arcs_75_1 = {
    new arc(32,2),
    new arc(0,1),
};
        static arc[] arcs_75_2 = {
    new arc(26,1),
    new arc(0,2),
};
        static state[] states_75 = {
    new state(1,arcs_75_0),
    new state(2,arcs_75_1),
    new state(2,arcs_75_2),
};
        static arc[] arcs_76_0 = {
    new arc(26,1),
    new arc(34,2),
    new arc(51,3),
};
        static arc[] arcs_76_1 = {
    new arc(27,4),
    new arc(165,5),
    new arc(32,6),
    new arc(0,1),
};
        static arc[] arcs_76_2 = {
    new arc(108,7),
};
        static arc[] arcs_76_3 = {
    new arc(165,5),
    new arc(32,6),
    new arc(0,3),
};
        static arc[] arcs_76_4 = {
    new arc(26,7),
};
        static arc[] arcs_76_5 = {
    new arc(0,5),
};
        static arc[] arcs_76_6 = {
    new arc(26,8),
    new arc(51,8),
    new arc(0,6),
};
        static arc[] arcs_76_7 = {
    new arc(165,5),
    new arc(32,9),
    new arc(0,7),
};
        static arc[] arcs_76_8 = {
    new arc(32,6),
    new arc(0,8),
};
        static arc[] arcs_76_9 = {
    new arc(26,10),
    new arc(34,11),
    new arc(0,9),
};
        static arc[] arcs_76_10 = {
    new arc(27,12),
};
        static arc[] arcs_76_11 = {
    new arc(108,13),
};
        static arc[] arcs_76_12 = {
    new arc(26,13),
};
        static arc[] arcs_76_13 = {
    new arc(32,9),
    new arc(0,13),
};
        static state[] states_76 = {
    new state(3,arcs_76_0),
    new state(4,arcs_76_1),
    new state(1,arcs_76_2),
    new state(3,arcs_76_3),
    new state(1,arcs_76_4),
    new state(1,arcs_76_5),
    new state(3,arcs_76_6),
    new state(3,arcs_76_7),
    new state(2,arcs_76_8),
    new state(3,arcs_76_9),
    new state(1,arcs_76_10),
    new state(1,arcs_76_11),
    new state(1,arcs_76_12),
    new state(2,arcs_76_13),
};
        static arc[] arcs_77_0 = {
    new arc(169,1),
};
        static arc[] arcs_77_1 = {
    new arc(23,2),
};
        static arc[] arcs_77_2 = {
    new arc(13,3),
    new arc(27,4),
};
        static arc[] arcs_77_3 = {
    new arc(14,5),
    new arc(15,6),
};
        static arc[] arcs_77_4 = {
    new arc(28,7),
};
        static arc[] arcs_77_5 = {
    new arc(15,6),
};
        static arc[] arcs_77_6 = {
    new arc(27,4),
};
        static arc[] arcs_77_7 = {
    new arc(0,7),
};
        static state[] states_77 = {
    new state(1,arcs_77_0),
    new state(1,arcs_77_1),
    new state(2,arcs_77_2),
    new state(2,arcs_77_3),
    new state(1,arcs_77_4),
    new state(1,arcs_77_5),
    new state(1,arcs_77_6),
    new state(1,arcs_77_7),
};
        static arc[] arcs_78_0 = {
    new arc(170,1),
};
        static arc[] arcs_78_1 = {
    new arc(32,2),
    new arc(0,1),
};
        static arc[] arcs_78_2 = {
    new arc(170,1),
    new arc(0,2),
};
        static state[] states_78 = {
    new state(1,arcs_78_0),
    new state(2,arcs_78_1),
    new state(2,arcs_78_2),
};
        static arc[] arcs_79_0 = {
    new arc(26,1),
    new arc(34,2),
    new arc(33,2),
};
        static arc[] arcs_79_1 = {
    new arc(165,3),
    new arc(31,2),
    new arc(0,1),
};
        static arc[] arcs_79_2 = {
    new arc(26,3),
};
        static arc[] arcs_79_3 = {
    new arc(0,3),
};
        static state[] states_79 = {
    new state(3,arcs_79_0),
    new state(3,arcs_79_1),
    new state(1,arcs_79_2),
    new state(1,arcs_79_3),
};
        static arc[] arcs_80_0 = {
    new arc(165,1),
    new arc(172,1),
};
        static arc[] arcs_80_1 = {
    new arc(0,1),
};
        static state[] states_80 = {
    new state(2,arcs_80_0),
    new state(1,arcs_80_1),
};
        static arc[] arcs_81_0 = {
    new arc(101,1),
};
        static arc[] arcs_81_1 = {
    new arc(66,2),
};
        static arc[] arcs_81_2 = {
    new arc(102,3),
};
        static arc[] arcs_81_3 = {
    new arc(112,4),
};
        static arc[] arcs_81_4 = {
    new arc(171,5),
    new arc(0,4),
};
        static arc[] arcs_81_5 = {
    new arc(0,5),
};
        static state[] states_81 = {
    new state(1,arcs_81_0),
    new state(1,arcs_81_1),
    new state(1,arcs_81_2),
    new state(1,arcs_81_3),
    new state(2,arcs_81_4),
    new state(1,arcs_81_5),
};
        static arc[] arcs_82_0 = {
    new arc(21,1),
    new arc(173,2),
};
        static arc[] arcs_82_1 = {
    new arc(173,2),
};
        static arc[] arcs_82_2 = {
    new arc(0,2),
};
        static state[] states_82 = {
    new state(2,arcs_82_0),
    new state(1,arcs_82_1),
    new state(1,arcs_82_2),
};
        static arc[] arcs_83_0 = {
    new arc(97,1),
};
        static arc[] arcs_83_1 = {
    new arc(114,2),
};
        static arc[] arcs_83_2 = {
    new arc(171,3),
    new arc(0,2),
};
        static arc[] arcs_83_3 = {
    new arc(0,3),
};
        static state[] states_83 = {
    new state(1,arcs_83_0),
    new state(1,arcs_83_1),
    new state(2,arcs_83_2),
    new state(1,arcs_83_3),
};
        static arc[] arcs_84_0 = {
    new arc(23,1),
};
        static arc[] arcs_84_1 = {
    new arc(0,1),
};
        static state[] states_84 = {
    new state(1,arcs_84_0),
    new state(1,arcs_84_1),
};
        static arc[] arcs_85_0 = {
    new arc(175,1),
};
        static arc[] arcs_85_1 = {
    new arc(176,2),
    new arc(0,1),
};
        static arc[] arcs_85_2 = {
    new arc(0,2),
};
        static state[] states_85 = {
    new state(1,arcs_85_0),
    new state(2,arcs_85_1),
    new state(1,arcs_85_2),
};
        static arc[] arcs_86_0 = {
    new arc(77,1),
    new arc(9,2),
};
        static arc[] arcs_86_1 = {
    new arc(26,2),
};
        static arc[] arcs_86_2 = {
    new arc(0,2),
};
        static state[] states_86 = {
    new state(2,arcs_86_0),
    new state(1,arcs_86_1),
    new state(1,arcs_86_2),
};
        static dfa[] dfas = {
    new dfa(256, "single_input", 0, 3, states_0,
     "004050340000002000000000012076011007262004020002000300220050037202000"),
    new dfa(257, "file_input", 0, 2, states_1,
     "204050340000002000000000012076011007262004020002000300220050037202000"),
    new dfa(258, "eval_input", 0, 3, states_2,
     "000040200000000000000000000000010000000000020002000300220050037000000"),
    new dfa(259, "decorator", 0, 7, states_3,
     "000010000000000000000000000000000000000000000000000000000000000000000"),
    new dfa(260, "decorators", 0, 2, states_4,
     "000010000000000000000000000000000000000000000000000000000000000000000"),
    new dfa(261, "decorated", 0, 3, states_5,
     "000010000000000000000000000000000000000000000000000000000000000000000"),
    new dfa(262, "async_funcdef", 0, 3, states_6,
     "000000040000000000000000000000000000000000000000000000000000000000000"),
    new dfa(263, "funcdef", 0, 8, states_7,
     "000000100000000000000000000000000000000000000000000000000000000000000"),
    new dfa(264, "parameters", 0, 4, states_8,
     "000040000000000000000000000000000000000000000000000000000000000000000"),
    new dfa(265, "typedargslist", 0, 19, states_9,
     "000000200000006000000000000000000000000000000000000000000000000000000"),
    new dfa(266, "tfpdef", 0, 4, states_10,
     "000000200000000000000000000000000000000000000000000000000000000000000"),
    new dfa(267, "varargslist", 0, 19, states_11,
     "000000200000006000000000000000000000000000000000000000000000000000000"),
    new dfa(268, "vfpdef", 0, 2, states_12,
     "000000200000000000000000000000000000000000000000000000000000000000000"),
    new dfa(269, "stmt", 0, 2, states_13,
     "000050340000002000000000012076011007262004020002000300220050037202000"),
    new dfa(270, "simple_stmt", 0, 4, states_14,
     "000040200000002000000000012076011007000000020002000300220050037200000"),
    new dfa(271, "small_stmt", 0, 2, states_15,
     "000040200000002000000000012076011007000000020002000300220050037200000"),
    new dfa(272, "expr_stmt", 0, 6, states_16,
     "000040200000002000000000000000010000000000020002000300220050037000000"),
    new dfa(273, "annassign", 0, 5, states_17,
     "000000000010000000000000000000000000000000000000000000000000000000000"),
    new dfa(274, "testlist_star_expr", 0, 3, states_18,
     "000040200000002000000000000000010000000000020002000300220050037000000"),
    new dfa(275, "augassign", 0, 2, states_19,
     "000000000000000000360377001000000000000000000000000000000000000000000"),
    new dfa(276, "del_stmt", 0, 3, states_20,
     "000000000000000000000000002000000000000000000000000000000000000000000"),
    new dfa(277, "pass_stmt", 0, 2, states_21,
     "000000000000000000000000010000000000000000000000000000000000000000000"),
    new dfa(278, "flow_stmt", 0, 2, states_22,
     "000000000000000000000000000036000000000000000000000000000000000200000"),
    new dfa(279, "break_stmt", 0, 2, states_23,
     "000000000000000000000000000002000000000000000000000000000000000000000"),
    new dfa(280, "continue_stmt", 0, 2, states_24,
     "000000000000000000000000000004000000000000000000000000000000000000000"),
    new dfa(281, "return_stmt", 0, 3, states_25,
     "000000000000000000000000000010000000000000000000000000000000000000000"),
    new dfa(282, "yield_stmt", 0, 2, states_26,
     "000000000000000000000000000000000000000000000000000000000000000200000"),
    new dfa(283, "raise_stmt", 0, 5, states_27,
     "000000000000000000000000000020000000000000000000000000000000000000000"),
    new dfa(284, "import_stmt", 0, 2, states_28,
     "000000000000000000000000000040001000000000000000000000000000000000000"),
    new dfa(285, "import_name", 0, 3, states_29,
     "000000000000000000000000000000001000000000000000000000000000000000000"),
    new dfa(286, "import_from", 0, 8, states_30,
     "000000000000000000000000000040000000000000000000000000000000000000000"),
    new dfa(287, "import_as_name", 0, 4, states_31,
     "000000200000000000000000000000000000000000000000000000000000000000000"),
    new dfa(288, "dotted_as_name", 0, 4, states_32,
     "000000200000000000000000000000000000000000000000000000000000000000000"),
    new dfa(289, "import_as_names", 0, 3, states_33,
     "000000200000000000000000000000000000000000000000000000000000000000000"),
    new dfa(290, "dotted_as_names", 0, 2, states_34,
     "000000200000000000000000000000000000000000000000000000000000000000000"),
    new dfa(291, "dotted_name", 0, 2, states_35,
     "000000200000000000000000000000000000000000000000000000000000000000000"),
    new dfa(292, "global_stmt", 0, 3, states_36,
     "000000000000000000000000000000000001000000000000000000000000000000000"),
    new dfa(293, "nonlocal_stmt", 0, 3, states_37,
     "000000000000000000000000000000000002000000000000000000000000000000000"),
    new dfa(294, "assert_stmt", 0, 5, states_38,
     "000000000000000000000000000000000004000000000000000000000000000000000"),
    new dfa(295, "compound_stmt", 0, 2, states_39,
     "000010140000000000000000000000000000262004000000000000000000000002000"),
    new dfa(296, "async_stmt", 0, 3, states_40,
     "000000040000000000000000000000000000000000000000000000000000000000000"),
    new dfa(297, "if_stmt", 0, 8, states_41,
     "000000000000000000000000000000000000002000000000000000000000000000000"),
    new dfa(298, "while_stmt", 0, 8, states_42,
     "000000000000000000000000000000000000020000000000000000000000000000000"),
    new dfa(299, "for_stmt", 0, 10, states_43,
     "000000000000000000000000000000000000040000000000000000000000000000000"),
    new dfa(300, "try_stmt", 0, 13, states_44,
     "000000000000000000000000000000000000200000000000000000000000000000000"),
    new dfa(301, "with_stmt", 0, 5, states_45,
     "000000000000000000000000000000000000000004000000000000000000000000000"),
    new dfa(302, "with_item", 0, 4, states_46,
     "000040200000000000000000000000010000000000020002000300220050037000000"),
    new dfa(303, "except_clause", 0, 5, states_47,
     "000000000000000000000000000000000000000040000000000000000000000000000"),
    new dfa(304, "suite", 0, 5, states_48,
     "004040200000002000000000012076011007000000020002000300220050037200000"),
    new dfa(305, "test", 0, 6, states_49,
     "000040200000000000000000000000010000000000020002000300220050037000000"),
    new dfa(306, "test_nocond", 0, 2, states_50,
     "000040200000000000000000000000010000000000020002000300220050037000000"),
    new dfa(307, "lambdef", 0, 5, states_51,
     "000000000000000000000000000000000000000000020000000000000000000000000"),
    new dfa(308, "lambdef_nocond", 0, 5, states_52,
     "000000000000000000000000000000000000000000020000000000000000000000000"),
    new dfa(309, "or_test", 0, 2, states_53,
     "000040200000000000000000000000010000000000000002000300220050037000000"),
    new dfa(310, "and_test", 0, 2, states_54,
     "000040200000000000000000000000010000000000000002000300220050037000000"),
    new dfa(311, "not_test", 0, 3, states_55,
     "000040200000000000000000000000010000000000000002000300220050037000000"),
    new dfa(312, "comparison", 0, 2, states_56,
     "000040200000000000000000000000010000000000000000000300220050037000000"),
    new dfa(313, "comp_op", 0, 4, states_57,
     "000000000000000000000000000000000000100000000362017000000000000000000"),
    new dfa(314, "star_expr", 0, 3, states_58,
     "000000000000002000000000000000000000000000000000000000000000000000000"),
    new dfa(315, "expr", 0, 2, states_59,
     "000040200000000000000000000000010000000000000000000300220050037000000"),
    new dfa(316, "xor_expr", 0, 2, states_60,
     "000040200000000000000000000000010000000000000000000300220050037000000"),
    new dfa(317, "and_expr", 0, 2, states_61,
     "000040200000000000000000000000010000000000000000000300220050037000000"),
    new dfa(318, "shift_expr", 0, 2, states_62,
     "000040200000000000000000000000010000000000000000000300220050037000000"),
    new dfa(319, "arith_expr", 0, 2, states_63,
     "000040200000000000000000000000010000000000000000000300220050037000000"),
    new dfa(320, "term", 0, 2, states_64,
     "000040200000000000000000000000010000000000000000000300220050037000000"),
    new dfa(321, "factor", 0, 3, states_65,
     "000040200000000000000000000000010000000000000000000300220050037000000"),
    new dfa(322, "power", 0, 4, states_66,
     "000040200000000000000000000000010000000000000000000000200050037000000"),
    new dfa(323, "atom_expr", 0, 3, states_67,
     "000040200000000000000000000000010000000000000000000000200050037000000"),
    new dfa(324, "atom", 0, 9, states_68,
     "000040200000000000000000000000010000000000000000000000000050037000000"),
    new dfa(325, "testlist_comp", 0, 5, states_69,
     "000040200000002000000000000000010000000000020002000300220050037000000"),
    new dfa(326, "trailer", 0, 7, states_70,
     "000040000000000000000000000000004000000000000000000000000010000000000"),
    new dfa(327, "subscriptlist", 0, 3, states_71,
     "000040200010000000000000000000010000000000020002000300220050037000000"),
    new dfa(328, "subscript", 0, 5, states_72,
     "000040200010000000000000000000010000000000020002000300220050037000000"),
    new dfa(329, "sliceop", 0, 3, states_73,
     "000000000010000000000000000000000000000000000000000000000000000000000"),
    new dfa(330, "exprlist", 0, 3, states_74,
     "000040200000002000000000000000010000000000000000000300220050037000000"),
    new dfa(331, "testlist", 0, 3, states_75,
     "000040200000000000000000000000010000000000020002000300220050037000000"),
    new dfa(332, "dictorsetmaker", 0, 14, states_76,
     "000040200000006000000000000000010000000000020002000300220050037000000"),
    new dfa(333, "classdef", 0, 8, states_77,
     "000000000000000000000000000000000000000000000000000000000000000002000"),
    new dfa(334, "arglist", 0, 3, states_78,
     "000040200000006000000000000000010000000000020002000300220050037000000"),
    new dfa(335, "argument", 0, 4, states_79,
     "000040200000006000000000000000010000000000020002000300220050037000000"),
    new dfa(336, "comp_iter", 0, 2, states_80,
     "000000040000000000000000000000000000042000000000000000000000000000000"),
    new dfa(337, "sync_comp_for", 0, 6, states_81,
     "000000000000000000000000000000000000040000000000000000000000000000000"),
    new dfa(338, "comp_for", 0, 3, states_82,
     "000000040000000000000000000000000000040000000000000000000000000000000"),
    new dfa(339, "comp_if", 0, 4, states_83,
     "000000000000000000000000000000000000002000000000000000000000000000000"),
    new dfa(340, "encoding_decl", 0, 2, states_84,
     "000000200000000000000000000000000000000000000000000000000000000000000"),
    new dfa(341, "yield_expr", 0, 3, states_85,
     "000000000000000000000000000000000000000000000000000000000000000200000"),
    new dfa(342, "yield_arg", 0, 3, states_86,
     "000040200000000000000000000040010000000000020002000300220050037000000"),
};
        static label[] labels = {
    new label(0,"EMPTY"),
    new label(256,""),
    new label(4,""),
    new label(270,""),
    new label(295,""),
    new label(257,""),
    new label(269,""),
    new label(0,""),
    new label(258,""),
    new label(331,""),
    new label(259,""),
    new label(49,""),
    new label(291,""),
    new label(7,""),
    new label(334,""),
    new label(8,""),
    new label(260,""),
    new label(261,""),
    new label(333,""),
    new label(263,""),
    new label(262,""),
    new label(1,"async"),
    new label(1,"def"),
    new label(1,""),
    new label(264,""),
    new label(51,""),
    new label(305,""),
    new label(11,""),
    new label(304,""),
    new label(265,""),
    new label(266,""),
    new label(22,""),
    new label(12,""),
    new label(16,""),
    new label(35,""),
    new label(267,""),
    new label(268,""),
    new label(271,""),
    new label(13,""),
    new label(272,""),
    new label(276,""),
    new label(277,""),
    new label(278,""),
    new label(284,""),
    new label(292,""),
    new label(293,""),
    new label(294,""),
    new label(274,""),
    new label(273,""),
    new label(275,""),
    new label(341,""),
    new label(314,""),
    new label(36,""),
    new label(37,""),
    new label(38,""),
    new label(50,""),
    new label(39,""),
    new label(40,""),
    new label(41,""),
    new label(42,""),
    new label(43,""),
    new label(44,""),
    new label(45,""),
    new label(46,""),
    new label(48,""),
    new label(1,"del"),
    new label(330,""),
    new label(1,"pass"),
    new label(279,""),
    new label(280,""),
    new label(281,""),
    new label(283,""),
    new label(282,""),
    new label(1,"break"),
    new label(1,"continue"),
    new label(1,"return"),
    new label(1,"raise"),
    new label(1,"from"),
    new label(285,""),
    new label(286,""),
    new label(1,"import"),
    new label(290,""),
    new label(23,""),
    new label(52,""),
    new label(289,""),
    new label(287,""),
    new label(1,"as"),
    new label(288,""),
    new label(1,"global"),
    new label(1,"nonlocal"),
    new label(1,"assert"),
    new label(297,""),
    new label(298,""),
    new label(299,""),
    new label(300,""),
    new label(301,""),
    new label(296,""),
    new label(1,"if"),
    new label(1,"elif"),
    new label(1,"else"),
    new label(1,"while"),
    new label(1,"for"),
    new label(1,"in"),
    new label(1,"try"),
    new label(303,""),
    new label(1,"finally"),
    new label(1,"with"),
    new label(302,""),
    new label(315,""),
    new label(1,"except"),
    new label(5,""),
    new label(6,""),
    new label(309,""),
    new label(307,""),
    new label(306,""),
    new label(308,""),
    new label(1,"lambda"),
    new label(310,""),
    new label(1,"or"),
    new label(311,""),
    new label(1,"and"),
    new label(1,"not"),
    new label(312,""),
    new label(313,""),
    new label(20,""),
    new label(21,""),
    new label(27,""),
    new label(30,""),
    new label(29,""),
    new label(28,""),
    new label(28,""),
    new label(1,"is"),
    new label(316,""),
    new label(18,""),
    new label(317,""),
    new label(32,""),
    new label(318,""),
    new label(19,""),
    new label(319,""),
    new label(33,""),
    new label(34,""),
    new label(320,""),
    new label(14,""),
    new label(15,""),
    new label(321,""),
    new label(17,""),
    new label(24,""),
    new label(47,""),
    new label(31,""),
    new label(322,""),
    new label(323,""),
    new label(1,"await"),
    new label(324,""),
    new label(326,""),
    new label(325,""),
    new label(9,""),
    new label(10,""),
    new label(25,""),
    new label(332,""),
    new label(26,""),
    new label(2,""),
    new label(3,""),
    new label(1,"None"),
    new label(1,"True"),
    new label(1,"False"),
    new label(338,""),
    new label(327,""),
    new label(328,""),
    new label(329,""),
    new label(1,"class"),
    new label(335,""),
    new label(336,""),
    new label(339,""),
    new label(337,""),
    new label(340,""),
    new label(1,"yield"),
    new label(342,""),
        };
        public static grammar _Grammar;

        #endregion
    }
}
