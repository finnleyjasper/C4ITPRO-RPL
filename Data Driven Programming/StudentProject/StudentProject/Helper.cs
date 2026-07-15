using StudentProject.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace StudentProject
{
    internal static class Helper
    {

        public static string CsvEscape(string value)
        {
            if(String.IsNullOrEmpty(value)) return "";

            if(  value.Contains(",")
               || value.Contains("\"")
               || value.Contains("\n"))
            {
                //value = "\"" + value + "\"";
                value = $"\"{value}\"";
            }
            return value;
        }


        public static void ShowOneRecord(DataLayer _dataLayer, int id)
        {
            DataRow row = _dataLayer.ReadOneStudent(id);
            if (row == null)
            {
                MessageBox.Show("Student Record not found");
                return;
            }
            string output = "Student ID =" + row["studentID"] + "\n";
            output += "Name =" + row["firstName"] + " " + row["lastName"] + "\n";
            output += "Birth date = " + row["dob"] + "\n";
            output += "Status = " + row["completionStatus"] + "\n";

            MessageBox.Show(output);
        }



    }
}
