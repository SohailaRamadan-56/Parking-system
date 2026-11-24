using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Parking_System
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            menuEntry.Click += (s, e) => new EntryExitFrom().Show();
            menuTickets.Click += (s, e) => new TicketsForm().Show();
            menuReports.Click += (s, e) => new ReportForm().Show();
            menuSlots.Click += (s, e) => new SlotsForm().Show();

            Activated += (s, e) => LoadDashboard();

            ApplyDesign();
        }

        private void ApplyDesign()
        {

            this.Text = "Parking Management System";
            this.BackColor = Color.FromArgb(241, 244, 249);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1100, 500);
            this.Font = new Font("Segoe UI", 10);


            label1.Text = "Parking Dashboard";
            label1.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(36, 52, 71);
            label1.Location = new Point(30, 55);
            label1.AutoSize = true;


            toolStrip1.BackColor = Color.FromArgb(36, 52, 71);
            toolStrip1.ForeColor = Color.White;
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Padding = new Padding(10, 5, 10, 5);
            toolStrip1.Dock = DockStyle.Top;

            ToolStripLabel[] menuItems =
            {
                meunVehicles,
                menuEntry,
                menuTickets,
                menuReports,
                menuSlots,
                menuExit
            };

            foreach (ToolStripLabel item in menuItems)
            {
                item.ForeColor = Color.White;
                item.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                item.Padding = new Padding(12, 5, 12, 5);
            }

            panel4.SetBounds(30, 140, 240, 140);
            panel2.SetBounds(300, 140, 240, 140);
            panel3.SetBounds(570, 140, 240, 140);
            panel5.SetBounds(840, 140, 230, 140);

            panel4.BackColor = Color.FromArgb(220, 235, 250);
            panel2.BackColor = Color.FromArgb(252, 225, 222);
            panel3.BackColor = Color.FromArgb(221, 243, 228);
            panel5.BackColor = Color.FromArgb(255, 240, 204);

            panel4.BorderStyle = BorderStyle.None;
            panel2.BorderStyle = BorderStyle.None;
            panel3.BorderStyle = BorderStyle.None;
            panel5.BorderStyle = BorderStyle.None;


            Label[] titles =
            {
                lbltotalslots,
                lblOccupied,
                lblRevenue,
                lblfree
            };

            foreach (Label title in titles)
            {
                title.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                title.ForeColor = Color.FromArgb(52, 64, 84);
            }

            Label[] numbers =
            {
                lblNumSlot,
                lblNumOccupied,
                lblNumRevenue,
                lblNumFree
            };

            foreach (Label number in numbers)
            {
                number.Font = new Font("Segoe UI", 22, FontStyle.Bold);
                number.ForeColor = Color.FromArgb(36, 52, 71);
            }

            lbltotalslots.Location = new Point(20, 18);
            lblNumSlot.Location = new Point(20, 65);

            lblOccupied.Location = new Point(20, 18);
            lblNumOccupied.Location = new Point(20, 65);

            lblRevenue.Location = new Point(20, 18);
            lblNumRevenue.Location = new Point(20, 65);

            lblfree.Location = new Point(20, 18);
            lblNumFree.Location = new Point(20, 65);
        }

        private void LoadDashboard()
        {
   
            int totalSlots = DataStore.Slots.Count;

            int occupiedSlots = DataStore.Slots.Count( s => s.Status == "Occupied");

            int freeSlots = DataStore.Slots.Count( s => s.Status == "Free");

            decimal revenue = DataStore.Tickets
                .Where(t => t.ExitTime != null)
                .Sum(t => t.Fee);

            lblNumSlot.Text = totalSlots.ToString();
            lblNumOccupied.Text = occupiedSlots.ToString();
            lblNumFree.Text = freeSlots.ToString();
            lblNumRevenue.Text = revenue.ToString("N2") + " EGP";
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        private void meunVehicles_Click(object sender, EventArgs e)
        {
            new VehiclesForm().Show();
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}