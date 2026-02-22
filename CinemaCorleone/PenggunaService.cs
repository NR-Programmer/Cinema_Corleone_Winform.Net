using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace CinemaCorleone
{
    public class PenggunaService
    {
        public static Pengguna AuthenticateUser(string usernameOrEmail, string password)
        {
            Pengguna authenticatedPengguna = null;

            using (MySqlConnection connection = Koneksi.GetConn())
            {
                try
                {
                    connection.Open();

                    string query = "SELECT id, username, email, password, peran, nama_lengkap, no_telepon, alamat, bioskop_id, poin, tanggal_lahir, terakhir_login, status_aktif FROM pengguna WHERE username = @usernameOrEmail OR email = @usernameOrEmail LIMIT 1";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@usernameOrEmail", usernameOrEmail);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashedPasswordFromDb = reader["password"].ToString();

                            if (BCrypt.Net.BCrypt.Verify(password, hashedPasswordFromDb))
                            {
                                authenticatedPengguna = new Pengguna
                                {
                                    Id = reader.GetInt32("id"),
                                    Username = reader.GetString("username"),
                                    Email = reader.GetString("email"),
                                    Peran = reader.GetString("peran"),
                                    NamaLengkap = reader.IsDBNull(reader.GetOrdinal("nama_lengkap")) ? null : reader.GetString("nama_lengkap"),
                                    NoTelepon = reader.IsDBNull(reader.GetOrdinal("no_telepon")) ? null : reader.GetString("no_telepon"),
                                    Alamat = reader.IsDBNull(reader.GetOrdinal("alamat")) ? null : reader.GetString("alamat"),
                                    BioskopId = reader.IsDBNull(reader.GetOrdinal("bioskop_id")) ? (int?)null : reader.GetInt32("bioskop_id"),
                                    Poin = reader.GetInt32("poin"),
                                    TanggalLahir = reader.IsDBNull(reader.GetOrdinal("tanggal_lahir")) ? (DateTime?)null : reader.GetDateTime("tanggal_lahir"),
                                    TerakhirLogin = reader.IsDBNull(reader.GetOrdinal("terakhir_login")) ? (DateTime?)null : reader.GetDateTime("terakhir_login"),
                                    StatusAktif = reader.GetBoolean("status_aktif"),
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Kesalahan Otentikasi: " + ex.Message);
                    throw new Exception("Terjadi kesalahan selama otentikasi. Silakan coba lagi nanti.");
                }
            } 
            return authenticatedPengguna;
        }

        public class Pengguna
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Peran { get; set; }
            public string NamaLengkap { get; set; }
            public string NoTelepon { get; set; }
            public string Alamat { get; set; }
            public int? BioskopId { get; set; }
            public int Poin { get; set; }
            public DateTime? TanggalLahir { get; set; }
            public DateTime? TerakhirLogin { get; set; }
            public bool StatusAktif { get; set; }
        }
    }
}
