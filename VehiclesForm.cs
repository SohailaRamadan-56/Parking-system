using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Parking_System
{
    public partial class VehiclesForm : Form
    {
        private string selectedPlate = "";

        public VehiclesForm()
        {
            InitializeComponent();

            cmdFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmdFilter.Items.Clear();
            cmdFilter.Items.AddRange(new object[]
            {
                "All", "Car", "Motorbike", "Truck"
            });
            cmdFilter.SelectedIndex = 0;

            cmdType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmdType.Items.Clear();
            cmdType.Items.AddRange(new object[]
            {
                "Car", "Motorbike", "Truck"
            });
            cmdType.SelectedIndex = 0;

            Load += (s, e) => LoadGrid();
            txtSearch.TextChanged += (s, e) => LoadGrid();
            cmdFilter.SelectedIndexChanged += (s, e) => LoadGrid();

            dgv.CellClick += Dgv_CellClick;

            btnAdd.Click += (s, e) => AddVehicle();
            btnUpdate.Click += (s, e) => UpdateVehicle();
            btnDelete.Click += (s, e) => DeleteVehicle();
            btnClear.Click += (s, e) => ClearFields();

            ApplyDesign();
        }

        private void ApplyDesign()
        {

            this.Text = "Vehicles Management";
            this.BackColor = Color.FromArgb(241, 244, 249);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(1050, 570);
            this.Font = new Font("Segoe UI", 10);


            Label[] labels =
            {
                lblSearch, lblPlate, lblOwner, lblPhone, lblType
            };

            foreach (Label label in labels)
            {
                label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                label.ForeColor = Color.FromArgb(52, 64, 84);
            }

            lblSearch.Location = new Point(30, 25);
            txtSearch.SetBounds(100, 21, 210, 32);

            cmdFilter.SetBounds(340, 21, 150, 32);

            dgv.SetBounds(30, 80, 600, 440);
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;

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
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;

            lblPlate.Location = new Point(675, 110);
            txtPlate.SetBounds(770, 105, 220, 32);

            lblOwner.Location = new Point(675, 170);
            txtOwner.SetBounds(770, 165, 220, 32);

            lblPhone.Location = new Point(675, 230);
            txtPhone.SetBounds(770, 225, 220, 32);

            lblType.Location = new Point(675, 290);
            cmdType.SetBounds(770, 285, 220, 32);


            TextBox[] textBoxes =
            {
                txtSearch, txtPlate, txtOwner, txtPhone
            };

            foreach (TextBox box in textBoxes)
            {
                box.Font = new Font("Segoe UI", 10);
                box.BorderStyle = BorderStyle.FixedSingle;
            }

            StyleButton(btnAdd, Color.FromArgb(36, 52, 71));
            StyleButton(btnUpdate, Color.FromArgb(52, 120, 180));
            StyleButton(btnDelete, Color.FromArgb(233, 83, 34));
            StyleButton(btnClear, Color.FromArgb(110, 120, 135));

            btnAdd.SetBounds(675, 360, 145, 42);
            btnUpdate.SetBounds(845, 360, 145, 42);
            btnDelete.SetBounds(675, 425, 145, 42);
            btnClear.SetBounds(845, 425, 145, 42);

            cmdFilter.Font = new Font("Segoe UI", 10);
            cmdType.Font = new Font("Segoe UI", 10);
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

        private void LoadGrid()
        {
            dgv.Rows.Clear();

            string search = txtSearch.Text.Trim().ToLower();
            string type = cmdFilter.Text;

            foreach (Vehicle v in DataStore.Vehicles)
            {
                if (search != "" &&
                    !(v.Plate.ToLower().Contains(search) ||
                      v.Owner.ToLower().Contains(search) ||
                      v.Phone.Contains(search)))
                {
                    continue;
                }

                if (type != "" && type != "All" && v.Type != type)
                {
                    continue;
                }

                dgv.Rows.Add(v.Plate, v.Owner, v.Phone, v.Type);
            }
        }

        private void Dgv_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgv.Rows[e.RowIndex];

            selectedPlate = row.Cells[0].Value?.ToString() ?? "";

            txtPlate.Text = row.Cells[0].Value?.ToString();
            txtOwner.Text = row.Cells[1].Value?.ToString();
            txtPhone.Text = row.Cells[2].Value?.ToString();
            cmdType.Text = row.Cells[3].Value?.ToString();
        }

        private string ValidateVehicle()
        {
            string plate = txtPlate.Text.Trim();

            if (plate.Length < 3 || plate.Length > 10)
                return "Plate must be 3 to 10 characters.";

            if (txtOwner.Text.Trim().Length < 3)
                return "Enter the owner name (3 letters or more).";

            if (!Regex.IsMatch(txtPhone.Text.Trim(), @"^01[0125][0-9]{8}$"))
                return "Phone must be 11 digits, like 01012345678.";

            if (string.IsNullOrWhiteSpace(cmdType.Text))
                return "Please select vehicle type.";

            return "";
        }

        private void AddVehicle()
        {
            string error = ValidateVehicle();

            if (error != "")
            {
                MessageBox.Show(error, "Check the data");
                return;
            }

            string plate = txtPlate.Text.Trim().ToUpper();

            if (DataStore.Vehicles.Any(v => v.Plate == plate))
            {
                MessageBox.Show(
                    "A vehicle with this plate already exists.",
                    "Duplicate");

                return;
            }

            DataStore.Vehicles.Add(new Vehicle
            {
                Plate = plate,
                Owner = txtOwner.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Type = cmdType.Text
            });

            DataStore.SaveData();

            MessageBox.Show("Vehicle saved successfully!");

            ClearFields();
            LoadGrid();
        }

        private void UpdateVehicle()
        {
            if (selectedPlate == "")
            {
                MessageBox.Show("Select a vehicle from the list first.");
                return;
            }

            string error = ValidateVehicle();

            if (error != "")
            {
                MessageBox.Show(error, "Check the data");
                return;
            }

            Vehicle? v = DataStore.Vehicles
                .FirstOrDefault(x => x.Plate == selectedPlate);

            if (v == null)
                return;

            string newPlate = txtPlate.Text.Trim().ToUpper();

            if (newPlate != selectedPlate &&
                DataStore.Vehicles.Any(x => x.Plate == newPlate))
            {
                MessageBox.Show(
                    "Another vehicle already has this plate.",
                    "Duplicate");

                return;
            }

            if (newPlate != selectedPlate &&
                DataStore.Tickets.Any(t => t.Plate == selectedPlate))
            {
                MessageBox.Show(
                    "Cannot change the plate because this vehicle has parking history.");

                return;
            }

            v.Plate = newPlate;
            v.Owner = txtOwner.Text.Trim();
            v.Phone = txtPhone.Text.Trim();
            v.Type = cmdType.Text;

            DataStore.SaveData();

            MessageBox.Show("Vehicle updated successfully!");

            ClearFields();
            LoadGrid();
        }

        private void DeleteVehicle()
        {
            if (selectedPlate == "")
            {
                MessageBox.Show("Select a vehicle from the list first.");
                return;
            }

            if (DataStore.Tickets.Any(t => t.Plate == selectedPlate))
            {
                MessageBox.Show(
                    "This vehicle has parking history, so it can't be deleted.");

                return;
            }

            DialogResult result = MessageBox.Show(
                "Delete vehicle " + selectedPlate + "?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            DataStore.Vehicles.RemoveAll(v => v.Plate == selectedPlate);

            DataStore.SaveData();

            MessageBox.Show("Vehicle deleted successfully!");

            ClearFields();
            LoadGrid();
        }

        private void ClearFields()
        {
            selectedPlate = "";

            txtPlate.Clear();
            txtOwner.Clear();
            txtPhone.Clear();

            cmdType.SelectedIndex = 0;

            dgv.ClearSelection();
        }
    }
}