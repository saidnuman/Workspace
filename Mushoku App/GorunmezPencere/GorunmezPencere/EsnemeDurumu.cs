using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GorunmezPencere
{
    public class EsnemeDurumu : IDurum
    {
        private DurumYoneticisi _yonetici;
        private DispatcherTimer _zamanlayici;
        private int _kareIndeksi = 1;

        public EsnemeDurumu(DurumYoneticisi yonetici)
        {
            _yonetici = yonetici;
            _zamanlayici = new DispatcherTimer();

            // 142 değerini 100 (10 FPS) veya 85 (yaklaşık 12 FPS) olarak değiştir.
            // Rakam ne kadar küçülürse animasyon o kadar hızlanır!
            _zamanlayici.Interval = TimeSpan.FromMilliseconds(85);
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
                string yol = $"pack://application:,,,/Images/esneme_1/esneme_{_kareIndeksi:D2}.png";
                _yonetici.KarakterResmi.Source = new BitmapImage(new Uri(yol));
                _kareIndeksi++;
            }
            catch (Exception)
            {
                // DEDEKTİF KODU: Eğer 70. resme ulaşmadan (örneğin 30'da) hata verip kesilirse ekrana uyarı bas!
                if (_kareIndeksi <= 70)
                {
                    MessageBox.Show($"SORUN BULUNDU!\nAnimasyon tam 'esneme_{_kareIndeksi:D2}.png' dosyasını ararken koptu.\n\nLütfen klasörde bu isimde bir resim olduğundan ve özelliklerden 'Resource' yapıldığından emin ol.");
                }

                _yonetici.DurumDegistir(new NefesAlmaDurumu(_yonetici));
            }
        }

        public void CiftTiklandi()
        {
            _yonetici.DurumDegistir(new NefesAlmaDurumu(_yonetici));
        }

        public void UcKezTiklandi()
        {
            // Esnerken 3 kez tıklanırsa El Sallamaya geç (veya iptal etmesi için NefesAlmaDurumu yazabilirsin)
            _yonetici.DurumDegistir(new ElSallamaDurumu(_yonetici));
        }
    }
}