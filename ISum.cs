using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
// Recursive defined interface, program will jump from the parser to the interface and back through the progress of solving the sum
//
namespace Calculator
{
    public interface ISum
    {
        double Value();         // The value of the whole sum, updates as we progress through the sum
        string ToString();      // To print the sum with parenthesis, mostly for debugging, but is appealing for user too
    }



    public class Number : ISum
    {
        string digit;
        public Number(string digit)
        {
            this.digit = digit;
        }
        public double Value()
        {
            return double.Parse(digit);
        }
        public override string ToString()
        {
            return digit;
        }
    }

    public class Power : ISum
    {
        ISum Base;
        ISum Exponent;

        public Power(ISum ground, ISum power)
        {
            Base = ground;
            Exponent = power;
        }
        public double Value()
        {
            return Math.Pow(Base.Value(), Exponent.Value());
        }
        public override string ToString()
        {
            return "(" + Base.ToString() + "^" + "(" + Exponent.ToString() + "))";
        }
    }

    public class Root : ISum
    {
        ISum Base;
        ISum Exponent;

        public Root(ISum ground, ISum power)
        {
            Base = ground;
            Exponent = power;
        }
        public double Value()
        {
            return Math.Pow(Base.Value(), (1 / Exponent.Value()));                  // Since there is only a square-root within the Math class, a root is devined as the reverse power
        }
        public override string ToString()
        {
            return "(^" + Exponent.ToString() + (char)0x221A + Base.ToString() + ")";           // Char 0x221A is the root character
        }
    }

    public class Product : ISum
    {
        ISum MultiplicationLeft;
        ISum MultiplicationRight;

       public Product(ISum left, ISum right)
        {
            MultiplicationLeft = left;
            MultiplicationRight = right;
        }
        public double Value()
        {
            return MultiplicationLeft.Value() * MultiplicationRight.Value();
        }
        public override string ToString()
        {
            return "(" + MultiplicationLeft.ToString() + "*" + MultiplicationRight.ToString() + ")";
        }
    }

    public class Division : ISum
    {
        ISum DivisionLeft;
        ISum DivisionRight;

        public Division(ISum left, ISum right)
        {
            DivisionLeft = left;
            DivisionRight = right;
        }
        public double Value()
        {
            return DivisionLeft.Value() / DivisionRight.Value();
        }
        public override string ToString()
        {
            return "(" + DivisionLeft.ToString() + "/" + DivisionRight.ToString() + ")";
        }
    }

    public class Addition : ISum
    {
        ISum AdditionLeft;
        ISum AdditionRight;

        public Addition(ISum left, ISum right)
        {
            AdditionLeft = left;
            AdditionRight = right;
        }
        public double Value()
        {
            return AdditionLeft.Value() + AdditionRight.Value();
        }
        public override string ToString()
        {
            return "(" + AdditionLeft.ToString() + "+" + AdditionRight.ToString() + ")";
        }
    }

    public class Subtraction : ISum
    {
        ISum SubtractionLeft;
        ISum SubtractionRight;

        public Subtraction(ISum left, ISum right)
        {
            SubtractionLeft = left;
            SubtractionRight = right;
        }
        public double Value()
        {
            return SubtractionLeft.Value() - SubtractionRight.Value();
        }
        public override string ToString()
        {
            return "(" + SubtractionLeft.ToString() + "-" + SubtractionRight.ToString() + ")";
        }
    }
}
