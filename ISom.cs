using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekenmachine
{
    public interface ISom
    {
        double Waarde();
        string ToString();
    }

    public class Getal : ISom
    {
        string cijfer;
        public Getal(string getal)
        {
            cijfer = getal;
        }
        public double Waarde()
        {
            double d;
            Double.TryParse(cijfer, out d);
            return d;
            //return double.Parse(cijfer);
        }
        public override string ToString()
        {
            return cijfer;
        }
    }

    public class Macht : ISom
    {
        ISom Grondtal;
        ISom Exponent;

        public Macht(ISom grond, ISom macht)
        {
            Grondtal = grond;
            Exponent = macht;
        }
        public double Waarde()
        {
            double n;
            double getal = 1;
            for (n = 0; n < Exponent.Waarde(); n++)
            {
                getal *= Grondtal.Waarde();
            }
            return getal;
        }
        public override string ToString()
        {
            return "(" + Grondtal.ToString() + "^" + "(" + Exponent.ToString() + "))";
        }
    }

    public class Product : ISom
    {
        ISom ProductLinks;
        ISom ProductRechts;

       public Product(ISom links, ISom rechts)
        {
            ProductLinks = links;
            ProductRechts = rechts;
        }
        public double Waarde()
        {
            return ProductLinks.Waarde() * ProductRechts.Waarde();
        }
        public override string ToString()
        {
            return "(" + ProductLinks.ToString() + "*" + ProductRechts.ToString() + ")";
        }
    }

    public class Deling : ISom
    {
        ISom DelingLinks;
        ISom DelingRechts;

        public Deling(ISom links, ISom rechts)
        {
            DelingLinks = links;
            DelingRechts = rechts;
        }
        public double Waarde()
        {
            return DelingLinks.Waarde() / DelingRechts.Waarde();
        }
        public override string ToString()
        {
            return "(" + DelingLinks.ToString() + "/" + DelingRechts.ToString() + ")";
        }
    }

    public class Som : ISom
    {
        ISom SomLinks;
        ISom SomRechts;

        public Som(ISom links, ISom rechts)
        {
            SomLinks = links;
            SomRechts = rechts;
        }
        public double Waarde()
        {
            return SomLinks.Waarde() + SomRechts.Waarde();
        }
        public override string ToString()
        {
            return "(" + SomLinks.ToString() + "+" + SomRechts.ToString() + ")";
        }
    }

    public class Verschil : ISom
    {
        ISom VerschilLinks;
        ISom VerschilRechts;

        public Verschil(ISom links, ISom rechts)
        {
            VerschilLinks = links;
            VerschilRechts = rechts;
        }
        public double Waarde()
        {
            return VerschilLinks.Waarde() - VerschilRechts.Waarde();
        }
        public override string ToString()
        {
            return "(" + VerschilLinks.ToString() + "-" + VerschilRechts.ToString() + ")";
        }
    }
}
