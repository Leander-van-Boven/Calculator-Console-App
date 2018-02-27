using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        public static void Main()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Type in any calculation you want! \n-Use brackets for priority, * as multiplication symbol, " +                  //Welcome message
                        "\n-Use xVy for a \'factor y root out x\', and x^y for \'x to the power y\' \nTips and bugreports are appreciated! \n--- \n");

                    String input = Console.ReadLine();
                    ISum Sum = Parser.ParseSom(input);

                    Console.Write(" . ");
                    System.Threading.Thread.Sleep(250);             //     
                    Console.Write(" . ");
                    System.Threading.Thread.Sleep(250);             //  Used for the 'thinking' animation
                    Console.WriteLine(" . ");
                    System.Threading.Thread.Sleep(250);             //

                    Console.WriteLine(Sum.ToString() + " = " + Sum.Value());   // Returns the asnwer
                    Console.WriteLine("---" + "\n");                            // Seperation for next sum
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }
    }
}
