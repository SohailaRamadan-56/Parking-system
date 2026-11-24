using System;
using System.Drawing;
using System.Windows.Forms;

namespace Parking_System
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            ApplyDesign();
        }


        private void ApplyDesign()
        {

            BackColor = Color.FromArgb(241, 244, 249);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Size = new Size(800, 500);
            Text = "Parking System - Login";


            lblParking.Text = "PARKING SYSTEM";
            lblParking.Font = new Font("Segoe UI", 17, FontStyle.Bold);
            lblParking.ForeColor = Color.FromArgb(36, 52, 71);
            lblParking.AutoSize = true;
            lblParking.Location = new Point(300, 105);


            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Location = new Point(300, 10);
            pictureBox1.Size = new Size(200, 90);
            pictureBox1.BackColor = Color.Transparent;

            StyleLabel(lblUserName);
            StyleLabel(lblPassword);

            lblUserName.Location = new Point(220, 185);
            lblPassword.Location = new Point(220, 245);

            StyleTextBox(txtUserName);
            StyleTextBox(txtPassword);

            txtUserName.Location = new Point(340, 182);
            txtPassword.Location = new Point(340, 242);

            txtUserName.Size = new Size(230, 32);
            txtPassword.Size = new Size(230, 32);

            txtUserName.PlaceholderText = "Enter your username";
            txtPassword.PlaceholderText = "Enter your password";

            chkShowPassword.ForeColor = Color.FromArgb(52, 64, 84);
            chkShowPassword.Font = new Font("Segoe UI", 9);
            chkShowPassword.Location = new Point(340, 280);
            chkShowPassword.Cursor = Cursors.Hand;

            btnlogin.Text = "LOGIN";
            btnlogin.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnlogin.BackColor = Color.FromArgb(36, 52, 71);
            btnlogin.ForeColor = Color.White;
            btnlogin.FlatStyle = FlatStyle.Flat;
            btnlogin.FlatAppearance.BorderSize = 0;
            btnlogin.Cursor = Cursors.Hand;
            btnlogin.Size = new Size(350, 48);
            btnlogin.Location = new Point(220, 335);

            txtUserName.TabIndex = 0;
            txtPassword.TabIndex = 1;
            chkShowPassword.TabIndex = 2;
            btnlogin.TabIndex = 3;

            AcceptButton = btnlogin;
        }

        private void StyleLabel(Label lbl)
        {
            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(52, 64, 84);
            lbl.AutoSize = true;
        }

        private void StyleTextBox(TextBox txt)
        {
            txt.Font = new Font("Segoe UI", 10);
            txt.BackColor = Color.White;
            txt.ForeColor = Color.FromArgb(36, 52, 71);
            txt.BorderStyle = BorderStyle.FixedSingle;
        }
        private void chkShowPassword_CheckedChanged(object? sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void textBox1_TextChanged(object? sender, EventArgs e)
        {
        }
        private void btnlogin_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter your username and password.","Login Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            mainForm main = new mainForm();
            main.FormClosed += (s, args) => this.Close();
            main.Show();
            this.Hide();
        }
    }
}