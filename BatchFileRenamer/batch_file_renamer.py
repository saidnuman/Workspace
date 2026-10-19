import os

def resimleri_isimlendir():
    # 1. Değişkenler: Klasör yolu ve yeni verilecek temel isim
    klasor_yolu = input("Lütfen klasör yolunu girin (Örn: C:/kullanici/resimler): ")
    temel_isim = input("Resimlerin yeni adı ne olsun? (Örn: dosya): ")

    # Desteklenen resim uzantıları
    gecerli_uzantilar = ('.png', '.jpg', '.jpeg', '.gif', '.bmp', '.webp')

    try:
        # Klasördeki tüm dosyaları al ve sadece resimleri filtrele
        tum_dosyalar = os.listdir(klasor_yolu)
        resimler = [dosya for dosya in tum_dosyalar if dosya.lower().endswith(gecerli_uzantilar)]
        
        resim_sayisi = len(resimler)
        if resim_sayisi == 0:
            print("Belirtilen klasörde hiç resim bulunamadı.")
            return

        print(f"\nToplam {resim_sayisi} adet resim bulundu.")
        
        # 2. Numara Formatını Belirleme (01, 001, 0001 vs.)
        print("\nNumaralandırma formatı nasıl olsun?")
        print("1 - Otomatik (Dosya sayısına göre otomatik hesaplansın)")
        print("2 - Manuel (Kaç basamak olacağını ben gireyim)")
        secim = input("Seçiminiz (1 veya 2): ")
        
        if secim == '1':
            # Dosya sayısının kaç basamaklı olduğunu otomatik bulur. 
            # Örn: 15 dosya varsa 2 basamak (01, 02), 125 dosya varsa 3 basamak (001, 002)
            basamak_sayisi = len(str(resim_sayisi))
            # Göze güzel görünmesi için, 9'dan az dosya olsa bile en az 2 basamak (01, 02) yapalım
            if basamak_sayisi < 2:
                basamak_sayisi = 2
            print(f"Otomatik seçildi: Numaralar {basamak_sayisi} basamaklı olacak.")
            
        elif secim == '2':
            try:
                basamak_sayisi = int(input("Kaç basamaklı olsun? (Örn: 4 girerseniz 0001, 0002 olur): "))
            except ValueError:
                print("Hatalı giriş yaptınız! Otomatik ayara dönülüyor...")
                basamak_sayisi = len(str(resim_sayisi))
        else:
            print("Geçersiz seçim! Otomatik ayara dönülüyor...")
            basamak_sayisi = max(2, len(str(resim_sayisi)))

        print("\nİşlem başlatılıyor...\n")

        # 3. İsimlendirme işlemi
        for sayac, resim in enumerate(resimler, start=1):
            eski_yol = os.path.join(klasor_yolu, resim)
            dosya_uzantisi = os.path.splitext(resim)[1] 
            
            # zfill() komutu ile baştaki sıfırları ekliyoruz
            numara_metni = str(sayac).zfill(basamak_sayisi)
            
            # Yeni dosya adını oluştur (Örn: dosya_0001.jpg)
            yeni_isim = f"{temel_isim}_{numara_metni}{dosya_uzantisi}"
            yeni_yol = os.path.join(klasor_yolu, yeni_isim)
            
            # Dosyayı yeniden adlandır
            os.rename(eski_yol, yeni_yol)
            print(f"Başarılı: {resim} -> {yeni_isim}")
            
        print("\nTüm resimler başarıyla yeniden adlandırıldı!")

    except FileNotFoundError:
        print("Hata: Belirttiğiniz klasör yolu bulunamadı. Lütfen yolu kontrol edin.")
    except Exception as e:
        print(f"Beklenmeyen bir hata oluştu: {e}")

# Kodu çalıştır
if __name__ == "__main__":
    resimleri_isimlendir()


#Terminali açıp python isim_degistirici.py komutuyla çalıştır.
