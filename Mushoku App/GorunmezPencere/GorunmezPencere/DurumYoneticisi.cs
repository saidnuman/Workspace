using System.Windows.Controls;
using System.Windows.Media;

namespace GorunmezPencere
{
    public class DurumYoneticisi
    {
        public Image KarakterResmi { get; }
        public ScaleTransform Olcek { get; }

        private IDurum? _mevcutDurum;

        public DurumYoneticisi(Image karakterResmi, ScaleTransform olcek)
        {
            KarakterResmi = karakterResmi;
            Olcek = olcek;
        }

        public void DurumDegistir(IDurum yeniDurum)
        {
            if (_mevcutDurum != null)
            {
                _mevcutDurum.Durdur();
            }

            _mevcutDurum = yeniDurum;
            _mevcutDurum.Baslat();
        }

        public void CiftTiklandi()
        {
            _mevcutDurum?.CiftTiklandi();
        }

        // YENİ EKLENEN KISIM
        public void UcKezTiklandi()
        {
            _mevcutDurum?.UcKezTiklandi();
        }
    }
}