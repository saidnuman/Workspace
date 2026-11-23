using System.Windows;
using System.Windows.Input;

namespace GorunmezPencere
{
    public partial class MainWindow : Window
    {
        private DurumYoneticisi _durumYonetici;
        private bool _gizliMi = false;

        // Açık olan ayarlar penceresini takip etmek için ekledik
        private AyarlarPenceresi _ayarlarPenceresi;

        public MainWindow()
        {
            InitializeComponent();

            _durumYonetici = new DurumYoneticisi(PetImage, PetScale);

            this.MouseLeftButtonDown += Window_MouseLeftButtonDown;
            this.MouseRightButtonUp += Window_MouseRightButtonUp;

            _durumYonetici.DurumDegistir(new NefesAlmaDurumu(_durumYonetici));
        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuPopup.IsOpen = true;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_gizliMi) return;

            if (e.ClickCount == 3)
            {
                _durumYonetici.UcKezTiklandi();
                return;
            }

            if (e.ClickCount == 2)
            {
                _durumYonetici.CiftTiklandi();
                return;
            }

            this.DragMove();
        }

        private void BtnKapat_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnAyarlar_Click(object sender, RoutedEventArgs e)
        {
            MenuPopup.IsOpen = false;

            // Eğer ayarlar penceresi zaten açıksa yenisini açma, sadece öne getir
            if (_ayarlarPenceresi == null || !_ayarlarPenceresi.IsLoaded)
            {
                _ayarlarPenceresi = new AyarlarPenceresi();

                // DİKKAT: ShowDialog() yerine Show() kullandık. Artık karakter kilitlenmeyecek!
                _ayarlarPenceresi.Show();
            }
            else
            {
                _ayarlarPenceresi.Focus();
            }
        }

        private void BtnGizle_Click(object sender, RoutedEventArgs e)
        {
            if (!_gizliMi)
            {
                _gizliMi = true;
                PetImage.Opacity = 0.4;
                BtnGizle.Content = "Göster";
            }
            else
            {
                _gizliMi = false;
                PetImage.Opacity = 1.0;
                BtnGizle.Content = "Gizle";
            }

            MenuPopup.IsOpen = false;
        }
    }
}