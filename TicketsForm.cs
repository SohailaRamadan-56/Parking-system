using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Parking_System
{
    public partial class TicketsForm : Form
    {
        private const decimal RatePerHour = 50;

        public TicketsForm()
        {
            InitializeComponent();

            ApplyDesign();

            cmdStatus.Items.Clear();
            cmdStatus.Items.AddRange(new object[]
            {
                "All",
                "Active",
                "Completed",
                "Paid",
                "Unpaid"
            });

            cmdStatus.SelectedIndex = 0;

            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;

            Activated += (s, e) => LoadTickets();

            LoadTickets();
        }

        private void ApplyDesign()
        {
            BackColor = Color.FromArgb(241, 244, 249);
            Font = new Font("Segoe UI", 10);

            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(36, 52, 71);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgv.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(220, 235, 250);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            btnDelete.BackColor = Color.FromArgb(233, 83, 34);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Cursor = Cursors.Hand;
        }

        private void LoadTickets()
        {
            if (dgv.Columns.Count == 0)
                return;

            dgv.Rows.Clear();

            string searchPlate = textBox1.Text.Trim().ToLower();
            string selectedStatus = cmdStatus.SelectedItem?.ToString() ?? "All";

            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date.AddDays(1);

            var tickets = DataStore.Tickets
                .Where(t =>
                    t.EntryTime >= fromDate &&
                    t.EntryTime < toDate)
                .Where(t =>
                    string.IsNullOrEmpty(searchPlate) ||
                    t.Plate.ToLower().Contains(searchPlate))
                .Where(t =>
                    selectedStatus == "All" ||
                    (selectedStatus == "Active" && t.ExitTime == null) ||
                    (selectedStatus == "Completed" && t.ExitTime != null) ||
                    (selectedStatus == "Paid" && t.Paid) ||
                    (selectedStatus == "Unpaid" && !t.Paid))
                .OrderByDescending(t => t.EntryTime)
                .ToList();

            foreach (Ticket ticket in tickets)
            {
                TimeSpan duration;

                if (ticket.ExitTime.HasValue)
                {
                    duration = ticket.ExitTime.Value - ticket.EntryTime;
                }
                else
                {
                    duration = DateTime.Now - ticket.EntryTime;
                }

                decimal fee;

                if (ticket.ExitTime.HasValue)
                {
                    fee = ticket.Fee;
                }
                else
                {
                    double hours = Math.Ceiling(duration.TotalHours);
                    fee = (decimal)Math.Max(1, hours) * RatePerHour;
                }

                string exitTime = ticket.ExitTime.HasValue
                    ? ticket.ExitTime.Value.ToString("yyyy-MM-dd hh:mm tt")
                    : "Active";

                string paidStatus = ticket.Paid ? "Paid" : "Unpaid";

                dgv.Rows.Add(
                    ticket.Id,
                    ticket.Plate,
                    ticket.SlotNumber,
                    ticket.EntryTime.ToString("yyyy-MM-dd hh:mm tt"),
                    exitTime,
                    fee.ToString("0.00"),
                    paidStatus
                );
            }
        }

        private void DeleteSelectedTicket()
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a ticket first.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            object? cellValue = dgv.CurrentRow.Cells["colTicket"].Value;

            if (cellValue == null ||
                !int.TryParse(cellValue.ToString(), out int ticketId))
            {
                MessageBox.Show("Invalid ticket selection.");
                return;
            }

            Ticket? ticket = DataStore.Tickets
                .FirstOrDefault(t => t.Id == ticketId);

            if (ticket == null)
            {
                MessageBox.Show("Ticket not found.");
                LoadTickets();
                return;
            }

 
            if (ticket.ExitTime == null)
            {
                MessageBox.Show(
                    "You cannot delete an active ticket. Process the exit first.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this ticket?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                DataStore.Tickets.Remove(ticket);

                DataStore.SaveData();

                LoadTickets();

                MessageBox.Show(
                    "Ticket deleted successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnGenerate_Click(object? sender, EventArgs e)
        {
            DeleteSelectedTicket();
        }

        private void dtpFrom_ValueChanged(object? sender, EventArgs e)
        {
            LoadTickets();
        }

        private void dtpTo_ValueChanged(object? sender, EventArgs e)
        {
            LoadTickets();
        }

        private void lblFrom_Click(object? sender, EventArgs e)
        {
        }

        private void lblTo_Click(object? sender, EventArgs e)
        {
        }
    }
}