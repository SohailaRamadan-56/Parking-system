namespace Parking_System
{
    partial class ReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblFrom = new Label();
            lblTo = new Label();
            lblRevenue = new Label();
            lblAverage = new Label();
            lblOccupancy = new Label();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            dgvReports = new DataGridView();
            btnGenerate = new Button();
            btnExport = new Button();
            pnlChart1 = new Panel();
            pnlChart2 = new Panel();
            panel3 = new Panel();
            lblNumRevenue = new Label();
            panel1 = new Panel();
            lblNumCars = new Label();
            label2 = new Label();
            panel2 = new Panel();
            lblAverageStay = new Label();
            panel4 = new Panel();
            lblOccupancynow = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvReports).BeginInit();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Location = new Point(12, 5);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(43, 20);
            lblFrom.TabIndex = 0;
            lblFrom.Text = "From";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Location = new Point(326, 5);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(25, 20);
            lblTo.TabIndex = 1;
            lblTo.Text = "To";
            // 
            // lblRevenue
            // 
            lblRevenue.AutoSize = true;
            lblRevenue.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRevenue.Location = new Point(3, 0);
            lblRevenue.Name = "lblRevenue";
            lblRevenue.Size = new Size(185, 28);
            lblRevenue.TabIndex = 2;
            lblRevenue.Text = "Total revenue(EGP)";
            // 
            // lblAverage
            // 
            lblAverage.AutoSize = true;
            lblAverage.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAverage.Location = new Point(3, 0);
            lblAverage.Name = "lblAverage";
            lblAverage.Size = new Size(128, 28);
            lblAverage.TabIndex = 4;
            lblAverage.Text = "Average stay";
            // 
            // lblOccupancy
            // 
            lblOccupancy.AutoSize = true;
            lblOccupancy.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOccupancy.Location = new Point(-2, 0);
            lblOccupancy.Name = "lblOccupancy";
            lblOccupancy.Size = new Size(155, 28);
            lblOccupancy.TabIndex = 5;
            lblOccupancy.Text = "Occupancy now";
            lblOccupancy.Click += lblOccupancy_Click;
            // 
            // dtpFrom
            // 
            dtpFrom.Location = new Point(61, 5);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(250, 27);
            dtpFrom.TabIndex = 6;
            // 
            // dtpTo
            // 
            dtpTo.Location = new Point(368, 5);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(250, 27);
            dtpTo.TabIndex = 7;
            // 
            // dgvReports
            // 
            dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReports.Location = new Point(12, 161);
            dgvReports.Name = "dgvReports";
            dgvReports.RowHeadersWidth = 51;
            dgvReports.Size = new Size(403, 277);
            dgvReports.TabIndex = 8;
            dgvReports.CellContentClick += dgvReports_CellContentClick;
            // 
            // btnGenerate
            // 
            btnGenerate.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerate.Location = new Point(639, 6);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(94, 29);
            btnGenerate.TabIndex = 9;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // btnExport
            // 
            btnExport.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.Location = new Point(739, 6);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(133, 29);
            btnExport.TabIndex = 10;
            btnExport.Text = "ExportCSV";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += s;
            // 
            // pnlChart1
            // 
            pnlChart1.Location = new Point(437, 173);
            pnlChart1.Name = "pnlChart1";
            pnlChart1.Size = new Size(418, 125);
            pnlChart1.TabIndex = 11;
            // 
            // pnlChart2
            // 
            pnlChart2.Location = new Point(437, 313);
            pnlChart2.Name = "pnlChart2";
            pnlChart2.Size = new Size(425, 125);
            pnlChart2.TabIndex = 12;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(lblNumRevenue);
            panel3.Controls.Add(lblRevenue);
            panel3.Location = new Point(12, 38);
            panel3.Name = "panel3";
            panel3.Size = new Size(236, 101);
            panel3.TabIndex = 13;
            // 
            // lblNumRevenue
            // 
            lblNumRevenue.AutoSize = true;
            lblNumRevenue.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNumRevenue.Location = new Point(15, 47);
            lblNumRevenue.Name = "lblNumRevenue";
            lblNumRevenue.Size = new Size(26, 25);
            lblNumRevenue.TabIndex = 6;
            lblNumRevenue.Text = "--";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(lblNumCars);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(254, 38);
            panel1.Name = "panel1";
            panel1.Size = new Size(205, 101);
            panel1.TabIndex = 15;
            // 
            // lblNumCars
            // 
            lblNumCars.AutoSize = true;
            lblNumCars.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNumCars.Location = new Point(15, 47);
            lblNumCars.Name = "lblNumCars";
            lblNumCars.Size = new Size(26, 25);
            lblNumCars.TabIndex = 6;
            lblNumCars.Text = "--";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(-2, 0);
            label2.Name = "label2";
            label2.Size = new Size(154, 28);
            label2.TabIndex = 3;
            label2.Text = "Cars that exited";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(lblAverage);
            panel2.Controls.Add(lblAverageStay);
            panel2.Location = new Point(465, 38);
            panel2.Name = "panel2";
            panel2.Size = new Size(197, 101);
            panel2.TabIndex = 16;
            // 
            // lblAverageStay
            // 
            lblAverageStay.AutoSize = true;
            lblAverageStay.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAverageStay.Location = new Point(15, 47);
            lblAverageStay.Name = "lblAverageStay";
            lblAverageStay.Size = new Size(26, 25);
            lblAverageStay.TabIndex = 6;
            lblAverageStay.Text = "--";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(lblOccupancynow);
            panel4.Controls.Add(lblOccupancy);
            panel4.Location = new Point(668, 40);
            panel4.Name = "panel4";
            panel4.Size = new Size(218, 101);
            panel4.TabIndex = 17;
            // 
            // lblOccupancynow
            // 
            lblOccupancynow.AutoSize = true;
            lblOccupancynow.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOccupancynow.Location = new Point(15, 47);
            lblOccupancynow.Name = "lblOccupancynow";
            lblOccupancynow.Size = new Size(26, 25);
            lblOccupancynow.TabIndex = 6;
            lblOccupancynow.Text = "--";
            // 
            // ReportForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(895, 450);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(pnlChart2);
            Controls.Add(pnlChart1);
            Controls.Add(btnExport);
            Controls.Add(btnGenerate);
            Controls.Add(dgvReports);
            Controls.Add(dtpTo);
            Controls.Add(dtpFrom);
            Controls.Add(lblTo);
            Controls.Add(lblFrom);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ReportForm";
            Text = "Reports";
            Load += Form7_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReports).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFrom;
        private Label lblTo;
        private Label lblRevenue;
        private Label lblAverage;
        private Label lblOccupancy;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private DataGridView dgvReports;
        private Button btnGenerate;
        private Button btnExport;
        private Panel pnlChart1;
        private Panel pnlChart2;
        private Panel panel3;
        private Label lblNumRevenue;
        private Panel panel1;
        private Label lblNumCars;
        private Label label2;
        private Panel panel2;
        private Label lblAverageStay;
        private Panel panel4;
        private Label lblOccupancynow;
    }
}