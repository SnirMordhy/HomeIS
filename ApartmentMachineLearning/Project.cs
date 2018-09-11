using System;
using System.Collections.Generic;
using System.Text;


namespace ApartmentMachineLearning
{
    class Project
    {
        static void Main(string[] args)
        {
            // Display the number of command line arguments:
            System.Console.WriteLine(new PredictApartment().PredictApartmentSale(5,5,5));
        }
    }
}
