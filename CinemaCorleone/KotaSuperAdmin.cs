using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaCorleone
{
    public partial class KotaSuperAdmin : UserControl
    {
        private KotaService _kotaService;
        private List<KotaService.KotaDisplay> _allKotas;
        private int _kotaIdToEdit = -1;

        public KotaSuperAdmin()
        {
            InitializeComponent();
            _kotaService = new KotaService();
            SetupDgvKota();
            LoadKotaData();
        }

        private void SetupDgvKota()
        {
            this.SuspendLayout();

            ApplyDataGridViewStyling(dgvKota);
            dgvKota.RowTemplate.Height = 30;

            dgvKota.AutoGenerateColumns = false;
            dgvKota.Columns.Clear();

            dgvKota.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colNo",
                HeaderText = "No",
                DataPropertyName = "No",
                Width = 50,
                Resizable = DataGridViewTriState.False
            });

            dgvKota.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colKodeKota",
                HeaderText = "Kode Kota",
                DataPropertyName = "KodeKota",
                Width = 100
            });

            dgvKota.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colNamaKota",
                HeaderText = "Nama Kota",
                DataPropertyName = "NamaKota",
                MinimumWidth = 200
            });

            dgvKota.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colProvinsi",
                HeaderText = "Provinsi",
                DataPropertyName = "Provinsi",
                Width = 150
            });

            dgvKota.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colZonaWaktu",
                HeaderText = "Zona Waktu",
                DataPropertyName = "ZonaWaktu",
                Width = 100
            });

            dgvKota.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colSlug",
                HeaderText = "Slug",
                DataPropertyName = "Slug",
                MinimumWidth = 150
            });

            dgvKota.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colStatusAktif",
                HeaderText = "Status Aktif",
                DataPropertyName = "StatusAktifDisplay",
                Width = 100
            });

            this.ResumeLayout(false);
        }

        private void LoadKotaData()
        {
            try
            {
                _allKotas = _kotaService.GetKotasForDisplay();
                dgvKota.DataSource = _allKotas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat memuat data kota: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            headerStyle.SelectionBackColor = Color.FromArgb(4, 136, 145);
            headerStyle.SelectionForeColor = Color.White;
            headerStyle.WrapMode = DataGridViewTriState.True;
            dgv.ColumnHeadersDefaultCellStyle = headerStyle;

            DataGridViewCellStyle defaultCellStyle = new DataGridViewCellStyle();
            defaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            defaultCellStyle.BackColor = Color.White;
            defaultCellStyle.ForeColor = Color.Black;
            defaultCellStyle.Font = new Font("Segoe UI", 10F);
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
        

        private void tbCariData_TextChanged(object sender, EventArgs e)
        {
            string searchText = (sender as Guna.UI2.WinForms.Guna2TextBox)?.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                dgvKota.DataSource = _allKotas;
            }
            else
            {
                var filteredKotas = _allKotas.Where(kota =>
                    kota.KodeKota.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    kota.NamaKota.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    kota.Provinsi.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    kota.ZonaWaktu.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    kota.StatusAktifDisplay.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    kota.Slug.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
                dgvKota.DataSource = filteredKotas;
            }
        }

        private void DgvKota_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvKota.Rows.Count)
            {
                if (e.RowIndex == -1) return;

                KotaService.KotaDisplay selectedKotaDisplay = dgvKota.Rows[e.RowIndex].DataBoundItem as KotaService.KotaDisplay;

                if (selectedKotaDisplay != null)
                {
                    _kotaIdToEdit = selectedKotaDisplay.Id;

                    try
                    {
                        KotaService.Kota fullKotaDetails = _kotaService.GetKotaById(selectedKotaDisplay.Id);
                        if (fullKotaDetails != null)
                        {
                            tbKodeKota.Text = fullKotaDetails.KodeKota;
                            tbNamaKota.Text = fullKotaDetails.NamaKota;
                            tbProvinsi.Text = fullKotaDetails.Provinsi;
                            cbZonaWaktu.SelectedItem = fullKotaDetails.ZonaWaktu;
                            tbSlug.Text = fullKotaDetails.Slug;
                            cbStatusAktif.SelectedItem = fullKotaDetails.StatusAktif ? "Aktif" : "Tidak Aktif";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal memuat detail kota: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool IsValidInput(int kotaId = 0)
        {
            string kodeKota = tbKodeKota.Text.Trim();
            string namaKota = tbNamaKota.Text.Trim();
            string provinsi = tbProvinsi.Text.Trim();
            string zonaWaktu = cbZonaWaktu.SelectedItem?.ToString();
            string slug = tbSlug.Text.Trim();
            string statusAktifDisplay = cbStatusAktif.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(kodeKota))
            {
                MessageBox.Show("Kode Kota tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(namaKota))
            {
                MessageBox.Show("Nama Kota tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(provinsi))
            {
                MessageBox.Show("Provinsi tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(zonaWaktu))
            {
                MessageBox.Show("Zona Waktu tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(slug))
            {
                MessageBox.Show("Slug tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(statusAktifDisplay))
            {
                MessageBox.Show("Status Aktif tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Uniqueness checks
            if (!_kotaService.IsKodeKotaUnique(kodeKota, kotaId))
            {
                MessageBox.Show("Kode Kota harus unik.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!_kotaService.IsNamaKotaUnique(namaKota, kotaId))
            {
                MessageBox.Show("Nama Kota harus unik.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!_kotaService.IsSlugUnique(slug, kotaId))
            {
                MessageBox.Show("Slug harus unik.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnTambahKota_Click(object sender, EventArgs e)
        {
            if (!IsValidInput())
            {
                return;
            }

            string kodeKota = tbKodeKota.Text.Trim();
            string namaKota = tbNamaKota.Text.Trim();
            string provinsi = tbProvinsi.Text.Trim();
            string zonaWaktu = cbZonaWaktu.SelectedItem?.ToString();
            string slug = tbSlug.Text.Trim();
            string statusAktifDisplay = cbStatusAktif.SelectedItem?.ToString();
            bool statusAktif = (statusAktifDisplay == "Aktif");

            try
            {
                KotaService.Kota newKota = new KotaService.Kota
                {
                    KodeKota = kodeKota,
                    NamaKota = namaKota,
                    Provinsi = provinsi,
                    ZonaWaktu = zonaWaktu,
                    Slug = slug,
                    StatusAktif = statusAktif
                };

                _kotaService.AddKota(newKota);
                MessageBox.Show("Kota berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadKotaData();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menambahkan kota: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUbahKota_Click(object sender, EventArgs e)
        {
            if (_kotaIdToEdit == -1)
            {
                MessageBox.Show("Pilih kota yang ingin diubah dari tabel terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidInput(_kotaIdToEdit))
            {
                return;
            }

            string kodeKota = tbKodeKota.Text.Trim();
            string namaKota = tbNamaKota.Text.Trim();
            string provinsi = tbProvinsi.Text.Trim();
            string zonaWaktu = cbZonaWaktu.SelectedItem?.ToString();
            string slug = tbSlug.Text.Trim();
            string statusAktifDisplay = cbStatusAktif.SelectedItem?.ToString();
            bool statusAktif = (statusAktifDisplay == "Aktif");

            try
            {
                KotaService.Kota originalKota = _kotaService.GetKotaById(_kotaIdToEdit);
                if (originalKota == null)
                {
                    MessageBox.Show("Kota tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                KotaService.Kota updatedKota = new KotaService.Kota
                {
                    Id = _kotaIdToEdit,
                    KodeKota = kodeKota,
                    NamaKota = namaKota,
                    Provinsi = provinsi,
                    ZonaWaktu = zonaWaktu,
                    Slug = slug,
                    StatusAktif = statusAktif
                };

                bool isChanged = false;
                if (!string.Equals(originalKota.KodeKota, updatedKota.KodeKota)) isChanged = true;
                if (!string.Equals(originalKota.NamaKota, updatedKota.NamaKota)) isChanged = true;
                if (!string.Equals(originalKota.Provinsi, updatedKota.Provinsi)) isChanged = true;
                if (!string.Equals(originalKota.ZonaWaktu, updatedKota.ZonaWaktu)) isChanged = true;
                if (!string.Equals(originalKota.Slug, updatedKota.Slug)) isChanged = true;
                if (originalKota.StatusAktif != updatedKota.StatusAktif) isChanged = true;

                if (isChanged)
                {
                    _kotaService.UpdateKota(updatedKota);
                    MessageBox.Show("Kota berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadKotaData();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Tidak ada perubahan yang terdeteksi.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memperbarui kota: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapusKota_Click(object sender, EventArgs e)
        {
            if (_kotaIdToEdit == -1)
            {
                MessageBox.Show("Pilih kota yang ingin dihapus dari tabel terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                "Apakah Anda yakin ingin menghapus kota ini? Tindakan ini tidak dapat dibatalkan.",
                "Konfirmasi Hapus Kota",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    _kotaService.DeleteKota(_kotaIdToEdit);
                    MessageBox.Show("Kota berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadKotaData();
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menghapus kota: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearInputFields()
        {
            tbKodeKota.Clear();
            tbNamaKota.Clear();
            tbProvinsi.Clear();
            cbZonaWaktu.SelectedIndex = -1; // Clear selection
            tbSlug.Clear();
            cbStatusAktif.SelectedIndex = -1; // Clear selection
            _kotaIdToEdit = -1; // Reset city ID being edited
        }

        private void btnBersih_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }
    }
}
