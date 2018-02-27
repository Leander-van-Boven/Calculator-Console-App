using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Rekenmachine
{
    class Parser
    {
        private string inhoud;
        private int cursor;
        private int lengte;
        private char d = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        public static ISom ParseSom(string s)
        {
            Parser parser = new Parser(s);
            return parser.ParseSom();
        }

        private Parser(string s)
        {
            inhoud = s;
            cursor = 0;
            lengte = s.Length;
        }

        private void SkipSpaces()
        {
            while (cursor < lengte && char.IsWhiteSpace(inhoud[cursor]))
                cursor++;
        }

        private ISom ParseSom()
        {
            ISom g = ParseVerschil();
            SkipSpaces();
            //if (cursor < lengte)
            //    throw new Exception($"Extra input op positie {cursor} {inhoud[cursor]}");
            return g;
        }

        private ISom ParseGetal()
        {
            SkipSpaces();
            if (cursor<lengte && inhoud[cursor] == '(')
            {
                cursor++;
                ISom resultaat = ParseVerschil();
                SkipSpaces();
                if (inhoud[cursor] != ')') throw new Exception("Sluithaakje ontbreekt op positie " + cursor);
                cursor++;
                return resultaat;
            }
            else
            {
                string getal = "";
                while (cursor < lengte && char.IsDigit(inhoud[cursor]) == true)
                {
                    getal = getal + char.ToString(inhoud[cursor]);
                    cursor++;
                }
                if (cursor < lengte && (inhoud[cursor].Equals(',') || inhoud[cursor].Equals('.') || inhoud[cursor].Equals(d)))
                {
                    getal = getal + d;
                    cursor++;
                    while (cursor < lengte && char.IsDigit(inhoud[cursor]) == true)
                    {
                        getal = getal + char.ToString(inhoud[cursor]);
                        cursor++;
                    }
                }
                ISom resultaat = MaakGetal(getal);
                return resultaat;
            }
        }

        private ISom ParseVerschil()
        {
            ISom o = ParseOptelling();
            SkipSpaces();
            if (cursor < lengte - 1 && inhoud[cursor] == '-')
            {
                cursor++;
                ISom a = ParseVerschil();
                return MaakVerschil(o, a);
            }
            return o;
        }

        private ISom ParseOptelling()
        {
            ISom d = ParseDeling();
            SkipSpaces();
            if (cursor < lengte - 1 && inhoud[cursor] == '+')
            {
                cursor++;
                ISom o = ParseOptelling();
                return MaakOptelling(d, o);
            }
            return d;
        }

        private ISom ParseDeling()
        {
            ISom v = ParseVermenigvuldiging();
            SkipSpaces();
            if (cursor < lengte -1 && inhoud[cursor] == '/')
            {
                cursor++;
                ISom d = ParseDeling();
                return MaakDeling(v, d);
            }
            return v;
        }

        private ISom ParseVermenigvuldiging()
        {
            ISom m = ParseMacht();
            SkipSpaces();
            if (cursor < lengte - 1 && inhoud[cursor] == '*')
            {
                cursor++;
                ISom v = ParseVermenigvuldiging();
                return MaakVermenigvuldiging(m, v);
            }
            return m;
        }

        private ISom ParseMacht()
        {
            ISom g = ParseGetal();
            SkipSpaces();
            if (cursor < lengte - 1 && inhoud[cursor] == '^')
            {
                cursor++;
                ISom m = ParseMacht();
                return MaakMacht(g, m);
            }
            return g;
        }
        //
        // //
        //
        private ISom MaakGetal(string getal)
        {
            Getal cijfer = new Getal(getal);
            return cijfer;
        }
        private ISom MaakVerschil(ISom links, ISom rechts)
        {
            Verschil verschil = new Verschil(links, rechts);
            return verschil;
        }
        private ISom MaakOptelling(ISom links, ISom rechts)
        {
            Som optelling = new Som(links, rechts);
            return optelling;
        }
        private ISom MaakDeling(ISom links, ISom rechts)
        {
            Deling deling = new Deling(links, rechts);
            return deling;
        }
        private ISom MaakVermenigvuldiging(ISom links, ISom rechts)
        {
            Product product = new Product(links, rechts);
            return product;
        }
        private ISom MaakMacht(ISom grond, ISom expo)
        {
            Macht macht = new Macht(grond, expo);
            return macht;
        }
    }
}
