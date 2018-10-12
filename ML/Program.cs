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
            // Changing the default output to prevent unwanted output to be passed
            Console.SetOut(new StringWriter());
            Console.SetError(new StringWriter());

            // Predict the output
            bool output = new PredictApartment().PredictApartmentSale(Int32.Parse(args[0]), Int32.Parse(args[1]), Int32.Parse(args[2]));

            // Go back to the default output
            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);

            // Write the prediction to the output
            Console.WriteLine(output);
        }
    }
}