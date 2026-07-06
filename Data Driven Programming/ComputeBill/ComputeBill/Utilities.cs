using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputeBill
{
    static class Utilities
    {
        public static IConfigurationRoot config = new ConfigurationBuilder()
                                                 .SetBasePath(AppContext.BaseDirectory)
                                                 .AddJsonFile("appSettings.json", optional:false)
                                                 .Build();



        public static string CreateBillInterface()
        {
            string msg = "";
            msg += "*********************************************************\n";
            msg += "1. Commercial Bill\n";
            msg += "2. Domestic Bill\n";
            msg += "3. Industrial Bill\n";
            msg += "4. Exit\n";
            msg += "*********************************************************\n";

            return msg;

        }

        public static double ComputeBill(char op, int n)
        {
            double amt = 0.0;
            ElectricityBill e = null;

            switch (op)
            {
                case '1':
                    e = new CommercialBill(n);
                    break;

                case '2':
                    e = new DomesticBill(n);
                    break;

                case '3':
                    e = new IndustialBill(n);
                    break;

                case '4':
                default:
                    System.Environment.Exit(0);
                    break;
            }

            amt = e.CalculateBill();
            return amt;
        }


    }
}
