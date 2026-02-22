using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaCorleone
{
    internal class KotaService
    {
        public class Kota
        {
            public int Id { get; set; }
            public string KodeKota { get; set; }
            public string NamaKota { get; set; }
            public string Provinsi { get; set; }
            public string ZonaWaktu { get; set; }
            public string Slug { get; set; }
            public bool StatusAktif { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }

        public class KotaDisplay
        {
            public int No { get; set; }
            public int Id { get; set; }
            public string KodeKota { get; set; }
            public string NamaKota { get; set; }
            public string Provinsi { get; set; }
            public string ZonaWaktu { get; set; }
            public string StatusAktifDisplay { get; set; }
            public string Slug { get; set; }
        }

        public List<KotaDisplay> GetKotasForDisplay()
        {
            List<KotaDisplay> kotas = new List<KotaDisplay>();
            MySqlConnection connection = null;

            try
            {
                connection = Koneksi.GetConn();
                connection.Open();

                string query = "SELECT id, kode_kota, nama_kota, provinsi, zona_waktu, slug, status_aktif FROM kota ORDER BY nama_kota ASC";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                int no = 1;
                while (reader.Read())
                {
                    kotas.Add(new KotaDisplay
                    {
                        No = no++,
                        Id = reader.GetInt32("id"),
                        KodeKota = reader.GetString("kode_kota"),
                        NamaKota = reader.GetString("nama_kota"),
                        Provinsi = reader.GetString("provinsi"),
                        ZonaWaktu = reader.GetString("zona_waktu"),
                        Slug = reader.GetString("slug"),
                        StatusAktifDisplay = reader.GetBoolean("status_aktif") ? "Aktif" : "Tidak Aktif"
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving city data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return kotas;
        }

        public Kota GetKotaById(int id)
        {
            Kota kota = null;
            MySqlConnection connection = null;

            try
            {
                connection = Koneksi.GetConn();
                connection.Open();

                string query = "SELECT id, kode_kota, nama_kota, provinsi, zona_waktu, slug, status_aktif, created_at, updated_at FROM kota WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    kota = new Kota
                    {
                        Id = reader.GetInt32("id"),
                        KodeKota = reader.GetString("kode_kota"),
                        NamaKota = reader.GetString("nama_kota"),
                        Provinsi = reader.GetString("provinsi"),
                        ZonaWaktu = reader.GetString("zona_waktu"),
                        Slug = reader.GetString("slug"),
                        StatusAktif = reader.GetBoolean("status_aktif"),
                        CreatedAt = reader.GetDateTime("created_at"),
                        UpdatedAt = reader.GetDateTime("updated_at")
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving city by ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return kota;
        }

        public bool IsKodeKotaUnique(string kodeKota, int excludeId = 0)
        {
            MySqlConnection connection = null;
            try
            {
                connection = Koneksi.GetConn();
                connection.Open();

                string query = "SELECT COUNT(*) FROM kota WHERE kode_kota = @kode_kota AND id != @excludeId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@kode_kota", kodeKota);
                command.Parameters.AddWithValue("@excludeId", excludeId);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count == 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking Kode Kota uniqueness: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public bool IsNamaKotaUnique(string namaKota, int excludeId = 0)
        {
            MySqlConnection connection = null;
            try
            {
                connection = Koneksi.GetConn();
                connection.Open();

                string query = "SELECT COUNT(*) FROM kota WHERE nama_kota = @nama_kota AND id != @excludeId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nama_kota", namaKota);
                command.Parameters.AddWithValue("@excludeId", excludeId);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count == 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking Nama Kota uniqueness: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public bool IsSlugUnique(string slug, int excludeId = 0)
        {
            MySqlConnection connection = null;
            try
            {
                connection = Koneksi.GetConn();
                connection.Open();

                string query = "SELECT COUNT(*) FROM kota WHERE slug = @slug AND id != @excludeId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@slug", slug);
                command.Parameters.AddWithValue("@excludeId", excludeId);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count == 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking Slug uniqueness: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void AddKota(Kota kota)
        {
            MySqlConnection connection = null;
            try
            {
                connection = Koneksi.GetConn();
                connection.Open();

                string query = "INSERT INTO kota (kode_kota, nama_kota, provinsi, zona_waktu, slug, status_aktif, created_at, updated_at) " +
                               "VALUES (@kode_kota, @nama_kota, @provinsi, @zona_waktu, @slug, @status_aktif, NOW(), NOW())";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@kode_kota", kota.KodeKota);
                command.Parameters.AddWithValue("@nama_kota", kota.NamaKota);
                command.Parameters.AddWithValue("@provinsi", kota.Provinsi);
                command.Parameters.AddWithValue("@zona_waktu", kota.ZonaWaktu);
                command.Parameters.AddWithValue("@slug", kota.Slug);
                command.Parameters.AddWithValue("@status_aktif", kota.StatusAktif);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add city: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void UpdateKota(Kota kota)
        {
            MySqlConnection connection = null;
            try
            {
                connection = Koneksi.GetConn();
                connection.Open();

                string query = "UPDATE kota SET kode_kota = @kode_kota, nama_kota = @nama_kota, provinsi = @provinsi, " +
                               "zona_waktu = @zona_waktu, slug = @slug, status_aktif = @status_aktif, updated_at = NOW() WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@kode_kota", kota.KodeKota);
                command.Parameters.AddWithValue("@nama_kota", kota.NamaKota);
                command.Parameters.AddWithValue("@provinsi", kota.Provinsi);
                command.Parameters.AddWithValue("@zona_waktu", kota.ZonaWaktu);
                command.Parameters.AddWithValue("@slug", kota.Slug);
                command.Parameters.AddWithValue("@status_aktif", kota.StatusAktif);
                command.Parameters.AddWithValue("@id", kota.Id);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update city: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void DeleteKota(int id)
        {
            MySqlConnection connection = null;
            try
            {
                connection = Koneksi.GetConn();
                connection.Open();

                string query = "DELETE FROM kota WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete city: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
