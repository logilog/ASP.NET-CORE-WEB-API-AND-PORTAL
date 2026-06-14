# ⚽ Dünya Kupası 2026 Skor Tahmin Ligi

Bu proje, mevcut **ASP.NET Core 5 (Web API + MVC Portal)** sigorta uygulamasının
katmanlı mimarisi üzerine inşa edilmiş, **para kazandıran** bir oyunlaştırma ürünüdür:
kullanıcılar Dünya Kupası 2026 maçlarının skorlarını tahmin eder, isabetlerine göre
puan toplar, liderlik tablosunda yarışır ve **Premium üyelik** ile gelir üretir.

> Var olan kullanıcı/JWT altyapısını (T_KULLANICI + Login) aynen kullanır;
> hiçbir mevcut özellik bozulmadan yeni bir dikey eklenmiştir.

---

## 💰 Gelir Modeli (Nasıl para kazandırır?)

| Kaynak | Açıklama |
|---|---|
| **Premium üyelik (VIP / PRO)** | Aylık abonelik. Reklamsız deneyim, detaylı istatistik, ödüllü özel ligler. `UYELIKPAKET` kataloğundan fiyatlandırılır. |
| **Ödüllü özel ligler (PRO)** | Arkadaş grupları katılım ücretiyle özel lig kurar; ödül havuzundan komisyon. |
| **Reklam (FREE kademe)** | Ücretsiz kullanıcılara reklam; en geniş kitleden gelir. |
| **Sponsorluk / marka iş birlikleri** | Liderlik tablosu ve maç sayfaları yüksek etkileşimli reklam alanı. |

Ödeme altyapısı (iyzico / Stripe) entegrasyonu için kod hazır noktalara sahiptir
(`UyelikService.SatinAl`). Bu sürümde satın alma, üyelik kaydını oluşturur ve fiyat
**sunucu tarafında katalogdan** alınır (istemciden gelen tutara güvenilmez).

---

## 🎮 Özellikler

- **Fikstür / Maçlar** — 48 takım, 12 grup; takım adları, bayraklar, stadyum ve durum.
- **Skor Tahmini** — Maç başlamadan önce tahmin; başladıktan sonra otomatik kilit.
- **Otomatik Puanlama** — Sonuç girilince ilgili tüm tahminler tek SQL ile puanlanır.
- **Liderlik Tablosu** — Toplam puana göre canlı sıralama, madalyalar, tam skor sayısı.
- **Premium Üyelik** — Paket vitrini, satın alma ve aktif üyelik durumu.

### Puanlama Kuralları

| Durum | Puan |
|---|---|
| Tam skor (ör. 2-1 → 2-1) | **5** |
| Doğru sonuç **ve** gol farkı (ör. 2-1 tahmin, 3-2 sonuç) | **3** |
| Doğru sonuç yönü (galibiyet/beraberlik tutar) | **2** |
| Yanlış | **0** |

---

## 🏗️ Mimari

Mevcut N-katmanlı yapıya birebir uyumludur (Dapper + MySQL, AutoMapper, JWT):

```
Entities (BUSINESS + REPOSITORY/DTO)
   └─ Repositories (Dapper, MySqlConnector)   ← ITakim/IMac/ITahmin/ILiderlik/IUyelik
        └─ Business (Service + AutoMapper)     ← scoring & iş kuralları
             └─ The_Case2 (Web API + JWT)      ← Takim/Mac/Tahmin/Liderlik/Uyelik Controller
                  └─ TheCase2WebPortal (MVC)   ← HttpClient ile API'yi tüketir
```

### Eklenen Tablolar (`database/worldcup2026.sql`)

- `TAKIM` — milli takımlar (grup, kıta, bayrak)
- `MAC` — fikstür (takımlara FK, tarih, skor, durum)
- `TAHMIN` — kullanıcı tahminleri; `UNIQUE(KULLANICIID, MACID)` ile upsert
- `UYELIKPAKET` — satılabilir paket kataloğu
- `UYELIK` — kullanıcının satın aldığı üyelikler

---

## 🔐 Güvenlik Notları

- Tahmin/üyelik işlemlerinde kullanıcı kimliği **daima JWT'deki `UserId` claim'inden**
  alınır; istemci başkası adına işlem yapamaz.
- Tahmin kilidi ve üyelik fiyatı **veritabanı/sunucu tarafında** uygulanır
  (`TARIH > NOW()` ve katalog fiyatı), istemci manipülasyonuna kapalıdır.
- Tüm parametreler Dapper ile **parametreli sorgu** olarak gönderilir (SQL injection yok).

---

## 🚀 Kurulum ve Çalıştırma

### 1) Veritabanı

`appsettings.Development.json` içindeki bağlantı `StajCase2` veritabanını kullanır.
Şemayı ve örnek veriyi yükleyin:

```bash
mysql -u root -p StajCase2 < database/worldcup2026.sql
```

> Yüklenen veri: 48 takım, 24 maç (1. hafta — A/B grubu oynanmış, C-L tahmine açık),
> 3 üyelik paketi. Fikstür **demo** amaçlıdır; resmi çekiliş açıklanınca güncellenmelidir.

### 2) API

```bash
dotnet run --project The_Case2
# Swagger: https://localhost:44334/swagger
```

### 3) Portal

```bash
dotnet run --project TheCase2WebPortal
# Giriş yaptıktan sonra sol menü: "⚽ Dünya Kupası 2026"
```

---

## 📡 API Uç Noktaları

> Tümü `Authorization: Bearer <token>` ister. Token: `POST /Login/Token`.

### Takım
- `GET  /Takim/GetList` · `GET /Takim/GetById?Id=` · `POST /Takim/Add` · `POST /Takim/Update`

### Maç
- `GET  /Mac/GetList` — takım adları/bayraklarıyla fikstür
- `GET  /Mac/GetById?Id=`
- `POST /Mac/Add` · `POST /Mac/Update`
- `POST /Mac/SonucGir` — `{ ID, EVSAHIBISKOR, DEPLASMANSKOR }` → sonucu kaydeder **ve tahminleri puanlar**

### Tahmin
- `GET  /Tahmin/Listem` — oturum açan kullanıcının tahminleri (+puan)
- `POST /Tahmin/Tahminle` — `{ MACID, EVSAHIBISKOR, DEPLASMANSKOR }` (kullanıcı token'dan)

### Liderlik
- `GET  /Liderlik/GetList` — sıralı liderlik tablosu

### Üyelik
- `GET  /Uyelik/Paketler` — paket vitrini
- `GET  /Uyelik/Durumum` — aktif üyelik
- `POST /Uyelik/SatinAl` — `{ PAKETKOD }` (ücret katalogdan)

---

## 🧪 Doğrulama

Tüm çözüm **0 hata** ile derlenir (Razor view'lar dahil). Backend mantığı ve API
uçtan uca gerçek bir MariaDB üzerinde test edilmiştir: tahmin upsert + maç-başladı
kilidi, 5/3/2/0 puanlama, otomatik skorlama döngüsü, liderlik sıralaması ve
üyelik satın alma akışı.

---

## 🗺️ Yol Haritası (öneri)

- [ ] iyzico / Stripe ile gerçek ödeme entegrasyonu
- [ ] Özel (arkadaş) ligleri ve davet bağlantıları
- [ ] Resmi fikstür/çekiliş verisinin otomatik içe aktarımı (data feed)
- [ ] Yönetici paneli (maç sonucu girişi için UI)
- [ ] Push/e-posta hatırlatmaları (maç öncesi "tahminini yap")
- [ ] Haftalık ödüller ve rozet sistemi
