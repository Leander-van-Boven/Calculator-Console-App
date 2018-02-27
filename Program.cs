using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekenmachine
{
    class Program
    {
        public static void Main()
        {
            while (true)
            {
                try
                {
                    String invoer = Console.ReadLine();
                    ISom Som = Parser.ParseSom(invoer);
                    Console.Write(" . ");
                    System.Threading.Thread.Sleep(250);
                    Console.Write(" . ");
                    System.Threading.Thread.Sleep(250);
                    Console.WriteLine(" . ");
                    System.Threading.Thread.Sleep(250);
                    Console.WriteLine(Som.ToString() + " = " + Som.Waarde());
                    Console.WriteLine("---" + "\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"FOUT: {e.Message}");
                }
            }
        }
    }
}
