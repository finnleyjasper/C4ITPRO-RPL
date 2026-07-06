using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ComputeBill
{
    public class DomesticBill : ElectricityBill
    {

        public DomesticBill()
        {

        }
        public DomesticBill(int n)
        {
            numUnits = n;
        }

        public override double CalculateBill()
        {
            if (numUnits <= 0)
            {
                Console.WriteLine("You cannot enter number of units zero or -ve number");
                return -1;
            }
            return numUnits * Convert.ToDouble(Utilities.config["DomesticRate"]);
        }

    }
}
