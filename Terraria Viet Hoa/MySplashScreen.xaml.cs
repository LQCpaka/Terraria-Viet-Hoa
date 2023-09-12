using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Terraria_Viet_Hoa
{
    /// <summary>
    /// Interaction logic for MySplashScreen.xaml
    /// </summary>
    public partial class MySplashScreen : Window
    {
        DispatcherTimer loadformTimer = new DispatcherTimer();

        public MySplashScreen()
        {
            InitializeComponent();

            loadformTimer.Tick += new EventHandler(mySplash);
            loadformTimer.Interval = new TimeSpan(0, 0, 3);
            loadformTimer.Start();
        }

        private void mySplash(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            loadformTimer.Stop();
            this.Close();
        }
    }
}
