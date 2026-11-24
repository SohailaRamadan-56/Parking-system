using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Parking_System
{
    public partial class SlotsForm : Form
    {
        public SlotsForm()
        {
            InitializeComponent();

            cmbZoneFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbZoneFilter.Items.Clear();
            cmbZoneFilter.Items.AddRange(new object[]
            {
                "All", "A", "B"
            });
            cmbZoneFilter.SelectedIndex = 0;


            cmdStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmdStatus.Items.Clear();
            cmdStatus.Items.AddRange(new object[]
            {
                "All", "Free", "Occupied"
            });
            cmdStatus.SelectedIndex = 0;


            txtSearch.TextChanged += (s, e) => LoadSlotsTable();
            cmbZoneFilter.SelectedIndexChanged += (s, e) => LoadSlotsTable();
            cmdStatus.SelectedIndexChanged += (s, e) => LoadSlotsTable();

            pnlParking.Paint -= PnlParking_Paint;
            pnlParking.Paint += PnlParking_Paint;

            Load += (s, e) => RefreshSlots();
            Activated += (s, e) => RefreshSlots();

            ApplyDesign();
        }

        private void ApplyDesign()
        {
            this.Text = "Parking Slots";
            this.BackColor = Color.FromArgb(241, 244, 249);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1050, 650);
            this.Font = new Font("Segoe UI", 10);

            lblSearch.ForeColor = Color.FromArgb(36, 52, 71);
            lblZoneFilter.ForeColor = Color.FromArgb(36, 52, 71);
            lblOwner.ForeColor = Color.FromArgb(36, 52, 71);

            lblSearch.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblZoneFilter.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblOwner.Font = new Font("Segoe UI", 10, FontStyle.Bold);


            lblSearch.SetBounds(30, 25, 70, 30);
            txtSearch.SetBounds(100, 22, 190, 32);

            lblZoneFilter.SetBounds(320, 25, 90, 30);
            cmbZoneFilter.SetBounds(410, 22, 130, 32);


            lblOwner.Text = "Status";
            lblOwner.SetBounds(570, 25, 70, 30);
            cmdStatus.SetBounds(640, 22, 150, 32);

            txtSearch.Font = new Font("Segoe UI", 10);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            cmbZoneFilter.Font = new Font("Segoe UI", 10);
            cmdStatus.Font = new Font("Segoe UI", 10);


            dgv.SetBounds(30, 80, 420, 500);
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(36, 52, 71);

            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgv.ColumnHeadersHeight = 40;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgv.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(220, 235, 250);

            dgv.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(36, 52, 71);

            dgv.RowTemplate.Height = 35;
            dgv.GridColor = Color.FromArgb(225, 230, 237);


            pnlParking.SetBounds(480, 80, 540, 500);
            pnlParking.BackColor = Color.White;
            pnlParking.BorderStyle = BorderStyle.None;
        }

        private void RefreshSlots()
        {
            LoadSlotsTable();
            pnlParking.Invalidate();
        }


        private void LoadSlotsTable()
        {
            dgv.Rows.Clear();

            string search = txtSearch.Text.Trim().ToLower();
            string selectedZone = cmbZoneFilter.Text;
            string selectedStatus = cmdStatus.Text;

            foreach (Slot slot in DataStore.Slots)
            {
                if (search != "" &&
                    !slot.Number.ToLower().Contains(search))
                {
                    continue;
                }

                if (selectedZone != "All" &&
                    selectedZone != "" &&
                    slot.Zone != selectedZone)
                {
                    continue;
                }

                if (selectedStatus != "All" &&
                    selectedStatus != "" &&
                    slot.Status != selectedStatus)
                {
                    continue;
                }

                dgv.Rows.Add(
                    slot.Number,
                    slot.Zone,
                    slot.Status
                );
            }

            pnlParking.Invalidate();
        }


        private void PnlParking_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            using Font titleFont =
                new Font("Segoe UI", 13, FontStyle.Bold);

            using Font slotFont =
                new Font("Segoe UI", 8, FontStyle.Bold);

            using Brush titleBrush =
                new SolidBrush(Color.FromArgb(36, 52, 71));

            using Brush freeBrush =
                new SolidBrush(Color.FromArgb(220, 243, 228));

            using Brush occupiedBrush =
                new SolidBrush(Color.FromArgb(252, 225, 222));

            using Pen freePen =
                new Pen(Color.FromArgb(55, 145, 90), 2);

            using Pen occupiedPen =
                new Pen(Color.FromArgb(220, 90, 75), 2);

            using Brush freeText =
                new SolidBrush(Color.FromArgb(35, 110, 65));

            using Brush occupiedText =
                new SolidBrush(Color.FromArgb(170, 55, 45));

            g.DrawString("Parking Layout", titleFont, titleBrush, 20, 15);

            g.DrawString("Zone A", titleFont, titleBrush, 20, 55);
            g.DrawString("Zone B", titleFont, titleBrush, 20, 270);

            DrawZone(
                g,
                "A",
                20,
                90,
                freeBrush,
                occupiedBrush,
                freePen,
                occupiedPen,
                slotFont,
                freeText,
                occupiedText);

            DrawZone(
                g,
                "B",
                20,
                305,
                freeBrush,
                occupiedBrush,
                freePen,
                occupiedPen,
                slotFont,
                freeText,
                occupiedText);


            int legendY = pnlParking.Height - 45;

            g.FillRectangle(freeBrush, 25, legendY, 20, 18);
            g.DrawRectangle(freePen, 25, legendY, 20, 18);

            g.DrawString(
                "Free",
                slotFont,
                freeText,
                52,
                legendY + 2);

            g.FillRectangle(occupiedBrush, 120, legendY, 20, 18);
            g.DrawRectangle(occupiedPen, 120, legendY, 20, 18);

            g.DrawString(
                "Occupied",
                slotFont,
                occupiedText,
                147,
                legendY + 2);
        }


        private void DrawZone(
            Graphics g,
            string zone,
            int startX,
            int startY,
            Brush freeBrush,
            Brush occupiedBrush,
            Pen freePen,
            Pen occupiedPen,
            Font slotFont,
            Brush freeText,
            Brush occupiedText)
        {
            int slotWidth = 75;
            int slotHeight = 65;
            int gap = 12;

            var zoneSlots = DataStore.Slots
                .Where(s => s.Zone == zone)
                .OrderBy(s => s.Number)
                .ToList();

            for (int i = 0; i < zoneSlots.Count; i++)
            {
                Slot slot = zoneSlots[i];

                int row = i / 4;
                int column = i % 4;

                int x = startX + column * (slotWidth + gap);
                int y = startY + row * (slotHeight + 12);

                bool occupied = slot.Status == "Occupied";

                Brush fill = occupied ? occupiedBrush : freeBrush;
                Pen border = occupied ? occupiedPen : freePen;
                Brush text = occupied ? occupiedText : freeText;

                Rectangle rect = new Rectangle(
                    x, y, slotWidth, slotHeight);

                g.FillRectangle(fill, rect);
                g.DrawRectangle(border, rect);

                if (occupied)
                {
                    DrawCar(g, x + 23, y + 7);
                }

                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawString(
                    slot.Number,
                    slotFont,
                    text,
                    new RectangleF(x, y + 35, slotWidth, 25),
                    format);
            }
        }

        private void DrawCar(Graphics g, int x, int y)
        {
            using Brush carBrush =
                new SolidBrush(Color.FromArgb(36, 52, 71));

            using Brush windowBrush =
                new SolidBrush(Color.LightBlue);

            using Brush wheelBrush =
                new SolidBrush(Color.FromArgb(45, 45, 45));


            g.FillRectangle(carBrush, x, y + 7, 30, 15);

  
            Point[] roof =
            {
                new Point(x + 6, y + 7),
                new Point(x + 11, y),
                new Point(x + 23, y),
                new Point(x + 27, y + 7)
            };

            g.FillPolygon(carBrush, roof);

            g.FillRectangle(windowBrush, x + 11, y + 2, 11, 5);

            g.FillEllipse(wheelBrush, x + 3, y + 18, 7, 7);
            g.FillEllipse(wheelBrush, x + 21, y + 18, 7, 7);
        }
        private void dgv_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
        }
    }
}