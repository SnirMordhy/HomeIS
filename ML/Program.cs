using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetOut(new StringWriter());
            Console.SetError(new StringWriter());
            bool output = new PredictApartment().PredictApartmentSale(Int32.Parse(args[0]), Int32.Parse(args[1]), Int32.Parse(args[2]));

            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);

            Console.WriteLine(output);
        }
    }
}