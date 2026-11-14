# MYAcademy Portfolyo

ASP.NET Core MVC ve Entity Framework Core ile geliştirilmiş, yönetim panelinden tamamen yönetilebilen kişisel portfolyo sitesi.

> **M&Y Yazılım Eğitim Akademi** · 12. Dönem Akşam Grubu · 1. Proje: Portfolyo Case Çalışması
> Geliştiren: **Ümit Bahçivan**

<!-- Ekran görüntülerini docs/screenshots klasörüne ekleyip aşağıdaki satırları açabilirsin -->
<!--
![Ana Sayfa](docs/screenshots/anasayfa.png)
![Admin Dashboard](docs/screenshots/dashboard.png)
-->

🎬 **Tanıtım videosu:** _[link eklenecek]_

---

## İçindekiler

- [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
- [Kurulum](#kurulum)
- [Özellikler](#özellikler)
- [Case Maddelerinin Karşılanması](#case-maddelerinin-karşılanması)
- [Proje Yapısı](#proje-yapısı)

---

## Kullanılan Teknolojiler

| Katman | Teknoloji |
|---|---|
| Framework | ASP.NET Core MVC (.NET 9) |
| ORM | Entity Framework Core 9 (Code First, Migrations) |
| Veritabanı | Microsoft SQL Server (Express) |
| Arayüz | Bootstrap 5.3, Bootstrap Icons, Razor View Components |
| Bildirim / Popup | SweetAlert2, Bootstrap Modal |
| Doğrulama | Data Annotations + `ModelState.IsValid`, jQuery Validation |

---

## Kurulum

### Gereksinimler

- Visual Studio 2022 veya üzeri (ASP.NET ve web geliştirme iş yüküyle)
- .NET 9 SDK
- SQL Server (Express ya da LocalDB) ve SQL Server Management Studio

### Adımlar

**1. Projeyi klonlayın**

```bash
git clone https://github.com/ubahcivan75/MYAcademy_Portfolya.git
```

**2. Veritabanı bağlantı adresini kontrol edin**

Bağlantı adresi `MYAcademy_Portfolya/Context/PortfolioContext.cs` dosyasındadır:

```csharp
optionsBuilder.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=PortfolioDbNight;integrated security=true; trust server certificate=true");
```

SQL Server örneğinizin adı farklıysa `Server=` kısmını değiştirin. Örnekler:

| SQL Server türü | `Server=` değeri |
|---|---|
| SQL Server Express | `(local)\\SQLEXPRESS` |
| LocalDB | `(localdb)\\MSSQLLocalDB` |
| Varsayılan örnek | `localhost` |

**3. Veritabanını oluşturun**

Visual Studio'da **Tools → NuGet Package Manager → Package Manager Console**:

```powershell
Update-Database
```

Bu komut `Migrations` klasöründeki migration'ları sırayla uygulayarak bütün tabloları oluşturur.

**4. Örnek verileri yükleyin**

SQL Server Management Studio'da oluşan `PortfolioDbNight` veritabanını seçin ve `Database/seed-data.sql` dosyasını açıp çalıştırın (**F5**).

> Örnek veriler; Hero, Hakkımda ve İletişim bilgileri, kategoriler, projeler (biri pasif), eğitim ve deneyim kayıtları, yüzdeli yetenekler ve okunmuş/okunmamış mesajlar içerir. Proje ve profil görselleri `wwwroot/images`, CV dosyası `App_Data/cv` klasöründe repo ile birlikte gelir.

**5. Çalıştırın**

**F5** ile projeyi başlatın.

| Sayfa | Adres |
|---|---|
| Portfolyo (ziyaretçi) | `https://localhost:7217/` |
| Yönetim Paneli | `https://localhost:7217/Admin` |

> Case kapsamında kimlik doğrulama (Authentication) zorunlu olmadığı için yönetim paneli girişsiz açılır.

---

## Özellikler

### Ziyaretçi Sayfası (Tek Sayfa Portfolyo)

- **Hero:** Ad, unvan, tanıtım metni, profil fotoğrafı ve sosyal bağlantılar veritabanından gelir.
- **Hakkımda, Eğitim, Deneyim:** Eğitim ve deneyim kayıtları başlangıç tarihine göre sıralı; pasif kayıtlar gösterilmez.
- **Yetenekler:** Kategorilere göre gruplu kartlar; her yetenek ekrana gelince dolan yüzde çubuğuyla gösterilir.
- **Projeler:** Kart görünümü ve **veritabanındaki kategorilerden üretilen** filtre butonları (proje sayılarıyla). Sadece aktif projeler listelenir.
- **İletişim:** Sunucu tarafında doğrulanan iletişim formu; mesajlar veritabanına kaydedilir.
- **CV İndir:** Yönetim panelinden yüklenen PDF, ziyaretçiye anlamlı bir dosya adıyla indirilir.
- Navigasyon bağlantıları ilgili bölümlere kayar; footer'daki sosyal bağlantılar ve telif yılı dinamiktir.

### Yönetim Paneli

| Modül | İşlemler |
|---|---|
| **Dashboard** | 7 istatistik kartı, son 5 mesaj, son 3 proje, kategori özeti grafiği, hızlı işlemler |
| **Hero Bölümü** | Başlık, isim, açıklama ve profil görseli (yükleme + önizleme) |
| **Hakkımda** | Kişisel bilgiler |
| **Eğitim / Deneyim** | CRUD, başlangıç–bitiş tarihi, aktif/pasif durumu, renkli durum etiketi |
| **Yetenek Kategorileri / Yetenekler** | CRUD, ikon ve renk seçimi, yüzde (slider ile) |
| **Proje Kategorileri** | CRUD; içinde proje olan kategori silinemez, aynı ad tekrar eklenemez |
| **Projeler** | CRUD, kategori seçimi (dropdown), kapak görseli yükleme, aktif/pasif |
| **Mesajlar** | Gelen kutusu: okundu/okunmadı, arama, tarih sıralaması, durum filtresi, Bootstrap Modal ile detay, onaylı silme |
| **İletişim Bilgileri** | E-posta, konum, LinkedIn, GitHub |
| **CV Yönetimi** | PDF yükleme, önizleme, silme |

---

## Case Maddelerinin Karşılanması

<details>
<summary><b>01 · Yönetim paneli ve dinamik içerik</b></summary>

- Bütün içerik modülleri yönetim panelinden CRUD / güncelleme ile yönetilir; değişiklikler anında ziyaretçi sayfasına yansır.
- Ana sayfadaki ad, unvan, tanıtım metni, profil görseli ve sosyal bağlantılar dinamiktir.
- Eğitim ve deneyim kayıtları `StartDate` alanına göre sıralanır; aktif/pasif bilgisi yönetim tablosunda renkli etiketle gösterilir, pasif kayıtlar ziyaretçi sayfasında görünmez.
- Projeler kartlarla, yetenekler yüzde çubuklarıyla gösterilir.
</details>

<details>
<summary><b>02 · İlişkili tablo ve kategori yönetimi</b></summary>

- `Category` – `Project` arasında bire-çok ilişki (FK: `CategoryId`, navigation: `Category.Projects` / `Project.Category`).
- Proje kategorileri için ayrı CRUD ekranı.
- Proje formunda kategoriler veritabanından gelen `SelectListItem` listesiyle (`asp-items`) seçilir.
- Ziyaretçi sayfasında kategori filtresi `GroupBy` ile veritabanındaki kategorilerden üretilir.
</details>

<details>
<summary><b>03 · Mesajlar ve iletişim</b></summary>

- İletişim formu Data Annotations ile doğrulanır (`Required`, `StringLength`, `EmailAddress`) ve veritabanına kaydedilir.
- Gelen kutusunda okundu/okunmadı durumu, gönderen/e-posta/konuya göre arama, tarihe göre sıralama ve okunmamış mesaj sayısı vardır.
- Mesaj detayı Bootstrap Modal ile açılır; içerik salt okunurdur, düzenlenemez. Silmeden önce onay penceresi gösterilir.
</details>

<details>
<summary><b>04 · CV İndir</b></summary>

- CV dosyası `wwwroot` dışında, `App_Data/cv/cv.pdf` konumunda saklanır; doğrudan URL ile erişilemez, yalnızca `/Cv/Download` üzerinden indirilir.
- Yönetim panelinden güncellenir. Yalnızca PDF kabul edilir: uzantı, boyut (5 MB) ve dosya içeriği (`%PDF-` imzası) kontrol edilir.
- Yükleme önce geçici dosyaya yapılır, başarılı olunca eski CV'nin yerine geçer.
</details>

<details>
<summary><b>05 · Dashboard</b></summary>

- `DashboardViewModel` ile tek modelde: toplam/aktif proje, yetenek, deneyim, eğitim, toplam ve okunmamış mesaj sayıları.
- Son 5 mesaj (tıklayınca gelen kutusunda ilgili mesajın modalı açılır ve okundu işaretlenir), son 3 proje (düzenleme bağlantısıyla).
- Kategori bazında proje sayıları (`GroupBy`, aktif proje sayısıyla birlikte) ve çubuk grafik.
- Hızlı işlemler: yeni proje ekle, mesajları görüntüle, portfolyoyu aç.
</details>

<details>
<summary><b>06 · Araştırılarak uygulanan teknikler</b></summary>

| Teknik | Kullanıldığı yer |
|---|---|
| `Required`, `StringLength`, `Range`, `EmailAddress`, `Url`, `RegularExpression` | Bütün entity'ler |
| `ModelState.IsValid` + `ModelState.AddModelError` | Bütün POST action'ları (tarih aralığı, aynı kategori adı, görsel kontrolü) |
| Bootstrap Modal | Mesaj detayı, mesaj silme onayı |
| SweetAlert2 | Silme onayları ve işlem sonucu bildirimleri (toast) |
| TempData | Yönlendirme sonrası bildirimler, iletişim formu hata/başarı mesajları |
| `Include`, `Where`, `Count`, `OrderByDescending`, `Take`, `GroupBy` | Dashboard, listeler, View Component'ler |
| `SelectListItem` / `asp-items` | Proje ve yetenek formlarındaki kategori seçimi |
| `IFormFile` | Proje ve profil görselleri: tür + 2 MB boyut kontrolü, GUID ile benzersiz ad, `wwwroot/images` altında saklama |
| `DashboardViewModel` | Dashboard verilerinin tek modelle taşınması |
</details>

<details>
<summary><b>07 · Ek kullanıcı deneyimi geliştirmeleri</b></summary>

- Bütün listelerde boş liste mesajları
- Dashboard'da kategori grafiği
- Yetenek yüzde çubuklarının ekrana gelince dolma animasyonu
- Görsel yüklemede anlık önizleme
- Duyarlı (responsive) tablolar
- İçinde proje olan kategori için pasif silme butonu
</details>

---

## Proje Yapısı

```
MYAcademy_Portfolya/
├── Areas/Admin/
│   ├── Controllers/        # Yönetim paneli controller'ları
│   ├── Models/             # DashboardViewModel
│   └── Views/              # Yönetim paneli sayfaları ve AdminLayout
├── App_Data/cv/            # CV dosyası (wwwroot dışında)
├── Context/                # PortfolioContext (DbContext)
├── Controllers/            # Ziyaretçi tarafı (Default, SendMessage, Cv)
├── Entites/                # Veritabanı entity'leri
├── Migrations/             # EF Core migration'ları
├── ViewComponents/         # Ana sayfa bölümleri
├── Views/
│   ├── Default/            # Tek sayfa portfolyo
│   └── Shared/Components/  # View Component görünümleri
└── wwwroot/
    ├── css/
    └── images/             # Yüklenen proje ve profil görselleri

Database/
└── seed-data.sql           # Örnek veriler
```

---

© 2026 Ümit Bahçivan
