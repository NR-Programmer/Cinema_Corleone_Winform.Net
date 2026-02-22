using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CinemaCorleone
{
    public class DashboardService
    {
        public class PendapatanHarian
        {
            public DateTime Tanggal { get; set; }
            public decimal Total { get; set; }
        }

        public class FilmTerpopuler
        {
            public string JudulFilm { get; set; }
            public int TiketTerjual { get; set; }
        }

        public class AktifitasTerbaru
        {
            public string NamaPengguna { get; set; }
            public string JudulFilm { get; set; }
            public DateTime TanggalPemesanan { get; set; }
            public int JumlahTiket { get; set; }
            public decimal TotalPembayaran { get; set; }
        }

        public Dictionary<string, int> GetSummaryData()
        {
            Dictionary<string, int> summaryData = new Dictionary<string, int>();

            using (MySqlConnection connection = Koneksi.GetConn())
            {
                try
                {
                    connection.Open();

                    string filmCountQuery = "SELECT COUNT(*) FROM film";
                    MySqlCommand filmCountCommand = new MySqlCommand(filmCountQuery, connection);
                    summaryData["TotalFilms"] = Convert.ToInt32(filmCountCommand.ExecuteScalar());

                    string userCountQuery = "SELECT COUNT(*) FROM pengguna";
                    MySqlCommand userCountCommand = new MySqlCommand(userCountQuery, connection);
                    summaryData["TotalPengguna"] = Convert.ToInt32(userCountCommand.ExecuteScalar());
                    

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Kesalahan mengambil data ringkasan dashboard: " + ex.Message);
                    throw new Exception("Terjadi kesalahan saat mengambil data ringkasan dashboard. Silakan coba lagi nanti.");
                }
            }
            return summaryData;
        }

        public int GetTotalFilmAktif()
        {
            int count = 0;
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM film WHERE status_tayang = 'sedang_tayang'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error mengambil total film aktif: " + ex.Message);
                    throw new Exception("Terjadi kesalahan saat mengambil total film aktif.");
                }
            }
            return count;
        }

        public int GetPenggunaTerdaftar()
        {
            int count = 0;
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM pengguna";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error mengambil jumlah pengguna terdaftar: " + ex.Message);
                    throw new Exception("Terjadi kesalahan saat mengambil jumlah pengguna terdaftar.");
                }
            }
            return count;
        }

        public decimal GetPendapatanHariIni()
        {
            decimal total = 0;
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SUM(jumlah_bayar) FROM pembayaran WHERE DATE(tanggal_pembayaran) = CURDATE()";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        total = Convert.ToDecimal(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error mengambil pendapatan hari ini: " + ex.Message);
                    throw new Exception("Terjadi kesalahan saat mengambil pendapatan hari ini.");
                }
            }
            return total;
        }

        public int GetPenayanganAktif()
        {
            int count = 0;
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM jadwal_tayang WHERE tanggal = CURDATE() AND jam_mulai > CURTIME()";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error mengambil penayangan aktif: " + ex.Message);
                    throw new Exception("Terjadi kesalahan saat mengambil penayangan aktif.");
                }
            }
            return count;
        }

        public List<PendapatanHarian> GetTrenPendapatan30Hari()
        {
            var dataPendapatan = new List<PendapatanHarian>();
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT
                            CAST(tanggal_pembayaran AS DATE) as Tanggal,
                            SUM(jumlah_bayar) as Total
                        FROM pembayaran
                        WHERE tanggal_pembayaran >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)
                        GROUP BY CAST(tanggal_pembayaran AS DATE)
                        ORDER BY Tanggal ASC;";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataPendapatan.Add(new PendapatanHarian
                            {
                                Tanggal = reader.GetDateTime("Tanggal"),
                                Total = reader.GetDecimal("Total")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error mengambil tren pendapatan: " + ex.Message);
                    throw new Exception("Terjadi kesalahan saat mengambil tren pendapatan.");
                }
            }
            return dataPendapatan;
        }

        public List<FilmTerpopuler> GetFilmTerpopulerData(int limit = 5)
        {
            var popularMovies = new List<FilmTerpopuler>();
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = $@"
                        SELECT
                            f.judul AS JudulFilm,
                            SUM(p.jumlah_tiket) AS TiketTerjual
                        FROM film f
                        JOIN jadwal_tayang jt ON f.id = jt.film_id
                        JOIN pemesanan p ON jt.id = p.jadwal_id
                        GROUP BY f.judul
                        ORDER BY TiketTerjual DESC
                        LIMIT {limit};";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            popularMovies.Add(new FilmTerpopuler
                            {
                                JudulFilm = reader.GetString("JudulFilm"),
                                TiketTerjual = reader.GetInt32("TiketTerjual")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error mengambil film terpopuler: " + ex.Message);
                    throw new Exception("Terjadi kesalahan saat mengambil film terpopuler.");
                }
            }
            return popularMovies;
        }

        public List<AktifitasTerbaru> GetAktifitasTerbaruData(int limit = 10)
        {
            var aktifitas = new List<AktifitasTerbaru>();
            using (MySqlConnection conn = Koneksi.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = $@"
                        SELECT
                            p.nama_lengkap AS NamaPengguna,
                            f.judul AS JudulFilm,
                            pem.tanggal_pemesanan AS TanggalPemesanan,
                            pem.jumlah_tiket AS JumlahTiket,
                            pem.total_harga AS TotalPembayaran
                        FROM pemesanan pem
                        JOIN pengguna p ON pem.pengguna_id = p.id
                        JOIN jadwal_tayang jt ON pem.jadwal_id = jt.id
                        JOIN film f ON jt.film_id = f.id
                        ORDER BY pem.tanggal_pemesanan DESC
                        LIMIT {limit};";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            aktifitas.Add(new AktifitasTerbaru
                            {
                                NamaPengguna = reader.GetString("NamaPengguna"),
                                JudulFilm = reader.GetString("JudulFilm"),
                                TanggalPemesanan = reader.GetDateTime("TanggalPemesanan"),
                                JumlahTiket = reader.GetInt32("JumlahTiket"),
                                TotalPembayaran = reader.GetDecimal("TotalPembayaran")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error mengambil aktifitas terbaru: " + ex.Message);
                    throw new Exception("Terjadi kesalahan saat mengambil aktifitas terbaru.");
                }
            }
            return aktifitas;
        }
    }
}
