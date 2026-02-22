using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms; 

namespace CinemaCorleone
{
    public partial class SuperAdminMain : Form
    {
        private readonly Color activeBackColor = Color.FromArgb(64, 0, 0);
        private readonly Color activeForeColor = Color.White;             
        private readonly Color inactiveBackColor = Color.Transparent;     
        private readonly Color inactiveForeColor = Color.FromArgb(64, 0, 0); 

        private UserControl _currentActiveUserControl;

        public SuperAdminMain()
        {
            InitializeComponent();
        }

        private void SuperAdminMain_Load(object sender, EventArgs e)
        {
            LoadUserControlIntoPanel(new DashboardSuperAdmin(), "Dashboard");
            SetActiveButton(btnDashboard);
        }

        private void LoadUserControlIntoPanel(UserControl userControlToLoad, string formTitle = "Cinema Corleone")
        {
            // Clear existing controls from the panel, except for the min/max/close buttons
            List<Control> controlsToRemove = new List<Control>();
            foreach (Control control in panelKonten.Controls)
            {
                // Assuming cbClose, cbMaximaze, cbMinimize are indeed not user controls to be removed
                if (control.Name != "cbClose" &&
                    control.Name != "cbMaximaze" &&
                    control.Name != "cbMinimize")
                {
                    controlsToRemove.Add(control);
                }
            }

            foreach (Control control in controlsToRemove)
            {
                panelKonten.Controls.Remove(control);
                control.Dispose(); // Dispose of the control to free up resources
            }

            userControlToLoad.Dock = DockStyle.Fill;
            panelKonten.Controls.Add(userControlToLoad);
            userControlToLoad.BringToFront(); // Ensure the new control is visible

            _currentActiveUserControl = userControlToLoad;

            // Ensure these buttons are always on top
            cbClose.BringToFront();
            cbMaximaze.BringToFront();
            cbMinimize.BringToFront();

            this.Text = "Cinema Corleone - " + formTitle;
        }

        private void SetActiveButton(Guna2Button activeButton)
        {
            foreach (Control control in panelSidebar.Controls)
            {
                if (control is Guna2Button button)
                {
                    button.FillColor = inactiveBackColor;
                    button.ForeColor = inactiveForeColor;
                }
            }

            if (activeButton != null)
            {
                activeButton.FillColor = activeBackColor;
                activeButton.ForeColor = activeForeColor;
            }
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            if (_currentActiveUserControl is DashboardSuperAdmin)
            {
                return;
            }

            LoadUserControlIntoPanel(new DashboardSuperAdmin(), "Dashboard");
            SetActiveButton(btnDashboard);
        }

        private void btnFilm_Click_1(object sender, EventArgs e)
        {
            if (_currentActiveUserControl is FilmSuperAdmin)
            {
                return;
            }

            LoadUserControlIntoPanel(new FilmSuperAdmin(), "Manajemen Film");
            SetActiveButton(btnFilm);
        }

        private void btnKota_Click(object sender, EventArgs e)
        {
            if (_currentActiveUserControl is KotaSuperAdmin)
            {
                return;
            }

            LoadUserControlIntoPanel(new KotaSuperAdmin(), "Manajemen Kota");
            SetActiveButton(btnKota);
        }
    }
}
