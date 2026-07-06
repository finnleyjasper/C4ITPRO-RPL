using System;
using System.Collections.Generic;
using System.Text;

///////////////////////////////////////////////////////////////////////////////
///Purpose      : This program computes the bill based upon different bill category such as industrial....
///Date         : 2026-March-25
///Author       : Jefi Varghese
///Copy Rights  : Holmesglen
//////////////////////////////////////////////////////////////////////////////

namespace ComputeBill
{
    public class ElectricityBill
    {
        protected int numUnits { get; set; }

        //Compute the bill which will overridden
        public virtual double CalculateBill()
        {
            return 0;
        }
    }
}
