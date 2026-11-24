namespace Parking_System
{
    partial class SlotsForm
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
            dgv = new DataGridView();
            colSlot = new DataGridViewTextBoxColumn();
            colZone = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            cmbZoneFilter = new ComboBox();
            txtSearch = new TextBox();
            lblOwner = new Label();
            lblZoneFilter = new Label();
            lblSearch = new Label();
            cmdStatus = new ComboBox();
            pnlParking = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = SystemColors.ButtonFace;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Columns.AddRange(new DataGridViewColumn[] { colSlot, colZone, colStatus });
            dgv.Location = new Point(12, 83);
            dgv.Name = "dgv";
            dgv.RowHeadersWidth = 51;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(426, 395);
            dgv.TabIndex = 31;
            dgv.CellContentClick += dgv_CellContentClick;
            // 
            // colSlot
            // 
            colSlot.HeaderText = "Slot";
            colSlot.MinimumWidth = 6;
            colSlot.Name = "colSlot";
            colSlot.ReadOnly = true;
            // 
            // colZone
            // 
            colZone.HeaderText = "Zone";
            colZone.MinimumWidth = 6;
            colZone.Name = "colZone";
            colZone.ReadOnly = true;
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Status";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            // 
            // cmbZoneFilter
            // 
            cmbZoneFilter.FormattingEnabled = true;
            cmbZoneFilter.Location = new Point(310, 12);
            cmbZoneFilter.Name = "cmbZoneFilter";
            cmbZoneFilter.Size = new Size(151, 28);
            cmbZoneFilter.TabIndex = 30;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(79, 14);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(125, 27);
            txtSearch.TabIndex = 21;
            // 
            // lblOwner
            // 
            lblOwner.AutoSize = true;
            lblOwner.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOwner.Location = new Point(481, 15);
            lblOwner.Name = "lblOwner";
            lblOwner.Size = new Size(57, 23);
            lblOwner.TabIndex = 18;
            lblOwner.Text = "Status";
            // 
            // lblZoneFilter
            // 
            lblZoneFilter.AutoSize = true;
            lblZoneFilter.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblZoneFilter.Location = new Point(235, 18);
            lblZoneFilter.Name = "lblZoneFilter";
            lblZoneFilter.Size = new Size(49, 23);
            lblZoneFilter.TabIndex = 17;
            lblZoneFilter.Text = "Zone";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(23, 17);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(53, 20);
            lblSearch.TabIndex = 16;
            lblSearch.Text = "Search";
            // 
            // cmdStatus
            // 
            cmdStatus.FormattingEnabled = true;
            cmdStatus.Location = new Point(567, 13);
            cmdStatus.Name = "cmdStatus";
            cmdStatus.Size = new Size(151, 28);
            cmdStatus.TabIndex = 32;
            // 
            // pnlParking
            // 
            pnlParking.Location = new Point(605, 83);
            pnlParking.Name = "pnlParking";
            pnlParking.Size = new Size(525, 397);
            pnlParking.TabIndex = 33;
            // 
            // SlotsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1142, 504);
            Controls.Add(pnlParking);
            Controls.Add(cmdStatus);
            Controls.Add(dgv);
            Controls.Add(cmbZoneFilter);
            Controls.Add(txtSearch);
            Controls.Add(lblOwner);
            Controls.Add(lblZoneFilter);
            Controls.Add(lblSearch);
            Name = "SlotsForm";
            Text = "Slots";
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgv;
        private ComboBox cmbZoneFilter;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnAdd;
        private TextBox textBox5;
        private TextBox txtPhone;
        private TextBox txtSearch;
        private Label lblZone;
        private Label lblSlot;
        private Label lblOwner;
        private Label lblZoneFilter;
        private Label lblSearch;
        private ComboBox cmdStatus;
        private DataGridViewTextBoxColumn colSlot;
        private DataGridViewTextBoxColumn colZone;
        private DataGridViewTextBoxColumn colStatus;
        private Panel pnlParking;
    }
}