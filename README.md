# CinemaCorleone

> Aplikasi desktop manajemen bioskop berbasis Windows Forms (.NET Framework 4.8)

## 📋 Deskripsi Proyek & Progres

CinemaCorleone adalah aplikasi manajemen bioskop berbasis desktop yang dibangun dengan teknologi Windows Forms. Proyek ini bertujuan untuk menyediakan antarmuka intuitif bagi Super Admin dalam mengelola berbagai aspek operasional bioskop.

**Progres yang Telah Dicapai:**
Kami telah berhasil mengimplementasikan sistem autentikasi dengan manajemen peran (khusus Super Admin), serta modul lengkap untuk manajemen data film dan kota. Dashboard Super Admin juga telah selesai, menampilkan ringkasan statistik dan visualisasi data penting seperti tren pendapatan dan film terpopuler. Beberapa fitur utama yang sudah berjalan meliputi:

### 🔐 Sistem Autentikasi

- Login dengan Username atau Email
- Sistem Role-based Access (Super Admin)
- Validasi kredensial pengguna

### 📊 Dashboard Super Admin

- **Ringkasan Statistik:**
  - Total Film Aktif
  - Pengguna Terdaftar
  - Pendapatan Hari Ini
  - Jumlah Penayangan Aktif
- **Visualisasi Data:**
  - Grafik Tren Pendapatan 30 Hari terakhir
  - Daftar Film Terpopuler (berdasarkan tiket terjual)
  - Aktivitas Pemesanan Terbaru
- Tampilan DataGridView dengan styling profesional

### 🎬 Manajemen Film

- **CRUD Film Lengkap:**
  - Judul, Sinopsis, Genre, Durasi
  - Rating Usia (SU, 13+, 17+, 21+)
  - Sutradara & Pemeran Utama
  - Tanggal Rilis & Tanggal Berakhir Tayang
  - URL Poster & Trailer (load image dari URL)
- **Status Tayang:** Sedang Tayang, Akan Datang, Selesai
- **Fitur Pencarian:** Filter film berdasarkan judul, genre, rating, status
- **Pengelolaan Poster:** Load poster dari URL secara async

### 🏙️ Manajemen Kota

- **CRUD Data Kota:**
  - Kode Kota, Nama Kota, Provinsi
  - Zona Waktu
  - Slug (URL-friendly)
  - Status Aktif/Tidak Aktif
- **Validasi Data:**
  - Kode Kota unik
  - Nama Kota unik
  - Slug unik

## 🛠️ Teknologi yang Digunakan

- **Framework:** .NET Framework 4.8
- **UI:** Windows Forms
- **Database:** MySQL
- **Bahasa:** C#
- **Library Hashing:** BCrypt.Net-Next (untuk keamanan password)

## 🎯 Progres Selanjutnya

Fokus pengembangan selanjutnya adalah memperluas fungsionalitas manajemen dan menambahkan fitur-fitur transaksi. Rencana kerja meliputi:

- **Manajemen Pengguna (CRUD):** Mengelola data pengguna, termasuk pembuatan, pembacaan, pembaruan, dan penghapusan akun.
- **Manajemen Jadwal Tayang:** Sistem untuk membuat dan mengelola jadwal penayangan film di berbagai studio.
- **Sistem Pemesanan Tiket:** Implementasi alur pemesanan tiket, pemilihan kursi, dan integrasi pembayaran.
- **Laporan Keuangan Lengkap:** Pengembangan modul pelaporan untuk analisis pendapatan dan performa operasional yang lebih mendalam.
- **Fitur Tambahan:** Export/Import Data, Backup & Restore Database untuk kemudahan operasional dan keamanan data.

## 📁 Struktur Proyek

```
CinemaCorleone/
├── CinemaCorleone/
│   ├── DashboardSuperAdmin.cs/.designer.cs
│   ├── FilmSuperAdmin.cs/.designer.cs
│   ├── KotaSuperAdmin.cs/.designer.cs
│   ├── Login.cs/.designer.cs
│   ├── SuperAdminMain.cs/.designer.cs
│   ├── Services/
│   │   ├── DashboardService.cs
│   │   ├── FilmService.cs
│   │   ├── KotaService.cs
│   │   └── PenggunaService.cs
│   ├── Koneksi.cs
│   └── Program.cs
├── CinemaCorleone.sln
└── README.md
```

## 🚀 Cara Menjalankan

1. Buka solution `CinemaCorleone.sln` menggunakan Visual Studio
2. Pastikan database MySQL sudah terinstall dan berjalan
3. Import database dari file `cinema_corleone` (cek folder database)
4. Konfigurasi koneksi database di `Koneksi.cs`
5. Tekan `F5` atau klik tombol Run untuk menjalankan aplikasi

## 🔧 Konfigurasi Database

Edit file `Koneksi.cs` untuk mengatur koneksi database:

```csharp
private string server = "localhost";
private string database = "cinema_corleone";
private string username = "root";
private string password = "...";
```

## 📌 Requirements

- Windows 7 atau versi lebih baru
- .NET Framework 4.8
- Visual Studio 2022 atau versi lebih baru
- MySQL Server 5.7 atau versi lebih baru

## 👨‍💻 Author

- **Nama:** Densus Prabowo Sugiharto
- **GitHub:** NR-Programmer

## 📄 License

This project is licensed under the MIT License.
