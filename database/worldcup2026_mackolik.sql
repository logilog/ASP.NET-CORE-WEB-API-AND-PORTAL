-- ============================================================================
--  Dünya Kupası 2026 - Maçkolik tarzı modüller (Şema + Örnek Veri)
--  Önce database/worldcup2026.sql çalıştırılmış olmalıdır.
--  Çalıştırma:  mysql -u root -p StajCase2 < database/worldcup2026_mackolik.sql
--  Eklenenler: canlı skor (DAKIKA), OYUNCU (kadro), GOL (gol krallığı/olaylar),
--              ORAN (iddaa oranları), HABER (haberler).
-- ============================================================================

SET NAMES utf8mb4;

-- Canlı skor için maç dakikası (MariaDB: IF NOT EXISTS; MySQL 8'de "IF NOT EXISTS" kaldırılabilir)
ALTER TABLE MAC ADD COLUMN IF NOT EXISTS DAKIKA INT NULL AFTER DURUM;

-- ----------------------------------------------------------------------------
--  Tablolar
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS OYUNCU (
    ID      INT AUTO_INCREMENT PRIMARY KEY,
    TAKIMID INT NOT NULL,
    AD      VARCHAR(80) NOT NULL,
    NUMARA  INT NULL,
    MEVKI   VARCHAR(30) NULL,
    KEY IX_OYUNCU_TAKIM (TAKIMID),
    CONSTRAINT FK_OYUNCU_TAKIM FOREIGN KEY (TAKIMID) REFERENCES TAKIM(ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS GOL (
    ID       INT AUTO_INCREMENT PRIMARY KEY,
    MACID    INT NOT NULL,
    OYUNCUID INT NOT NULL,
    TAKIMID  INT NOT NULL,
    DAKIKA   INT NULL,
    PENALTI  TINYINT(1) NOT NULL DEFAULT 0,
    KEY IX_GOL_MAC (MACID),
    KEY IX_GOL_OYUNCU (OYUNCUID),
    CONSTRAINT FK_GOL_MAC    FOREIGN KEY (MACID)    REFERENCES MAC(ID),
    CONSTRAINT FK_GOL_OYUNCU FOREIGN KEY (OYUNCUID) REFERENCES OYUNCU(ID),
    CONSTRAINT FK_GOL_TAKIM  FOREIGN KEY (TAKIMID)  REFERENCES TAKIM(ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS ORAN (
    ID     INT AUTO_INCREMENT PRIMARY KEY,
    MACID  INT NOT NULL,
    MS1    DECIMAL(5,2) NULL,
    MSX    DECIMAL(5,2) NULL,
    MS2    DECIMAL(5,2) NULL,
    ALT25  DECIMAL(5,2) NULL,
    UST25  DECIMAL(5,2) NULL,
    KGVAR  DECIMAL(5,2) NULL,
    KGYOK  DECIMAL(5,2) NULL,
    UNIQUE KEY UX_ORAN_MAC (MACID),
    CONSTRAINT FK_ORAN_MAC FOREIGN KEY (MACID) REFERENCES MAC(ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS HABER (
    ID      INT AUTO_INCREMENT PRIMARY KEY,
    BASLIK  VARCHAR(200) NOT NULL,
    OZET    VARCHAR(400) NULL,
    ICERIK  TEXT NULL,
    GORSEL  VARCHAR(300) NULL,
    YAZAR   VARCHAR(80) NULL,
    TARIH   DATETIME NOT NULL,
    KEY IX_HABER_TARIH (TARIH)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ----------------------------------------------------------------------------
--  Oyuncular (kadro - demo)
-- ----------------------------------------------------------------------------
INSERT INTO OYUNCU (TAKIMID, AD, NUMARA, MEVKI)
SELECT t.ID, v.AD, v.NUMARA, v.MEVKI
FROM (
    SELECT 'MEX' KOD,'Raul Jimenez' AD, 9 NUMARA,'Forvet' MEVKI UNION ALL
    SELECT 'MEX','Hirving Lozano',22,'Kanat' UNION ALL
    SELECT 'MEX','Edson Alvarez',4,'Orta Saha' UNION ALL
    SELECT 'MEX','Guillermo Ochoa',13,'Kaleci' UNION ALL
    SELECT 'NED','Memphis Depay',10,'Forvet' UNION ALL
    SELECT 'NED','Cody Gakpo',11,'Kanat' UNION ALL
    SELECT 'NED','Virgil van Dijk',4,'Defans' UNION ALL
    SELECT 'NED','Frenkie de Jong',21,'Orta Saha' UNION ALL
    SELECT 'USA','Christian Pulisic',10,'Kanat' UNION ALL
    SELECT 'USA','Weston McKennie',8,'Orta Saha' UNION ALL
    SELECT 'USA','Folarin Balogun',9,'Forvet' UNION ALL
    SELECT 'CRO','Luka Modric',10,'Orta Saha' UNION ALL
    SELECT 'CRO','Andrej Kramaric',9,'Forvet' UNION ALL
    SELECT 'CRO','Josko Gvardiol',20,'Defans' UNION ALL
    SELECT 'SEN','Sadio Mane',10,'Kanat' UNION ALL
    SELECT 'SEN','Nicolas Jackson',19,'Forvet' UNION ALL
    SELECT 'SEN','Ismaila Sarr',18,'Kanat' UNION ALL
    SELECT 'TUR','Arda Guler',10,'Orta Saha' UNION ALL
    SELECT 'TUR','Kenan Yildiz',11,'Kanat' UNION ALL
    SELECT 'TUR','Hakan Calhanoglu',8,'Orta Saha' UNION ALL
    SELECT 'TUR','Mert Gunok',1,'Kaleci' UNION ALL
    SELECT 'BRA','Vinicius Junior',7,'Kanat' UNION ALL
    SELECT 'BRA','Rodrygo',10,'Kanat' UNION ALL
    SELECT 'BRA','Endrick',9,'Forvet' UNION ALL
    SELECT 'ARG','Lionel Messi',10,'Forvet' UNION ALL
    SELECT 'ARG','Julian Alvarez',9,'Forvet' UNION ALL
    SELECT 'ARG','Enzo Fernandez',24,'Orta Saha' UNION ALL
    SELECT 'FRA','Kylian Mbappe',10,'Forvet' UNION ALL
    SELECT 'FRA','Ousmane Dembele',11,'Kanat' UNION ALL
    SELECT 'ENG','Harry Kane',9,'Forvet' UNION ALL
    SELECT 'ENG','Jude Bellingham',10,'Orta Saha' UNION ALL
    SELECT 'ESP','Lamine Yamal',19,'Kanat' UNION ALL
    SELECT 'ESP','Nico Williams',17,'Kanat'
) v JOIN TAKIM t ON t.KISAKOD = v.KOD;

-- ----------------------------------------------------------------------------
--  Goller (oynanmış 4 maç: 1-MEX2-1NED, 3-USA1-2CRO, 4-SEN3-0NZL)
-- ----------------------------------------------------------------------------
INSERT INTO GOL (MACID, OYUNCUID, TAKIMID, DAKIKA, PENALTI)
SELECT m.ID, o.ID, o.TAKIMID, v.DAKIKA, v.PENALTI
FROM (
    SELECT 'MEX' EV,'NED' DEP,'Raul Jimenez' OYUNCU,23 DAKIKA,0 PENALTI UNION ALL
    SELECT 'MEX','NED','Hirving Lozano',58,0 UNION ALL
    SELECT 'MEX','NED','Memphis Depay',71,1 UNION ALL
    SELECT 'USA','CRO','Christian Pulisic',34,0 UNION ALL
    SELECT 'USA','CRO','Andrej Kramaric',12,0 UNION ALL
    SELECT 'USA','CRO','Luka Modric',80,1 UNION ALL
    SELECT 'SEN','NZL','Sadio Mane',9,0 UNION ALL
    SELECT 'SEN','NZL','Nicolas Jackson',45,0 UNION ALL
    SELECT 'SEN','NZL','Ismaila Sarr',67,0
) v
JOIN TAKIM te ON te.KISAKOD = v.EV
JOIN TAKIM td ON td.KISAKOD = v.DEP
JOIN MAC m ON m.EVSAHIBIID = te.ID AND m.DEPLASMANID = td.ID
JOIN OYUNCU o ON o.AD = v.OYUNCU;

-- ----------------------------------------------------------------------------
--  İddaa oranları (yaklaşan maçlar - demo)
-- ----------------------------------------------------------------------------
INSERT INTO ORAN (MACID, MS1, MSX, MS2, ALT25, UST25, KGVAR, KGYOK)
SELECT m.ID, v.MS1, v.MSX, v.MS2, v.ALT25, v.UST25, v.KGVAR, v.KGYOK
FROM (
    SELECT 'CAN' EV,'BEL' DEP, 3.40 MS1, 3.30 MSX, 2.10 MS2, 2.05 ALT25, 1.75 UST25, 1.70 KGVAR, 2.05 KGYOK UNION ALL
    SELECT 'MAR','UZB', 1.80, 3.40, 4.50, 1.95, 1.80, 1.85, 1.90 UNION ALL
    SELECT 'ARG','DEN', 1.55, 3.90, 6.00, 2.10, 1.70, 1.80, 1.95 UNION ALL
    SELECT 'NGA','KSA', 2.05, 3.20, 3.60, 1.90, 1.85, 1.75, 2.00 UNION ALL
    SELECT 'FRA','SRB', 1.50, 4.10, 6.50, 2.15, 1.65, 1.90, 1.85 UNION ALL
    SELECT 'AUS','PAN', 2.40, 3.10, 2.90, 1.85, 1.90, 1.80, 1.95 UNION ALL
    SELECT 'BRA','SUI', 1.45, 4.30, 7.00, 2.20, 1.62, 1.95, 1.80 UNION ALL
    SELECT 'CIV','QAT', 1.70, 3.50, 4.80, 1.92, 1.82, 1.82, 1.92 UNION ALL
    SELECT 'ENG','POL', 1.40, 4.50, 7.50, 2.25, 1.60, 2.00, 1.75 UNION ALL
    SELECT 'GHA','KOR', 2.90, 3.20, 2.35, 1.88, 1.88, 1.78, 1.98 UNION ALL
    SELECT 'ESP','URU', 1.75, 3.50, 4.40, 2.00, 1.78, 1.80, 1.92 UNION ALL
    SELECT 'GER','COL', 1.85, 3.40, 4.10, 1.95, 1.80, 1.82, 1.90
) v
JOIN TAKIM te ON te.KISAKOD = v.EV
JOIN TAKIM td ON td.KISAKOD = v.DEP
JOIN MAC m ON m.EVSAHIBIID = te.ID AND m.DEPLASMANID = td.ID;

-- ----------------------------------------------------------------------------
--  Haberler (demo)
-- ----------------------------------------------------------------------------
INSERT INTO HABER (BASLIK, OZET, ICERIK, GORSEL, YAZAR, TARIH) VALUES
('Dünya Kupası 2026 başladı!','48 takımın mücadele edeceği tarihteki en büyük Dünya Kupası ABD, Kanada ve Meksika''da start aldı.','ABD, Kanada ve Meksika ev sahipliğindeki turnuvada 48 takım 104 maçta kupa için ter dökecek. Açılış maçında Meksika, Hollanda''yı 2-1 mağlup etti.','📰','Spor Servisi','2026-06-11 23:30:00'),
('Meksika açılışta güldü: 2-1','Ev sahibi Meksika, Azteca''da Hollanda''yı devirdi.','Raul Jimenez ve Hirving Lozano''nun golleriyle öne geçen Meksika, Depay''ın penaltısına rağmen sahadan 3 puanla ayrıldı.','⚽','Ahmet K.','2026-06-12 01:00:00'),
('Senegal farka koştu: 3-0','Sadio Mane şov yaptı, Senegal Yeni Zelanda''yı dağıttı.','Aslanlar, gruba galibiyetle başladı. Mane, Jackson ve Sarr fileleri havalandırdı.','🦁','Spor Servisi','2026-06-13 20:30:00'),
('Türkiye sahne alıyor','Milliler L Grubu''nda Paraguay ile karşılaşacak.','Arda Güler ve Kenan Yıldız''lı genç kadro, gruptan çıkma mücadelesine 24 Haziran''da başlıyor.','🇹🇷','Spor Servisi','2026-06-14 12:00:00'),
('Gol krallığı yarışı kızıştı','İlk maçların ardından gol krallığında liderlik için kıyasıya rekabet var.','Turnuvanın ilk günlerinde formda isimler öne çıkıyor. Tahminlerinizi yapmayı unutmayın!','🏆','Editör','2026-06-14 18:00:00');

-- ----------------------------------------------------------------------------
--  Canlı maç demo: Kanada - Belçika şu an oynanıyor
-- ----------------------------------------------------------------------------
UPDATE MAC m
JOIN TAKIM e ON e.ID = m.EVSAHIBIID
JOIN TAKIM d ON d.ID = m.DEPLASMANID
SET m.DURUM='OYNANIYOR', m.DAKIKA=63, m.EVSAHIBISKOR=1, m.DEPLASMANSKOR=0
WHERE e.KISAKOD='CAN' AND d.KISAKOD='BEL';
