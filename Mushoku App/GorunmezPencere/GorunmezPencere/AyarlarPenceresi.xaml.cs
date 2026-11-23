using System.Windows;
using System.Windows.Media;

namespace GorunmezPencere
{
    public partial class AyarlarPenceresi : Window
    {
        private bool _koyuTema = true;

        public AyarlarPenceresi()
        {
            InitializeComponent();

            // 1. Önce karakterin şu anki mevcut boyutunu okuyup Slider'a eşitliyoruz
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                SldBoyut.Value = mainWindow.Width / 250.0;
            }

            // 2. MEVCUT BOYUTU YÜKLEDİKTEN SONRA TETİKLEYİCİYİ BAĞLIYORUZ (Sıfırlanma hatasını çözer)
            SldBoyut.ValueChanged += SldBoyut_ValueChanged;
        }

        private void SldBoyut_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                double bazBoyut = 250.0;
                mainWindow.Width = bazBoyut * e.NewValue;
                mainWindow.Height = bazBoyut * e.NewValue;

                if (mainWindow.PetScale != null)
                {
                    mainWindow.PetScale.ScaleX = 1;
                    mainWindow.PetScale.ScaleY = 1;
                }
            }
        }

        private void ChkSes_Checked(object sender, RoutedEventArgs e)
        {
            if (ChkSes != null)
            {
                ChkSes.Content = "Açık";
                ChkSes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"));
            }
        }

        private void ChkSes_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ChkSes != null)
            {
                ChkSes.Content = "Kapalı";
                ChkSes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5252"));
            }
        }

        private void BtnTemaToggle_Click(object sender, RoutedEventArgs e)
        {
            _koyuTema = !_koyuTema;

            if (_koyuTema)
            {
                BtnTemaToggle.Content = "🌙 Koyu Tema";
                BtnTemaToggle.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                BtnTemaToggle.Foreground = new SolidColorBrush(Colors.White);

                this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1E1E1E"));
                TxtBaslik.Foreground = new SolidColorBrush(Colors.White);
                TxtBoyut.Foreground = new SolidColorBrush(Colors.LightGray);
                TxtSes.Foreground = new SolidColorBrush(Colors.LightGray);
                TxtTema.Foreground = new SolidColorBrush(Colors.LightGray);
                SepCizgi.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#444444"));

                BtnHakkinda.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                BtnHakkinda.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                BtnTemaToggle.Content = "☀️ Açık Tema";
                BtnTemaToggle.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0E0E0"));
                BtnTemaToggle.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));

                this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5F6F8"));
                TxtBaslik.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
                TxtBoyut.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555555"));
                TxtSes.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555555"));
                TxtTema.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555555"));
                SepCizgi.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDDDDD"));

                BtnHakkinda.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0E0E0"));
                BtnHakkinda.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
            }
        }

        private void BtnHakkinda_Click(object sender, RoutedEventArgs e)
        {
            // Şık bir bilgi kutucuğu (Information icon ile birlikte) açılır
            MessageBox.Show(
                "Masaüstü Anime Asistanı\nSürüm: v1.0.0\n\nGeliştirici: [Said Numan ÇALIŞKAN]\n\nBu uygulama sanal masaüstü asistanı mantığıyla geliştirilmiştir.",
                "Sürüm Bilgisi",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void BtnCikis_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}