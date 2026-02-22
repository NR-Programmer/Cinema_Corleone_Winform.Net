using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace CinemaCorleone
{
    public partial class FilmSuperAdmin : UserControl
    {
        private static Bitmap _noImageAvailableBitmap;
        private FilmService _filmService; // Declare FilmService instance
        private int _filmIdToEdit = -1; // -1 menunjukkan tidak ada film yang dipilih untuk diedit
        private List<FilmService.FilmDisplay> _allFilms; // Store all films for searching

        public FilmSuperAdmin()
        {
            InitializeComponent();
            _filmService = new FilmService();
            SetupDgvFilms();
        }

        private void SetupDgvFilms()
        {
            this.SuspendLayout();

            if (_noImageAvailableBitmap == null)
            {
                _noImageAvailableBitmap = new Bitmap(100, 150);
                using (Graphics g = Graphics.FromImage(_noImageAvailableBitmap))
                {
                    g.FillRectangle(Brushes.LightGray, 0, 0, 100, 150);
                }
            }

            ApplyDataGridViewStyling(dgvFilms);
            dgvFilms.RowTemplate.Height = 150;

            dgvFilms.CellFormatting += DgvFilms_CellFormatting;

            dgvFilms.AutoGenerateColumns = false;
            dgvFilms.Columns.Clear();

            dgvFilms.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colNo",
                HeaderText = "No",
                DataPropertyName = "No",
                Width = 50,
                Resizable = DataGridViewTriState.False
            });

            DataGridViewImageColumn colPoster = new DataGridViewImageColumn();
            colPoster.Name = "colPoster";
            colPoster.HeaderText = "Poster";
            colPoster.DataPropertyName = "Poster";
            colPoster.ImageLayout = DataGridViewImageCellLayout.Zoom;
            colPoster.Width = 100;
            dgvFilms.Columns.Add(colPoster);

            dgvFilms.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colJudul",
                HeaderText = "Judul",
                DataPropertyName = "Judul",
                MinimumWidth = 200
            });

            dgvFilms.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colGenre",
                HeaderText = "Genre",
                DataPropertyName = "Genre",
                Width = 120
            });

            dgvFilms.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colDurasi",
                HeaderText = "Durasi",
                DataPropertyName = "DurasiDisplay",
                Width = 80
            });

            dgvFilms.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colRating",
                HeaderText = "Rating",
                DataPropertyName = "Rating",
                Width = 60
            });

            dgvFilms.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colStatus",
                HeaderText = "Status",
                DataPropertyName = "StatusTayangDisplay",
                Width = 100
            });

            this.ResumeLayout(false);

            LoadFilmData();
        }

        private void LoadFilmData()
        {
            try
            {
                _allFilms = _filmService.GetFilmsForDisplay();
                dgvFilms.DataSource = _allFilms;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading film data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbCariData_TextChanged(object sender, EventArgs e)
        {
            string searchText = (sender as Guna.UI2.WinForms.Guna2TextBox)?.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                dgvFilms.DataSource = _allFilms;
            }
            else
            {
                var filteredFilms = _allFilms.Where(film =>
                    film.Judul.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    film.Genre.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    film.Rating.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    film.StatusTayangDisplay.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    film.DurasiDisplay.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();
                dgvFilms.DataSource = filteredFilms;
            }
        }

        private async void DgvFilms_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Styling for Poster column (existing logic)
            if (dgvFilms.Columns[e.ColumnIndex].Name == "colPoster")
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvFilms.Rows.Count)
                {
                    FilmService.FilmDisplay film = dgvFilms.Rows[e.RowIndex].DataBoundItem as FilmService.FilmDisplay;
                    if (film != null && !string.IsNullOrEmpty(film.PosterUrl))
                    {
                        if (e.Value == null || !(e.Value is Image))
                        {
                            try
                            {
                                using (HttpClient client = new HttpClient())
                                {
                                    DataGridViewCell targetCell = dgvFilms.Rows[e.RowIndex].Cells[e.ColumnIndex];

                                    byte[] imageBytes = await client.GetByteArrayAsync(film.PosterUrl);
                                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes))
                                    {
                                        Image originalImg = Image.FromStream(ms);
                                        if (originalImg != null)
                                        {
                                            Image img = new Bitmap(originalImg);

                                            if (targetCell.OwningRow != null && !targetCell.OwningRow.IsNewRow && targetCell.OwningColumn != null)
                                            {
                                                targetCell.Value = img;
                                                // dgvFilms.InvalidateCell(targetCell.ColumnIndex, targetCell.RowIndex); // This causes infinite loop if not careful
                                            }
                                            originalImg.Dispose();
                                        }
                                        else
                                        {
                                            if (targetCell.OwningRow != null && !targetCell.OwningRow.IsNewRow && targetCell.OwningColumn != null)
                                            {
                                                targetCell.Value = _noImageAvailableBitmap;
                                                // dgvFilms.InvalidateCell(targetCell.ColumnIndex, targetCell.RowIndex);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                e.Value = _noImageAvailableBitmap;
                            }
                        }
                    }
                    else
                    {
                        e.Value = _noImageAvailableBitmap;
                    }
                }
            }
            else if (dgvFilms.Columns[e.ColumnIndex].Name == "colStatus")
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvFilms.Rows.Count)
                {
                    FilmService.FilmDisplay film = dgvFilms.Rows[e.RowIndex].DataBoundItem as FilmService.FilmDisplay;
                    if (film != null)
                    {
                        switch (film.StatusTayangDisplay)
                        {
                            case "Sedang Tayang":
                                e.CellStyle.BackColor = Color.FromArgb(204, 255, 204); // Light Green
                                e.CellStyle.ForeColor = Color.DarkGreen;
                                break;
                            case "Akan Datang":
                                e.CellStyle.BackColor = Color.FromArgb(204, 229, 255); // Light Blue
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                break;
                            case "Selesai":
                                e.CellStyle.BackColor = Color.FromArgb(224, 224, 224); // Light Gray
                                e.CellStyle.ForeColor = Color.DimGray;
                                break;
                            default:
                                e.CellStyle.BackColor = dgvFilms.DefaultCellStyle.BackColor;
                                e.CellStyle.ForeColor = dgvFilms.DefaultCellStyle.ForeColor;
                                break;
                        }
                    }
                }
            }
        }

        private void DgvFilms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvFilms.Rows.Count)
            {
                // Pastikan bukan baris header
                if (e.RowIndex == -1) return;

                // Ambil objek FilmDisplay dari baris yang diklik
                FilmService.FilmDisplay selectedFilmDisplay = dgvFilms.Rows[e.RowIndex].DataBoundItem as FilmService.FilmDisplay;

                if (selectedFilmDisplay != null)
                {
                    // Simpan ID film untuk operasi Edit/Delete
                    _filmIdToEdit = selectedFilmDisplay.Id;

                    // Perlu mengambil detail film lengkap dari FilmService karena FilmDisplay tidak memiliki semua properti
                    try
                    {
                        FilmService.Film fullFilmDetails = _filmService.GetFilmById(selectedFilmDisplay.Id);
                        if (fullFilmDetails != null)
                        {
                            tbJudulFilm.Text = fullFilmDetails.Judul;
                            tbSinopsis.Text = fullFilmDetails.Sinopsis;
                            
                            // Konversi durasi total menit ke jam dan menit
                            int durasiJam = fullFilmDetails.Durasi / 60;
                            int durasiMenit = fullFilmDetails.Durasi % 60;
                            tbDurasiJam.Text = durasiJam.ToString();
                            tbDurasiMenit.Text = durasiMenit.ToString();

                            // Set ComboBox selected item by text. This assumes the ComboBox items are strings matching the values.
                            // If ComboBox items are objects, this logic might need adjustment (e.g., finding item by value).
                            cbGenre.SelectedItem = fullFilmDetails.Genre; 
                            cbRatingUsia.SelectedItem = ConvertDbValueToDisplayRatingUsia(fullFilmDetails.Rating);
                            tbSutradara.Text = fullFilmDetails.Sutradara;
                            tbPemeranUtama.Text = fullFilmDetails.PemeranUtama;
                            dtpTanggalRilis.Value = fullFilmDetails.TanggalRilis;
                            dtpTanggalBerakhir.Value = fullFilmDetails.TanggalBerakhir;
                            tbUrlPoster.Text = fullFilmDetails.PosterUrl;
                            tbUrlTrailer.Text = fullFilmDetails.TrailerUrl;

                            // Konversi nilai status_tayang dari database ke tampilan yang sesuai untuk ComboBox
                            string displayStatus = _filmService.GetFilmStatusDisplay(fullFilmDetails.StatusTayang);
                            cbStatusTayang.SelectedItem = displayStatus;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal memuat detail film: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnTambahFilm_Click(object sender, EventArgs e)
        {
            // 1. Ambil nilai dari kontrol input
            string judul = tbJudulFilm.Text.Trim();
            string durasiJamText = tbDurasiJam.Text.Trim();
            string durasiMenitText = tbDurasiMenit.Text.Trim();
            string statusTayangDisplay = cbStatusTayang.SelectedItem?.ToString();
            string sinopsis = tbSinopsis.Text.Trim();
            string genre = cbGenre.SelectedItem?.ToString();
            string ratingUsia = cbRatingUsia.SelectedItem?.ToString();
            string sutradara = tbSutradara.Text.Trim();
            string pemeranUtama = tbPemeranUtama.Text.Trim();
            DateTime tanggalRilis = dtpTanggalRilis.Value;
            DateTime tanggalBerakhir = dtpTanggalBerakhir.Value;
            string urlPoster = tbUrlPoster.Text.Trim();
            string urlTrailer = tbUrlTrailer.Text.Trim();

            // 2. Validasi input
            if (string.IsNullOrEmpty(judul))
            {
                MessageBox.Show("Judul Film tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int durasiJam, durasiMenit;
            if (!int.TryParse(durasiJamText, out durasiJam) || !int.TryParse(durasiMenitText, out durasiMenit) || durasiJam < 0 || durasiMenit < 0 || durasiMenit >= 60)
            {
                MessageBox.Show("Durasi Jam dan Menit harus berupa angka positif dan Menit kurang dari 60.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(statusTayangDisplay))
            {
                MessageBox.Show("Status Tayang tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            string statusTayangDb = ConvertStatusTayangToDbValue(statusTayangDisplay);
            if (string.IsNullOrEmpty(statusTayangDb))
            {
                MessageBox.Show("Nilai Status Tayang tidak valid.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (string.IsNullOrEmpty(genre))
            {
                MessageBox.Show("Genre tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(ratingUsia))
            {
                MessageBox.Show("Rating Usia tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string RatingUsiaDb = ConvertRatingUsiaToDbValue(ratingUsia);
            if (string.IsNullOrEmpty(RatingUsiaDb))
            {
                MessageBox.Show("Nilai Rating Usia tidak valid.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tanggalRilis > tanggalBerakhir)
            {
                MessageBox.Show("Tanggal Rilis tidak boleh setelah Tanggal Berakhir.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // 3. Konversi durasi
            int durasiTotalMenit = (durasiJam * 60) + durasiMenit;

            try
            {
                // 4. Buat objek FilmService.Film baru
                FilmService.Film newFilm = new FilmService.Film
                {
                    Judul = judul,
                    Sinopsis = sinopsis,
                    Durasi = durasiTotalMenit,
                    Genre = genre,
                    Sutradara = sutradara,
                    PemeranUtama = pemeranUtama,
                    Rating = RatingUsiaDb, // Use the converted DB value
                    PosterUrl = urlPoster,
                    TrailerUrl = urlTrailer,
                    TanggalRilis = tanggalRilis,
                    TanggalBerakhir = tanggalBerakhir,
                    StatusTayang = statusTayangDb // Gunakan nilai yang sudah dikonversi
                };

                // 5. Panggil _filmService.AddFilm()
                _filmService.AddFilm(newFilm);

                // 6. Berikan umpan balik sukses
                MessageBox.Show("Film berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 7. Refresh dgvFilms dan kosongkan input
                LoadFilmData();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                // 6. Berikan umpan balik gagal
                MessageBox.Show($"Gagal menambahkan film: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUbahFilm_Click(object sender, EventArgs e)
        {
            if (_filmIdToEdit == -1)
            {
                MessageBox.Show("Pilih film yang ingin diubah dari tabel terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Ambil nilai dari kontrol input
            string judul = tbJudulFilm.Text.Trim();
            string durasiJamText = tbDurasiJam.Text.Trim();
            string durasiMenitText = tbDurasiMenit.Text.Trim();
            string statusTayangDisplay = cbStatusTayang.SelectedItem?.ToString();
            string sinopsis = tbSinopsis.Text.Trim();
            string genre = cbGenre.SelectedItem?.ToString();
            string ratingUsia = cbRatingUsia.SelectedItem?.ToString();
            string sutradara = tbSutradara.Text.Trim();
            string pemeranUtama = tbPemeranUtama.Text.Trim();
            DateTime tanggalRilis = dtpTanggalRilis.Value;
            DateTime tanggalBerakhir = dtpTanggalBerakhir.Value;
            string urlPoster = tbUrlPoster.Text.Trim();
            string urlTrailer = tbUrlTrailer.Text.Trim();

            // 2. Validasi input (sama seperti btnTambahFilm_Click)
            if (string.IsNullOrEmpty(judul))
            {
                MessageBox.Show("Judul Film tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int durasiJam, durasiMenit;
            if (!int.TryParse(durasiJamText, out durasiJam) || !int.TryParse(durasiMenitText, out durasiMenit) || durasiJam < 0 || durasiMenit < 0 || durasiMenit >= 60)
            {
                MessageBox.Show("Durasi Jam dan Menit harus berupa angka positif dan Menit kurang dari 60.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(statusTayangDisplay))
            {
                MessageBox.Show("Status Tayang tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            string statusTayangDb = ConvertStatusTayangToDbValue(statusTayangDisplay);
            if (string.IsNullOrEmpty(statusTayangDb))
            {
                MessageBox.Show("Nilai Status Tayang tidak valid.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (string.IsNullOrEmpty(genre))
            {
                MessageBox.Show("Genre tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(ratingUsia))
            {
                MessageBox.Show("Rating Usia tidak boleh kosong.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string RatingUsiaDb = ConvertRatingUsiaToDbValue(ratingUsia);
            if (string.IsNullOrEmpty(RatingUsiaDb))
            {
                MessageBox.Show("Nilai Rating Usia tidak valid.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tanggalRilis > tanggalBerakhir)
            {
                MessageBox.Show("Tanggal Rilis tidak boleh setelah Tanggal Berakhir.", "Validasi Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Konversi durasi
            int durasiTotalMenit = (durasiJam * 60) + durasiMenit;

            try
            {
                // Ambil data film asli dari database
                FilmService.Film originalFilm = _filmService.GetFilmById(_filmIdToEdit);
                if (originalFilm == null)
                {
                    MessageBox.Show("Film tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Buat objek FilmService.Film yang diperbarui dari input pengguna
                FilmService.Film updatedFilm = new FilmService.Film
                {
                    Id = _filmIdToEdit,
                    Judul = judul,
                    Sinopsis = sinopsis,
                    Durasi = durasiTotalMenit,
                    Genre = genre,
                    Sutradara = sutradara,
                    PemeranUtama = pemeranUtama,
                    Rating = RatingUsiaDb, // Use the converted DB value
                    PosterUrl = urlPoster,
                    TrailerUrl = urlTrailer,
                    TanggalRilis = tanggalRilis,
                    TanggalBerakhir = tanggalBerakhir,
                    StatusTayang = statusTayangDb
                };

                // Periksa apakah ada perubahan
                bool isChanged = false;
                if (!string.Equals(originalFilm.Judul, updatedFilm.Judul)) isChanged = true;
                if (!string.Equals(originalFilm.Sinopsis, updatedFilm.Sinopsis)) isChanged = true;
                if (originalFilm.Durasi != updatedFilm.Durasi) isChanged = true;
                if (!string.Equals(originalFilm.Genre, updatedFilm.Genre)) isChanged = true;
                if (!string.Equals(originalFilm.Sutradara, updatedFilm.Sutradara)) isChanged = true;
                if (!string.Equals(originalFilm.PemeranUtama, updatedFilm.PemeranUtama)) isChanged = true;
                if (!string.Equals(originalFilm.Rating, updatedFilm.Rating)) isChanged = true;
                if (!string.Equals(originalFilm.PosterUrl, updatedFilm.PosterUrl)) isChanged = true;
                if (!string.Equals(originalFilm.TrailerUrl, updatedFilm.TrailerUrl)) isChanged = true;
                if (originalFilm.TanggalRilis.Date != updatedFilm.TanggalRilis.Date) isChanged = true;
                if (originalFilm.TanggalBerakhir.Date != updatedFilm.TanggalBerakhir.Date) isChanged = true;
                if (!string.Equals(originalFilm.StatusTayang, updatedFilm.StatusTayang)) isChanged = true;
                
                if (isChanged)
                {
                    // 5. Panggil _filmService.UpdateFilm()
                    _filmService.UpdateFilm(updatedFilm);

                    // 6. Berikan umpan balik sukses
                    MessageBox.Show("Film berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 7. Refresh dgvFilms dan kosongkan input
                    LoadFilmData();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Tidak ada perubahan yang terdeteksi.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // 6. Berikan umpan balik gagal
                MessageBox.Show($"Gagal memperbarui film: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapusFilm_Click(object sender, EventArgs e)
        {
            if (_filmIdToEdit == -1)
            {
                MessageBox.Show("Pilih film yang ingin dihapus dari tabel terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                "Apakah Anda yakin ingin menghapus film ini? Tindakan ini tidak dapat dibatalkan.",
                "Konfirmasi Hapus Film",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    _filmService.DeleteFilm(_filmIdToEdit);
                    MessageBox.Show("Film berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadFilmData();
                    ClearInputFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menghapus film: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearInputFields()
        {
            tbJudulFilm.Clear();
            tbDurasiJam.Clear();
            tbDurasiMenit.Clear();
            cbStatusTayang.SelectedIndex = -1; // Clear selection
            tbSinopsis.Clear();
            cbGenre.SelectedIndex = -1;
            cbRatingUsia.SelectedIndex = -1;
            tbSutradara.Clear();
            tbPemeranUtama.Clear();
            dtpTanggalRilis.Value = DateTime.Now; // Reset to current date
            dtpTanggalBerakhir.Value = DateTime.Now;
            tbUrlPoster.Clear();
            tbUrlTrailer.Clear();
            _filmIdToEdit = -1; // Reset film ID being edited
        }

        private void btnBersih_Click(object sender, EventArgs e)
        {
            ClearInputFields();
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

        private string ConvertStatusTayangToDbValue(string displayValue)
        {
            switch (displayValue)
            {
                case "Sedang Tayang":
                    return "sedang_tayang";
                case "Akan Datang":
                    return "akan_datang";
                case "Selesai": 
                    return "selesai"; 
                default:
                    return "";
            }
        }

        private string ConvertRatingUsiaToDbValue(string displayValue)
        {
            switch (displayValue)
            {
                case "SU (Semua Umur)":
                    return "SU";
                case "13+":
                    return "13+";
                case "17+":
                    return "17+";
                case "21+":
                    return "21+";
                default:
                    throw new ArgumentException($"Nilai Rating Usia tidak valid: {displayValue}");
            }
        }

        private string ConvertDbValueToDisplayRatingUsia(string dbValue)
        {
            switch (dbValue)
            {
                case "SU":
                    return "SU (Semua Umur)";
                case "13+":
                    return "13+";
                case "17+":
                    return "17+";
                case "21+":
                    return "21+";
                default:
                    // If DB value is not recognized, return it as is or throw an exception.
                    // For now, returning as is to prevent crashes for unforeseen data.
                    return dbValue; 
            }
        }
    }
}