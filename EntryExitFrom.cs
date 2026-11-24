using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Parking_System
{
    public partial class EntryExitFrom : Form
    {
        private const decimal RatePerHour = 50;
        public EntryExitFrom()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            btnEntry.Click += (s, e) => RegisterEntry();
            btnExit.Click += (s, e) => ProcessExit();

            Load += (s, e) => RefreshData();
            Activated += (s, e) => RefreshData();

            ApplyDesign();
        }

        private void ApplyDesign()
        {
            this.Text = "Vehicle Entry and Exit";
            this.BackColor = Color.FromArgb(241, 244, 249);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1050, 600);
            this.Font = new Font("Segoe UI", 10);

            grpEntry.BackColor = Color.White;
            grpEntry.ForeColor = Color.FromArgb(36, 52, 71);
            grpEntry.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            grpEntry.SetBounds(30, 25, 990, 145);

            grpExit.BackColor = Color.White;
            grpExit.ForeColor = Color.FromArgb(36, 52, 71);
            grpExit.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            grpExit.SetBounds(30, 190, 990, 100);

            lblVehicle.SetBounds(30, 45, 100, 30);
            comboBox1.SetBounds(140, 42, 230, 32);

            lblSlot.SetBounds(410, 45, 100, 30);
            comboBox2.SetBounds(520, 42, 200, 32);

            btnEntry.SetBounds(760, 40, 180, 42);

            btnExit.SetBounds(30, 35, 300, 42);

            lblRate.SetBounds(380, 40, 250, 35);
            lblRate.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblRate.ForeColor = Color.FromArgb(36, 52, 71);

            StyleButton(btnEntry, Color.FromArgb(36, 52, 71));
            StyleButton(btnExit, Color.FromArgb(233, 83, 34));

            comboBox1.Font = new Font("Segoe UI", 10);
            comboBox2.Font = new Font("Segoe UI", 10);

            dgv.SetBounds(30, 320, 990, 240);

            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;

            dgv.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(36, 52, 71);

            dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.ColumnHeadersHeight = 40;

            dgv.DefaultCellStyle.Font =
                new Font("Segoe UI", 9);

            dgv.DefaultCellStyle.SelectionBackColor =Color.FromArgb(220, 235, 250);
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(36, 52, 71);
            dgv.RowTemplate.Height = 35;
            dgv.GridColor = Color.FromArgb(225, 230, 237);
        }

        private void StyleButton(Button button, Color color)
        {
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }
        private void RefreshData()
        {
            LoadVehicles();
            LoadFreeSlots();
            LoadActiveTickets();
        }

        private void LoadVehicles()
        {
            comboBox1.Items.Clear();

            foreach (Vehicle vehicle in DataStore.Vehicles)
            {
                comboBox1.Items.Add(vehicle.Plate);
            }

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }
        private void LoadFreeSlots()
        {
            comboBox2.Items.Clear();

            foreach (Slot slot in DataStore.Slots)
            {
                if (slot.Status == "Free")
                {
                    comboBox2.Items.Add(slot.Number);
                }
            }

            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
        }
        private void RegisterEntry()
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a vehicle.");
                return;
            }

            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("No free parking slots available.");
                return;
            }

            string plate = comboBox1.SelectedItem.ToString()!;
            string slotNumber = comboBox2.SelectedItem.ToString()!;

            bool alreadyInside = DataStore.Tickets.Any(t => t.Plate == plate && t.ExitTime == null);

            if (alreadyInside)
            {
                MessageBox.Show("This vehicle is already inside the parking.");
                return;
            }
            Slot? selectedSlot = DataStore.Slots.FirstOrDefault(s =>s.Number == slotNumber);

            if (selectedSlot == null ||selectedSlot.Status != "Free")
            {
                MessageBox.Show( "This slot is no longer available. Please refresh.");
                RefreshData();
                return;
            }
            int newId = DataStore.Tickets.Count == 0
                ? 1
                : DataStore.Tickets.Max(t => t.Id) + 1;

            Ticket ticket = new Ticket
            {
                Id = newId,
                Plate = plate,
                SlotNumber = slotNumber,
                EntryTime = DateTime.Now,
                ExitTime = null,
                Fee = 0,
                Paid = false
            };
            DataStore.Tickets.Add(ticket);
            selectedSlot.Status = "Occupied";

            DataStore.SaveData();

            MessageBox.Show("Vehicle entry registered successfully!","Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            RefreshData();
        }
        private void LoadActiveTickets()
        {
            dgv.Rows.Clear();

            var activeTickets = DataStore.Tickets
                .Where(t => t.ExitTime == null)
                .OrderBy(t => t.EntryTime)
                .ToList();

            foreach (Ticket ticket in activeTickets)
            {
                TimeSpan duration =DateTime.Now - ticket.EntryTime;

                decimal fee =Math.Ceiling((decimal)duration.TotalHours) * RatePerHour;

                dgv.Rows.Add(
                    ticket.Id,
                    ticket.Plate,
                    ticket.SlotNumber,
                    ticket.EntryTime.ToString("g"),
                    FormatDuration(duration),
                    fee.ToString("0.00") + " EGP"
                );
            }
        }
        private string FormatDuration(TimeSpan duration)
        {
            int totalHours = (int)duration.TotalHours;

            return totalHours + "h " +
                   duration.Minutes + "m";
        }
        private void ProcessExit()
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an active ticket first.");

                return;
            }

            int ticketId = Convert.ToInt32(
                dgv.SelectedRows[0].Cells[0].Value);

            Ticket? ticket = DataStore.Tickets.FirstOrDefault(t => t.Id == ticketId && t.ExitTime == null);

            if (ticket == null)
            {
                MessageBox.Show("Active ticket not found.");
                RefreshData();
                return;
            }

            TimeSpan duration = DateTime.Now - ticket.EntryTime;

            decimal finalFee =Math.Ceiling((decimal)duration.TotalHours)  * RatePerHour;

            DialogResult result = MessageBox.Show(
                "Vehicle: " + ticket.Plate +
                "\nDuration: " + FormatDuration(duration) +
                "\nTotal Fee: " + finalFee.ToString("0.00") +
                " EGP\n\nConfirm vehicle exit?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            ticket.ExitTime = DateTime.Now;
            ticket.Fee = finalFee;

            Slot? slot = DataStore.Slots.FirstOrDefault(s =>s.Number == ticket.SlotNumber);

            if (slot != null)
            {
                slot.Status = "Free";
            }

            DataStore.SaveData();
            MessageBox.Show(
                "Vehicle exit processed successfully!" +
                "\nTotal Fee: " + finalFee.ToString("0.00") +
                " EGP",
                "Exit Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            RefreshData();
        }
        private void lblRate_Click(object? sender, EventArgs e)
        {
        }

    }
}