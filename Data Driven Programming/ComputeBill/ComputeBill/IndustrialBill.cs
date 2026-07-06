using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace ComputeBill
{
    public class IndustialBill : ElectricityBill
    {

        public IndustialBill()
        {

        }
        public IndustialBill(int n)
        {
            numUnits = n;
        }

        public override double CalculateBill()
        {
            if(numUnits<=0)
            {
                Console.WriteLine("You cannot enter number of units zero or -ve number");
                return -1;
            }

            return numUnits * Convert.ToDouble(Utilities.config["IndustryRate"]);
        }

    }
}
