using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Parking_System
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();

            dtpFrom.Value = DateTime.Today.AddDays(-30);
            dtpTo.Value = DateTime.Today;

            pnlChart1.Paint += PnlChart1_Paint;
            pnlChart2.Paint += PnlChart2_Paint;

            Activated += (s, e) => GenerateReport();
        }

        private void Form7_Load(object? sender, EventArgs e)
        {
            ApplyDesign();
            GenerateReport();
        }

        private void ApplyDesign()
        {
            this.Text = "Parking Reports";
            this.BackColor = Color.FromArgb(241, 244, 249);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10);

            Label[] labels =
            {
                lblFrom, lblTo, lblRevenue,
                lblAverage, lblOccupancy
            };

            foreach (Label label in labels)
            {
                label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                label.ForeColor = Color.FromArgb(36, 52, 71);
            }


            dtpFrom.Font = new Font("Segoe UI", 10);
            dtpTo.Font = new Font("Segoe UI", 10);


            lblNumRevenue.Font =new Font("Segoe UI", 16, FontStyle.Bold);
            lblNumCars.Font =new Font("Segoe UI", 16, FontStyle.Bold);
            lblAverageStay.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblOccupancynow.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            lblNumRevenue.ForeColor = Color.FromArgb(36, 52, 71);
            lblNumCars.ForeColor = Color.FromArgb(36, 52, 71);
            lblAverageStay.ForeColor = Color.FromArgb(36, 52, 71);
            lblOccupancynow.ForeColor = Color.FromArgb(36, 52, 71);

            StyleButton(btnGenerate, Color.FromArgb(36, 52, 71));
            StyleButton(btnExport, Color.FromArgb(52, 120, 180));

            dgvReports.BackgroundColor = Color.White;
            dgvReports.BorderStyle = BorderStyle.None;
            dgvReports.EnableHeadersVisualStyles = false;
            dgvReports.RowHeadersVisible = false;
            dgvReports.AllowUserToAddRows = false;
            dgvReports.ReadOnly = true;
            dgvReports.SelectionMode =  DataGridViewSelectionMode.FullRowSelect;

            dgvReports.AutoSizeColumnsMode =DataGridViewAutoSizeColumnsMode.Fill;
            dgvReports.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(36, 52, 71);
            dgvReports.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvReports.ColumnHeadersDefaultCellStyle.Font =new Font("Segoe UI", 9, FontStyle.Bold);

            dgvReports.ColumnHeadersHeight = 38;

            dgvReports.DefaultCellStyle.Font =new Font("Segoe UI", 9);

            dgvReports.DefaultCellStyle.SelectionBackColor =Color.FromArgb(220, 235, 250);

            dgvReports.DefaultCellStyle.SelectionForeColor = Color.FromArgb(36, 52, 71);

            dgvReports.RowTemplate.Height = 32;
            dgvReports.GridColor = Color.FromArgb(225, 230, 237);

            pnlChart1.BackColor = Color.White;
            pnlChart2.BackColor = Color.White;
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

        private void btnGenerate_Click(object? sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date.AddDays(1);

            if (fromDate >= toDate)
            {
                MessageBox.Show( "The From date must be before or equal to the To date.","Invalid Date Range",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            List<Ticket> filteredTickets = DataStore.Tickets
                .Where(t =>t.EntryTime >= fromDate && t.EntryTime < toDate).ToList();

            List<Ticket> completedTickets = filteredTickets
                .Where(t => t.ExitTime != null)
                .ToList();

            decimal totalRevenue = completedTickets.Sum(t => t.Fee);

            int exitedCars = completedTickets.Count;

            double averageHours = 0;

            if (completedTickets.Count > 0)
            {
                averageHours = completedTickets
                    .Average(t => (t.ExitTime!.Value - t.EntryTime).TotalHours);
            }

            int averageWholeHours = (int)averageHours;
            int averageMinutes = (int)((averageHours - averageWholeHours) * 60);

            int occupiedSlots = DataStore.Slots
                .Count(s => s.Status == "Occupied");

            int totalSlots = DataStore.Slots.Count;

            double occupancyPercentage = totalSlots > 0
                ? (double)occupiedSlots / totalSlots * 100
                : 0;

            lblNumRevenue.Text = totalRevenue.ToString("0.00") + " EGP";

            lblNumCars.Text = exitedCars.ToString();

            lblAverageStay.Text = averageWholeHours + "h " + averageMinutes + "m";

            lblOccupancynow.Text = occupiedSlots + " / " + totalSlots + " (" + occupancyPercentage.ToString("0.0") + "%)";

            LoadReportTable(filteredTickets);

            pnlChart1.Invalidate();
            pnlChart2.Invalidate();
        }

        private void LoadReportTable(List<Ticket> tickets)
        {
            dgvReports.Rows.Clear();
            dgvReports.Columns.Clear();

            dgvReports.Columns.Add("colTicket", "Ticket ID");
            dgvReports.Columns.Add("colPlate", "Plate");
            dgvReports.Columns.Add("colSlot", "Slot");
            dgvReports.Columns.Add("colEntry", "Entry Time");
            dgvReports.Columns.Add("colExit", "Exit Time");
            dgvReports.Columns.Add("colDuration", "Duration");
            dgvReports.Columns.Add("colFee", "Fee");
            dgvReports.Columns.Add("colStatus", "Status");

            foreach (Ticket ticket in tickets)
            {
                string exitTime = ticket.ExitTime.HasValue
                    ? ticket.ExitTime.Value.ToString("g")
                    : "Still Inside";

                TimeSpan duration = ticket.ExitTime.HasValue
                    ? ticket.ExitTime.Value - ticket.EntryTime
                    : DateTime.Now - ticket.EntryTime;

                string durationText =
                    ((int)duration.TotalHours) + "h " +
                    duration.Minutes + "m";

                string status = ticket.ExitTime == null
                    ? "Active"
                    : "Completed";

                dgvReports.Rows.Add(
                    ticket.Id,
                    ticket.Plate,
                    ticket.SlotNumber,
                    ticket.EntryTime.ToString("g"),
                    exitTime,
                    durationText,
                    ticket.Fee.ToString("0.00") + " EGP",
                    status
                );
            }
        }

        private void PnlChart1_Paint(object? sender, PaintEventArgs e)
        {
            if (pnlChart1.Width <= 0 || pnlChart1.Height <= 0)
                return;

            e.Graphics.Clear(Color.White);

            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date.AddDays(1);

            var dailyRevenue = DataStore.Tickets
                .Where(t =>
                    t.ExitTime != null &&
                    t.EntryTime >= fromDate &&
                    t.EntryTime < toDate)
                .GroupBy(t => t.EntryTime.Date)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Date = g.Key,
                    Revenue = g.Sum(t => t.Fee)
                })
                .ToList();

            DrawChart(
                e.Graphics,
                pnlChart1.ClientRectangle,
                dailyRevenue.Select(x => x.Date.ToString("dd/MM")).ToList(),
                dailyRevenue.Select(x => (double)x.Revenue).ToList(),
                "Daily Revenue (EGP)",
                Color.SteelBlue
            );
        }

        private void PnlChart2_Paint(object? sender, PaintEventArgs e)
        {
            if (pnlChart2.Width <= 0 || pnlChart2.Height <= 0)
                return;

            e.Graphics.Clear(Color.White);

            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date.AddDays(1);

            var dailyExits = DataStore.Tickets
                .Where(t =>
                    t.ExitTime != null &&
                    t.ExitTime.Value >= fromDate &&
                    t.ExitTime.Value < toDate)
                .GroupBy(t => t.ExitTime!.Value.Date)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .ToList();

            DrawChart(
                e.Graphics,
                pnlChart2.ClientRectangle,
                dailyExits.Select(x => x.Date.ToString("dd/MM")).ToList(),
                dailyExits.Select(x => (double)x.Count).ToList(),
                "Daily Exits",
                Color.SeaGreen
            );
        }

        private void DrawChart(
            Graphics graphics,
            Rectangle area,
            List<string> labels,
            List<double> values,
            string title,
            Color barColor)
        {
            using Font titleFont =
                new Font("Segoe UI", 10, FontStyle.Bold);

            using Font labelFont =
                new Font("Segoe UI", 8);

            using Brush textBrush =
                new SolidBrush(Color.FromArgb(36, 52, 71));

            using Brush barBrush =
                new SolidBrush(barColor);

            using Pen axisPen =
                new Pen(Color.LightGray, 1);

            graphics.DrawString(
                title,
                titleFont,
                textBrush,
                10,
                8);

            if (values.Count == 0 || area.Width < 80 || area.Height < 80)
            {
                graphics.DrawString(
                    "No data available",
                    labelFont,
                    Brushes.Gray,
                    15,
                    45);

                return;
            }

            int left = 40;
            int top = 40;
            int bottom = area.Height - 35;
            int right = area.Width - 15;

            int chartHeight = bottom - top;
            int chartWidth = right - left;

            if (chartHeight <= 0 || chartWidth <= 0)
                return;

            double maxValue = values.Max();

            if (maxValue <= 0)
                maxValue = 1;

            graphics.DrawLine(
                axisPen,
                left,
                top,
                left,
                bottom);

            graphics.DrawLine(
                axisPen,
                left,
                bottom,
                right,
                bottom);

            int count = values.Count;
            int spacing = chartWidth / count;

            int barWidth = Math.Max(
                5,
                Math.Min(35, spacing / 2));

            for (int i = 0; i < count; i++)
            {
                int barHeight = (int)(
                    values[i] / maxValue * (chartHeight - 10));

                int x = left + i * spacing +
                    (spacing - barWidth) / 2;

                int y = bottom - barHeight;

                graphics.FillRectangle(
                    barBrush,
                    x,
                    y,
                    barWidth,
                    barHeight);

                string valueText = values[i].ToString("0");

                SizeF valueSize = graphics.MeasureString(
                    valueText,
                    labelFont);

                graphics.DrawString(
                    valueText,
                    labelFont,
                    textBrush,
                    x + (barWidth - valueSize.Width) / 2,
                    Math.Max(top, y - 18));

                if (count <= 12 || i % 2 == 0)
                {
                    graphics.DrawString(
                        labels[i],
                        labelFont,
                        textBrush,
                        x - 8,
                        bottom + 5);
                }
            }
        }


        private void s(object? sender, EventArgs e)
        {
            if (dgvReports.Rows.Count == 0)
            {
                MessageBox.Show(
                    "There is no report data to export.",
                    "Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            using SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "CSV File (*.csv)|*.csv";
            saveDialog.FileName = "Parking_Report.csv";
            saveDialog.Title = "Export Parking Report";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                using StreamWriter writer = new StreamWriter(
                    saveDialog.FileName,
                    false,
                    new UTF8Encoding(true));


                List<string> headers = new List<string>();

                foreach (DataGridViewColumn column in dgvReports.Columns)
                {
                    headers.Add(EscapeCsv(column.HeaderText));
                }

                writer.WriteLine(string.Join(",", headers));

                foreach (DataGridViewRow row in dgvReports.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    List<string> values = new List<string>();

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        values.Add(
                            EscapeCsv(cell.Value?.ToString() ?? ""));
                    }

                    writer.WriteLine(string.Join(",", values));
                }

                MessageBox.Show(
                    "Report exported successfully!",
                    "Export Completed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not export the report.\n" + ex.Message,
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string EscapeCsv(string value)
        {
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }


        private void dgvReports_CellContentClick(
            object? sender, DataGridViewCellEventArgs e)
        {
        }

        private void lblOccupancy_Click(object? sender, EventArgs e)
        {
        }
    }
}