using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing; // Added for Image type

namespace CinemaCorleone
{
    public class FilmService
    {
        // Inner class for Film data model, following PenggunaService pattern
        public class Film
        {
            public int Id { get; set; }
            public string Judul { get; set; }
            public string Sinopsis { get; set; }
            public int Durasi { get; set; } // in minutes
            public string Genre { get; set; }
            public string Sutradara { get; set; }
            public string PemeranUtama { get; set; }
            public string Rating { get; set; }
            public string PosterUrl { get; set; }
            public string TrailerUrl { get; set; }
            public DateTime TanggalRilis { get; set; }
            public DateTime TanggalBerakhir { get; set; }
            public string StatusTayang { get; set; } // Changed to string to match DB potentially
            public string ThumbnailUrl { get; set; }
            public string BackdropUrl { get; set; }
        }

        public class FilmDisplay
        {
            public int No { get; set; } // For row numbering in DGV
            public int Id { get; set; } // Hidden, for internal use
            public string Judul { get; set; }
            public Image Poster { get; set; } // For displaying poster image
            public string PosterUrl { get; set; } // Keep URL for loading
            public string Genre { get; set; }
            public string DurasiDisplay { get; set; } // Formatted duration
            public string Rating { get; set; }
            public string StatusTayangDisplay { get; set; } // Formatted status
        }
        
        public string GetFilmStatusDisplay(string statusTayang)
        {
            switch (statusTayang.ToLower())
            {
                case "sedang_tayang":
                    return "Sedang Tayang";
                case "akan_datang":
                    return "Akan Datang";
                case "telah_tayang":
                    return "Telah Tayang";
                default:
                    return statusTayang;
            }
        }

        public List<FilmDisplay> GetFilmsForDisplay()
        {
            List<Film> films = GetAllFilms();
            List<FilmDisplay> filmsForDisplay = new List<FilmDisplay>();
            int no = 1;
            foreach (var film in films)
            {
                filmsForDisplay.Add(new FilmDisplay
                {
                    No = no++,
                    Id = film.Id,
                    Judul = film.Judul,
                    PosterUrl = film.PosterUrl, // Store URL, load image in CellFormatting
                    Genre = film.Genre,
                    DurasiDisplay = $"{film.Durasi / 60}j {film.Durasi % 60}m",
                    Rating = film.Rating,
                    StatusTayangDisplay = GetFilmStatusDisplay(film.StatusTayang)
                });
            }
            return filmsForDisplay;
        }

        public List<Film> GetAllFilms()
        {
            List<Film> films = new List<Film>();

            using (MySqlConnection connection = Koneksi.GetConn())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id, judul, sinopsis, durasi, genre, sutradara, pemeran_utama, rating, poster_url, trailer_url, tanggal_rilis, tanggal_berakhir, status_tayang, thumbnail_url, backdrop_url FROM film";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            films.Add(new Film
                            {
                                Id = reader.GetInt32("id"),
                                Judul = reader.GetString("judul"),
                                Sinopsis = reader.GetString("sinopsis"),
                                Durasi = reader.GetInt32("durasi"),
                                Genre = reader.GetString("genre"),
                                Sutradara = reader.GetString("sutradara"),
                                PemeranUtama = reader.GetString("pemeran_utama"),
                                Rating = reader.GetString("rating"),
                                PosterUrl = reader.IsDBNull(reader.GetOrdinal("poster_url")) ? null : reader.GetString("poster_url"),
                                TrailerUrl = reader.IsDBNull(reader.GetOrdinal("trailer_url")) ? null : reader.GetString("trailer_url"),
                                TanggalRilis = reader.GetDateTime("tanggal_rilis"),
                                TanggalBerakhir = reader.GetDateTime("tanggal_berakhir"),
                                StatusTayang = reader.GetString("status_tayang"), // Corrected from GetBoolean to GetString
                                ThumbnailUrl = reader.IsDBNull(reader.GetOrdinal("thumbnail_url")) ? null : reader.GetString("thumbnail_url"),
                                BackdropUrl = reader.IsDBNull(reader.GetOrdinal("backdrop_url")) ? null : reader.GetString("backdrop_url")
                            });
                        }
                    }
                }
                catch
                {
                    throw new Exception("Terjadi kesalahan saat mengambil daftar film. Silakan coba lagi nanti.");
                }
            }
            return films;
        }

        // TODO: Add GetFilmById, AddFilm, UpdateFilm, DeleteFilm methods
        public Film GetFilmById(int id)
        {
            Film film = null;
            using (MySqlConnection connection = Koneksi.GetConn())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id, judul, sinopsis, durasi, genre, sutradara, pemeran_utama, rating, poster_url, trailer_url, tanggal_rilis, tanggal_berakhir, status_tayang, thumbnail_url, backdrop_url FROM film WHERE id = @id LIMIT 1";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            film = new Film
                            {
                                Id = reader.GetInt32("id"),
                                Judul = reader.GetString("judul"),
                                Sinopsis = reader.GetString("sinopsis"),
                                Durasi = reader.GetInt32("durasi"),
                                Genre = reader.GetString("genre"),
                                Sutradara = reader.GetString("sutradara"),
                                PemeranUtama = reader.GetString("pemeran_utama"),
                                Rating = reader.GetString("rating"),
                                PosterUrl = reader.IsDBNull(reader.GetOrdinal("poster_url")) ? null : reader.GetString("poster_url"),
                                TrailerUrl = reader.IsDBNull(reader.GetOrdinal("trailer_url")) ? null : reader.GetString("trailer_url"),
                                TanggalRilis = reader.GetDateTime("tanggal_rilis"),
                                TanggalBerakhir = reader.GetDateTime("tanggal_berakhir"),
                                StatusTayang = reader.GetString("status_tayang"),
                                ThumbnailUrl = reader.IsDBNull(reader.GetOrdinal("thumbnail_url")) ? null : reader.GetString("thumbnail_url"),
                                BackdropUrl = reader.IsDBNull(reader.GetOrdinal("backdrop_url")) ? null : reader.GetString("backdrop_url")
                            };
                        }
                    }
                }
                catch
                {
                    // Console.WriteLine($"Kesalahan mengambil film dengan ID {id}: " + ex.Message); // Komentar dihapus
                    throw new Exception("Terjadi kesalahan saat mengambil film. Silakan coba lagi nanti.");
                }
            }
            return film;
        }

        public void AddFilm(Film film)
        {
            using (MySqlConnection connection = Koneksi.GetConn())
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO film (judul, sinopsis, durasi, genre, sutradara, pemeran_utama, rating, poster_url, trailer_url, tanggal_rilis, tanggal_berakhir, status_tayang, thumbnail_url, backdrop_url)
                                     VALUES (@judul, @sinopsis, @durasi, @genre, @sutradara, @pemeranUtama, @rating, @posterUrl, @trailerUrl, @tanggalRilis, @tanggalBerakhir, @statusTayang, @thumbnailUrl, @backdropUrl)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@judul", film.Judul);
                    command.Parameters.AddWithValue("@sinopsis", film.Sinopsis);
                    command.Parameters.AddWithValue("@durasi", film.Durasi);
                    command.Parameters.AddWithValue("@genre", film.Genre);
                    command.Parameters.AddWithValue("@sutradara", film.Sutradara);
                    command.Parameters.AddWithValue("@pemeranUtama", film.PemeranUtama);
                    command.Parameters.AddWithValue("@rating", film.Rating);
                    command.Parameters.AddWithValue("@posterUrl", film.PosterUrl);
                    command.Parameters.AddWithValue("@trailerUrl", film.TrailerUrl);
                    command.Parameters.AddWithValue("@tanggalRilis", film.TanggalRilis);
                    command.Parameters.AddWithValue("@tanggalBerakhir", film.TanggalBerakhir);
                    command.Parameters.AddWithValue("@statusTayang", film.StatusTayang);
                    command.Parameters.AddWithValue("@thumbnailUrl", film.ThumbnailUrl);
                    command.Parameters.AddWithValue("@backdropUrl", film.BackdropUrl);
                    command.ExecuteNonQuery();
                }
                catch (MySqlException mySqlEx)
                {
                    throw new Exception("Kesalahan database saat menambahkan film: " + mySqlEx.Message, mySqlEx);
                }
                catch (Exception ex)
                {
                    throw new Exception("Terjadi kesalahan saat menambahkan film: " + ex.Message, ex);
                }
            }
        }

        public void UpdateFilm(Film film)
        {
            using (MySqlConnection connection = Koneksi.GetConn())
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE film SET
                                         judul = @judul,
                                         sinopsis = @sinopsis,
                                         durasi = @durasi,
                                         genre = @genre,
                                         sutradara = @sutradara,
                                         pemeran_utama = @pemeranUtama,
                                         rating = @rating,
                                         poster_url = @posterUrl,
                                         trailer_url = @trailerUrl,
                                         tanggal_rilis = @tanggalRilis,
                                         tanggal_berakhir = @tanggalBerakhir,
                                         status_tayang = @statusTayang,
                                         thumbnail_url = @thumbnailUrl,
                                         backdrop_url = @backdropUrl
                                     WHERE id = @id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@judul", film.Judul);
                    command.Parameters.AddWithValue("@sinopsis", film.Sinopsis);
                    command.Parameters.AddWithValue("@durasi", film.Durasi);
                    command.Parameters.AddWithValue("@genre", film.Genre);
                    command.Parameters.AddWithValue("@sutradara", film.Sutradara);
                    command.Parameters.AddWithValue("@pemeranUtama", film.PemeranUtama);
                    command.Parameters.AddWithValue("@rating", film.Rating);
                    command.Parameters.AddWithValue("@posterUrl", film.PosterUrl);
                    command.Parameters.AddWithValue("@trailerUrl", film.TrailerUrl);
                    command.Parameters.AddWithValue("@tanggalRilis", film.TanggalRilis);
                    command.Parameters.AddWithValue("@tanggalBerakhir", film.TanggalBerakhir);
                    command.Parameters.AddWithValue("@statusTayang", film.StatusTayang);
                    command.Parameters.AddWithValue("@thumbnailUrl", film.ThumbnailUrl);
                    command.Parameters.AddWithValue("@backdropUrl", film.BackdropUrl);
                    command.Parameters.AddWithValue("@id", film.Id);
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Terjadi kesalahan saat memperbarui film. Silakan coba lagi nanti.");
                }
            }
        }

        public void DeleteFilm(int id)
        {
            using (MySqlConnection connection = Koneksi.GetConn())
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM film WHERE id = @id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Terjadi kesalahan saat menghapus film. Silakan coba lagi nanti.");
                }
            }
        }
    }
}