# CinemaCorleone

> Aplikasi desktop manajemen bioskop berbasis Windows Forms (.NET Framework 4.8)

## 📋 Deskripsi

CinemaCorleone adalah aplikasi manajemen bioskop yang memungkinkan pengguna untuk mengelola data film, kota, dan pengguna dengan antarmuka desktop yang intuitif.

## 🛠️ Teknologi

- **Framework:** .NET Framework 4.8
- **UI:** GUNA UI
- **Database:** MySQL
- **Bahasa:** C#

## ✨ Fitur

- 🔐 **Login System** - Sistem autentikasi pengguna
- 📊 **Dashboard** - Overview data dan statistik
- 🎬 **Manajemen Film** - Tambah, edit, hapus data film
- 🏙️ **Manajemen Kota** - Kelola data kota
- 👥 **Manajemen Pengguna** - Kelola data pengguna

## 🚀 Cara Menjalankan

1. Buka solution `CinemaCorleone.sln` menggunakan Visual Studio
2. Pastikan database MySQL sudah terinstall dan berjalan
3. Import database dari file `cinema_corleone` (cek folder database)
4. Konfigurasi koneksi database di `Koneksi.cs`
5. Tekan `F5` atau klik tombol Run untuk menjalankan aplikasi

## 🔧 Konfigurasi Database

Edit file `Koneksi.cs` untuk mengatur koneksi database:

```csharp
private string server = "127.0.0.1";
private string database = "laravel_cinema_corleone";
private string username = "root";
private string password = "Dens999201";
```

## 📌 Requirements

- Windows 7 atau versi lebih baru
- .NET Framework 4.8
- Visual Studio 2026 atau versi lebih baru
- MySQL Server 9.4 atau versi lebih baru

## 👨‍💻 Author

- **Nama:** [Densus Prabowo Sugiharto]
- **GitHub:** [NR-Programmer]

## 📄 License

This project is licensed under the MIT License.
