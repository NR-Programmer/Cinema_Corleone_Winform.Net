using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CinemaCorleone.PenggunaService;

namespace CinemaCorleone
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = txtUsernameEmail.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(usernameOrEmail) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Nama Pengguna/Email dan Kata Sandi tidak boleh kosong.", "Kesalahan Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Pengguna authenticatedPengguna = PenggunaService.AuthenticateUser(usernameOrEmail, password);

                if (authenticatedPengguna != null)
                {
                    MessageBox.Show($"Login berhasil! Selamat datang, {authenticatedPengguna.NamaLengkap ?? authenticatedPengguna.Username}!", "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (authenticatedPengguna.Peran == "super_admin")
                    {
                        SuperAdminMain superadmin = new SuperAdminMain();
                        superadmin.Show();
                    }
                    else
                    {
                        MessageBox.Show($"Mengarahkan ke dashboard umum untuk peran: {authenticatedPengguna.Peran}", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Nama Pengguna/Email atau Kata Sandi tidak valid.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat login: " + ex.Message, "Kesalahan Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }
    }
}