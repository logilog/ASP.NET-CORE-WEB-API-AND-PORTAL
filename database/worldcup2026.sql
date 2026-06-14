-- ============================================================================
--  Dünya Kupası 2026 Skor Tahmin Ligi - Şema ve Örnek Veri (MySQL / MariaDB)
--  Mevcut "StajCase2" veritabanına eklenir. T_KULLANICI tablosu zaten vardır.
--  Çalıştırma:  mysql -u root -p StajCase2 < database/worldcup2026.sql
--  NOT: Takım/grup/fikstür verileri DEMO amaçlıdır; resmi çekiliş açıklanınca
--       gerçek verilerle güncellenmelidir.
-- ============================================================================

SET NAMES utf8mb4;

-- ----------------------------------------------------------------------------
--  Tablolar
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS TAKIM (
    ID       INT AUTO_INCREMENT PRIMARY KEY,
    AD       VARCHAR(80)  NOT NULL,
    KISAKOD  VARCHAR(5)   NOT NULL,
    GRUP     VARCHAR(2)   NULL,
    KITA     VARCHAR(20)  NULL,
    BAYRAK   VARCHAR(20)  NULL,
    UNIQUE KEY UX_TAKIM_KOD (KISAKOD)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS MAC (
    ID            INT AUTO_INCREMENT PRIMARY KEY,
    EVSAHIBIID    INT NOT NULL,
    DEPLASMANID   INT NOT NULL,
    TARIH         DATETIME NOT NULL,
    GRUP          VARCHAR(2)  NULL,
    ASAMA         VARCHAR(20) NULL,
    STADYUM       VARCHAR(80) NULL,
    SEHIR         VARCHAR(80) NULL,
    EVSAHIBISKOR  INT NULL,
    DEPLASMANSKOR INT NULL,
    DURUM         VARCHAR(20) NOT NULL DEFAULT 'BEKLIYOR',
    KEY IX_MAC_TARIH (TARIH),
    CONSTRAINT FK_MAC_EV  FOREIGN KEY (EVSAHIBIID)  REFERENCES TAKIM(ID),
    CONSTRAINT FK_MAC_DEP FOREIGN KEY (DEPLASMANID) REFERENCES TAKIM(ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS TAHMIN (
    ID            INT AUTO_INCREMENT PRIMARY KEY,
    KULLANICIID   INT NOT NULL,
    MACID         INT NOT NULL,
    EVSAHIBISKOR  INT NOT NULL,
    DEPLASMANSKOR INT NOT NULL,
    PUAN          INT NULL,
    TAHMINTARIHI  DATETIME NULL,
    UNIQUE KEY UX_TAHMIN (KULLANICIID, MACID),   -- upsert (ON DUPLICATE KEY) için zorunlu
    KEY IX_TAHMIN_MAC (MACID),
    CONSTRAINT FK_TAHMIN_MAC FOREIGN KEY (MACID) REFERENCES MAC(ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS UYELIKPAKET (
    ID         INT AUTO_INCREMENT PRIMARY KEY,
    KOD        VARCHAR(10)  NOT NULL,
    AD         VARCHAR(50)  NOT NULL,
    ACIKLAMA   VARCHAR(200) NULL,
    AYLIKUCRET DECIMAL(10,2) NOT NULL DEFAULT 0,
    OZELLIKLER VARCHAR(500) NULL,            -- '|' ile ayrılmış özellik listesi
    UNIQUE KEY UX_PAKET_KOD (KOD)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS UYELIK (
    ID          INT AUTO_INCREMENT PRIMARY KEY,
    KULLANICIID INT NOT NULL,
    PAKETKOD    VARCHAR(10) NOT NULL,
    TUTAR       DECIMAL(10,2) NULL,
    BASLANGIC   DATETIME NULL,
    BITIS       DATETIME NULL,
    DURUM       VARCHAR(20) NOT NULL DEFAULT 'AKTIF',
    KEY IX_UYELIK_K (KULLANICIID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------------------------------------------------------
--  Takımlar (48 takım, 12 grup: A-L)
-- ----------------------------------------------------------------------------
INSERT INTO TAKIM (AD, KISAKOD, GRUP, KITA, BAYRAK) VALUES
('Meksika','MEX','A','CONCACAF','🇲🇽'),
('Hollanda','NED','A','UEFA','🇳🇱'),
('Mısır','EGY','A','CAF','🇪🇬'),
('Irak','IRQ','A','AFC','🇮🇶'),
('ABD','USA','B','CONCACAF','🇺🇸'),
('Hırvatistan','CRO','B','UEFA','🇭🇷'),
('Senegal','SEN','B','CAF','🇸🇳'),
('Yeni Zelanda','NZL','B','OFC','🇳🇿'),
('Kanada','CAN','C','CONCACAF','🇨🇦'),
('Belçika','BEL','C','UEFA','🇧🇪'),
('Fas','MAR','C','CAF','🇲🇦'),
('Özbekistan','UZB','C','AFC','🇺🇿'),
('Arjantin','ARG','D','CONMEBOL','🇦🇷'),
('Danimarka','DEN','D','UEFA','🇩🇰'),
('Nijerya','NGA','D','CAF','🇳🇬'),
('Suudi Arabistan','KSA','D','AFC','🇸🇦'),
('Fransa','FRA','E','UEFA','🇫🇷'),
('Sırbistan','SRB','E','UEFA','🇷🇸'),
('Avustralya','AUS','E','AFC','🇦🇺'),
('Panama','PAN','E','CONCACAF','🇵🇦'),
('Brezilya','BRA','F','CONMEBOL','🇧🇷'),
('İsviçre','SUI','F','UEFA','🇨🇭'),
('Fildişi Sahili','CIV','F','CAF','🇨🇮'),
('Katar','QAT','F','AFC','🇶🇦'),
('İngiltere','ENG','G','UEFA','🇬🇧'),
('Polonya','POL','G','UEFA','🇵🇱'),
('Gana','GHA','G','CAF','🇬🇭'),
('Güney Kore','KOR','G','AFC','🇰🇷'),
('İspanya','ESP','H','UEFA','🇪🇸'),
('Uruguay','URU','H','CONMEBOL','🇺🇾'),
('Cezayir','ALG','H','CAF','🇩🇿'),
('Japonya','JPN','H','AFC','🇯🇵'),
('Portekiz','POR','I','UEFA','🇵🇹'),
('Avusturya','AUT','I','UEFA','🇦🇹'),
('Tunus','TUN','I','CAF','🇹🇳'),
('Kosta Rika','CRC','I','CONCACAF','🇨🇷'),
('Almanya','GER','J','UEFA','🇩🇪'),
('Kolombiya','COL','J','CONMEBOL','🇨🇴'),
('İran','IRN','J','AFC','🇮🇷'),
('Jamaika','JAM','J','CONCACAF','🇯🇲'),
('İtalya','ITA','K','UEFA','🇮🇹'),
('Ekvador','ECU','K','CONMEBOL','🇪🇨'),
('Kamerun','CMR','K','CAF','🇨🇲'),
('Mali','MLI','K','CAF','🇲🇱'),
('Türkiye','TUR','L','UEFA','🇹🇷'),
('Paraguay','PAR','L','CONMEBOL','🇵🇾'),
('Ukrayna','UKR','L','UEFA','🇺🇦'),
('Norveç','NOR','L','UEFA','🇳🇴');

-- ----------------------------------------------------------------------------
--  Üyelik paketleri (gelir modeli)
-- ----------------------------------------------------------------------------
INSERT INTO UYELIKPAKET (KOD, AD, ACIKLAMA, AYLIKUCRET, OZELLIKLER) VALUES
('FREE','Ücretsiz','Temel tahmin deneyimi', 0.00,
 'Tüm maçlara skor tahmini|Genel liderlik tablosu|Reklam destekli'),
('VIP','VIP', 'Reklamsız ve istatistikli deneyim', 49.90,
 'Reklamsız deneyim|Detaylı tahmin istatistikleri|Maç bazlı analizler|Rozet ve profil çerçevesi'),
('PRO','PRO', 'Kazananlara ödüllü özel ligler', 99.90,
 'VIP''nin tüm özellikleri|Sınırsız özel (arkadaş) ligi kur|Ödüllü turnuvalara katılım|Öncelikli destek|Erken erişim özellikleri');

-- ----------------------------------------------------------------------------
--  Fikstür - Grup aşaması 1. maç günü (24 maç).
--  A ve B grubu oynandı (BITTI), C-L grupları BEKLIYOR (tahmine açık).
--  MySQL VALUES içinde alt sorgu desteklemediğinden INSERT...SELECT kullanıldı.
-- ----------------------------------------------------------------------------

-- Grup A (oynandı)
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM,EVSAHIBISKOR,DEPLASMANSKOR)
SELECT e.ID,d.ID,'2026-06-11 21:00:00','A','GRUP','Estadio Azteca','Mexico City','BITTI',2,1
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='MEX' AND d.KISAKOD='NED';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM,EVSAHIBISKOR,DEPLASMANSKOR)
SELECT e.ID,d.ID,'2026-06-12 18:00:00','A','GRUP','Estadio Akron','Guadalajara','BITTI',0,0
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='EGY' AND d.KISAKOD='IRQ';

-- Grup B (oynandı)
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM,EVSAHIBISKOR,DEPLASMANSKOR)
SELECT e.ID,d.ID,'2026-06-12 21:00:00','B','GRUP','SoFi Stadium','Los Angeles','BITTI',1,2
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='USA' AND d.KISAKOD='CRO';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM,EVSAHIBISKOR,DEPLASMANSKOR)
SELECT e.ID,d.ID,'2026-06-13 18:00:00','B','GRUP','Lumen Field','Seattle','BITTI',3,0
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='SEN' AND d.KISAKOD='NZL';

-- Grup C
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-15 19:00:00','C','GRUP','BMO Field','Toronto','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='CAN' AND d.KISAKOD='BEL';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-15 22:00:00','C','GRUP','BC Place','Vancouver','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='MAR' AND d.KISAKOD='UZB';

-- Grup D
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-16 19:00:00','D','GRUP','MetLife Stadium','New York','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='ARG' AND d.KISAKOD='DEN';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-16 22:00:00','D','GRUP','Hard Rock Stadium','Miami','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='NGA' AND d.KISAKOD='KSA';

-- Grup E
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-17 19:00:00','E','GRUP','AT&T Stadium','Dallas','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='FRA' AND d.KISAKOD='SRB';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-17 22:00:00','E','GRUP','Mercedes-Benz Stadium','Atlanta','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='AUS' AND d.KISAKOD='PAN';

-- Grup F
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-18 19:00:00','F','GRUP','NRG Stadium','Houston','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='BRA' AND d.KISAKOD='SUI';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-18 22:00:00','F','GRUP','Levi''s Stadium','San Francisco','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='CIV' AND d.KISAKOD='QAT';

-- Grup G
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-19 19:00:00','G','GRUP','Lincoln Financial Field','Philadelphia','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='ENG' AND d.KISAKOD='POL';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-19 22:00:00','G','GRUP','Gillette Stadium','Boston','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='GHA' AND d.KISAKOD='KOR';

-- Grup H
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-20 19:00:00','H','GRUP','Arrowhead Stadium','Kansas City','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='ESP' AND d.KISAKOD='URU';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-20 22:00:00','H','GRUP','Estadio BBVA','Monterrey','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='ALG' AND d.KISAKOD='JPN';

-- Grup I
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-21 19:00:00','I','GRUP','SoFi Stadium','Los Angeles','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='POR' AND d.KISAKOD='AUT';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-21 22:00:00','I','GRUP','BC Place','Vancouver','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='TUN' AND d.KISAKOD='CRC';

-- Grup J
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-22 19:00:00','J','GRUP','MetLife Stadium','New York','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='GER' AND d.KISAKOD='COL';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-22 22:00:00','J','GRUP','AT&T Stadium','Dallas','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='IRN' AND d.KISAKOD='JAM';

-- Grup K
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-23 19:00:00','K','GRUP','Hard Rock Stadium','Miami','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='ITA' AND d.KISAKOD='ECU';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-23 22:00:00','K','GRUP','Mercedes-Benz Stadium','Atlanta','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='CMR' AND d.KISAKOD='MLI';

-- Grup L
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-24 19:00:00','L','GRUP','NRG Stadium','Houston','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='TUR' AND d.KISAKOD='PAR';
INSERT INTO MAC (EVSAHIBIID,DEPLASMANID,TARIH,GRUP,ASAMA,STADYUM,SEHIR,DURUM)
SELECT e.ID,d.ID,'2026-06-24 22:00:00','L','GRUP','Lumen Field','Seattle','BEKLIYOR'
FROM TAKIM e, TAKIM d WHERE e.KISAKOD='UKR' AND d.KISAKOD='NOR';
