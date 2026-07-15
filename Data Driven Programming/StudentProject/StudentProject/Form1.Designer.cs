namespace StudentProject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            IDC_txtFirstName = new TextBox();
            IDC_lblFirstName = new Label();
            IDC_txtLastName = new TextBox();
            IDC_lblLastName = new Label();
            IDC_txtBackground = new TextBox();
            IDC_lblBackground = new Label();
            IDC_dtpDOB = new DateTimePicker();
            IDC_lblDOB = new Label();
            IDC_drpStatus = new ComboBox();
            label1 = new Label();
            IDC_grdStudent = new DataGridView();
            IDC_bsData = new BindingSource(components);
            IDC_btnAdd = new Button();
            IDC_btnPopulate = new Button();
            IDC_btnUpdate = new Button();
            IDC_btnDelete = new Button();
            IDC_btnReadOne = new Button();
            IDC_btnExport = new Button();
            IDC_btnImport = new Button();
            ((System.ComponentModel.ISupportInitialize)IDC_grdStudent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IDC_bsData).BeginInit();
            SuspendLayout();
            // 
            // IDC_txtFirstName
            // 
            IDC_txtFirstName.Location = new Point(217, 75);
            IDC_txtFirstName.Name = "IDC_txtFirstName";
            IDC_txtFirstName.Size = new Size(150, 31);
            IDC_txtFirstName.TabIndex = 0;
            // 
            // IDC_lblFirstName
            // 
            IDC_lblFirstName.AutoSize = true;
            IDC_lblFirstName.Location = new Point(104, 81);
            IDC_lblFirstName.Name = "IDC_lblFirstName";
            IDC_lblFirstName.Size = new Size(97, 25);
            IDC_lblFirstName.TabIndex = 1;
            IDC_lblFirstName.Text = "First Name";
            // 
            // IDC_txtLastName
            // 
            IDC_txtLastName.Location = new Point(569, 73);
            IDC_txtLastName.Name = "IDC_txtLastName";
            IDC_txtLastName.Size = new Size(150, 31);
            IDC_txtLastName.TabIndex = 2;
            // 
            // IDC_lblLastName
            // 
            IDC_lblLastName.AutoSize = true;
            IDC_lblLastName.Location = new Point(481, 76);
            IDC_lblLastName.Name = "IDC_lblLastName";
            IDC_lblLastName.Size = new Size(82, 25);
            IDC_lblLastName.TabIndex = 3;
            IDC_lblLastName.Text = "Surname";
            // 
            // IDC_txtBackground
            // 
            IDC_txtBackground.Location = new Point(217, 167);
            IDC_txtBackground.Multiline = true;
            IDC_txtBackground.Name = "IDC_txtBackground";
            IDC_txtBackground.Size = new Size(502, 146);
            IDC_txtBackground.TabIndex = 4;
            // 
            // IDC_lblBackground
            // 
            IDC_lblBackground.AutoSize = true;
            IDC_lblBackground.Location = new Point(103, 217);
            IDC_lblBackground.Name = "IDC_lblBackground";
            IDC_lblBackground.Size = new Size(107, 25);
            IDC_lblBackground.TabIndex = 5;
            IDC_lblBackground.Text = "Background";
            // 
            // IDC_dtpDOB
            // 
            IDC_dtpDOB.Location = new Point(217, 372);
            IDC_dtpDOB.Name = "IDC_dtpDOB";
            IDC_dtpDOB.Size = new Size(300, 31);
            IDC_dtpDOB.TabIndex = 6;
            // 
            // IDC_lblDOB
            // 
            IDC_lblDOB.AutoSize = true;
            IDC_lblDOB.Location = new Point(103, 372);
            IDC_lblDOB.Name = "IDC_lblDOB";
            IDC_lblDOB.Size = new Size(90, 25);
            IDC_lblDOB.TabIndex = 7;
            IDC_lblDOB.Text = "Birth Date";
            // 
            // IDC_drpStatus
            // 
            IDC_drpStatus.FormattingEnabled = true;
            IDC_drpStatus.Items.AddRange(new object[] { "--Select--", "Not Passed", "Partial", "Passed", "Started", "Withdrawn" });
            IDC_drpStatus.Location = new Point(217, 455);
            IDC_drpStatus.Name = "IDC_drpStatus";
            IDC_drpStatus.Size = new Size(182, 33);
            IDC_drpStatus.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(104, 458);
            label1.Name = "label1";
            label1.Size = new Size(97, 25);
            label1.TabIndex = 9;
            label1.Text = "Complete?";
            // 
            // IDC_grdStudent
            // 
            IDC_grdStudent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            IDC_grdStudent.Location = new Point(217, 563);
            IDC_grdStudent.Name = "IDC_grdStudent";
            IDC_grdStudent.RowHeadersWidth = 62;
            IDC_grdStudent.Size = new Size(800, 225);
            IDC_grdStudent.TabIndex = 10;
            IDC_grdStudent.CellClick += IDC_grdStudent_CellClick;
            // 
            // IDC_btnAdd
            // 
            IDC_btnAdd.Location = new Point(217, 509);
            IDC_btnAdd.Name = "IDC_btnAdd";
            IDC_btnAdd.Size = new Size(182, 34);
            IDC_btnAdd.TabIndex = 11;
            IDC_btnAdd.Text = "Add";
            IDC_btnAdd.UseVisualStyleBackColor = true;
            IDC_btnAdd.Click += IDC_btnAdd_Click;
            // 
            // IDC_btnPopulate
            // 
            IDC_btnPopulate.Location = new Point(424, 509);
            IDC_btnPopulate.Name = "IDC_btnPopulate";
            IDC_btnPopulate.Size = new Size(193, 34);
            IDC_btnPopulate.TabIndex = 12;
            IDC_btnPopulate.Text = "Populate";
            IDC_btnPopulate.UseVisualStyleBackColor = true;
            IDC_btnPopulate.Click += IDC_btnPopulate_Click;
            // 
            // IDC_btnUpdate
            // 
            IDC_btnUpdate.Location = new Point(642, 509);
            IDC_btnUpdate.Name = "IDC_btnUpdate";
            IDC_btnUpdate.Size = new Size(157, 34);
            IDC_btnUpdate.TabIndex = 13;
            IDC_btnUpdate.Text = "Update";
            IDC_btnUpdate.UseVisualStyleBackColor = true;
            IDC_btnUpdate.Click += IDC_btnUpdate_Click;
            // 
            // IDC_btnDelete
            // 
            IDC_btnDelete.Location = new Point(833, 509);
            IDC_btnDelete.Name = "IDC_btnDelete";
            IDC_btnDelete.Size = new Size(146, 34);
            IDC_btnDelete.TabIndex = 14;
            IDC_btnDelete.Text = "Delete";
            IDC_btnDelete.UseVisualStyleBackColor = true;
            IDC_btnDelete.Click += IDC_btnDelete_Click;
            // 
            // IDC_btnReadOne
            // 
            IDC_btnReadOne.Location = new Point(213, 817);
            IDC_btnReadOne.Name = "IDC_btnReadOne";
            IDC_btnReadOne.Size = new Size(240, 34);
            IDC_btnReadOne.TabIndex = 15;
            IDC_btnReadOne.Text = "Get a Record";
            IDC_btnReadOne.UseVisualStyleBackColor = true;
            IDC_btnReadOne.Click += IDC_btnReadOne_Click;
            // 
            // IDC_btnExport
            // 
            IDC_btnExport.Location = new Point(505, 817);
            IDC_btnExport.Name = "IDC_btnExport";
            IDC_btnExport.Size = new Size(175, 34);
            IDC_btnExport.TabIndex = 16;
            IDC_btnExport.Text = "Export";
            IDC_btnExport.UseVisualStyleBackColor = true;
            IDC_btnExport.Click += IDC_btnExport_Click;
            // 
            // IDC_btnImport
            // 
            IDC_btnImport.Location = new Point(731, 817);
            IDC_btnImport.Name = "IDC_btnImport";
            IDC_btnImport.Size = new Size(158, 34);
            IDC_btnImport.TabIndex = 17;
            IDC_btnImport.Text = "Import";
            IDC_btnImport.UseVisualStyleBackColor = true;
            IDC_btnImport.Click += IDC_btnImport_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1046, 867);
            Controls.Add(IDC_btnImport);
            Controls.Add(IDC_btnExport);
            Controls.Add(IDC_btnReadOne);
            Controls.Add(IDC_btnDelete);
            Controls.Add(IDC_btnUpdate);
            Controls.Add(IDC_btnPopulate);
            Controls.Add(IDC_btnAdd);
            Controls.Add(IDC_grdStudent);
            Controls.Add(label1);
            Controls.Add(IDC_drpStatus);
            Controls.Add(IDC_lblDOB);
            Controls.Add(IDC_dtpDOB);
            Controls.Add(IDC_lblBackground);
            Controls.Add(IDC_txtBackground);
            Controls.Add(IDC_lblLastName);
            Controls.Add(IDC_txtLastName);
            Controls.Add(IDC_lblFirstName);
            Controls.Add(IDC_txtFirstName);
            Name = "Form1";
            Text = "Student";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)IDC_grdStudent).EndInit();
            ((System.ComponentModel.ISupportInitialize)IDC_bsData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox IDC_txtFirstName;
        private Label IDC_lblFirstName;
        private TextBox IDC_txtLastName;
        private Label IDC_lblLastName;
        private TextBox IDC_txtBackground;
        private Label IDC_lblBackground;
        private DateTimePicker IDC_dtpDOB;
        private Label IDC_lblDOB;
        private ComboBox IDC_drpStatus;
        private Label label1;
        private DataGridView IDC_grdStudent;
        private BindingSource IDC_bsData;
        private Button IDC_btnAdd;
        private Button IDC_btnPopulate;
        private Button IDC_btnUpdate;
        private Button IDC_btnDelete;
        private Button IDC_btnReadOne;
        private Button IDC_btnExport;
        private Button IDC_btnImport;
    }
}
