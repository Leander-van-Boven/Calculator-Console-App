using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Calculator
{
    class Parser
    {
        private string contents;
        private int cursor;
        private int length;
        private char d = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        public static ISum ParseSom(string s)
        {
            Parser parser = new Parser(s);
            return parser.ParseSum();
        }

        private Parser(string s)            
        {
            contents = s;
            cursor = 0;                 // Parser setup, cursor starts at the first character
            length = s.Length;
        }

        private void SkipSpaces()
        {
            while (cursor < length && char.IsWhiteSpace(contents[cursor]))      // Deals with extra whitespaces in between parts of the sum
                cursor++;
        }
        //
        // // Recognition of different kind of operators, set up so that priority is taken into account, recursively defined
        //
        private ISum ParseSum()
        {
            ISum g = ParseSubtraction();
            SkipSpaces();
            return g;
        }
        private ISum ParseSubtraction()
        {
            ISum o = ParseAddition();
            SkipSpaces();
            if (cursor < length - 1 && contents[cursor] == '-')
            {
                cursor++;
                ISum a = ParseSubtraction();
                return MakeSubtraction(o, a);
            }
            return o;
        }
        private ISum ParseAddition()
        {
            ISum d = ParseDivision();
            SkipSpaces();
            if (cursor < length - 1 && contents[cursor] == '+')
            {
                cursor++;
                ISum o = ParseAddition();
                return MakeAddition(d, o);
            }
            return d;
        }
        private ISum ParseDivision()
        {
            ISum v = ParseMultiplication();
            SkipSpaces();
            if (cursor < length -1 && contents[cursor] == '/')
            {
                cursor++;
                ISum d = ParseDivision();
                return MakeDivision(v, d);
            }
            return v;
        }
        private ISum ParseMultiplication()
        {
            ISum w = ParseRoot();
            SkipSpaces();
            if (cursor < length - 1 && contents[cursor] == '*')
            {
                cursor++;
                ISum v = ParseMultiplication();
                return MakeMultiplication(w, v);
            }
            return w;
        }
        private ISum ParseRoot()
        {
            ISum m = ParsePower();
            SkipSpaces();
            if (cursor < length - 1 && contents[cursor] == 'V')
            {
                cursor++;
                ISum w = ParseRoot();
                return MakeRoot(m, w);
            }
            return m;
        }
        private ISum ParsePower()
        {
            ISum g = ParseNumber();
            SkipSpaces();
            if (cursor < length - 1 && contents[cursor] == '^')
            {
                cursor++;
                ISum m = ParsePower();
                return MakePower(g, m);
            }
            return g;
        }
        private ISum ParseNumber()
        {
            SkipSpaces();
            if (cursor < length && contents[cursor] == '(')
            {
                cursor++;
                ISum resultaat = ParseSubtraction();
                SkipSpaces();
                if (contents[cursor] != ')') throw new Exception("Sluithaakje ontbreekt op positie " + cursor);
                cursor++;
                return resultaat;
            }
            else
            {
                string number = "";
                while (cursor < length && char.IsDigit(contents[cursor]) == true)
                {
                    number = number + char.ToString(contents[cursor]);
                    cursor++;
                }
                if (cursor < length && (contents[cursor].Equals(',') || contents[cursor].Equals('.') || contents[cursor].Equals(d)))
                {
                    number = number + d;
                    cursor++;
                    while (cursor < length && char.IsDigit(contents[cursor]) == true)
                    {
                        number = number + char.ToString(contents[cursor]);
                        cursor++;
                    }
                }
                ISum resultaat = MaakGetal(number);
                return resultaat;
            }
        }
        //
        // // 
        //
        private ISum MaakGetal(string number)
        {
            Number digit = new Number(number);
            return digit;
        }
        private ISum MakeSubtraction(ISum links, ISum rechts)
        {
            Subtraction difference = new Subtraction(links, rechts);
            return difference;
        }
        private ISum MakeAddition(ISum links, ISum rechts)
        {
            Addition addition = new Addition(links, rechts);
            return addition;
        }
        private ISum MakeDivision(ISum left, ISum right)
        {
            Division division = new Division(left, right);
            return division;
        }
        private ISum MakeMultiplication(ISum links, ISum rechts)
        {
            Product product = new Product(links, rechts);
            return product;
        }
        private ISum MakeRoot(ISum grond, ISum expo)
        {
            Root wortel = new Root(grond, expo);
            return wortel;
        }
        private ISum MakePower(ISum grond, ISum expo)
        {
            Power macht = new Power(grond, expo);
            return macht;
        }
    }
}
