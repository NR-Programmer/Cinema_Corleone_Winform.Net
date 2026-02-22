# CinemaCorleone

> Aplikasi desktop manajemen bioskop berbasis Windows Forms (.NET Framework 4.8)

## 📋 Deskripsi

CinemaCorleone adalah aplikasi manajemen bioskop yang memungkinkan pengguna untuk mengelola data film, kota, dan pengguna dengan antarmuka desktop yang intuitif.

## 🛠️ Teknologi

- **Framework:** .NET Framework 4.8
- **UI:** Windows Forms
- **Database:** MySQL
- **Bahasa:** C#

## ✨ Fitur

- 🔐 **Login System** - Sistem autentikasi pengguna
- 📊 **Dashboard** - Overview data dan statistik
- 🎬 **Manajemen Film** - Tambah, edit, hapus data film
- 🏙️ **Manajemen Kota** - Kelola data kota
- 👥 **Manajemen Pengguna** - Kelola data pengguna

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
private string password = "";
```

## 📌 Requirements

- Windows 7 atau versi lebih baru
- .NET Framework 4.8
- Visual Studio 2019 atau versi lebih baru
- MySQL Server 5.7 atau versi lebih baru

## 👨‍💻 Author

- **Nama:** [Your Name]
- **GitHub:** [your-username]

## 📄 License

This project is licensed under the MIT License.
