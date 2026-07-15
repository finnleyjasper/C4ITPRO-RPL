using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;


namespace StudentProject.Data
{
    internal class DataLayer
    {
        private string connectionString;
        private SQLiteConnection conn;

        public DataLayer()
        {
            try
            {
                connectionString = @"Data Source=C:\GiftofGod\Holmesglen\Data Driven Apps\Database\StudentDB.db";
                conn = new SQLiteConnection(connectionString);
                conn.Open();
             
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception happened. The reason is \n" + ex.Message);
            }
        }


        public DataTable LoadStudentData()
        {
            DataTable dt = new DataTable();
            string strSQL = "SELECT * FROM Student";

            try
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(strSQL, conn))
                {
                    adapter.Fill(dt);
                    dt.Dispose();
                    adapter.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception happened. The reason is \n" + ex.Message);
            }

            return dt;
        }

        public void AddRecord(string fn, string ln, string bg, string dob, string cs)
        {
            string strSQL = "SELECT * FROM Student";

            try
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(strSQL, conn))
                {
                    SQLiteCommandBuilder command = new SQLiteCommandBuilder(adapter);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    DataRow row = dt.NewRow();
                    row["firstName"] = fn;
                    row["lastName"] = ln;
                    row["background"] = bg;
                    row["dob"] = dob;
                    row["completionStatus"] = cs;

                    dt.Rows.Add(row);
                    adapter.Update(dt);

                    dt.Dispose();
                    command.Dispose();
                }

                MessageBox.Show("Record inserted");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception happened. The reason is \n" + ex.Message);
            }
        }


        public void UpdateRecord(string fn, string ln, string bg, string dob, string cs, int id)
        {
            try
            {

                // string strSQL = "SELECT * FROM Student WHERE studentID = " + id; //can cause SQL Injection
                string strSQL = "SELECT * FROM Student WHERE studentID =@idSQL";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(strSQL, conn))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@idSQL", id);
                    SQLiteCommandBuilder command = new SQLiteCommandBuilder(adapter);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows[0]["firstName"] = fn;
                        dt.Rows[0]["lastName"] = ln;
                        dt.Rows[0]["background"] = bg;
                        dt.Rows[0]["dob"] = dob;
                        dt.Rows[0]["completionStatus"] = cs;

                        adapter.Update(dt);
                    }
                    dt.Dispose();
                    command.Dispose();
                }
                MessageBox.Show("Record updated");
            }
          
            catch (Exception ex)
            {
                MessageBox.Show("Exception happened. The reason is \n" + ex.Message);
            }
        }


        public void DeleteRecord(int id)
        {
            //string strSQL = "SELECT * FROM Student WHERE studentID = " + id;
            string strSQL = "SELECT * FROM Student WHERE studentID = @idSQL";

            try
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(strSQL, conn))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@idSQL", id);
                    SQLiteCommandBuilder command = new SQLiteCommandBuilder(adapter);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows[0].Delete();
                        adapter.Update(dt);
                    }
                    dt.Dispose();
                    command.Dispose();

                }

                MessageBox.Show("Record deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception happened. The reason is \n" + ex.Message);
            }
        }

        public DataRow ReadOneStudent(int id)
        {
            try
            {
                string strSQL = "SELECT * FROM Student WHERE studentID = @idSQL";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(strSQL, conn))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@idSQL", id);
                    SQLiteCommandBuilder command = new SQLiteCommandBuilder(adapter);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];

                    }
                    dt.Dispose();
                    command.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception happened. The reason is \n" + ex.Message);
            }



            return null;
        }

        public void ExportCSV()
        {
            try
            {
                SaveFileDialog sfg = new SaveFileDialog();
                sfg.Filter = "CSV Files (*.csv)|*.csv";
                sfg.FileName = "ExportedStudent.csv";

                if(sfg.ShowDialog()==DialogResult.OK)
                {
                    string strSQL = "SELECT * FROM Student";

                    using (SQLiteCommand cmd = new SQLiteCommand(strSQL, conn))
                    {
                        using (SQLiteDataReader reader= cmd.ExecuteReader())
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("First Name, Surname, Background, Birthdate, Status");
                            while(reader.Read())
                            {
                                 string firstName = Helper.CsvEscape(reader["firstName"].ToString());
                                 string surname   = Helper.CsvEscape(reader["lastName"].ToString());
                                 string bg        = Helper.CsvEscape(reader["background"].ToString());
                                 string dob       = Helper.CsvEscape(reader["dob"].ToString());
                                 string status    = Helper.CsvEscape(reader["completionStatus"].ToString());
                                
                                sb.AppendLine(firstName + "," + surname + "," + bg + "," + dob + "," + status);
                            }

                            File.WriteAllText(sfg.FileName, sb.ToString());
                        }
                    }

                    MessageBox.Show("CSV file exported successfully");

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception happened. The reason is \n" + ex.Message);
            }
        }

        public void ImportCSV()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Files (*.csv)|*.csv";
            try
            {
                if(ofd.ShowDialog()==DialogResult.OK)
                {
                    using (TextFieldParser parser = new TextFieldParser(ofd.FileName))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");
                        parser.HasFieldsEnclosedInQuotes = true;

                        parser.ReadLine();

                        while(!parser.EndOfData)
                        {
                            string[] values = parser.ReadFields();

                            if(values.Length >=5)
                            {
                                string firstName    = values[0].Trim();
                                string surname      = values[1].Trim(); ;
                                string bg           = values[2].Trim(); ;
                                string dob          = Convert.ToDateTime(values[3].Trim()).ToString("yyyy-MM-dd"); ;
                                string status       = values[4].Trim(); ;

                                string strSQL = "INSERT INTO Student (firstName, lastName, background, dob, completionStatus) VALUES ";
                                strSQL += "(@fn, @ln, @bg, @dob, @cs)";

                                using (SQLiteCommand cmd = new SQLiteCommand(strSQL, conn))
                                {
                                    cmd.Parameters.AddWithValue("@fn", firstName);
                                    cmd.Parameters.AddWithValue("@ln", surname);
                                    cmd.Parameters.AddWithValue("@bg", bg);
                                    cmd.Parameters.AddWithValue("@dob", dob);
                                    cmd.Parameters.AddWithValue("@cs", status);

                                    cmd.ExecuteNonQuery();
                                }



                            }

                        }

                        MessageBox.Show("CSV Imported successfully");

                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception happened. The reason is \n" + ex.Message);
            }
        }


   }
}
