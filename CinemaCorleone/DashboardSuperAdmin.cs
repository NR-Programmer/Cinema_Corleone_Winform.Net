using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Still needed for MySqlException if not wrapped by service completely

namespace CinemaCorleone
{
    public partial class DashboardSuperAdmin : UserControl
    {
        private DashboardService _dashboardService;

        public DashboardSuperAdmin()
        {
            InitializeComponent();
            _dashboardService = new DashboardService();
        }

        private void DashboardSuperAdmin_Load_1(object sender, EventArgs e)
        {
            RefreshDashboardSummary();
            PopulateChartPendapatan();
            PopulateDgvFilmTerpopuler();
            PopulateDgvAktifitasTerbaru();
        }

        private void RefreshDashboardSummary()
        {
            try
            {
                labelTotalFilmAktif.Text = _dashboardService.GetTotalFilmAktif().ToString();
                labelPenggunaTerdaftar.Text = _dashboardService.GetPenggunaTerdaftar().ToString();
                labelPendapatanHariIni.Text = _dashboardService.GetPendapatanHariIni().ToString("C0");
                panelPenayanganAktif.Text = _dashboardService.GetPenayanganAktif().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat memuat ringkasan dashboard: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateChartPendapatan()
        {
            List<DashboardService.PendapatanHarian> data = null;
            try
            {
                data = _dashboardService.GetTrenPendapatan30Hari();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error mengambil tren pendapatan: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            chartTrenPendapatan.Series["Pendapatan"].Points.Clear();
            chartTrenPendapatan.Series["Pendapatan"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;

            // Explicitly enable axes and their labels, disabling auto-fitting
            chartTrenPendapatan.ChartAreas[0].AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartTrenPendapatan.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
            chartTrenPendapatan.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false; // Prevent staggering
            chartTrenPendapatan.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true; // Ensure end labels visible
            chartTrenPendapatan.ChartAreas[0].AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.None; // Disable auto-fitting
            chartTrenPendapatan.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8F); // Explicitly set font
            chartTrenPendapatan.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black; // Explicitly set fore color
            chartTrenPendapatan.ChartAreas[0].AxisX.LabelStyle.Format = "dd MMM"; // Moved here
            chartTrenPendapatan.ChartAreas[0].AxisX.IntervalOffset = 0; // Explicitly set offset

            chartTrenPendapatan.ChartAreas[0].AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartTrenPendapatan.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            chartTrenPendapatan.ChartAreas[0].AxisY.LabelStyle.IsEndLabelVisible = true; // Ensure end labels visible
            chartTrenPendapatan.ChartAreas[0].AxisY.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.None; // Disable auto-fitting
            chartTrenPendapatan.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 8F); // Explicitly set font
            chartTrenPendapatan.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black; // Explicitly set fore color
            chartTrenPendapatan.ChartAreas[0].AxisY.IntervalOffset = 0; // Explicitly set offset

            // Ensure Y-axis starts from zero and has a minimum value
            chartTrenPendapatan.ChartAreas[0].AxisY.IsStartedFromZero = true;
            chartTrenPendapatan.ChartAreas[0].AxisY.Minimum = 0;

            chartTrenPendapatan.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartTrenPendapatan.ChartAreas[0].AxisX.Interval = double.NaN;


            // Set axis ranges based on data presence
            if (data == null || data.Count == 0)
            {
                chartTrenPendapatan.ChartAreas[0].AxisY.Maximum = 100; // Default max if no data
                chartTrenPendapatan.ChartAreas[0].AxisY.Interval = 20; // Explicit interval for empty chart
                // Set X-axis (Date) range explicitly for empty chart
                DateTime endDate = DateTime.Today.Date; // Use .Date to remove time component
                DateTime startDate = endDate.AddDays(-30); // Last 30 days
                chartTrenPendapatan.ChartAreas[0].AxisX.Minimum = startDate.ToOADate();
                chartTrenPendapatan.ChartAreas[0].AxisX.Maximum = endDate.ToOADate();
                chartTrenPendapatan.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
                chartTrenPendapatan.ChartAreas[0].AxisX.Interval = 5; // Show labels every 5 days for the 30-day span
            }
            else
            {
                chartTrenPendapatan.ChartAreas[0].AxisY.Maximum = double.NaN; // Let it auto-scale
                chartTrenPendapatan.ChartAreas[0].AxisY.Interval = double.NaN; // Auto interval for Y-axis
                chartTrenPendapatan.ChartAreas[0].AxisX.Minimum = double.NaN; // Auto-scale
                chartTrenPendapatan.ChartAreas[0].AxisX.Maximum = double.NaN; // Auto-scale
                chartTrenPendapatan.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto; // Auto interval type
                chartTrenPendapatan.ChartAreas[0].AxisX.Interval = double.NaN; // Auto interval
            }

            if (data != null)
            {
                foreach (var item in data)
                {
                    chartTrenPendapatan.Series["Pendapatan"].Points.AddXY(item.Tanggal, item.Total);
                }
            }


            chartTrenPendapatan.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartTrenPendapatan.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartTrenPendapatan.Series["Pendapatan"].BorderWidth = 3;
            chartTrenPendapatan.Series["Pendapatan"].Color = System.Drawing.Color.FromArgb(4, 136, 145);


            chartTrenPendapatan.Invalidate();
        }

        private void PopulateDgvFilmTerpopuler()
        {
            ApplyDataGridViewStyling(dgvFilmTerpopuler);

            dgvFilmTerpopuler.AutoGenerateColumns = false;
            dgvFilmTerpopuler.Columns.Clear();

            DataGridViewTextBoxColumn colJudul = new DataGridViewTextBoxColumn();
            colJudul.DataPropertyName = "JudulFilm";
            colJudul.HeaderText = "Judul Film";
            colJudul.Name = "JudulFilm";
            dgvFilmTerpopuler.Columns.Add(colJudul);

            DataGridViewTextBoxColumn colTiket = new DataGridViewTextBoxColumn();
            colTiket.DataPropertyName = "TiketTerjual";
            colTiket.HeaderText = "Tiket Terjual";
            colTiket.Name = "TiketTerjual";
            dgvFilmTerpopuler.Columns.Add(colTiket);

            List<DashboardService.FilmTerpopuler> data = null;
            try
            {
                data = _dashboardService.GetFilmTerpopulerData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error mengambil film terpopuler: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (data == null || data.Count == 0)
            {
                dgvFilmTerpopuler.DataSource = null;
                dgvFilmTerpopuler.Rows.Clear();
                dgvFilmTerpopuler.Rows.Add("Belum ada data");
                dgvFilmTerpopuler.Rows[0].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                dgvFilmTerpopuler.DataSource = data;
            }
        }

        private void PopulateDgvAktifitasTerbaru()
        {
            ApplyDataGridViewStyling(dgvAktifitasTerbaru);

            dgvAktifitasTerbaru.AutoGenerateColumns = false;
            dgvAktifitasTerbaru.Columns.Clear();

            DataGridViewTextBoxColumn colNama = new DataGridViewTextBoxColumn();
            colNama.DataPropertyName = "NamaPengguna";
            colNama.HeaderText = "Nama Pengguna";
            colNama.Name = "NamaPengguna";
            dgvAktifitasTerbaru.Columns.Add(colNama);

            DataGridViewTextBoxColumn colJudul = new DataGridViewTextBoxColumn();
            colJudul.DataPropertyName = "JudulFilm";
            colJudul.HeaderText = "Judul Film";
            colJudul.Name = "JudulFilm";
            dgvAktifitasTerbaru.Columns.Add(colJudul);

            DataGridViewTextBoxColumn colTanggal = new DataGridViewTextBoxColumn();
            colTanggal.DataPropertyName = "TanggalPemesanan";
            colTanggal.HeaderText = "Tanggal Pemesanan";
            colTanggal.Name = "TanggalPemesanan";
            colTanggal.DefaultCellStyle.Format = "dd MMM yyyy HH:mm";
            dgvAktifitasTerbaru.Columns.Add(colTanggal);

            DataGridViewTextBoxColumn colJumlah = new DataGridViewTextBoxColumn();
            colJumlah.DataPropertyName = "JumlahTiket";
            colJumlah.HeaderText = "Jumlah Tiket";
            colJumlah.Name = "JumlahTiket";
            dgvAktifitasTerbaru.Columns.Add(colJumlah);

            DataGridViewTextBoxColumn colTotal = new DataGridViewTextBoxColumn();
            colTotal.DataPropertyName = "TotalPembayaran";
            colTotal.HeaderText = "Total Pembayaran";
            colTotal.Name = "TotalPembayaran";
            colTotal.DefaultCellStyle.Format = "C0";
            dgvAktifitasTerbaru.Columns.Add(colTotal);

            List<DashboardService.AktifitasTerbaru> data = null;
            try
            {
                data = _dashboardService.GetAktifitasTerbaruData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error mengambil aktifitas terbaru: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (data == null || data.Count == 0)
            {
                dgvAktifitasTerbaru.DataSource = null;
                dgvAktifitasTerbaru.Rows.Clear();
                dgvAktifitasTerbaru.Rows.Add("Belum ada data");
                dgvAktifitasTerbaru.Rows[0].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                dgvAktifitasTerbaru.DataSource = data;
            }
        }

        private void dgvFilmTerpopuler_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ApplyDataGridViewStyling(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = Color.FromArgb(4, 136, 145);
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            headerStyle.SelectionBackColor = Color.FromArgb(4, 136, 145);
            headerStyle.SelectionForeColor = Color.White;
            headerStyle.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;

            DataGridViewCellStyle defaultCellStyle = new DataGridViewCellStyle();
            defaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            defaultCellStyle.BackColor = Color.White;
            defaultCellStyle.ForeColor = Color.Black;
            defaultCellStyle.SelectionBackColor = Color.LightGray;
            defaultCellStyle.SelectionForeColor = Color.Black;
            defaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv.DefaultCellStyle = defaultCellStyle;

            DataGridViewCellStyle alternatingRowStyle = new DataGridViewCellStyle();
            alternatingRowStyle.BackColor = Color.WhiteSmoke;
            dgv.AlternatingRowsDefaultCellStyle = alternatingRowStyle;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 35;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
    }
}