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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using MessageBox = System.Windows.Forms.MessageBox;

//To Do:
//Set youtubeDL.exe to custom/temp dir
//Set Config.txt to custom/temp Path
//Set FFMPEG to custom/temp path


namespace YTDownloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        FolderBrowserDialog SavePath;

        public MainWindow()
        {
           // CanvasBorder.BorderThickness = new Thickness(1);

            InitializeComponent();
            SavePath = new FolderBrowserDialog();
            SavePath.RootFolder = Environment.SpecialFolder.MyComputer;
            
        }
        
        public void LocationButton_Click(object sender, RoutedEventArgs e)
        {
            
            SavePath.RootFolder = Environment.SpecialFolder.MyComputer;
            
            if (SavePath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                 MessageBox.Show("Music will be downloaded to: " + SavePath.SelectedPath);
                // string URLPath = string.Format(SavePath.SelectedPath);
                InfoStatusBar.Text =  "Save path is: " + SavePath.SelectedPath;
            MessageBox.Show(System.Windows.Forms.Application.StartupPath);
            MessageBox.Show(System.Windows.Forms.Application.StartupPath + "\\Data");
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            string YTDLURL = URLTextBox.Text;
            string YTDLConfigPath = (System.Windows.Forms.Application.StartupPath + "\\Data\\Mp3Config.txt");
            string YTDLAppPath = (System.Windows.Forms.Application.StartupPath + "\\Data\\youtube-dl.exe");
            string YTDLDownloadPath = SavePath.SelectedPath;
            string YTDLFFMpegPath = (System.Windows.Forms.Application.StartupPath + "\\Data\\ffmpeg\\bin");
            
            ProcessStartInfo YDTLstartinfo = new ProcessStartInfo();
            YDTLstartinfo.FileName = YTDLAppPath;
            YDTLstartinfo.Arguments = ("--config-location " + YTDLConfigPath + " -o " + YTDLDownloadPath + "\\%(title)s.%(ext)s" + " --ffmpeg-location " + YTDLFFMpegPath + " " + YTDLURL);
            Process.Start(YDTLstartinfo);
            
            InfoStatusBar.Text = "Finished Downloading";
        }
        private void TestDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            string YTDLURL = URLTextBox.Text;
            string YTDLConfigPath = "D:\\Programs\\YTDL\\Mp3Config.txt";
            string YTDLAppPath = (System.Windows.Forms.Application.StartupPath + "D:\\Programs\\YTDL\\youtube - dl.exe");
            string YTDLDownloadPath = SavePath.SelectedPath;
            string YTDLFFMpegPath = (System.Windows.Forms.Application.StartupPath + "D:\\Programs\\YTDL\\ffmpeg\\bin");

            ProcessStartInfo YDTLstartinfo = new ProcessStartInfo();
            YDTLstartinfo.FileName = YTDLAppPath;
            YDTLstartinfo.Arguments = ("--config-location " + YTDLConfigPath + " -o " + YTDLDownloadPath + "\\%(title)s.%(ext)s" + " --ffmpeg-location " + YTDLFFMpegPath + " " + YTDLURL);
            Process.Start(YDTLstartinfo);

            InfoStatusBar.Text = "Finished Downloading";
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
