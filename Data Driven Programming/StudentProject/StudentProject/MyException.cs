using System;
using System.Collections.Generic;
using System.Text;

namespace StudentProject
{
    internal class MyException: Exception
    {
        public MyException() { }
        public string ShowMessage() { return "This is do nothing "; }
    }
}
