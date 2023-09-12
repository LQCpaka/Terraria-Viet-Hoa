using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Terraria_Viet_Hoa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //drag point 
        private bool isDragging = false;
        private Point offset;

        //selectedPath
        string selectedPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Minimized_Exit_Buttons
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Windows Dragging Funciton
        private void Window_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.GetPosition(this).Y <= 40)
            {
                isDragging = true;
                offset = e.GetPosition(this);
                this.CaptureMouse();
            }
        }

        private void Window_MouseLeftUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = false;
                this.ReleaseMouseCapture();
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(this);
                double offsetX = currentPosition.X - offset.X;
                double offsetY = currentPosition.Y - offset.Y;

                this.Left += offsetX;
                this.Top += offsetY;
            }
        }
        #endregion

        #region InstallButton
        // Should check If want to change smt! 
        #region InstallFunction
        public void updateTxtnotify(string a, SolidColorBrush b)
        {
            txtNotification.Text = a;
            txtNotification.Foreground = b;

            //set timer after 3 seconds, text will return to Null

            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(2);

            timer.Tick += Timer_Tick;


            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Đặt giá trị của txtNotification thành null
            txtNotification.Text = null;

            // Dừng timer
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
        }
        public void installfile(string filePath, string output)
        {

            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    if (!string.IsNullOrEmpty(output))
                    {
                        using (ZipArchive archive = ZipFile.OpenRead(filePath))
                        {
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                string entryFullPath = System.IO.Path.Combine(selectedPath, entry.FullName);

                                if (string.IsNullOrEmpty(entry.Name))
                                {
                                    Directory.CreateDirectory(entryFullPath);
                                }
                                else
                                {
                                    entry.ExtractToFile(entryFullPath, true);
                                }
                            }
                        }
                        updateTxtnotify("Đã cài đặt thành công", new SolidColorBrush(Colors.Gold));
                    }
                }
                catch
                {
                    txtNotification.Text = "Cài đặt thất bại";
                }
            }
        }
        #endregion
        private void installBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPath == null) { updateTxtnotify("Vui lòng chọn đường dẫn file", new SolidColorBrush(Colors.Red)); }
            else
            {
                string filepathT = @"./Tresources.otp";
                string filepathC = @"./Cresources.otp";
                ComboBoxItem selectedItem = cbVersionCheck.SelectedItem as ComboBoxItem;
                if (selectedItem == null)
                {
                    updateTxtnotify("Vui lòng chọn phiên bản Terraria", new SolidColorBrush(Colors.OrangeRed));
                }
                else
                {
                    string value = selectedItem.Content.ToString();
                    // Thực hiện hành động dựa trên giá trị được chọn

                    try
                    {
                        if (value == "Terraria Classic")
                        {
                            this.installfile(filepathC, selectedPath);

                        }
                        else if (value == "Tmodloader")
                        {
                            this.installfile(filepathT, selectedPath);
                        }
                    }
                    catch
                    {
                        txtNotification.Text = "Cài đặt thất bại";
                    }
                }
            }
        }

        private void browseBtn_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog browse = new CommonOpenFileDialog();
            browse.IsFolderPicker = true;
            if (browse.ShowDialog() == CommonFileDialogResult.Ok)
            {
                selectedPath = browse.FileName;
                txtSelectedPathBox.Text = selectedPath;
            }
        }

        #endregion
        #region HideAndShow_Link
        bool flag;
        private void btnHideShow(object sender, RoutedEventArgs e)
        {

            if (!flag)
            {
                flag = !flag;
                linklayout.Opacity = 1;

            }
            else if (flag)
            {
                flag = !flag;
                linklayout.Opacity = 0;

            }
        }
        private void link_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://discord.gg/yakKk6ZWqC";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        #endregion
    }
}
