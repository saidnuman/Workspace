using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GorunmezPencere
{
    public class ElSallamaDurumu : IDurum
    {
        private DurumYoneticisi _yonetici;
        private DispatcherTimer _zamanlayici;
        private int _kareIndeksi = 1;

        public ElSallamaDurumu(DurumYoneticisi yonetici)
        {
            _yonetici = yonetici;
            _zamanlayici = new DispatcherTimer();
            // Animasyon hızı (142 ms yaklaşık 7 FPS'dir. İstersen değiştirebilirsin)
            _zamanlayici.Interval = TimeSpan.FromMilliseconds(142);
            _zamanlayici.Tick += Zamanlayici_Tick;
        }

        public void Baslat()
        {
            _kareIndeksi = 1;
            _zamanlayici.Start();
        }

        public void Durdur()
        {
            _zamanlayici.Stop();
        }

        private void Zamanlayici_Tick(object? sender, EventArgs e)
        {
            try
            {
                // D2 formatı ile "el_sallama_01.png", "el_sallama_02.png" şeklinde arar
                string yol = $"pack://application:,,,/Images/el_sallama/el_sallama_{_kareIndeksi:D2}.png";
                _yonetici.KarakterResmi.Source = new BitmapImage(new Uri(yol));
                _kareIndeksi++;
            }
            catch (Exception)
            {
                // Resimler bittiyse (veya bulunamazsa) nefes almaya geri dön
                _yonetici.DurumDegistir(new NefesAlmaDurumu(_yonetici));
            }
        }

        public void CiftTiklandi()
        {
            // El sallarken çift tıklanırsa esnemeye geç (veya nefes almaya dön)
            _yonetici.DurumDegistir(new EsnemeDurumu(_yonetici));
        }

        public void UcKezTiklandi()
        {
            // Zaten el sallıyor, bir şey yapmasına gerek yok ama istersen iptal ettirebilirsin
            _yonetici.DurumDegistir(new NefesAlmaDurumu(_yonetici));
        }
    }
}