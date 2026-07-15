using Microsoft.VisualBasic;
using StudentProject.Data;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace StudentProject
{
    public partial class Form1 : Form
    {
        private string connectionString;
        private SQLiteConnection conn;
        private int selectedId;

        private DataLayer _dataLayer;
        public Form1()
        {
            selectedId = -1;
            InitializeComponent();
            _dataLayer = new DataLayer();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = _dataLayer.LoadStudentData();
            IDC_grdStudent.DataSource = dt;
            MakeDisable();
            ClearFields();
        }



        private void IDC_btnAdd_Click(object sender, EventArgs e)
        {
            _dataLayer.AddRecord(IDC_txtFirstName.Text, IDC_txtLastName.Text,
                  IDC_txtBackground.Text, IDC_dtpDOB.Value.ToString("yyyy-MM-dd"), IDC_drpStatus.Text);
            Form1_Load(sender, e);
        }

        private void IDC_grdStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            if (idx < 0 || IDC_grdStudent.Rows[e.RowIndex].Cells["studentID"].Value == DBNull.Value)
            {
                selectedId = -1;
                IDC_btnPopulate.Enabled = false;
                return;
            }
            selectedId = Convert.ToInt32(IDC_grdStudent.Rows[e.RowIndex].Cells["studentID"].Value);
            IDC_btnPopulate.Enabled = true;
            IDC_btnDelete.Enabled = true;

        }

        private void IDC_btnPopulate_Click(object sender, EventArgs e)
        {
            if (IDC_grdStudent.CurrentRow == null || selectedId == -1)
            {
                MessageBox.Show("You selected the wrong portion of data view, Sorry");
                return;
            }
            IDC_txtFirstName.Text = IDC_grdStudent.CurrentRow.Cells["firstName"].Value.ToString();
            IDC_txtLastName.Text = IDC_grdStudent.CurrentRow.Cells["lastName"].Value.ToString();
            IDC_txtBackground.Text = IDC_grdStudent.CurrentRow.Cells["background"].Value.ToString();
            IDC_dtpDOB.Value = Convert.ToDateTime(IDC_grdStudent.CurrentRow.Cells["dob"].Value.ToString());


            switch (IDC_grdStudent.CurrentRow.Cells["completionStatus"].Value)
            {
                case "Not Passed":
                    IDC_drpStatus.SelectedIndex = 1;
                    break;

                case "Partial":
                    IDC_drpStatus.SelectedIndex = 2;
                    break;

                case "Passed":
                    IDC_drpStatus.SelectedIndex = 3;
                    break;

                case "Started":
                    IDC_drpStatus.SelectedIndex = 4;
                    break;

                case "Withdrawn":
                    IDC_drpStatus.SelectedIndex = 5;
                    break;

                default:
                    IDC_drpStatus.SelectedIndex = 0;
                    break;

            }
            IDC_btnUpdate.Enabled = true;

        }

        private void IDC_btnUpdate_Click(object sender, EventArgs e)
        {
            if (IDC_grdStudent.CurrentRow == null || selectedId == -1)
            {
                MessageBox.Show("You selected the wrong portion of data view, Sorry");
                return;
            }
            _dataLayer.UpdateRecord(IDC_txtFirstName.Text, IDC_txtLastName.Text,
                  IDC_txtBackground.Text, IDC_dtpDOB.Value.ToString("yyyy-MM-dd"), IDC_drpStatus.Text, selectedId);

            Form1_Load(sender, e);
        }

        private void IDC_btnDelete_Click(object sender, EventArgs e)
        {
            if (IDC_grdStudent.CurrentRow == null || selectedId == -1)
            {
                MessageBox.Show("You selected the wrong portion of data view, Sorry");
                return;
            }
            _dataLayer.DeleteRecord(selectedId);
            Form1_Load(sender, e);

        }

        private void ClearFields()
        {
            IDC_txtFirstName.Text = "";
            IDC_txtLastName.Text = "";
            IDC_txtBackground.Text = "";
            IDC_dtpDOB.Value = DateTime.Today;
            IDC_drpStatus.SelectedIndex = 0;
        }

        private void MakeDisable()
        {
            IDC_btnPopulate.Enabled = false;
            IDC_btnUpdate.Enabled = false;
            IDC_btnDelete.Enabled = false;
        }

        private void IDC_btnReadOne_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter the Student ID to search");
            if (String.IsNullOrEmpty(input))
            {
                MessageBox.Show("Bad value");
                return;
            }

            try
            {
                int studentID = Convert.ToInt32(input);
                Helper.ShowOneRecord(_dataLayer, studentID);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Dont play with the system " + ex.Message);
            }
        }

        private void IDC_btnExport_Click(object sender, EventArgs e)
        {
            _dataLayer.ExportCSV();
        }

        private void IDC_btnImport_Click(object sender, EventArgs e)
        {
            _dataLayer.ImportCSV();
            Form1_Load(sender, e);
        }
    }
}
