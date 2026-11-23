using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading; // Zamanlayıcı kullanabilmek için eklendi

namespace GorunmezPencere
{
    public class NefesAlmaDurumu : IDurum
    {
        private DurumYoneticisi _yonetici;
        private DoubleAnimation? _nefesAnimasyonu;

        // Resimleri arka arkaya değiştirecek zamanlayıcı
        private DispatcherTimer _zamanlayici;
        private int _kareIndeksi = 1;

        public NefesAlmaDurumu(DurumYoneticisi yonetici)
        {
            _yonetici = yonetici;

            // 7 FPS hızına ayarlı (saniyede 7 resim değiştirir, istersen 142 değerini değiştirebilirsin)
            _zamanlayici = new DispatcherTimer();
            _zamanlayici.Interval = TimeSpan.FromMilliseconds(142);
            _zamanlayici.Tick += Zamanlayici_Tick;
        }

        public void Baslat()
        {
            _kareIndeksi = 1;

            // 1. YUKARI-AŞAĞI ESNEME KODU (Silmedik, aynen duruyor)
            _nefesAnimasyonu = new DoubleAnimation();
            _nefesAnimasyonu.To = 1.015;
            _nefesAnimasyonu.Duration = new Duration(TimeSpan.FromSeconds(1.5));
            _nefesAnimasyonu.AutoReverse = true;

            // Sonsuz döngü (Sen müdahale edene kadar devam eder)
            _nefesAnimasyonu.RepeatBehavior = RepeatBehavior.Forever;
            _yonetici.Olcek.BeginAnimation(ScaleTransform.ScaleYProperty, _nefesAnimasyonu);

            // 2. RESİM DEĞİŞTİRME MOTORUNU BAŞLAT
            _zamanlayici.Start();
        }

        private void Zamanlayici_Tick(object? sender, EventArgs e)
        {
            try
            {
                // D2 formatı ile "nefes_01.png", "nefes_02.png" şeklinde arar
                string yol = $"pack://application:,,,/Images/nefes2/nefes_{_kareIndeksi:D2}.png";
                _yonetici.KarakterResmi.Source = new BitmapImage(new Uri(yol));
                _kareIndeksi++;
            }
            catch (Exception)
            {
                // SONSUZ DÖNGÜ MANTIĞI: Eğer klasördeki resimler bittiyse (hata verdiyse)
                // İndeksi başa (1'e) sar ve döngüyü kesintiye uğratmadan tekrar oynatmaya başla
                _kareIndeksi = 1;

                try
                {
                    string basaSarilanYol = $"pack://application:,,,/Images/nefes2/nefes_{_kareIndeksi:D2}.png";
                    _yonetici.KarakterResmi.Source = new BitmapImage(new Uri(basaSarilanYol));
                    _kareIndeksi++;
                }
                catch (Exception)
                {
                    // Eğer klasörde resim hiç yoksa uygulamanın çökmesini engeller
                }
            }
        }

        public void Durdur()
        {
            // Müdahale edildiğinde resim değiştirme döngüsünü durdur
            _zamanlayici.Stop();

            // Nefes (esneme) animasyonunu güvenlice iptal et ve karakteri orijinal boyutuna döndür
            _yonetici.Olcek.BeginAnimation(ScaleTransform.ScaleYProperty, null);
            _yonetici.Olcek.ScaleY = 1.0;
        }

        public void CiftTiklandi()
        {
            // Çift tıklanınca Esneme moduna geç
            _yonetici.DurumDegistir(new EsnemeDurumu(_yonetici));
        }

        public void UcKezTiklandi()
        {
            // 3 kez tıklanınca El Sallama moduna geç
            _yonetici.DurumDegistir(new ElSallamaDurumu(_yonetici));
        }
    }
}